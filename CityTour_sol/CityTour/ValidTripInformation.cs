namespace CityTour;

public class ValidTripInformation : TripInformation
{
    public ValidTripInformation(double cost, int[] nodesInTrip) 
        : base(true, cost, nodesInTrip)
    {
    }
}