namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery() : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> products);

    public class GetProductsHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToListAsync(cancellationToken);
            return new GetProductResult(products);
        }
    }
}