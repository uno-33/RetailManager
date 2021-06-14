using RMDesktopUI.Models;
using System.Threading.Tasks;

namespace RMDesktopUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string userName, string password);
    }
}