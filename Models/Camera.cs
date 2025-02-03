namespace Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
    }
    public class MQTTConfig : BaseEntity
    {
        public Guid CameraId { get; set; }
        public string Name { get; set; }
        public string HostName { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BaseTopicName { get; set; }
        public bool IsActive { get; set; }
        public void Update(MQTTConfig config)
        {
            HostName = config.HostName;
            Port = config.Port;
            UserName = config.UserName;
            Password = config.Password;
            BaseTopicName = config.BaseTopicName;
            CameraId = config.CameraId;
            IsActive = config.IsActive;
            Name = config.Name;
        }
        public override string ToString() => Name;
    }
}