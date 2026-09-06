using ContractMaster.Data;
using ContractMaster.EndPoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
builder.AddData();
var app = builder.Build();
app.GetUserEndPoints();
app.GetContractEndPoints();
app.GetApprovalsEndPoints();
app.MigrateDB();
app.Run();

