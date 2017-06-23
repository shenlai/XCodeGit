
using XCode.Infrastructure;
namespace XCode.Domain
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        public int Id { get; set; }
    }
}
