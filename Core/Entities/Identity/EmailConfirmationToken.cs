using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Identity
{
    public class EmailConfirmationToken: BaseEntity
    {
        public EmailConfirmationToken()
        {
            
        }
        public EmailConfirmationToken(string userId, string hashedToken, string csrfToken, DateTime createdAt)
        {
            UserId = userId;
            HashedToken = hashedToken;
            CsrfToken = csrfToken;
            CreatedAt = createdAt;
            IsUsed = false;
        }

        public string UserId { get; set; }
        public string HashedToken { get; set; }
        public string CsrfToken { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsUsed { get; set; }
        public ApplicationUser User { get; set; }
    }
}
