using BusinessObjects;
using DataAccessObjects;
using Repositories.Interfaces;

namespace Repositories
{
	public class SystemAccountRepository : ISystemAccountRepository
	{
		public void DeleteAccount(SystemAccount n) => SystemAccountDAO.DeleteAccount(n);

		public SystemAccount GetAccountById(short accountID) => SystemAccountDAO.GetAccountById(accountID);

		public List<SystemAccount> GetAccounts() => SystemAccountDAO.GetAccounts();

		public void SaveAccount(SystemAccount s) => SystemAccountDAO.SaveAccount(s);

		public void UpdateAccount(SystemAccount n) => SystemAccountDAO.UpdateAccount(n);
	}
}
