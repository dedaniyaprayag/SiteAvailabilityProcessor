namespace SiteAvailabilityProcessor.Provider
{
    /// <summary>
    /// PostgresSql Configuration
    /// </summary>
    public class PostgreSqlConfiguration : IPostgreSqlConfiguration
    {
        public string ConnectionString { get; set; }
    }
}
