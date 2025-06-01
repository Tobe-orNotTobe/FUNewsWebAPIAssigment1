using BusinessObjects;

namespace DataAccessObjects
{
	public class SystemAccountDAO
	{
		public static SystemAccount GetAccountById(short accountID)
		{
			using var db = new FunewsManagementContext();
			return db.SystemAccounts.FirstOrDefault(c => c.AccountId.Equals(accountID));
		}

		public static List<SystemAccount> GetAccounts()
		{
			var listAccounts = new List<SystemAccount>();
			try
			{
				using var db = new FunewsManagementContext();
				listAccounts = db.SystemAccounts.ToList();
			}
			catch (Exception e) { }
			return listAccounts;
		}

		public static void SaveAccount(SystemAccount s)
		{
			try
			{
				using var context = new FunewsManagementContext();
				context.SystemAccounts.Add(s);
				context.SaveChanges();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public static void UpdateAccount(SystemAccount s)
		{
			try
			{
				using var context = new FunewsManagementContext();
				context.Entry(s).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				context.SaveChanges();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public static void DeleteAccount(SystemAccount s)
		{
			try
			{
				using var context = new FunewsManagementContext();
				var s1 = context.SystemAccounts.SingleOrDefault(c => c.AccountId == s.AccountId);
				context.SystemAccounts.Remove(s1);

				context.SaveChanges();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
