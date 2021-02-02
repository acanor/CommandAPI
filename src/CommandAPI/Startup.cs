using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.Extensions.Configuration;

using Microsoft.EntityFrameworkCore;

using CommandAPI.Data;

using Npgsql;

using AutoMapper;

using CommandAPI.Profiles;

using Newtonsoft.Json.Serialization;

namespace CommandAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

    
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            //Agregado para pone en secreor usuario y contraseña
            var builder = new NpgsqlConnectionStringBuilder();
            builder.ConnectionString = 
                Configuration.GetConnectionString("PostgreSqlConnection");
            builder.Username = Configuration["UserID"];
            builder.Password = Configuration["Password"];
            
            // Agregando servicio para base de datos
            services.AddDbContext<CommandContext>(opt => opt.UseNpgsql
                (builder.ConnectionString));
            // Seccion 1 Agregado codigo de abajo
            //Agregado PATCH de json
            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            //Esto cambio con respecto al original
            services.AddAutoMapper(typeof(CommandsProfile));

            //Para agregar inyeccion de interfaces
            services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CommandContext context)
        {
            context.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                    //Seccion 2. Agrega codigo abajo
                    endpoints.MapControllers();
            });
        }
    }
}
