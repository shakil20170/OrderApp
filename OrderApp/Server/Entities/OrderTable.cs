using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Server.Entity
{
    public class OrderTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public string  Name { get; set; }
        public string  State { get; set; }
        public ICollection<WindowTable> Windows { get; set; }
    }
}
