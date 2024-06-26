namespace Bank.Requests;

public class BalanceRequest
{
    public readonly string AccountNumber;
    public readonly LoginRequest LoginRequest;

    public BalanceRequest(string accountNumber, LoginRequest loginRequest)
    {
        AccountNumber = accountNumber;
        LoginRequest = loginRequest;
    }
}