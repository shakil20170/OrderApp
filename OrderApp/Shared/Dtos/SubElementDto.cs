using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Shared.Dtos
{
    public class SubElementDto
    {
        //public int OrderId { get; set; }
        //public string Name { get; set; }
        //public string State { get; set; }

        //public int QuantityOfWindows { get; set; }
        //public int TotalSubElements { get; set; }
        //public int OrderTableOrderId { get; set; }

        //public int WindowID { get; set; }
        public string WindowName { get; set; }

        public int SubElementID { get; set; }
        public int WindowTableWindowID { get; set; }
        public int Element { get; set; }
        public string SubElementType { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        //public Task<Dictionary<int, string>> windowsList { get; set; }

    }
}
