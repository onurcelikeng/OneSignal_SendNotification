using Newtonsoft.Json;
using OneSignal_SendNotification.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace OneSignal_SendNotification.Helpers
{
    public enum DeviceEnum
    {
        iOS = 0,
        Android = 1,
        WindowsPhone = 2
    }

    public class OneSignalHelper
    {
        private readonly string BaseUrl = "https://onesignal.com/api/v1/notifications";


        public static OneSignalHelper Instance
        {
            get
            {
                return new OneSignalHelper();
            }
        }


        public DeviceResultModel AddDevice(AddUserDeviceDTO model)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/players") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("authorization", "Basic --rest api key--");

            var serializer = new JavaScriptSerializer();
            var obj = new
            {
                app_id = model.app_id, //req.
                device_type = model.device_type, //req.
                identifier = model.identifier,
                language = model.language,
                timezone = model.timezone,
                game_version = model.game_version,
                device_model = model.device_model,
                device_os = model.device_os
            };
            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = JsonConvert.DeserializeObject<DeviceResultModel>(reader.ReadToEnd());
                        return result;                         
                    };
                }
            }

            catch (Exception)
            {
                return new DeviceResultModel()
                {
                    success = false,
                    id = null
                };
            }
        }

        public void SendtoAllSubscribers()
        {
            var request = WebRequest.Create(BaseUrl) as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("authorization", "Basic --rest api key--");

            var serializer = new JavaScriptSerializer();
            var obj = new
            {
                app_id = "5eb5a37e-b458-11e3-ac11-000c2940e62c",
                contents = new { en = "Test Notification" },
                included_segments = new string[] { "All" }
            };
            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }

            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void SendToSpecificSegment()
        {
            var request = WebRequest.Create(BaseUrl) as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("authorization", "Basic --rest api key--");

            var serializer = new JavaScriptSerializer();
            var obj = new
            {
                app_id = "5eb5a37e-b458-11e3-ac11-000c2940e62c",
                contents = new { en = "English Message" },
                included_segments = new string[] { "Active Users" }
            };
            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
        }
    }
}