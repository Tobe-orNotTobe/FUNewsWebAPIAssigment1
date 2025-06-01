using BusinessObjects;

namespace Repositories.Interfaces
{
	public interface ISystemAccountRepository
	{
		void SaveAccount(SystemAccount s);
		void DeleteAccount(SystemAccount s);
		void UpdateAccount(SystemAccount s);
		List<SystemAccount> GetAccounts();
		SystemAccount GetAccountById(short accountID);
	}
}
