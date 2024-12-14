using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Appoinment;
using CuidandoPawsApi.Application.DTOs.ServiceCatalog;
using CuidandoPawsApi.Domain.Enum;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase.Appoinment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.Appointment
{
    public class GetAppoinmentLastAddedOnDate : IGetAppoinmentLastAddedOndate<AppoinmentDTos>
    {
        private readonly IAppoinmentRepository _appoinmentRepository;
        private readonly IMapper _mapper;

        public GetAppoinmentLastAddedOnDate(IAppoinmentRepository appoinmentRepository, IMapper mapper)
        {
            _appoinmentRepository = appoinmentRepository;
            _mapper = mapper;
        }

        public async Task<AppoinmentDTos> GetLastAddedOnDateAsync(FilterDate filterDate, CancellationToken cancellationToken)
        {
            DateTime date = DateTime.UtcNow;

            if (filterDate == FilterDate.LastDay)
            {
                date = date.AddDays(-1);
                var lastDay = await _appoinmentRepository.GetLastAppoinmentAddedOnDateAsync(date,cancellationToken);
                var filter = _mapper.Map<AppoinmentDTos>(lastDay);
                return filter;
            }
            else if (filterDate == FilterDate.LasWeek)
            {
                date = date.AddDays(-7);
                var lastWeek = await _appoinmentRepository.GetLastAppoinmentAddedOnDateAsync(date,cancellationToken);
                var filterDto = _mapper.Map<AppoinmentDTos>(lastWeek);
                return filterDto;
            }
            else if (filterDate == FilterDate.LastThreeDay)
            {
                date = date.AddDays(-3);
                var lastThreeDay = await _appoinmentRepository.GetLastAppoinmentAddedOnDateAsync(date,cancellationToken);
                var filterDto = _mapper.Map<AppoinmentDTos>(lastThreeDay);
                return filterDto;
            }

            return null;
        }
    }
}
