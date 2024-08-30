using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agro.Model.Entities
{
    public class RequestAttachment : Attachment
    {
        public int RequestId { get; set; }
        public virtual Intention Request { get; set; }        
    }
}
