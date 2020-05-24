using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frank.HaveIBeenPwned
{
    public class HaveIBeenPwnedService : IHaveIBeenPwnedService
    {
        public async Task<bool> CheckPassword(string password, int threshold = 0)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Password> GetPasswordDetails(string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> CheckForPastes(string username)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Paste>> GetPastes(string username)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> CheckSiteForBreaches(string website)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Breach>> GetBreachesForSite(string website)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Breach>> GetBreachesForAccount(string account)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Breach>> GetAllBreaches()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<string>> GetDataClasses()
        {
            throw new System.NotImplementedException();
        }
    }
}