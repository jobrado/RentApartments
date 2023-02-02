using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DAL.Model
{
    public class User
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [DisplayName("Email: ")]
        [EmailAddress(ErrorMessage = "Please enter valid email!")]
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        [DisplayName("Password: ")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        [DisplayName("Phone number: ")]
        [Required(ErrorMessage = "Phone number is required!")]
        [Phone(ErrorMessage ="Please enter valid phone number!")]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public DateTime LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        [DisplayName("Username: ")]
        [Required(ErrorMessage = "Username is required!")]
        public string UserName { get; set; }
        [DisplayName("Address: ")]
        [Required(ErrorMessage = "Adress is required!")]
        public string Address { get; set; }
        public Role Role { get; set; }

        public User(int id, Guid guid, DateTime createdAt, DateTime? deletedAt, string email, bool emailConfirmed, string passwordHash, string securityStamp, string phoneNumber, bool phoneNumberConfirmed, DateTime lockoutEndDateUtc, bool lockoutEnabled, int accessFailedCount, string userName, string address)
        {
            Id = id;
            Guid = guid;
            CreatedAt = createdAt;
            DeletedAt = deletedAt;
            Email = email;
            EmailConfirmed = emailConfirmed;
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            LockoutEndDateUtc = lockoutEndDateUtc;
            LockoutEnabled = lockoutEnabled;
            AccessFailedCount = accessFailedCount;
            UserName = userName;
            Address = address;
        
        }

        public User()
        {
            Guid = Guid.NewGuid();
            CreatedAt= DateTime.Now;
            PhoneNumberConfirmed = true;
            EmailConfirmed= true;
            LockoutEnabled= false;
            AccessFailedCount = 0;
        }

        public override string ToString()
        {
            return $"{UserName}";
        }
    }
    public enum Role
    {
        User,
        Admin
    }

}