using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SEWKTand.Data;
using SEWKTand.Interfaces.Data;
using SEWKTand.Features.Admin.Interfaces;
using SEWKTand.Features.Dentist.Interfaces;
using SEWKTand.Features.Admin;
using SEWKTand.Features.Dentist;
using AutoMapper;
using SEWKTand.Features.Shared.User;
using SEWKTand.Features.Patient.Interfaces;
using SEWKTand.Features.Patient;
using SEWKTand.Features.Shared.Helpers;
using SEWKTand.Features.Shared.Security.Interfaces;
using SEWKTand.Features.Shared.Security;
using SEWKTand.Features.MedicalRecord.Interfaces;
using SEWKTand.Features.MedicalRecord;
using SEWKTand.Features.Shared.SSNRValidator.Interfaces;
using SEWKTand.Logging;

namespace SEWKTand
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
            //services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase());
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IDentistService, DentistServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IGenerateSecurePassword, GenerateSecurePassword>();
            services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            //services.AddScoped<IRfc2898DeriveBytes, Rfc2898DeriveBytesAdapter>();
            services.AddScoped<ISocialSecurityNumberVerification, SocialSecurityNumberVerification>();
            services.AddScoped<IDataContext, DataContext>();
            services.AddCors(); //To add CORS
            services.AddAutoMapper();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            app.UseCors(builder => //CORS Allow everything
           builder
           .AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod()
        );

            //Add the new middleware to pipeline
            app.UseMiddleware<RequestAndResponseLogging>();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
