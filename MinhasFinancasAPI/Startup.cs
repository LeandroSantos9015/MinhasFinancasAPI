//using Mapper;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinhasFinancasAPI.Mapper;
using System.Text;

namespace MinhasFinancasAPI
{
    public class Startup
    {
        private const string securityKey = "AchaveDeSegurancaTemQueSerMaiorQue128Bits";
        private const string nomeEmpresa = "Minhas Finanças";
        private const string versaoSw = "v1";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddRouting(r => r.SuppressCheckForUnhandledSecurityMetadata = true);

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(versaoSw, new OpenApiInfo { Title = nomeEmpresa, Version = versaoSw });

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //x.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

            });

            DependencyMapper.MapDependenceInjection(services);

            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = "leandro",
                     ValidAudience = "leandro",
                     IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(securityKey))
                 };

                 options.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         Console.WriteLine("Token Inválido " + context.Exception.Message);
                         return Task.CompletedTask;
                     },
                     OnTokenValidated = context =>
                     {
                         Console.WriteLine("Token Válido " + context.SecurityToken);
                         return Task.CompletedTask;
                     }
                 };
             });




            services.AddMvc();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:3000");
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
          
            else
                app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.RoutePrefix = string.Empty; // apresentar swagger como default
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
