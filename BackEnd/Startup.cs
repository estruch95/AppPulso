using backPulso;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPulso
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey")); //Lectura de la llave secreta

            //Adición de la autenticación por JWT (Json Web Token)
            //Hay que instalar la librería JwtBearerDefaults netcore, no viene instalada de forma nativa
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //Aquí se define el método de autenticación que va a tener que va a ser por JWT
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false; //Aquí se define si van a ser obligatoriamente datos https
                x.SaveToken = true; //Aquí se define si vamos a guardar el token
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key), //Aquí incluimos la llave simétrica que nos servirá para encriptar y desencriptar los datos
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            

            services.AddCors(options => options.AddPolicy("AllowWebApp",
                           builder => builder.AllowAnyOrigin()
                           .WithOrigins("http://localhost:4200")
                         .AllowAnyHeader()
                       .AllowAnyMethod()));


            services.AddTransient<AplicationDbContext>();
            services.AddTransient<AplicationDbContext_DC>();
            /*services.AddDbContext<AplicationDbContext>(options =>
                            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
            services.AddDbContext<AplicationDbContext_DC>(options =>
                           options.UseSqlServer(Configuration.GetConnectionString("DevConnection_DC")));*/
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackEnd", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("AllowWebApp");
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackEnd v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); //Utilizar la autenticación - MUY IMPORTANTE - Debe ir antes del app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
