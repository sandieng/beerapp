using BeerRosterAPI.ViewModels;
using SendGrid;
using System.Threading.Tasks;

namespace BeerRosterAPI.Services
{
    public interface IEmailService
    {
        Task<Response> Send(EmailVM email);
    }
}
