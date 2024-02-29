using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Concretes;
using BACampusApp.Business.TypedHttpClients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace BACampusApp.Business.Extensions
{
    public static class DependencyInjection
    {
        private const string TurkishLanguageCode = "tr-TR";
        private const string EnglishLanguageCode = "en-US";

        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //Her gelen request için oluşturulacak instance oluşturuyor

            services.AddScoped<IStudentService, StudentManager>();
            services.AddScoped<IAdminService, AdminManager>();

            services.AddScoped<ITrainerService, TrainerManager>();

            services.AddHttpClient<IpApiService>();

            services.AddScoped<IQuestionService, QuestionManager>();
            services.AddScoped<IQuestionAnswerService, QuestionAnswerManager>();
            services.AddScoped<IExamService, ExamManager>();
            services.AddScoped<IQuestionTopicService, QuestionTopicManager>();
            services.AddScoped<IStudentExamService, StudentExamManager>();
            services.AddScoped<IStudentQuestionService, StudentQuestionManager>();
            services.AddScoped<IStudentAnswerService, StudentAnswerManager>();

            //Bu servisler error ve success messagge'lar için kullanılacak Localization extension'ının sisteme entegre edilmesini içerir.
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo(TurkishLanguageCode),
                    new CultureInfo(EnglishLanguageCode),
                };
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.DefaultRequestCulture = new RequestCulture(EnglishLanguageCode);

                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
                {
                    string defaultLanguage = EnglishLanguageCode;
                    var languages = context.Request.Headers["Accept-Language"].ToString().Split(',');
                    if (languages.Any(a => a.Contains("tr")))                    
                        defaultLanguage = TurkishLanguageCode;
                    
                    return await Task.FromResult(new ProviderCultureResult(defaultLanguage, defaultLanguage));
                }));
            });
            

            return services;
        }
    }
}
