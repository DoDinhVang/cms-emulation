using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher06.CeGov.Application;
using MISA.WebFresher06.CeGov.Application.Dto;
using MISA.WebFresher06.CeGov.Domain;
using MISA.WebFresher06.CeGov.Domain.Constants;

namespace MISA.WebFresher06.CeGov.Controllers;


[Route("api/v1/[controller]")]
[ApiController]
public class RewarderController : BaseReadOnlyController<RewarderDto, Guid>
{
    private readonly IRewarderService _rewarderService;
    public RewarderController(IRewarderService rewarderService) : base(rewarderService)
    {
        _rewarderService = rewarderService;
    }
    
}
