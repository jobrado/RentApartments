using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Tag 
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public TagType TypeId { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public IList<Apartment> Apartment { get; set; }

        public Tag()
        {
            Guid = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
