namespace CityTour;

public class Graph
{
    private Edges edges;
    private int startingNode;
    private string route;

    public Graph(Edges edges)
        => this.edges = edges;
    
    public TripInformation GetTripInfo(int startingNode, string route)
    {
        this.startingNode = startingNode;
        this.route = route;
        return GetTripInfo();
    }

    private TripInformation GetTripInfo()
    {
        try
        {
            return GetTripInformationAssumingThatTheTripIsValid();
        }
        catch (InvalidTripException)
        {
            return new InvalidTripInformation();
        }
    }

    private TripInformation GetTripInformationAssumingThatTheTripIsValid()
    {
        Trip trip = ComputeTrip();
        double cost = trip.GetCost();
        int[] nodesInTrip = trip.GetNodesInTrip();
        
        return new ValidTripInformation(cost, nodesInTrip);
    }
    
    private Trip ComputeTrip()
    {
        Trip trip = new Trip(startingNode);
        foreach (char edgeName in route)
        {
            Edge nextEdge = edges.GetEdge(trip.GetCurrentNode(), edgeName);
            trip.Add(nextEdge);
        }

        return trip;
    }

}