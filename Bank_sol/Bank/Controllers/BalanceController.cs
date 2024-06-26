using Bank.Requests;

namespace Bank.Controllers;

public class BalanceController : BankOperationController
{
    private BalanceRequest _request;
    protected override string InvalidOperationMessage => "Access denied.";
    public BalanceController(Accounts accounts) : base(accounts)
    {
    }

    public string GetBalance(BalanceRequest request)
    {
        _request = request;
        return Execute();
    }
    
    protected override string TryExecute()
    {   
        var account = Accounts.Get(_request.AccountNumber);
        return account.GetBalance(_request.LoginRequest);
    }
}