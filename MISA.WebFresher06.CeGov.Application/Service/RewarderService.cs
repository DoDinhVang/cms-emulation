using MISA.WebFresher06.CeGov.Application;
using MISA.WebFresher06.CeGov.Application.Dto;
using MISA.WebFresher06.CeGov.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Application
{
    public class RewarderService : BaseReadOnlyService<Rewarder, RewarderDto, Guid>, IRewarderService
    {
        private readonly IRewarderRepository _rewarderRepository;
        public RewarderService(IRewarderRepository repository) : base(repository)
        {
            _rewarderRepository = repository;
        }

        public override RewarderDto MapTEntityToEntityDto(Rewarder rewarder)
        {
            var rewarderDto = new RewarderDto()
            {
                RewarderId = rewarder.RewarderId,
                RewarderName = rewarder.RewarderName
            };
           
            return rewarderDto;
        }
    }
}
