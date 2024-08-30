using Agro.Model.Enums;
using Agro.Model.Dto;
using Agro.Model.Dto.Intention;
using System.Collections.Generic;

namespace Agro.Model.Dto.Request
{
    public class RequestDto : RefDto
    {
        public virtual IList<IntentionDto> Intentions { get; set; }
        public RequestStatus Status { get; set; }
        public string UserId { get; set; }
        public int? CityId { get; set; }
        public RequestStatus RequestStatus { get; set; }
    }
}
