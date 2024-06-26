namespace CityTour;

public class Edges
{
    private Dictionary<(int, char), Edge> edges = new ();

    public void AddEdge(Edge edge)
    {
        int startNode = edge.StartNode;
        char name = edge.Name;
        edges[(startNode, name)] = edge;
    }

    public Edge GetEdge(int startNode, char edgeName)
    {
        var key = (startNode, edgeName);
        if (edges.ContainsKey(key))
            return edges[key];
        throw new InvalidTripException($"There is no edge named {edgeName} in node {startNode}");
    }
}