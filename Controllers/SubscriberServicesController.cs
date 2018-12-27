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

    public class mealoffer
    {
        public int ID { get; set; }
        public string Start { get; set; }           // Returned as a displayable time
        public string End { get; set; }             // Returned as a displayable time
        public string Name { get; set; }
        public string Supplier { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public string FoodType { get; set; }
    }

    public class MealOfferReturn
    {
        public ServerError error { get; set; }
        public mealoffer[] mealoffers { get; set; }

        public MealOfferReturn()
        {
            error = new ServerError();
            // mealoffers remains a null array until the number of entries can be determined
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
                    r.error = new ServerError(ServerErrorNumber.InvalidLogin);
                    break;
                case LoginStatus.UnknownUser:
                    r.error = new ServerError(ServerErrorNumber.InvalidLogin);
                    break;
            }

            return Json(r, Global.JSONSettings());
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
            try
            {
                NameValueCollection f = HttpContext.Current.Request.Form;
                string username = f["username"];
                string password = f["password"];
                string address = f["address"];
                string phone = f["phone"];
                string friendlyname = f["friendlyname"];
                string foodprefs = f["preferences"];
                string passwordq = f["passwordq"];
                string passworda = f["passworda"];

                byte emailoffers = Convert.ToByte(f["emailoffers"]);
                byte mailinglist = Convert.ToByte(f["mailinglist"]);
                byte textoffers = Convert.ToByte(f["textoffers"]);

                lastcallEntities db = new lastcallEntities();
                subscriber s = (from x in db.subscribers where x.email == username select x).FirstOrDefault();

                if (s != null)
                    return Json(new ServerError(ServerErrorNumber.UsernameInUse));

                // The new subscriber record
                s = new subscriber()
                {
                    deliveryaddress = address,
                    email = username,
                    emailoffers = emailoffers,
                    friendlyname = friendlyname,
                    onmailinglist = mailinglist,
                    password = password,                                    //TODO: Need to encrypt
                    phone = phone,
                    textoffers = textoffers,
                };

                // The chosen preferences are returned as a semi-colon separated string of preference IDs
                if (!String.IsNullOrEmpty(foodprefs))
                {
                    string[] userprefs = foodprefs.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    // The subscriber's food preferences
                    foodpreference[] preferences = new foodpreference[userprefs.Length];

                    for (int i = 0; i < userprefs.Length; i++)
                    {
                        preferences[i] = new foodpreference()
                        {
                            subscriberid = s.id,
                            preferenceid = Int32.Parse(userprefs[i])
                        };
                    }

                    // Entity Framework goodness. The associated foodpreferences records will be created as the subscriber record is saved.
                    s.foodpreferences = preferences;
                }
                // Save the new subscriber record and associated food preferences
                db.subscribers.Add(s);
                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception X)
            {
                ServerError err = new ServerError(ServerErrorNumber.ThrewException);
                err.ErrorDetails = X.Message;
                return Json(err, Global.JSONSettings());
            }

            return Json(new ServerError(), Global.JSONSettings());
        }

        [Route("SubscriberServices/MealOffers")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult MealOffers()
        {
            // Might want to have a date parameter posted in. For now, just assume 'today'
            // NameValueCollection f = HttpContext.Current.Request.Form;
            // string requesteddate = f["requesteddate"];

            var r = new MealOfferReturn();

            lastcallEntities db = new lastcallEntities();

            DateTime today = DateTime.Today;

            // Select the offer records where the date matches today and the quantity available is not zero
            foodoffer[] offers = (from x in db.foodoffers where (x.offerdate == today) && (x.qtyavailable != 0) select x).ToArray();
            // Substitute this next line for the line above in order to test without the date constraint -- makes it easier to set up test data
            // Use '(from x in db.foodoffers select x).ToArray()' to see everything in the table
            //foodoffer[] offers = (from x in db.foodoffers where (x.qtyavailable != 0) select x).ToArray();

            if (offers == null)
            {
                // No offers available today
                r.error = new ServerError(ServerErrorNumber.NoOffersAvailable);

                return Json(r, Global.JSONSettings());
            }

            // Initialize the return array
            r.mealoffers = new mealoffer[offers.Length];

            // Fill it in
            // TODO: Start and end times should probably not allow nulls
            for (int i = 0; i < offers.Length; i++)
            {
                r.mealoffers[i] = new mealoffer
                {
                    ID = offers[i].ID,
                    Description = offers[i].offerdescription,
                    End = (offers[i].offerendtime == null ? "" : Convert.ToDateTime(offers[i].offerendtime).ToString("hh:mm tt")),
                    FoodType = offers[i].foodtype.foodtype1,
                    Name = offers[i].offername,
                    Qty = offers[i].qtyavailable,
                    Start = (offers[i].offerstarttime == null ? "" : Convert.ToDateTime(offers[i].offerstarttime).ToString("hh:mm tt")),
                    Supplier = offers[i].supplier.name
                };
            };

            return Json(r, Global.JSONSettings());
        }
    }
}

