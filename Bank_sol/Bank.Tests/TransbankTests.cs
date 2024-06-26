using System;
using System.IO;
using Bank.Requests;
using Xunit;

namespace Bank.Tests;

public class TransbankTests
{
    private const string AccessDeniedMessage = "Access denied.";
    private const string TransactionCompletedMessage = "Transaction completed.";
    private const string TransactionFailedMessage = "Transaction failed.";
    private const string WrongPin = "9999";
    private readonly string _acctPath = Path.Combine("data", "accounts.txt");

    [Theory]
    [InlineData("535431", "DOMINGO", "1111", "10000", 90000, "378342", "FELIPE", "2222", "290000")]
    [InlineData("854723", "SANTIAGO", "1313", "300000", 1000000, "757842", "VICENTE", "0000", "1000000")]
    [InlineData("877679", "DANIEL", "1414", "1050000", 350000, "757842", "VICENTE", "0000", "350000")]
    public void Transfer_ShouldTransferTheMoneyWhenTheInputsAreCorrect(string acctNumber, string acctOwner,
        string pinNumber, string acctExpectedBalance, int amountToTransfer, string destAccount, string destOwner,
        string destPin, string destExpectedBalance)
    {
        Transbank transbank = new Transbank(_acctPath);
        
        ValidateTransfer(transbank, acctNumber, acctOwner, pinNumber, 
            amountToTransfer, destAccount, TransactionCompletedMessage);
        ValidateBalance(transbank, acctNumber, acctOwner, pinNumber, acctExpectedBalance);
        ValidateBalance(transbank, destAccount, destOwner, destPin, destExpectedBalance);
    }

    [Theory]
    [InlineData("854723", "SANTIAGO", "1313", 1000001, "757842")]
    [InlineData("877679", "DANIEL", "1414", 350001, "757842")]
    public void Transfer_ShouldFailWhenTransferringMoreThanTheAllowedLimit(string acctNumber, string acctOwner,
        string pinNumber, int amountToTransfer, string destAccount)
        => AssertThatTransferFails(acctNumber, acctOwner, pinNumber, amountToTransfer, destAccount);

    [Theory]
    [InlineData("757842", "VICENTE", "0000",10, "757842")]
    [InlineData("854728", "MARTÍN", "7777",700001, "757842")]
    public void Transfer_ShouldFailWhenTransferringMoreThanTheCurrentAcctBalance(string acctNumber, string acctOwner,
        string pinNumber, int amountToTransfer, string destAccount)
        => AssertThatTransferFails(acctNumber, acctOwner, pinNumber, amountToTransfer, destAccount);

    [Theory]
    [InlineData("757842", "VICENTE", "0000",0, "757842")]
    [InlineData("854728", "MARTÍN", "7777",-10000, "757842")]
    public void Transfer_ShouldFailWhenTransferringZeroOrNegativeAmount(string acctNumber, string acctOwner,
        string pinNumber, int amountToTransfer, string destAccount)
        => AssertThatTransferFails(acctNumber, acctOwner, pinNumber, amountToTransfer, destAccount);

    [Theory]
    [InlineData("854728", "MARTÍN", "7777", 700000, "578422")]
    [InlineData("877679", "DANIEL", "1414", 350000, "737842")]
    public void Transfer_ShouldFailWhenDestAccountDoesNotExist(string acctNumber, string acctOwner,
        string pinNumber, int amountToTransfer, string destAccount)
        => AssertThatTransferFails(acctNumber, acctOwner, pinNumber, amountToTransfer, destAccount);

    [Theory]
    [InlineData("535431", "DOMINGO", "1111", 90000, "378342")]
    [InlineData("854723", "SANTIAGO", "1313", 1000000, "757842")]
    public void Transfer_ShouldFailWhenAccountIsBlocked(string acctNumber, string acctOwner,
        string pinNumber, int amountToTransfer, string destAccount)
    {
        Transbank transbank = new Transbank(_acctPath);
        BlockCard(transbank, acctNumber, acctOwner, amountToTransfer, destAccount);
        
        string result = GetTransferResult(transbank, acctNumber, acctOwner, pinNumber,
            amountToTransfer, destAccount);
        
        Assert.Equal(TransactionFailedMessage, result);
    }

    private static void BlockCard(Transbank transbank, string acctNumber, string acctOwner,
        int amountToTransfer, string destAccount)
    {
        GetAccountBalance(transbank, acctNumber, acctOwner, WrongPin);
        GetTransferResult(transbank, acctNumber, acctOwner, WrongPin, amountToTransfer, destAccount);
        GetTransferResult(transbank, acctNumber, acctOwner, WrongPin, amountToTransfer, destAccount);
    }

    private void AssertThatTransferFails(string acctNumber, string acctOwner, string pinNumber,
        int amountToTransfer, string destAccount)
    {
        Transbank transbank = new Transbank(_acctPath);

        ValidateTransfer(transbank, acctNumber, acctOwner, pinNumber, 
            amountToTransfer, destAccount, TransactionFailedMessage);
    }

    private static void ValidateTransfer(Transbank transbank, string acctNumber, string acctOwner,
        string pinNumber, int amountToTransfer, string destAccount, string expectedResult)
    {
        string result = GetTransferResult(transbank, acctNumber, acctOwner, pinNumber, amountToTransfer, destAccount);
        
        Assert.Equal(expectedResult, result);
    }

    private static string GetTransferResult(Transbank transbank, string acctNumber, string acctOwner,
        string pinNumber, int amountToTransfer, string destAccount)
    {
        TransferRequest transferRequest =
            BuildTransferRequest(acctNumber, acctOwner, pinNumber, amountToTransfer, destAccount);
        
        return transbank.Transfer(transferRequest);
    }

    private static TransferRequest BuildTransferRequest(string acctNumber, string acctOwner, string pinNumber, int amountToTransfer, string destAccount)
    {
        LoginRequest loginRequest = new LoginRequest(acctOwner, pinNumber);
        return new TransferRequest(acctNumber, amountToTransfer, 
            destAccount, loginRequest);
    }

    [Theory]
    [InlineData("535431", "DOMINGOS", "1111")]
    [InlineData("378342", "CATALINA", "2222")]
    [InlineData("946832", "FELIPE", "3333")]
    [InlineData("954722", "DIEGO", "4424")]
    public void GetAccountBalance_ShouldReturnAnAccessDeniedMessageWhenGivenIncorrectValues(string acctNumber,
        string acctOwner, string pinNumber)
    {
        Transbank transbank = new Transbank(_acctPath);
        ValidateBalance(transbank, acctNumber, acctOwner, pinNumber, AccessDeniedMessage);
    }

    [Theory]
    [InlineData("535431", "DOMINGO", "1111", "100000")]
    [InlineData("378342", "FELIPE", "2222", "200000")]
    [InlineData("946832", "CATALINA", "3333", "300000")]
    [InlineData("954722", "DIEGO", "4444", "400000")]
    public void GetAccountBalance_ShouldReturnBalanceWhenGivenTheRightValuesAfterTwoTries(string acctNumber, string acctOwner,
        string realPin, string expectedBalance)
    {
        Transbank transbank = new Transbank(_acctPath);
        ValidateBalance(transbank, acctNumber, acctOwner, WrongPin, AccessDeniedMessage);
        ValidateBalance(transbank, acctNumber, acctOwner, WrongPin, AccessDeniedMessage);
        ValidateBalance(transbank, acctNumber, acctOwner, realPin, expectedBalance);
    }

    [Theory]
    [InlineData("535431", "DOMINGO", "1111")]
    [InlineData("378342", "FELIPE", "2222")]
    [InlineData("946832", "CATALINA", "3333")]
    [InlineData("954722", "DIEGO", "4444")]
    public void GetAccountBalance_ShouldBlockAccountAfterThreeConsecutiveAccessDenied(string acctNumber, string acctOwner,
        string realPin)
    {
        Transbank transbank = new Transbank(_acctPath);
        ValidateBalanceWithWrongPinForXTimes(3, transbank, acctNumber, acctOwner);
        ValidateBalance(transbank, acctNumber, acctOwner, realPin, AccessDeniedMessage);
    }

    private void ValidateBalanceWithWrongPinForXTimes(int timesToValidate, Transbank transbank, string acctNumber, string acctOwner)
    {
        for(int i = 0; i < timesToValidate; i++)
            ValidateBalance(transbank, acctNumber, acctOwner,
                WrongPin, AccessDeniedMessage);
    }
    
    [Theory]
    [InlineData("535431", "DOMINGO", "1111", "100000")]
    [InlineData("378342", "FELIPE", "2222", "200000")]
    [InlineData("946832", "CATALINA", "3333", "300000")]
    [InlineData("954722", "DIEGO", "4444", "400000")]
    public void GetAccountBalance_ShouldReturnBalanceWhenGivenTheRightValues(string acctNumber, string acctOwner,
        string pinNumber, string expectedBalance)
    {
        Transbank transbank = new Transbank(_acctPath);
        ValidateBalance(transbank, acctNumber, acctOwner, pinNumber, expectedBalance);
    }

    private void ValidateBalance(Transbank transbank, string acctNumber, string acctOwner, string pinNumber, string expectedBalance)
    {
        string balance = GetAccountBalance(transbank, acctNumber, acctOwner, pinNumber);
        
        Assert.Equal(expectedBalance, balance);
    }

    private static string GetAccountBalance(Transbank transbank, string acctNumber, string acctOwner, string pinNumber)
    {
        BalanceRequest balanceRequest = BuildBalanceRequest(acctNumber, acctOwner, pinNumber);
        return transbank.GetAccountBalance(balanceRequest);
    }

    private static BalanceRequest BuildBalanceRequest(string acctNumber, string acctOwner, string pinNumber)
    {
        LoginRequest loginRequest = new LoginRequest(acctOwner, pinNumber);
        return new BalanceRequest(acctNumber, loginRequest);
    }
}