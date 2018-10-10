using BeerRosterAPI.DTOs;
using SendGrid;
using System.Threading.Tasks;

namespace BeerRosterAPI.Services
{
    public interface IEmailService
    {
        Task<Response> Send(EmailDto email);
    }
}
