using Bank.Requests;

namespace Bank.Controllers;

public class TransferController : BankOperationController
{
    private const string ValidOperationMessage = "Transaction completed.";
    private TransferRequest _request;
    protected override string InvalidOperationMessage => "Transaction failed.";
    public TransferController(Accounts accounts) : base(accounts)
    {
    }
    public string Transfer(TransferRequest request)
    {
        _request = request;
        return Execute();
    }

    protected override string TryExecute()
    {
        var account = Accounts.Get(_request.AccountNumber);
        var destinationAccount = Accounts.Get(_request.DestinationAccountNumber);
        account.TransferTo(destinationAccount, _request);
        return ValidOperationMessage;
    }
}