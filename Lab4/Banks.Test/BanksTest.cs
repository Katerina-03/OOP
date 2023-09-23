using System.Net.Mail;
using Banks.Accounts;
using Banks.Banks;
using Banks.Clients;
using Banks.Exception;
using Banks.Observe;
using Banks.Test;
using Banks.Transactions;
using Xunit;

namespace Banks.Test;

public class BanksTest
{
    [Fact]
    public void AccountBuilding()
    {
        CentralBank centralBank = new ();
        Bank bank = new Bank("Private", new BankRate(2.5m, 3m, 5, 6));
        centralBank.AddBank(bank);
        Builder builder = new Builder();
        PassportData passportData = builder.SetName("Kate").SetSurname("Vasileva").SetPassportNumber("1234567890").Create();
        Client client = new Client(passportData);
        bank.AddClient(client);
        Account account = bank.CreateAccount(AccountTypes.Deposit, client, 100000, new DateTime(2023, 10, 4));
        Assert.Equal(account, client.FindAccount(account.AccountId));
    }

    [Fact]
    public void TransferMoney()
    {
        CentralBank centralBank = new ();
        Bank bank = new Bank("Private", new BankRate(2.5m, 3m, 5, 6));
        Builder builder1 = new Builder();
        PassportData passportData1 = builder1.SetName("Kate").SetSurname("Vasileva").SetPassportNumber("1234567890").Create();
        Builder builder2 = new Builder();
        PassportData passportData2 = builder2.SetName("Liza").SetSurname("Fedorova").SetPassportNumber("4561237890").Create();
        Client client1 = new Client(passportData1);
        Client client2 = new Client(passportData2);
        bank.AddClient(client1);
        bank.AddClient(client2);
        Account account1 = bank.CreateAccount(AccountTypes.Debit, client1, 5000, DateTime.Today);
        Account account2 = bank.CreateAccount(AccountTypes.Debit, client2, 5000, DateTime.Today);
        Account account3 = bank.CreateAccount(AccountTypes.Deposit, client1, 100000, new DateTime(2023, 10, 4));
        Account account4 = bank.CreateAccount(AccountTypes.Deposit, client2, 100000, new DateTime(2023, 10, 4));
        bank.TransferBetweenClients(account1, account2, 3000);
        Assert.Equal(2000, account2.Balance);
        Assert.Throws<BankException>(testCode: () => bank.TransferBetweenClients(account3, account4, 50000));
    }
}