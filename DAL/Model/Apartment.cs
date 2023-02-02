using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Apartment
    {
        [DisplayName("ID")]
        public int IDApartment { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [Display(Name = "Owner", ResourceType = typeof(Resource))]
        public ApartmentOwner OwnerId { get; set; }
        public int TypeId { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public Status StatusId { get; set; }
        [Display(Name = "City", ResourceType = typeof(Resource))]
        public City CityId { get; set; }
        public ApartmentReservation Ar { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Resource))]
        public string Address { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        public string NameEng { get; set; }
        [Display(Name = "Price", ResourceType = typeof(Resource))]
        public decimal Price { get; set; }
        [Display(Name = "Maximumadults", ResourceType = typeof(Resource))]
        public int? MaxAdults { get; set; }
        [Display(Name = "Maximumchildren", ResourceType = typeof(Resource))]

        public int? MaxChildren { get; set; }
        [Display(Name = "Totalrooms", ResourceType = typeof(Resource))]

        public int? TotalRooms { get; set; }
        [Display(Name = "Distancefrombeach", ResourceType = typeof(Resource))]

        public int? BeachDistance { get; set; }
        public IList<Tag> Tag { get; set; }
        [Display(Name = "Pictures", ResourceType = typeof(Resource))]

        public IList<ApartmentPicture> Picture { get; set; }
        public List<Pictures> Pictures { get; set; }

        public Apartment()
        {
            Guid = Guid.NewGuid();

        }

        public override string ToString()
        {
            return $"{Name}";
        }
        public enum Status
        {
            Zauzeto = 1,
            Rezervirano = 2,
            Slobodno = 3

        }
    }

}
