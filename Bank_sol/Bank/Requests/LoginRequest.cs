namespace Bank.Requests;

public class LoginRequest
{
    public readonly string ClientName;
    public readonly string PinNumber;

    public LoginRequest(string clientName, string pinNumber)
    {
        ClientName = clientName;
        PinNumber = pinNumber;
    }
}