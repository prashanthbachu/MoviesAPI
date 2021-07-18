using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MoviesAPI.Repositories;
using MoviesAPI.Services;
using System.Text.Json.Serialization;

namespace MoviesAPI
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Movies API",
                    Version = "v1",
                    Description = "An API that allows movies to be searched and ranked by users."
                });

            });
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DoubleConverter());
            });
            services.AddDbContext<MoviesContext>();
            services
                .AddScoped<IMoviesContext, MoviesContext>()
                .AddScoped<IMapper, Mapper>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IMoviesSerivce, MoviesService>()
                .AddScoped<IMediator, MediatorService>()
                .AddScoped<IRatingHelper, RatingHelper>()
                .AddScoped<IMoviesRepository, MoviesRepository>()
                .AddScoped<IMovieRatingsRepository, MovieRatingsRepository>()
                ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies API V1");
            });

            var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<IMoviesContext>();
            bool dbCreated = context.Database.EnsureCreated();
            if (dbCreated)
            {
                InitializeData.Initialize();
                var ratingHelper = serviceScope.ServiceProvider.GetRequiredService<IRatingHelper>();
                ratingHelper.CalculateAverageRating().Wait();
            }
        }
    }
}
