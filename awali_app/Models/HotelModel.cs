using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Models
{
    [Table("Hotels")]
    public class HotelModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoomsNumber { get; set; }
        public string Address { get; set; }
        public float Distance { get; set; }
        public int Rate { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public HotelModel Copy()
        {
            return new HotelModel() { Address = Address, Distance = Distance, Rate = Rate, Id = Id, Name = Name, RoomsNumber = RoomsNumber};
        }

        public static bool operator == (HotelModel hotel1, HotelModel hotel2)
        {
           

            if (hotel1 is null)
            {
                if (hotel2 is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            else
            {
                if (hotel2 is not null)
                {
                    return hotel1.Equals(hotel2);
                }

                // Only the left side is null.
                return false;
            }
          
        }

        public override bool Equals(object obj) => this.Equals(obj as HotelModel);

        public bool Equals(HotelModel p)
        {
            if (p is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, p))
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(HotelModel hotel1, HotelModel hotel2)
        {
            if ((hotel1 is null && hotel2 is not null) || (hotel1 is not null && hotel2 is null))
            {
                return true;
            }
            if (hotel1 is null && hotel2 is null)
            {
                return false;
            }

            return hotel1.Id != hotel2.Id;
        }

        public HotelModel()
        {

        }
    }
}
