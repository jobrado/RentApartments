using System;

namespace DAL.Model
{
    public class TagType
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
    }
}