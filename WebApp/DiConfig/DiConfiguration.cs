﻿using BaseModule.Repository.User;
using DomainModule.BaseRepo;
using DomainModule.Entity.Menu;
using DomainModule.Repository;
using DomainModule.RepositoryInterface;
using DomainModule.RepositoryInterface.ActivityLog;
using DomainModule.RepositoryInterface.AuditLog;
using DomainModule.RepositoryInterface.Menu;
using DomainModule.Service;
using DomainModule.ServiceInterface;
using DomainModule.ServiceInterface.Account;
using DomainModule.ServiceInterface.Email;
using DomainModule.ServiceInterface.Menu;
using InfrastructureModule.Repository;
using InfrastructureModule.Repository.ActivityLog;
using InfrastructureModule.Repository.AuditLog;
using InfrastructureModule.Repository.Menu;
using ServiceModule.Service;
using ServiceModule.Service.Email;
using ServiceModule.Service.Menu;
using System.Runtime.CompilerServices;


namespace WebApp.DiConfig
{
    public static class DiConfiguration
    {
        public static void  UseDIConfig(this IServiceCollection services)
        {
            UseRepository(services);
            UseService(services);
        }
        private static void UseRepository(IServiceCollection services)
        {
         services.AddScoped<UserRepositoryInterface,UserRepository>();
         services.AddScoped<RoleRepositoryInterface,RoleRepository>();
         services.AddScoped<IAuditLogRepository,AuditLogRepository>();
         services.AddScoped<IActivityLogRepository,ActivityLogRepository>();
         services.AddScoped<ICompanyTypeRepository,CompanyTypeRepository>();
         services.AddScoped<ICompanyRepository,CompanyRepository>();
         services.AddScoped<IAttachmentRepository,AttachmentRepository>();
         services.AddScoped<IItemRepository,ItemRepository>();
         services.AddScoped<IMenuCategoryRepository,MenuCategoryRepository>();
         services.AddScoped<IMenuCategoryImagesRepository,MenuCategoryImagesRepository>();
        }
        private static void UseService(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<UserServiceInterface, UserService>();
            services.AddScoped<RoleServiceInterface, RoleService>();
            services.AddScoped<IActivityLogService, ActivityLogService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
			services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator>();
			services.AddScoped<ICompanyTypeService, CompanyTypeService>();
			services.AddScoped<IAttachmentService, AttachmentService>();
			services.AddScoped<ICompanyService, CompanyService>();
			services.AddScoped<IMenuCategoryService, MenuCategoryService>();
			services.AddScoped<IItemService, ItemService>();

		}
	}
}
