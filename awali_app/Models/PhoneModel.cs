using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Models
{
    [Table("Phones")]
    public class PhoneModel
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }
        public int ClientId { get; set; }
        public ClientModel Client { get; set; }
    }
}
