namespace Bank;

public class AccountInfo
{
    public readonly string Number;
    public readonly string ClientName;
    public readonly string PinNumber;
    public readonly string Type;
    public readonly int Balance;

    public AccountInfo(string[] info)
    {
        Number = info[0];
        ClientName = info[1];
        PinNumber = info[2]; 
        Type = info[3];
        Balance = Convert.ToInt32(info[4]);
    }
}