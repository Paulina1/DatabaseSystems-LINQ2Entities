using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    public class User
    {
        [Key]
        public String UserName { get; set; }
        public String Description { get; set; }
    }
}
