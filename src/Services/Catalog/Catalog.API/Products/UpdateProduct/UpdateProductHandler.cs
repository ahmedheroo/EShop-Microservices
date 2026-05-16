
using FluentValidation;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, string ImgUrl, List<string> Category)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(v => v.Id).NotEmpty().WithMessage("Id not provided");
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is empty")
                .Length(2,15).WithMessage("Name Length must be from 2 to 15 char");
            RuleFor(v => v.Description).NotEmpty().WithMessage("Description is empty");
            RuleFor(v => v.ImgUrl).NotEmpty().WithMessage("Image is empty");
            RuleFor(v => v.Category).NotEmpty().WithMessage("Category is empty");
            RuleFor(v => v.Price).GreaterThan(0).WithMessage("Price should be greater than 0");
        }
    }
    public class UpdateProductHandler(IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product =await session.LoadAsync<Product>(command.Id,cancellationToken);
            if (product is null)
                throw new ProductNotFoundException(command.Id);

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImgUrl = command.ImgUrl;
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);

        }
    }
}

