namespace PAY_UP.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
