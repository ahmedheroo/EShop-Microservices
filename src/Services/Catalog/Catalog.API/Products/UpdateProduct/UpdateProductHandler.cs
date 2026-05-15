
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, string ImgUrl, List<string> Category)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductHandler(IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product =await session.LoadAsync<Product>(command.Id,cancellationToken);
            if (product is null)
                throw new ProductNotFoundException();

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

