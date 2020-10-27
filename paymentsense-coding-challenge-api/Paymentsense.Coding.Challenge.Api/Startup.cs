using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Paymentsense.Coding.Challenge.Api.Cache;
using Paymentsense.Coding.Challenge.Api.Extensions;
using Paymentsense.Coding.Challenge.Api.Repository;
using Paymentsense.Coding.Challenge.Api.RestCountries;
using Paymentsense.Coding.Challenge.Api.Services;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Paymentsense.Coding.Challenge.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHealthChecks();
            services.AddCors(options =>
            {
                options.AddPolicy("PaymentsenseCodingChallengeOriginPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddDistributedMemoryCache();
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            
            services.AddSettings<RestCountriesSettings>(Configuration, RestCountriesSettings.SettingsKey);

            services.AddScoped<ICountryRepository, CountryRepository>();

            services.AddHttpClient<IRestCountriesClient, RestCountriesClient>(client =>
            {
                var restCountriesSettings =
                    Configuration.GetSection(RestCountriesSettings.SettingsKey).Get<RestCountriesSettings>();
                client.BaseAddress = new Uri(restCountriesSettings.APIURL);

            }).AddPolicyHandler(GetRetryPolicy());

            services.AddTransient<IRestCountriesService, RestCountriesService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors("PaymentsenseCodingChallengeOriginPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }

        IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            int retryCount = Configuration.GetValue<int>("DefaultHttpResiliencePolicy:RetryCount");
            int retryWaitMilliseconds = Configuration.GetValue<int>("DefaultHttpResiliencePolicy:RetryCount");
            int timeoutSeconds = Configuration.GetValue<int>("DefaultHttpResiliencePolicy:RetryCount");

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromMilliseconds(retryWaitMilliseconds))
                .WrapAsync(Policy.TimeoutAsync(timeoutSeconds, TimeoutStrategy.Optimistic));
        }
    }
}
