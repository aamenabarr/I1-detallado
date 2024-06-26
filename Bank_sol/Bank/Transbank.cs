using Bank.Controllers;
using Bank.Requests;

namespace Bank;

public class Transbank
{
    private readonly BalanceController _balanceController;
    private readonly TransferController _transferController;

    public Transbank(string path)
    {
        var accounts = AccountsBuilder.BuildFromFile(path);
        _balanceController = new BalanceController(accounts);
        _transferController = new TransferController(accounts);
    }

    public string GetAccountBalance(BalanceRequest balanceRequest)
        => _balanceController.GetBalance(balanceRequest);

    public string Transfer(TransferRequest transferRequest)
        => _transferController.Transfer(transferRequest);
}