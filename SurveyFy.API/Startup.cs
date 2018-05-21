using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SurveyFy.API.Models;
using Microsoft.EntityFrameworkCore;

namespace SurveyFy.API
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
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAllHeaders",
            //          builder =>
            //          {
            //              builder.AllowAnyOrigin()
            //                     .AllowAnyHeader()
            //                     .AllowAnyMethod();
            //          });
            //});

            var mvcCoreBuilder = services.AddMvcCore();

            mvcCoreBuilder
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            var connection = @"Server=ACCESS-FKKZK32\SQL2016;Database=Surveyfy;User Id=Surveyfy;Password=1q2w3e4r;ConnectRetryCount=0";
            services.AddDbContext<SurveyfyContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //     app.UseCors(builder =>
            //builder.WithOrigins("http://localhost.com"));
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            //app.UseCors("AllowAllHeaders");
            //}

            app.UseMvc();
        }
    }
}