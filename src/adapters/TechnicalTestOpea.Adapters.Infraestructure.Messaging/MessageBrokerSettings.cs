namespace TechnicalTestOpea.Adapters.Infraestructure.Messaging
{
    public class MessageBrokerSettings
    {
        public string Host { get; set; } = "/";
        public string User { get; set; } = string.Empty;
        public string Pass { get; set; } = string.Empty;
        public string VirtualHost { get; set; } = string.Empty;
    }
}
