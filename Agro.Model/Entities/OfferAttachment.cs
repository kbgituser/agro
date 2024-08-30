using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agro.Model.Entities
{
    public class OfferAttachment : Attachment
    {
        public int OfferId { get; set; }
        public virtual Offer Offer { get; set; }        
    }
}
