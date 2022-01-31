namespace MCBACommon.Models.Dto;
public class CustomerDto
{
    public string CustomerID { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? PostCode { get; set; }

    public List<AccountDto> Accounts { get; set; } = new List<AccountDto>();
    
    public LoginDto Login { get; set; }
}
