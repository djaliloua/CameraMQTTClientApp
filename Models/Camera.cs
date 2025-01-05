namespace Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
    }
    public class Camera : BaseEntity
    {
        public string Name { get; set; }
        public string TopicName { get; set; }
        public string HostName { get; set; }
        public bool IsActive { get; set; }
        public int Port { get; set; }
        public virtual Credential Credential { get; set; }
        public override string ToString() => Name;
    }
    public class  Credential:BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int CameraId { get; set; }
        public virtual Camera Camera { get; set; }
    }
}
