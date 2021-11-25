using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_DataLayer
{
    public class Customer: Person
    {
      
        public int CustomerID { get; set; }



    }
}
