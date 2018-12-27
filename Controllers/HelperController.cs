using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LastCallServer.Controllers
{
    public class FoodTypesReturn
    {
        public ServerError error { get; set; }
        public foodtype[] foodtypes { get; set; }

        public FoodTypesReturn()
        {
            error = new ServerError();
        }
    }

    public class HelperController : ApiController
    {
        [Route("HelperServices/FoodTypes")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult FoodTypes()
        {
            lastcallEntities db = new lastcallEntities();
            db.Configuration.ProxyCreationEnabled = true;
            db.Configuration.LazyLoadingEnabled = false;        // This disables the EF default behavior of loading related tables (regardless of how the foreign key is set up).
                                                                // In this instance all we want are the ID and name of the food type.

            var r = new FoodTypesReturn()
            {
                foodtypes = db.foodtypes.ToArray()
            };

            return Json(r, Global.JSONSettings());
        }
    }
}
