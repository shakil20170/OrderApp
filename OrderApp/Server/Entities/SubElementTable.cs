using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Server.Entity
{
    public  class SubElementTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubElementID { get; set; }
        public int WindowTableWindowID { get; set; } //Foreign Key of WindowTable
        public int Element { get; set; }
        public string  SubElementType { get; set; }
        public int  Width { get; set; }
        public int  Height { get; set; }

        //Referrence Table
        public WindowTable WindowTable { get; set; }
    }
}
