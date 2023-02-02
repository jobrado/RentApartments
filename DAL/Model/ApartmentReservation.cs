using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class ApartmentReservation
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public Apartment ApartmentId { get; set; }
        public string Details { get; set; }
        public User UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserAddress { get; set; }

        public ApartmentReservation()
        {
            Guid = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            Details = "lorem ipsum";
        }

        public override string ToString()
        {
            return $"Name: {UserName}, Email: {UserEmail}, Phone: {UserPhone}";
        }
    }
}
