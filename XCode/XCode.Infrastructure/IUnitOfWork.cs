
namespace XCode.Infrastructure
{
    /// <summary>
    /// 统一工作单元
    /// 参考： http://www.cnblogs.com/xishuai/p/3750154.html
    /// </summary>
    public interface IUnitOfWork
    {

        void Commit();

        bool Committed { get; }
        
        void Rollback();
    }
}
