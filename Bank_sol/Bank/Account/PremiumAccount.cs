namespace Bank;

public class PremiumAccount : Account
{
    protected override int MaxAllowedAmount => 1000000;

    public PremiumAccount(AccountInfo info) : base(info)
    {
    }
}