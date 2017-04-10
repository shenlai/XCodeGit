
using XCode.Infrastructure;
namespace XCode.Domain
{
    public abstract class AggreagteRoot : IAggregateRoot
    {
        public int Id { get; set; }
    }
}
