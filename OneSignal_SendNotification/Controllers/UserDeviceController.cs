using OneSignal_SendNotification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OneSignal_SendNotification.Controllers
{
    [RoutePrefix("api/devices")]
    public class UserDeviceController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult AddDevice(AddUserDeviceDTO model)
        {

            return Ok();
        }
    }
}
