namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, string Description, decimal Price, string ImgUrl, List<string> Category);
    public record CreateProductResponse(Guid Id);

    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/products", async (CreateProductRequest req, ISender Sender) =>
            {
                var command = req.Adapt<CreateProductCommand>();
                var result = await Sender.Send(command);
                var response = result.Adapt<CreateProductResponse>();
                return Results.Created($"/products/{result.Id}", response);
            });
        }
    }
}