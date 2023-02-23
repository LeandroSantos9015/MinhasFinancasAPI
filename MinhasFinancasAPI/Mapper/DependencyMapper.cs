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

            //services.AddScoped<ICompanyRepository, CompanyRepository>();
            //services.AddScoped<ICompanyGroupRepository, CompanyGroupRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IAccessGroupRepository, AccessGroupRepository>();
            //services.AddScoped<IModuleRepository, ModuleRepository>();
            //services.AddScoped<IPageRepository, PageRepository>();
            //services.AddScoped<IPermissionRepository, PermissionRepository>();

            #endregion

            #region Service

            //services.AddScoped<ICompanyService, CompanyService>();
            //services.AddScoped<ICompanyGroupService, CompanyGroupService>();
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IAccessGroupService, AccessGroupService>();
            //services.AddScoped<IModuleService, ModuleService>();
            //services.AddScoped<IPageService, PageService>();
            //services.AddScoped<IPermissionService, PermissionService>();

            #endregion
        }
    }
}