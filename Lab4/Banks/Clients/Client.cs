using Banks.Accounts;
using Banks.Banks;
using Banks.Observe;

namespace Banks.Clients;

public class Client : ISubscriber
{
    private List<Account> _accounts = new ();
    public Client(PassportData clientPassportData)
    {
        PassportData = clientPassportData;
    }

    public IReadOnlyCollection<Account> Accounts => _accounts;
    public PassportData PassportData { get; }
    private string Info { get; set; } = string.Empty;

    public void AddAccount(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);
        if (!CheckAccount(account))
            _accounts.Add(account);
    }

    public void DeleteAccount(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);
        if (CheckAccount(account))
            _accounts.Remove(account);
    }

    public void Update(string s)
    {
        Info += s;
    }

    public Account FindAccount(Guid accountId)
    {
        return Accounts.SingleOrDefault(x => x.AccountId == accountId);
    }

    private bool CheckAccount(Account account)
    {
        return _accounts.Any(x => x.AccountId == account.AccountId);
    }
}