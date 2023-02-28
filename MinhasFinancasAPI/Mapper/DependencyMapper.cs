using MinhasFinancasAPI.Repository.Implementation;
using MinhasFinancasAPI.Repository.Interface;
using MinhasFinancasAPI.Service.Implementation;
using MinhasFinancasAPI.Service.Interface;

namespace MinhasFinancasAPI.Mapper
{
    public class DependencyMapper
    {
        public static void MapDependenceInjection(IServiceCollection services)
        {
            #region Repository

            services.AddSingleton<IBaseRepository, BaseRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRegistroRepository, RegistroRepository>();


            #endregion

            #region Service

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRegistroService, RegistroService>();

            #endregion
        }
    }
}