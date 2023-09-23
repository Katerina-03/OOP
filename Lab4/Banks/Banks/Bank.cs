using System.Transactions;
using System.Xml.Schema;
using Banks.Accounts;
using Banks.Clients;
using Banks.Exception;
using Banks.Observe;

namespace Banks.Banks;

public class Bank : IPublisher
{
    private List<Account> _bankAccounts = new ();
    private List<Client> _clients = new ();
    private List<ISubscriber> _subcribes = new ();
    private List<Transaction> _allTransactions = new ();

    public Bank(string bankName, BankRate bankRate)
    {
        if (string.IsNullOrWhiteSpace(bankName))
            throw new BankException("Banks Name is null");
        ArgumentNullException.ThrowIfNull(bankRate);
        BankName = bankName;
        BankRate = bankRate;
    }

    public string BankName { get; private set; }
    public BankRate BankRate { get; private set; }
    public IReadOnlyCollection<Account> BankAccounts => _bankAccounts;
    public IReadOnlyCollection<Client> Clients => _clients;
    public IReadOnlyCollection<Transaction> Transactions => _allTransactions;

    public Account CreateAccount(AccountTypes accountType, Client client, decimal balance, DateTime term)
    {
        Account newAcc = null;
        if (accountType == AccountTypes.Credit)
        {
            newAcc = new AccountCredit(this, client, balance);
            _bankAccounts.Add(newAcc);
            client.AddAccount(newAcc);
        }

        if (accountType == AccountTypes.Debit)
        {
            newAcc = new AccountDebit(this, client, balance);
            _bankAccounts.Add(newAcc);
            client.AddAccount(newAcc);
        }

        if (accountType == AccountTypes.Deposit)
        {
            newAcc = new AccountDeposit(this, client, balance, term);
            _bankAccounts.Add(newAcc);
            client.AddAccount(newAcc);
        }

        if (!CheckClient(client))
            throw new BankException("Client already exist");
        if (CheckClient(client))
            _clients.Add(client);
        if (newAcc is not null)
            return newAcc;
        else
            throw new ArgumentNullException("no such account type");
    }

    public void DeleteAccount(Account account, Client client)
    {
        if (!CheckClient(client))
            throw new BankException("Client doesn't exist");
        _bankAccounts.Remove(account);
        client.DeleteAccount(account);
        _clients.Remove(client);
    }

    public void AddClient(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);
        if (CheckClient(client))
            throw new BankException("Client already exist");
        _clients.Add(client);
    }

    public void DeleteClient(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);
        if (!CheckClient(client))
            throw new BankException("Client doesn't exist");
        _clients.Remove(client);
    }

    public void AddObserver(ISubscriber subscriber)
    {
        if (subscriber == null)
            throw new BankException("Subscriber doesn't exist");
        _subcribes.Add(subscriber);
    }

    public void RemoveObserver(ISubscriber subscriber)
    {
        if (subscriber == null)
            throw new BankException("Subscriber doesn't exist");
        _subcribes.Remove(subscriber);
    }

    public void Inform()
    {
        foreach (ISubscriber subscriber in _subcribes)
        {
            subscriber.Update("Bank rate has changed");
        }
    }

    public void TransferBetweenClients(Account toAccount, Account fromAccount, decimal money)
    {
        if (toAccount == null || fromAccount == null || money < 0)
        {
            throw new BankException("Invalid value");
        }

        fromAccount.Withdraw(money);
        toAccount.AddSum(money);
    }

    private bool CheckClient(Client client)
    {
        return _clients.Any(vClient => vClient.PassportData == client.PassportData);
    }

    private void AnotherRate(BankRate bankRate, AccountsSettings accountsSettings, decimal newRate)
    {
        bankRate.ChangeRate(newRate, accountsSettings);
    }

    private void AddTransaction(Transaction transaction)
    {
        _allTransactions.Add(transaction);
    }
}