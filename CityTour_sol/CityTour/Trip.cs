namespace CityTour;

public class Trip
{
    private int initialNode;
    private List<Edge> edgesInTrip = new ();

    public Trip(int initialNode)
        => this.initialNode = initialNode;
    
    public void Add(Edge edge)
        => edgesInTrip.Add(edge);

    public double GetCost()
    {
        double cost = 0;
        foreach (var edge in edgesInTrip)
            cost += edge.Cost;
        return cost;
    }

    public int[] GetNodesInTrip()
    {
        List<int> nodes = new List<int>() { initialNode };
        foreach (var edge in edgesInTrip)
            nodes.Add(edge.EndNode);
        return nodes.ToArray();
    }

    public int GetCurrentNode()
        => edgesInTrip.Any() ? edgesInTrip[^1].EndNode : initialNode;
    
}