using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class DetailsViewModel
    {
        public Apartment apartment { get; set; }
        public User user { get; set; }
        public Stars Stars { get; set; }
        public ApartmentReview Review { get; set; }
    }
}
