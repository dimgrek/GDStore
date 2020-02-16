using System.Threading.Tasks;

namespace GDStore.BLL.Interfaces.Services
{
    public interface ICommandBus
    {
        Task SendAsync<T>(T command) where T : class;
    }
}