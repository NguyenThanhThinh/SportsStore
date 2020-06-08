using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SportsStore.Data;
using SportsStore.Data.Extensions;
using System;

namespace SportsStore
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
			services.AddCors();
			services.AddControllersWithViews();
			services.AddDbContext<SportsStoreDbContext>(options =>
			options.UseSqlServer(Configuration.GetConnectionString("SportsStoreDbContext")));
			services.AddSwaggerGen(options => {
				options.SwaggerDoc("v1",
				new OpenApiInfo { Title = "SportsStore API", Version = "v1" });
			});
		
			// In production, the Angular files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseCors(options =>
			{
				options.WithOrigins("https://localhost:5000").AllowAnyMethod().AllowAnyHeader();
				options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
				options.WithOrigins("https://localhost:44315").AllowAnyMethod().AllowAnyHeader();
			});
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			if (!env.IsDevelopment())
			{
				app.UseSpaStaticFiles();
			}

			app.UseRouting();
		
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
			});
			app.UseSwagger();
			app.UseSwaggerUI(options => {
				options.SwaggerEndpoint("/swagger/v1/swagger.json",
				"SportsStore API");
			});
			app.UseSpa(spa => {
				string strategy = Configuration
					.GetValue<string>("DevTools:ConnectionStrategy");
				if (strategy == "proxy")
				{
					spa.UseProxyToSpaDevelopmentServer("http://127.0.0.1:4200");
				}
				else if (strategy == "managed")
				{
					spa.Options.SourcePath = "ClientApp";
					spa.UseAngularCliServer("start");
				}
			});

			SeedData.SeedDatabase(services.GetRequiredService<SportsStoreDbContext>());
		}
	}
}
