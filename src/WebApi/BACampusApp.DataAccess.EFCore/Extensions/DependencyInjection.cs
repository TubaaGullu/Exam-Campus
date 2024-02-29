
namespace BACampusApp.DataAccess.EFCore.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddEFCoreServices(this IServiceCollection services)
    {


        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IAdminRepository,AdminRepository>();
        services.AddScoped<ITrainerRepository,TrainerRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IQuestionAnswerRepository, QuestionAnswerRepository>();
        services.AddScoped<IExamRepository, ExamRepository>();
        services.AddScoped<IQuestionTopicRepository, QuestionTopicRepository>();
        services.AddScoped<IStudentAnswerRepository, StudentAnswerRepository>();
        services.AddScoped<IStudentExamRepository, StudentExamRepository>();
        services.AddScoped<IStudentQuestionRepository, StudentQuestionRepository>();
   
        return services;

    }
}
