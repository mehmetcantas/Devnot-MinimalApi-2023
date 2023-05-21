using FluentValidation;
using MinimalApiWithScanning.Models;

namespace MinimalApiWithScanning.Endpoints.Validators;

public class ProductValidator : AbstractValidator<Product> {
    public ProductValidator() {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Stock).NotNull().GreaterThan(0);
        RuleFor(x => x.Price).NotNull().GreaterThan(10);
    }
}