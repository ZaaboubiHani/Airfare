using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Models
{
    [Table("FlightHotels")]
    public class FlightHotelModel:ICloneable
    {
        [Key]
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int FlightId { get; set; }
        public float Feed { get; set; }
        public FlightModel Flight { get; set; }
        public HotelModel Hotel { get; set; }

        public object Clone()
        {
            return new FlightHotelModel
            {
                Id = this.Id,
                HotelId = this.HotelId,
                FlightId = this.FlightId,
                Feed = this.Feed,
                Flight = (FlightModel)this.Flight?.Clone(),
                Hotel = (HotelModel)this.Hotel?.Clone(),
            };
        }

        public static bool operator ==(FlightHotelModel flightHotel1, FlightHotelModel flightHotel2)
        {
            if (flightHotel1 is null)
            {
                if (flightHotel2 is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            else
            {
                if (flightHotel2 is not null)
                {
                    return flightHotel1.Equals(flightHotel2);
                }

                // Only the left side is null.
                return false;
            }
        }


        public static bool operator !=(FlightHotelModel flightHotel1, FlightHotelModel flightHotel2)
        {
            if((flightHotel1 is null && flightHotel2 is not null) || (flightHotel1 is not null && flightHotel2 is null))
            {
                return true;
            }
            if(flightHotel1 is null && flightHotel2 is null)
            {
                return false;
            }
            return flightHotel1.Id != flightHotel2.Id;
        }

        public override string ToString()
        {
            if (Hotel == null)
                return "";
            return Hotel.Name;
        }

    }
}
