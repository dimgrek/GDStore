using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;
using GDStore.DAL.Interface.Domain;

namespace GDStore.Alterations.Services.Services
{
    public interface IAlterationService
    {
        Task<Alteration> AddAlteration(AddAlterationRequest request);
        Task MakeAlteration(MakeAlterationCommand command);
    }
}
