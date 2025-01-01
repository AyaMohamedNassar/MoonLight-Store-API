using Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEmailConfirmationService
    {
            Task<(bool success, string message)> SendConfirmationEmailAsync(ApplicationUser user);
            Task<(bool success, string message)> ConfirmEmailAsync(string email, string token, string csrfToken);
        
    }
}
