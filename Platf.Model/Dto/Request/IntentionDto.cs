using PlatF.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.Dto.Request
{
    public class IntentionDto
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public int CityId { get; set; }
        public string Message { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IntentionStatus IntentionStatus { get; set; }
        public int? CategoryId { get; set; }
    } 
}
