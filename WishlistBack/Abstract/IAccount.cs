using WishlistBack.Model;

namespace WishlistBack.Abstract
{
    public interface IAccount
    {
        IEnumerable<Account> GetAllAccounts();
        Account GetAccountById(int id);
        Result CreateAccount(Account account);
        Result UpdateAccount(Account account);
        Result DeleteAccount(int id);
        Account Login(string login, string password);

    }
}
