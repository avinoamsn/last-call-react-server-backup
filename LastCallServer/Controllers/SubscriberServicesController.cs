using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LastCallServer.Controllers
{
    public class LoginReturn
    {
        public ServerError error { get; set; }
        public string AuthToken { get; set; }

        public LoginReturn()
        {
            error = new ServerError();
            AuthToken = "";
        }
    }

    public class SubscriberServicesController : ApiController
    {
        [Route("SubscriberServices/Login")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Login()
        {
            NameValueCollection f = HttpContext.Current.Request.Form;
            string username = f["username"];
            string password = f["password"];

            var r = new LoginReturn();

            switch (CheckCredentials(username, password))
            {
                case LoginStatus.Okay:
                    r.AuthToken = "AUTHORIZED";
                    break;
                case LoginStatus.InvalidPassword:
                    r.error.ErrorNumber = 1;
                    r.error.ErrorMessage = ServerErrors.Errors[1].ErrorMessage;
                    break;
                case LoginStatus.UnknownUser:
                    r.error.ErrorNumber = 1;
                    r.error.ErrorMessage = ServerErrors.Errors[1].ErrorMessage;
                    break;
            }

            return Json(r);
        }

        private enum LoginStatus { UnknownUser, InvalidPassword, Okay };

        private LoginStatus CheckCredentials(string email, string password)
        {
            lastcallEntities db = new lastcallEntities();

            subscriber s = db.subscribers.Where((x) => x.email == email).FirstOrDefault();
            if (s == null)
                return LoginStatus.UnknownUser;
            if (s.password != password)
                return LoginStatus.InvalidPassword;

            return LoginStatus.Okay;
        }

        [Route("SubscriberServices/RegisterSubscriber")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult RegisterSubscriber()
        {
            NameValueCollection f = HttpContext.Current.Request.Form;
            string username = f["username"];
            string password = f["password"];

            var r = new LoginReturn();
            r.error.ErrorMessage = "Success";
            r.error.ErrorDetails = "No more info";
            r.AuthToken = username + " IS NO LONGER AUTHORIZED WITH PASSWORD " + password;

            return Json(r);
        }
    }
}
