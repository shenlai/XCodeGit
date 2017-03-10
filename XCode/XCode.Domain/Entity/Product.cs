
namespace XCode.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public string ImageUrl { get; set; }

        public bool IsNew { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
