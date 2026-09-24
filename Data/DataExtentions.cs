namespace ContractMaster.Data;

using ContractMaster.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

public static class DataExtention
{
    public static void MigrateDB(this WebApplication app)
    {
        using var Scope = app.Services.CreateScope();
        var DbContext = Scope.ServiceProvider.GetRequiredService<ContractMasterContext>();
        DbContext.Database.Migrate();
    }

    public static void AddData(this WebApplicationBuilder builder)
    {
        builder.Services.AddSqlServer<ContractMasterContext>(
            builder.Configuration.GetConnectionString("ConnString"));
    }
}