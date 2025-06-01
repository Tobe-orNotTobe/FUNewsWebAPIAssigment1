using BusinessObjects;

namespace Services.Interfaces
{
	public interface ISystemAccountService
	{
		void SaveAccount(SystemAccount s);
		void DeleteAccount(SystemAccount n);
		void UpdateAccount(SystemAccount n);
		List<SystemAccount> GetAccounts();
		SystemAccount GetAccountById(short accountID);
	}
}
