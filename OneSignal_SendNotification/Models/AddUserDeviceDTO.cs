namespace OneSignal_SendNotification.Models
{
    public class AddUserDeviceDTO
    {
        public string app_id { get; set; }

        public int device_type { get; set; }

        public string identifier { get; set; }

        public string language { get; set; }

        public string timezone { get; set; }

        public string game_version { get; set; } //Version of your app

        public string device_model { get; set; }

        public string device_os { get; set; }
    }
}