using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frank.HaveIBeenPwned
{
    public interface IHaveIBeenPwnedService
    {
        Task<bool> CheckPassword(string password, int threshold = 0);
        Task<Password> GetPasswordDetails(string password);
        Task<bool> CheckForPastes(string username);
        Task<IEnumerable<Paste>> GetPastes(string username);
        Task<bool> CheckSiteForBreaches(string website);
        Task<IEnumerable<Breach>> GetBreachesForSite(string website);
        Task<IEnumerable<Breach>> GetBreachesForAccount(string account);
        Task<IEnumerable<Breach>> GetAllBreaches();
        Task<IEnumerable<string>> GetDataClasses();
    }
}
