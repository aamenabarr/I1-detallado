namespace CityTour;

public class City
{
    private Graph graph;
    
    public City(string path)
    {
        Edges edges = EdgeReader.FromFile(path);
        graph = new Graph(edges);
    }

    public TripInformation GetTripInformation(int startingNode, string route)
        => graph.GetTripInfo(startingNode, route);
    
}