namespace XCode.Domain
{
    public class Category: AggregateRoot
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
