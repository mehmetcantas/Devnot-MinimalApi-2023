using FluentValidation;

namespace StructuredMinimalApi.Services.Product.Validators;

public class ProductValidator : AbstractValidator<Product> {
    public ProductValidator() {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Stock).NotNull().GreaterThan(0);
        RuleFor(x => x.Price).NotNull().GreaterThan(10);
    }
}