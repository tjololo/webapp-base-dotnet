using System.Reflection;
using Tjololo.DI.Defaults;
using Tjololo.DI.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<PercentageCalculator, SimpleCalculator>();

RegisterCustomOverrides(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void RegisterCustomOverrides(IServiceCollection services)
{
    AssemblyName an = new AssemblyName("Tjololo.DI.Customize");
    Type transient = typeof(TransientAttribute);
    try
    {
        var types = Enumerable.Where(Enumerable.Select(Assembly.Load(an).GetTypes()
            .Where(p => p.IsDefined(transient, false)), s => new
        {
            Service = ((TransientAttribute)Attribute.GetCustomAttribute((MemberInfo)s, transient)).Type,
            Implementation = s
        }), x => x.Service != null);
        foreach (var type in types)
        {
            services.AddTransient(type.Service, type.Implementation);
        }
    } catch(FileNotFoundException e)
    {
        Console.WriteLine(e.Message);
    }
}