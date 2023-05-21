using FluentValidation;

namespace StructuredMinimalApi.Services.Product.Validators;

public static class ValidationExtension
{
    public static RouteHandlerBuilder WithValidation<T>(this RouteHandlerBuilder builder) where T : class
    {
        builder.Add(eb =>
        {
            var originalDelegate = eb.RequestDelegate;

            eb.RequestDelegate = async context =>
            {
                var validator = context.RequestServices.GetService<IValidator<T>>();
                if (validator == null)
                {
                    await originalDelegate!(context);
                    return;
                }

                context.Request.EnableBuffering();

                var model = await context.Request.ReadFromJsonAsync<T>();

                if (model == null)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new {Message = "Invalid model"});
                }

                var validationResult = await validator.ValidateAsync(model!);
                if (!validationResult.IsValid)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(validationResult.Errors);
                    return;
                }

                context.Request.Body.Position = 0;
                await originalDelegate!(context);

            };
        });

        return builder;
    }
}