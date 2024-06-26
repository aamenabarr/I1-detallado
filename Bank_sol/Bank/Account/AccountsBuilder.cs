namespace Bank;

public static class AccountsBuilder
{
    private static readonly Accounts Accounts = new();
    public static Accounts BuildFromFile(string path)
    {
        string[] lines = File.ReadAllLines(path);
        AddFromLines(lines);
        return Accounts;
    }

    private static void AddFromLines(string[] lines)
    {
        foreach (string line in lines)
            AddFromLine(line);  
    }

    private static void AddFromLine(string line)
    {
        string[] info = line.Split(',');
        AccountInfo accountInfo = new AccountInfo(info);
        Account account = AccountBuilder.Build(accountInfo);
        Accounts.Add(accountInfo.Number, account); 
    }
}