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
        public override string ToString() => Name;
    }
}
