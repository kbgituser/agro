using Microsoft.Extensions.Logging;
using Agro.Model.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class OfferService
    {
        private UnitOfWork _unitOfWork;
        private readonly ILogger<OfferService> _logger;

        public OfferService(UnitOfWork unitOfWork, ILogger<OfferService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }



    }
}
