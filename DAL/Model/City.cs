using System;

namespace DAL.Model
{
    public class City
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public City()
        {
        }
        public City(int id, string name)
        {
            Id = id;
            Guid = Guid.NewGuid();
            Name = name;
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}