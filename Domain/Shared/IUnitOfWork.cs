using System.Threading.Tasks;

namespace miniprojeto_samsys.Domain.Shared
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}