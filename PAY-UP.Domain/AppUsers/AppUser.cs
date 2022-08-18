using Microsoft.AspNetCore.Identity;
using PAY_UP.Domain.Mailing;
using PAY_UP.Domain.Messaging;

namespace PAY_UP.Domain.AppUsers
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        //one to many relationship
        public virtual ICollection<Mail> Mails { get; set; }
        public virtual ICollection<Sms> Smses { get; set; }
    }
}
