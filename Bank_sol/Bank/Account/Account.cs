using Bank.Requests;

namespace Bank;

public abstract class Account
{
    private int _balance;
    private readonly LoginController _loginController;
    protected abstract int MaxAllowedAmount { get; }

    protected Account(AccountInfo info)
    {
        _balance = info.Balance;
        _loginController = new LoginController(info.ClientName, info.PinNumber);
    }

    public string GetBalance(LoginRequest loginRequest)
    {
        _loginController.TryLogin(loginRequest);
        return _balance.ToString();
    }

    public void TransferTo(Account destinationAccount, TransferRequest request)
    {
        _loginController.TryLogin(request.LoginRequest);
        ValidateAmount(request.Amount);
        UpdateBalances(destinationAccount, request.Amount);
    }

    private void ValidateAmount(int amount)
    {
        if (IsAmountValid(amount))
            return;
        throw new InvalidBankOperationException();
    }

    private bool IsAmountValid(int amount)
    {
        return amount <= MaxAllowedAmount &&
               0 < amount && amount <= _balance;
    }
    private void UpdateBalances(Account destinationAccount, int amount)
    {
        destinationAccount._balance += amount;
        _balance -= amount; 
    }
}