using System.Text;
using JavaCourseAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using JavaCourseAPI.Services.TestCodeServices;
using JavaCourseAPI.Services.TestCodeService;
using JavaCourseAPI.Services.GroovyCompilerServices;
using JavaCourseAPI.Repositories.AuthRepositories;
using JavaCourseAPI.Helpers;
using JavaCourseAPI.Models;
using JavaCourseAPI.Services.CategoryServices;
using JavaCourseAPI.Repositories.CategoryRepositories;
using JavaCourseAPI.Services.QuizServices;
using JavaCourseAPI.Repositories.QuizRepositories;
using JavaCourseAPI.Services.AdminPanelServices;
using JavaCourseAPI.Repositories.AdminPanelRepositories;
using JavaCourseAPI.Services.ExerciseServices;
using JavaCourseAPI.Repositories.ExerciseRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;

namespace JavaCourseAPI
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
            /* services.AddCors(options =>
             {
                 options.AddPolicy("CorsPolicy",
                     builder => builder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowCredentials());
             });*/

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            //var key = Encoding.ASCII.GetBytes(Configuration.GetSection("JWTSettings:JWTSecret").Value);
            //var key = Encoding.ASCII.GetBytes("Ptaki latajOM kluczem");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //database
            services.AddEntityFrameworkSqlServer().AddDbContext<Magisterka_v1Context>();
           // services.AddDbContext<Magisterka_v1Context>();

            //services:
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJavaCompilerService, JavaCompilerService>();
            services.AddScoped<ITestCodeService, TestCodeService>();
            services.AddScoped<IGroovyCompilerService, GroovyCompilerService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<IAdminPanelService, AdminPanelService>();
            services.AddScoped<IExerciseService, ExerciseService>();

            //repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<IAdminPanelRepository, AdminPanelRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();

            services.AddAutoMapper();

            /////////////////////
            services.AddCors();
            services.AddDirectoryBrowser();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            //////////////////////////
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            // app.UseAuthentication();
            //app.UseCors("CorsPolicy");

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseHttpsRedirection();


            app.UseMvc();
        }
    }
}
