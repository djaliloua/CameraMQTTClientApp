namespace Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
    }
    public class MQTTConfig : BaseEntity
    {
        public Guid CameraId { get; set; }
        public string HostName { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BaseTopicName { get; set; }
        public void Update(MQTTConfig config)
        {
            HostName = config.HostName;
            Port = config.Port;
            UserName = config.UserName;
            Password = config.Password;
            BaseTopicName = config.BaseTopicName;
        }
    }
    public class Camera : BaseEntity
    {
        public string Name { get; set; }
        public string TopicName { get; set; }
        public string HostName { get; set; }
        public bool IsActive { get; set; }
        public string Port { get; set; }
        public virtual MQTTCredential Credential { get; set; }
        public override string ToString() => Name;
    }
    public class MQTTCredential : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int CameraId { get; set; }
        public virtual Camera Camera { get; set; }
    }
    
}