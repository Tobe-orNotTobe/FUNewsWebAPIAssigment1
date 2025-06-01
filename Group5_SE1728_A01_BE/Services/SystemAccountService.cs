using BusinessObjects;
using Repositories;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
	public class SystemAccountService : ISystemAccountService
	{
		private readonly ISystemAccountRepository _repo;

		public SystemAccountService() => _repo = new SystemAccountRepository();

		public void DeleteAccount(SystemAccount n) => _repo.DeleteAccount(n);

		public SystemAccount GetAccountById(short accountID) => _repo.GetAccountById(accountID);

		public List<SystemAccount> GetAccounts() => _repo.GetAccounts();

		public void SaveAccount(SystemAccount s) => _repo.SaveAccount(s);

		public void UpdateAccount(SystemAccount n) => _repo.UpdateAccount(n);
	}
}
