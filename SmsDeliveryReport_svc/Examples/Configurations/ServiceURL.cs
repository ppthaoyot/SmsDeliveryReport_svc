namespace SmsDeliveryReport_svc.Examples.Configurations
{
    public class ServiceURL
    {
        public string ShortLinkApi { get; set; }
        public string SendSmsApi { get; set; }
        public bool SendSmsApiEnable { get; set; } = false;
    }
}