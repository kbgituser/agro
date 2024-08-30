using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agro.Model.Entities
{
    public class Advertisement : RefEntity
    {
        public Advertisement(DateTime entryDate, bool isDeleted, string name, bool isActive, string code) : base(entryDate, isDeleted, name, isActive, code)
        {
        }
        public string InstagramUlr { get; set; }
        public string Description { get; set; }

    }
}
