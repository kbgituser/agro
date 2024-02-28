using PlatF.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.Dto.Request
{
    public class RequestDto
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public int Price { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public int CityId { get; set; }
        public string Message { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public int? CategoryId { get; set; }
    } 
}
