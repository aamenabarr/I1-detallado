namespace Bank;

public class StandardAccount : Account
{
    protected override int MaxAllowedAmount => 350000;

    public StandardAccount(AccountInfo info) : base(info)
    {
    }
}