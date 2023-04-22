using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FleetApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Adding cors for the purpose of comsumption by other clients
            //who are not necessarilly be on the same server with this project.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", a => a.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            });

            //configuring and setting up the database path
            builder.Services.AddDbContext<FleetDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddTransient<IDbConnection>(prov => new SqlConnection(prov.GetService<IConfiguration>().GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();


            //Get all records from the database
            app.MapGet("/driver", async (FleetDbContext db) =>                
                await db.Drivers.ToListAsync()               

            );

            //Get specific records from the database
           app.MapGet("/driver/{id}", async (int id, FleetDbContext db) =>
                await db.Drivers.FindAsync(id)
                    is Driver drivers
                    ? Results.Ok(drivers)
                    : Results.NotFound()

            );

            //Modify specific records from the database
            app.MapPut("/driver/{id}", async (int id, Driver drivers, FleetDbContext db) =>
              {
                  var record = await db.Drivers.FindAsync(id);
                  if(record == null)
                  {
                      return Results.NotFound();
                  }

                  record.Name = drivers.Name;
                  record.Gender = drivers.Gender;
                  record.Address = drivers.Address;
                  record.Contact = drivers.Contact;
                  record.Rank = drivers.Rank;
                  record.LicenseNo = drivers.LicenseNo;


                  await db.SaveChangesAsync();

                  return Results.NoContent();

              });

            //Modify specific records from the database
            app.MapPost("/driver", async (Driver drivers, FleetDbContext db) =>
            {
                db.Add(drivers);

                await db.SaveChangesAsync();

                return Results.Created($"/driver", drivers);

            });

            //Delete specific records from the database
            app.MapDelete("/driver/{id}", async (int id, FleetDbContext db) =>
            {
                var record = await db.Drivers.FindAsync(id);
                if (record == null)
                {
                    return Results.NotFound();
                }

                db.Remove(record);

                await db.SaveChangesAsync();

                return Results.NoContent();

            });


            //Get all records from the database
            app.MapGet("/car", async (FleetDbContext db) =>
                await db.Cars.ToListAsync()

            );

            //Get specific records from the database
            app.MapGet("/car/{id}", async (int id, FleetDbContext db) =>
                 await db.Cars.FindAsync(id)
                     is Car cars
                     ? Results.Ok(cars)
                     : Results.NotFound()
            );


            //Modify specific records from the database
            app.MapPut("/car/{id}", async (int id, Car cars, FleetDbContext db) =>
            {
                var record = await db.Cars.FindAsync(id);
                if (record == null)
                {
                    return Results.NotFound();
                }

                record.Brand = cars.Brand;
                record.Model = cars.Model;
                record.EngineNumber = cars.EngineNumber;
                record.Status = cars.Status;
                
                await db.SaveChangesAsync();

                return Results.NoContent();

            });

            //Modify specific records from the database
            app.MapPost("/car", async (Car cars, FleetDbContext db) =>
            {
                db.Add(cars);

                await db.SaveChangesAsync();

                return Results.Created($"/car", cars);

            });

            //Delete specific records from the database
            app.MapDelete("/car/{id}", async (int id, FleetDbContext db) =>
            {
                var record = await db.Cars.FindAsync(id);
                if (record == null)
                {
                    return Results.NotFound();
                }

                db.Remove(record);

                await db.SaveChangesAsync();

                return Results.NoContent();

            });


            app.Run();
        }
    }
}