using MCBABackend.Models;
using MCBABackend.Utilities;
using MCBABackend.Utilities.Extensions;
using MCBAWebApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimpleHashing;

namespace MCBAWebApplication.Utilities
{
    internal static class ProfileActionValidation
    {
        public static void Login(ModelStateDictionary modelState, string passwordHash, string password)
        {
            if (!PBKDF2.Verify(passwordHash, password))
            {
                modelState.AddModelError("", "Incorrect password");
            }
        }
    }
}
