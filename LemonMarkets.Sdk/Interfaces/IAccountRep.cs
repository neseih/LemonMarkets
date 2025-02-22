using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;

namespace LemonMarkets.Interfaces
{

    public interface IAccountRepo
    {

        Task<LemonResult<Account>> GetAsync (  );

    }

}