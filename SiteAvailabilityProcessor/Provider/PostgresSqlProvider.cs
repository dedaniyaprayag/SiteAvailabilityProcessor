using Dapper;
using Npgsql;
using SiteAvailabilityProcessor.Models;
using System;
using System.Threading.Tasks;

namespace SiteAvailabilityProcessor.Provider
{
    public class PostgresSqlProvider : IDbProvider
    {
        private readonly IPostgreSqlConfiguration _postgreConfiguration;
        public PostgresSqlProvider(IPostgreSqlConfiguration postgreConfiguration)
        {
            _postgreConfiguration = postgreConfiguration;
        }
        public async Task<bool> InsertAsync(SiteDto site)
        {
            using var conn = new NpgsqlConnection(_postgreConfiguration.ConnectionString);
            conn.Open();
            string sql = @"INSERT INTO sitehistoricaldata (userid,site,status,timestamp)
                                VALUES (@UserId,@Site,@Status,@Timestamp)";
            return Convert.ToBoolean(await conn.ExecuteAsync(sql, site));
        }
    }
}
