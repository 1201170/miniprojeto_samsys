using System.Threading.Tasks;

namespace miniprojeto_samsys.DAL.Repositories.Shared
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}