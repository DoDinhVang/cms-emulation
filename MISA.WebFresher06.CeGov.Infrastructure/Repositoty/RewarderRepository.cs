using Dapper;
using MISA.WebFresher06.CeGov.Domain;
using MISA.WebFresher06.CeGov.Infrastructure;
using MySqlConnector;
using System.Data.Common;
using static Dapper.SqlMapper;

namespace MISA.WebFresher06.CeGov.Infrastructure
{
    public class RewarderRepository : BaseReadOnlyRepository<Rewarder, Guid>, IRewarderRepository
    {
        public RewarderRepository(string connectionString) : base(connectionString)
        {

        }

    }
}