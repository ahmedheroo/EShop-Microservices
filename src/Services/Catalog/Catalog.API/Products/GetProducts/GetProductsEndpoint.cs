namespace Catalog.API.Products.GetProducts
{
    //public record GetProductsRequest();
    public record GetProductResponse(IEnumerable<Product> products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/products", async (ISender sender) =>
            {
                //var query = request.Adapt<GetProductsQuery>();
                var result = await sender.Send(new GetProductsQuery());
                var response = result.Adapt<GetProductResponse>();
                return Results.Ok(response);
            });
        }
    }
}
