using FinAccountingWebService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc(opt => opt.InputFormatters.Insert(0, new TextMediaTypeFormatter()));

AppSettings.SetSettings(builder.Configuration);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

//app.MapGet("/", () => "Hello World!");

//app.MapPost("/api/recieptByRequisits", async (context) => await ReceiptByRequisits(context));

app.Run();