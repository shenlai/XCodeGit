namespace XCode.Domain
{
    public class Category:AggreagteRoot
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
