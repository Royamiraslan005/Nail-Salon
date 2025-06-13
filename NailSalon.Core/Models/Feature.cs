using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int AboutId { get; set; }
        public About About { get; set; }
    }

}
