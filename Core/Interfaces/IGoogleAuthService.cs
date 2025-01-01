using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGoogleAuthService
    {
        Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string idToken);
    }
}
