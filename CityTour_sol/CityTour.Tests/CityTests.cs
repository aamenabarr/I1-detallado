using System.Collections.Generic;
using System.IO;
using Xunit;

namespace CityTour.Tests;

public class CityTests
{
    [Theory]
    [InlineData("01.txt", 0, "r")]
    [InlineData("01.txt", 0, "baccbc")]
    [InlineData("01.txt", 0, "bbabbbcb")]
    [InlineData("01.txt", 0, "abcab")]
    [InlineData("02.txt", 0, "abcdefgh")]
    [InlineData("01.txt", 5, "abc")]
    [InlineData("02.txt", 1, "bcdega")]
    public void GetTripInfo_ThisTripShouldBeInvalid(string cityFile, int startingNode, string route)
    {
        City city = new City(GetPath(cityFile));

        TripInformation info = city.GetTripInformation(startingNode, route);
        
        Assert.False(info.IsValid);
        Assert.Equal(double.PositiveInfinity, info.Cost);
        Assert.Empty(info.NodesInTrip);
    }

    [Theory]
    [InlineData("01.txt", 0, "a")]
    [InlineData("01.txt", 0, "baccba")]
    [InlineData("01.txt", 0, "bbabbbca")]
    [InlineData("01.txt", 0, "abca")]
    [InlineData("02.txt", 0, "abcdeg")]
    [InlineData("02.txt", 0, "fg")]
    [InlineData("01.txt", 4, "cca")]
    [InlineData("02.txt", 2, "bbcddcb")]
    public void GetTripInfo_ThisTripShouldBeValid(string cityFile, int startingNode, string route)
    {
        City city = new City(GetPath(cityFile));

        TripInformation info = city.GetTripInformation(startingNode, route);
        
        Assert.True(info.IsValid);
    }

    [Theory]
    [InlineData("01.txt", 0, "a", 20)]
    [InlineData("01.txt", 0, "baccba", 186)]
    [InlineData("01.txt", 0, "bbabbbca", 184)]
    [InlineData("01.txt", 0, "abca", 82)]
    [InlineData("02.txt", 0, "abcdeg", 15)]
    [InlineData("02.txt", 0, "fg", 11)]
    [InlineData("01.txt", 4, "cca", 72)]
    [InlineData("02.txt", 2, "bbcddcb", 7)]
    public void GetTripInfo_CostsOfValidTripsShouldBeAddedCorrectly(string cityFile, int startingNode, string route, double expectedCost)
    {
        City city = new City(GetPath(cityFile));

        TripInformation info = city.GetTripInformation(startingNode, route);
        
        Assert.Equal(expectedCost, info.Cost);
    }

    [Theory]
    [InlineData("01.txt", 0, "a", new int[]{0, 1})]
    [InlineData("01.txt", 0, "baccba", new int[]{0, 2, 3, 4, 3, 1, 0})]
    [InlineData("01.txt", 0, "bbabbbca", new int[]{0, 2, 0, 1, 3, 1, 3, 4, 5})]
    [InlineData("01.txt", 0, "abca", new int[]{0, 1, 3, 4, 5})]
    [InlineData("02.txt", 0, "abcdeg", new int[]{0, 1, 2, 3, 4, 5, 6})]
    [InlineData("02.txt", 0, "fg", new int[]{0, 5, 6})]
    [InlineData("01.txt", 4, "cca", new int[]{4, 3, 4, 5})]
    [InlineData("02.txt", 2, "bbcddcb", new int[]{2, 1, 2, 3, 4, 3, 2, 1})]
    public void GetTripInfo_TheListOfVisitedNodesShouldBeCorrect(string cityFile, int startingNode, string route, int[] expectedNodes)
    {
        City city = new City(GetPath(cityFile));

        TripInformation info = city.GetTripInformation(startingNode, route);

        VerifyThatTheseCollectionsAreIdentical(expectedNodes, info.NodesInTrip);
    }

    private void VerifyThatTheseCollectionsAreIdentical(int[] expected, int[] actual)
    {
        Assert.Equal(expected.Length, actual.Length);
        for(int i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], actual[i]);
    }

    private string GetPath(string cityFile)
        => Path.Combine("maps", cityFile);
}