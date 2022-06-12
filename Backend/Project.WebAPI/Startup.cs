using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project.Bll;
using Project.Dal.Abstract;
using Project.Dal.Concrete.Entityframework.Context;
using Project.Dal.Concrete.EntityFramework.Repository;
using Project.Dal.Concrete.EntityFramework.UnitOfWork;
using Project.Entity.Dto;
using Project.Entity.Models;
using Project.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.WebAPI
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
            #region JWT Token Service
            services
                 .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(cfg =>
                 {
                     cfg.SaveToken = true;
                     cfg.RequireHttpsMetadata = false;

                     cfg.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidIssuer = Configuration["Tokens:Issuer"],
                         ValidAudience = Configuration["Tokens:Issuer"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                         RequireSignedTokens = true,
                         RequireExpirationTime = true
                     };
                 });

            #endregion

            #region Application Context
            services.AddDbContext<SiteDBContext>
                (
                    ob => ob.UseSqlServer(Configuration.GetConnectionString("SqlServer"))
                );
            services.AddScoped<DbContext, SiteDBContext>();
            #endregion

            #region Service Section
            services.AddScoped<IApartmentService, ApartmentManager>();
            services.AddScoped<IMessageService, MessageManager>();
            services.AddScoped<IInvoiceService, InvoiceManager>();
            services.AddScoped<IRoleService, RoleeManager>();
            services.AddScoped<IUserService, UserrManager>();
            #endregion

            #region Repository Section
            services.AddScoped<IApartmentRepository, ApartmentRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            #endregion

            #region Unit Of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region E-mail
            services.Configure<DtoMailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailManager>();
            #endregion

            #region CORS
            services.AddCors(c =>
            {
                c.AddPolicy("AllowPolicy", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            #endregion


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project.WebAPI", Version = "v1" });
            });
        }


        #region Data Seeding
        public static class DataSeeding
        {
            public static void Seed(IApplicationBuilder app, IConfiguration configuration)
            {              
                
                var scope = app.ApplicationServices.CreateScope();
                var context = scope.ServiceProvider.GetService<SiteDBContext>();
                //context.Database.Migrate();

                if (context.Roles.Count() == 0)
                {
                    context.Roles.AddRange(
                        new List<Role>() {
                         new Role() { RoleName="Admin" },
                         new Role() { RoleName="Yönetici" },
                        });
                }
                context.SaveChanges();

                if (context.ApartmentStatuses.Count() == 0)
                {
                    context.ApartmentStatuses.AddRange(
                        new List<ApartmentStatus>() {
                         new ApartmentStatus() { ApartmentStatus1 = "Dolu" },
                         new ApartmentStatus() { ApartmentStatus1 = "Boþ" },
                        }
                        );
                }
                context.SaveChanges();

                if (context.UserTypes.Count() == 0)
                {
                    context.UserTypes.AddRange(
                        new List<UserType>() {

                         new UserType() { UserTypes = "Ev Sahibi" },
                         new UserType() { UserTypes = "Kiracý" },
                         new UserType() { UserTypes = "Diðer" }
                        }
                        );
                }
                context.SaveChanges();

                if (context.InvoiceTypes.Count() == 0)
                {
                    context.InvoiceTypes.AddRange(
                    new List<InvoiceType>() {
                         new InvoiceType() { InvoiceTypes = "Elektrik" },
                         new InvoiceType() { InvoiceTypes = "Su" },
                         new InvoiceType() { InvoiceTypes = "Doðalgaz" },
                         new InvoiceType() { InvoiceTypes = "Aidat" },
                         new InvoiceType() { InvoiceTypes = "Diðer" },
                        }
                        );
                }
                context.SaveChanges();

                if (context.InvoiceStatuses.Count() == 0)
                {
                    context.InvoiceStatuses.AddRange(
                    new List<InvoiceStatus>() {
                         new InvoiceStatus() { InvoiceStatus1 = "Ödendi" },
                         new InvoiceStatus() { InvoiceStatus1 = "Ödenmedi" },
                        }
                        );
                }
                context.SaveChanges();

                if (context.MessageStatuses.Count() == 0)
                {
                    context.MessageStatuses.AddRange(
                    new List<MessageStatus>() {
                         new MessageStatus() { MessageStatu = "Okundu" },
                         new MessageStatus() { MessageStatu = "Okunmadý" },
                        }
                        );
                }
                context.SaveChanges();

                if (context.Users.Count() == 0)
                {
                    var encryptedLoginPass = new HashManager(configuration).Encrypt("123");

                    context.Users.AddRange(
                    new List<User>() {
                         new User() { UserName = "malitunay", Email="malitunay1@gmail.com", Password=encryptedLoginPass, IsActive=true, PhoneNumber="5554443322", RolId=1, Tcnumber="1231231231", UserTypeId = 3, Plate = "34KR344"  },
                         new User() { UserName = "unassignedUser", Email="malitunay2@gmail.com", Password=encryptedLoginPass, IsActive=true, PhoneNumber="5554443322", RolId=2, Tcnumber="1231231231", UserTypeId = 3, Plate = "34KR344"  },
                        }
                        );
                }
                context.SaveChanges();
                
            }
        }
        #endregion

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration con)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DataSeeding.Seed(app, con);
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project.WebApi v1"));
            }

            #region CORS
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            #endregion

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
