namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, decimal Price, string ImgUrl, List<string> Category)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductHandler(IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Pass the product data
            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                ImgUrl = command.ImgUrl,
                Category = command.Category
            };
            // Save to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            // Return response
            return new CreateProductResult(product.Id);
        }
    }
}