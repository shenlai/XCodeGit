
namespace XCode.Domain
{
    public class ProductCategorization : AggregateRoot
    {

        public ProductCategorization()
        { }

        public ProductCategorization(int productID, int categoryID)
        {
            this.CategoryId = categoryID;
            this.ProductId = productID;
        }

        public int CategoryId { get; set; }

        public int ProductId { get; set; }

        public override string ToString() => string.Format("CategoryID: {0}, ProductID: {1}", this.CategoryId, this.ProductId);

        // 通过商品对象和分类对象来创建商品分类对象
        public static ProductCategorization CreateCategorization(Product product, Category category)
        {
            return new ProductCategorization(product.Id, category.Id);
        }
    }
}
