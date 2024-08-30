using Agro.Model.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Agro.Model.Entities
{
    public class Request: RefEntity
    {
        public Request() : base()
        {
            Status = RequestStatus.Active;
            Intentions = Enumerable.Empty<Intention>().ToList();
        }

        public virtual IList<Intention> Intentions { get; set; }
        public RequestStatus Status { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Исполнитель")]
        public virtual ApplicationUser User { get; set; }
        [Display(Name = "Город")]
        public int? CityId { get; set; }
        [Display(Name = "Город")]
        public virtual City City { get; set; }
        //public RequestStatus RequestStatus { get; set; }

    }
}
