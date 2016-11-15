using System.Web.Security;
using Market.WEB.UI.Infrastructure.Abstract;

namespace Market.WEB.UI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, true);
            }
            return result;
        }

    }
}