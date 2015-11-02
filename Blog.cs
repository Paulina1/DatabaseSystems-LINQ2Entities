using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    public class Blog
    {
        public int BlogId { get; set; }
        public String Name { get; set; }
        public String Url { get; set; }
        public List<Post> Posts { get; set; }
    }

}
