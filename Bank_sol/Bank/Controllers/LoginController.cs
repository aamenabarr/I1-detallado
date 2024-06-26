using Bank.Requests;

namespace Bank;

public class LoginController
{
    private readonly string _clientName;
    private readonly string _pinNumber;
    private int _accessDeniedCounter;
    private const int MaxAccessDeniedCounter = 3;

    public LoginController(string clientName, string pinNumber)
    {
        _clientName = clientName;
        _pinNumber = pinNumber;
        _accessDeniedCounter = 0;
    }

    public void TryLogin(LoginRequest loginRequest)
    {
        if (!IsLoginInfoValid(loginRequest) || IsBlocked())
        {
            _accessDeniedCounter++;
            throw new InvalidBankOperationException();
        }
        
        _accessDeniedCounter = 0;
    }
    
    private bool IsLoginInfoValid(LoginRequest request)
    {
        return _clientName == request.ClientName 
               && _pinNumber == request.PinNumber;
    }

    private bool IsBlocked()
    {
        return _accessDeniedCounter >= MaxAccessDeniedCounter;
    }
}