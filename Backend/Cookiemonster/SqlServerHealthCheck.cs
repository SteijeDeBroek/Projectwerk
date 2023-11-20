using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public class SqlServerHealthCheck : IHealthCheck
{
    private readonly IApplicationConfiguration _applicationConfiguration;
    public SqlServerHealthCheck(IApplicationConfiguration applicationConfiguration)
    {
        _applicationConfiguration = applicationConfiguration;
    }
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
        SqlConnection connection = new(_applicationConfiguration.ConnectionString);
        try
        {
            await connection.OpenAsync(cancellationToken);
        }
        catch (SqlException)
        {
            return HealthCheckResult.Unhealthy();
        }
        return HealthCheckResult.Healthy();
    }
}
