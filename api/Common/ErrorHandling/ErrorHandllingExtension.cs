namespace newFitnet.Common.ErrorHandling
{
    internal static class ErrorHandllingExtension
    {
        internal static IServiceCollection AddExceptionHandling(this IServiceCollection services) 
        {
            services.AddExceptionHandler<GlobalExceptionHandller>();
            services.AddProblemDetails();
            return services;
        }

        internal static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        {
            app.UseExceptionHandler();
            return app;
        }

    }
}
