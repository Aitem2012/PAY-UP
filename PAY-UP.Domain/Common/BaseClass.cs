namespace PAY_UP.Domain.Common
{
    public abstract class BaseClass
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
