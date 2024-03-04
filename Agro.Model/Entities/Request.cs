using Agro.Model.Enums;
using PlatF.Model.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Agro.Model.Entities
{
    public class Request: RefEntity
    {
        public virtual IList<Intention> Intentions { get; set; }
        public PlatF.Model.Enums.IntentionStatus Status { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Исполнитель")]
        public virtual ApplicationUser User { get; set; }
        [Display(Name = "Город")]
        public int? CityId { get; set; }
        [Display(Name = "Город")]
        public virtual City City { get; set; }
        public RequestStatus RequestStatus { get; set; }

    }
}
