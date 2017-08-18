using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;

namespace API_CheckInCheckOut.Providers
{
    public class DomainUserLoginProvider : ILoginProvider
    {
        public bool ValidateCredentials(string userName, string password, out ClaimsIdentity identity)
        {
            try
            {
                using (var pc = new PrincipalContext(ContextType.Domain, "un.corp"))
                {
                    bool isValid = pc.ValidateCredentials(userName, password);
                    if (isValid)
                    {
                        identity = new ClaimsIdentity(Startup.OAuthOptions.AuthenticationType);
                        identity.AddClaim(new Claim(ClaimTypes.Name, userName));
                    }
                    else
                    {
                        identity = null;
                    }

                    return isValid;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}