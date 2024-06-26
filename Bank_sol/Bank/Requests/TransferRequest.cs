namespace Bank.Requests;

public class TransferRequest : BalanceRequest
{
    public readonly int Amount;
    public readonly string DestinationAccountNumber;
    
    public TransferRequest(string accountNumber, int amount
        , string destinationAccountNumber, LoginRequest loginRequest) 
        : base(accountNumber, loginRequest)
    {
        Amount = amount;
        DestinationAccountNumber = destinationAccountNumber;
    }
}