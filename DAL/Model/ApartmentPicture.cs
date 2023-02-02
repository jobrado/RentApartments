using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class ApartmentPicture
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Apartment ApartmentId { get; set; }
        public string Path { get; set; }
        public string Base64Content { get; set; }
        public string Name { get; set; }
        public bool? IsRepresentative { get; set; }

        public ApartmentPicture(string path, string base64Content, string name, bool? isRepresentative)
        {
            Guid = Guid.NewGuid();
            CreatedAt = DateTime.Now;

            Path = path;
            Base64Content = base64Content;
            Name = name;
            IsRepresentative = isRepresentative;
        }
        public ApartmentPicture(string path, string name, bool? isRepresentative)
        {

            Path = path;
            Name = name;
            IsRepresentative = isRepresentative;
        }

        public ApartmentPicture()
        {
            Guid = Guid.NewGuid();
        }
    }
}
