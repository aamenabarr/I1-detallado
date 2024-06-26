namespace Bank.Controllers;

public abstract class BankOperationController
{
    protected readonly Accounts Accounts;
    protected abstract string InvalidOperationMessage { get; }
    
    protected BankOperationController(Accounts accounts)
    {
        Accounts = accounts;
    }

    protected string Execute()
    {
        try
        {
            return TryExecute();
        }
        catch (InvalidBankOperationException)
        {
            return InvalidOperationMessage;
        }
    }

    protected abstract string TryExecute();
}