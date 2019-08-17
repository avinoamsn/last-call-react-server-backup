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
            FoodTypesReturn r = new FoodTypesReturn();
            r.foodtypes = db.foodtypes.ToArray();

            //TODO: temp, for debug
            Newtonsoft.Json.JsonSerializerSettings serializer = new Newtonsoft.Json.JsonSerializerSettings();
            serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

            return Json(r,serializer);
        }
    }
}
