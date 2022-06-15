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
        var assembly = Assembly.Load(an);
        foreach(AssemblyName subAssembly in assembly.GetReferencedAssemblies())
        {
            Console.WriteLine($"Assembly: {subAssembly.Name} referenced by {an.Name}");
            RegisterTransientsInAssembly(services, Assembly.Load(subAssembly));
        }
        RegisterTransientsInAssembly(services, assembly);
    } catch(FileNotFoundException e)
    {
        Console.WriteLine(e.Message);
    }
}

static void RegisterTransientsInAssembly(IServiceCollection services, Assembly assembly)
{
    var types = Enumerable.Where(Enumerable.Select(assembly.GetTypes()
        .Where(p => p.IsDefined(typeof(TransientAttribute), false)), s => new
    {
        Service = ((TransientAttribute)Attribute.GetCustomAttribute((MemberInfo)s, typeof(TransientAttribute))).Type,
        Implementation = s
    }), x => x.Service != null);
    foreach (var type in types)
    {
        services.AddTransient(type.Service, type.Implementation);
    }
}