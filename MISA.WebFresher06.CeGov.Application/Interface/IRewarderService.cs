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
    public interface IRewarderService : IReadOnlyService<RewarderDto, Guid>
    {
    }
}
