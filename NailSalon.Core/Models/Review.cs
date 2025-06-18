using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
