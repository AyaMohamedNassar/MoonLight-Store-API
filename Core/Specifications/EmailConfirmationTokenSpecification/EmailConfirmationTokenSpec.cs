using Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.EmailConfirmationTokenSpecification
{
    public class EmailConfirmationTokenSpec : BaseSpecification<EmailConfirmationToken>
    {
        public EmailConfirmationTokenSpec(string userId) : base(eConToken => eConToken.UserId == userId)
        {
            AddOrderByDescending(eConToken => eConToken.CreatedAt);
            ApplyPagination(0, 1);
        }
    }
}
