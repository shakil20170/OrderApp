using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Server.Entity
{
    public class WindowTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WindowID { get; set; }
        public string WindowName { get; set; }
        public int QuantityOfWindows { get; set; }
        public int TotalSubElements { get; set; }
        public int OrderTableOrderId { get; set; } //Foreign Key of WindowTable
        //Referrence Table
        public OrderTable OrderTable { get; set; }
        public ICollection<SubElementTable> SubElements { get; set; }
    }
}
