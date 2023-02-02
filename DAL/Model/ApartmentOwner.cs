using System;

namespace DAL.Model
{
    public class ApartmentOwner
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }

        public ApartmentOwner()
        {
        }

        public ApartmentOwner(int id, string name)
        {
            Id = id;
            Guid = Guid.NewGuid();
            Name = name;
        }
        public ApartmentOwner(int id, Guid guid, string name)
        {
            Id = id;
            Guid = guid;
            Name = name;
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}