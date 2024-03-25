using Dotnet8_DockerProfile;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add DI - Add Service
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<VehicleDb>(opt => opt.UseInMemoryDatabase("VehicleList"));

var app = builder.Build();

// Configure Pipeline - Use Method...
app.MapGet("/vehicles", async (VehicleDb db) =>
    await db.Vehicles.ToListAsync());

app.MapGet("/vehicles/{id}", async (int id, VehicleDb db) =>
    await db.Vehicles.FindAsync(id));

app.MapPost("/vehicles", async (Vehicle vehicle, VehicleDb db) =>
{
    db.Vehicles.Add(vehicle);
    await db.SaveChangesAsync();
    return Results.Created($"/vehicles/{vehicle.Id}", vehicle);
});

app.MapPut("/vehicles/{id}", async (int id, Vehicle vehicle, VehicleDb db) =>
{
    var veh = await db.Vehicles.FindAsync(id);
    if (veh == null) return Results.NotFound();

    veh.Name = vehicle.Name;
    veh.IsComplete = vehicle.IsComplete;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/vehicles/{id}", async (int id, VehicleDb db) =>
{
    if (await db.Vehicles.FindAsync(id) is Vehicle vehicle)
    {
        db.Vehicles.Remove(vehicle);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});

app.UseSwagger();
app.UseSwaggerUI();
app.Run();
