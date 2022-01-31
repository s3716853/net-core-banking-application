﻿using MCBABackend.Contexts;
using MCBABackend.Models;
using MCBABackend.Repositories;
using MCBABackend.Utilities;
using MCBABackend.Utilities.Extensions;

namespace MCBACustomerApi.Services
{
    public class BillPayBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<BillPayBackgroundService> _logger;

        public BillPayBackgroundService(IServiceProvider services, ILogger<BillPayBackgroundService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Running BillPay Background Service");
            while (!cancellationToken.IsCancellationRequested)
            {
                await CompleteBillPayRequests();
                
                const int waitTime = 1;

                _logger.LogInformation($"BillPay Background Service Complete, sleeping for {waitTime} minute(s)");

                await Task.Delay(TimeSpan.FromMinutes(waitTime), cancellationToken);
            }
        }

        private async Task CompleteBillPayRequests()
        {
            using var scope = _services.CreateScope();
            BillPayRepository billPayRepository = scope.ServiceProvider.GetRequiredService<BillPayRepository>();
            TransactionRepository transactionRepository = scope.ServiceProvider.GetRequiredService<TransactionRepository>();

            List<BillPay> billPays = await billPayRepository.GetAll();

            foreach (BillPay billPay in billPays)
            {
                // if scheduled for before current time and hasnt been complete then complete
                if (billPay.ScheduleTimeUtc <= DateTime.Now && !billPay.Completed)
                {
                    decimal minBalance = Constants.MinBalances[billPay.Account.AccountType];
                    if (billPay.Account.Balance() - billPay.Amount >= minBalance)
                    {
                        await transactionRepository.Add(new Transaction()
                        {
                            Amount = billPay.Amount,
                            Comment = Constants.BillPayComment,
                            OriginAccountNumber = billPay.AccountNumber,
                            TransactionTimeUtc = DateTime.Now.ToUniversalTime(),
                            TransactionType = TransactionType.BillPay
                        });

                        if (billPay.Period == Period.OneOff)
                        {
                            billPay.Completed = true;
                            await billPayRepository.Update(billPay);
                        }
                    }
                }
            }
        }
    }
}
