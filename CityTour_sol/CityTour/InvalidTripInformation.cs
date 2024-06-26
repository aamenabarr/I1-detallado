namespace CityTour;

public class InvalidTripInformation:TripInformation
{    
    public InvalidTripInformation() 
        : base(false, Double.PositiveInfinity, Array.Empty<int>())
    { }
}