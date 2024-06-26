namespace Bank;

public static class AccountBuilder
{
    public static Account Build(AccountInfo info)
        => info.Type switch
        {
            "standard" => new StandardAccount(info),
            "premium" => new PremiumAccount(info),
            _ => throw new InvalidAccountTypeException()
        };
}