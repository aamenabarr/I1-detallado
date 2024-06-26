namespace Bank;

public class Accounts
{
    private readonly Dictionary<string, Account> _accounts = new();

    public void Add(string accountNumber, Account account)
    {
        _accounts[accountNumber] = account;
    }

    public Account Get(string accountNumber)
    {
        try
        {
            return _accounts[accountNumber];
        }
        catch (KeyNotFoundException)
        {
            throw new AccountNotFoundException();
        }
    }
}