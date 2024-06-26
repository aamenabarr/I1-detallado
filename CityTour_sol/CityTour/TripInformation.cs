namespace CityTour;

public abstract class TripInformation
{
    protected TripInformation(bool isValid, double cost, int[] nodesInTrip)
    {
        IsValid = isValid;
        Cost = cost;
        NodesInTrip = nodesInTrip;
    }

    public bool IsValid { get; }
    public double Cost { get; }
    public int[] NodesInTrip { get; }

}