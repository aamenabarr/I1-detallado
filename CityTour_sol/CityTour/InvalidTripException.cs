namespace CityTour;

public class InvalidTripException : ApplicationException
{
    public InvalidTripException(string message)
        : base(message)
    {
    }
}