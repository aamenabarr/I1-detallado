namespace CityTour;

public struct Edge
{
    public int StartNode { get; }
    public char Name { get; }
    public int EndNode { get; }
    public double Cost { get; }

    public Edge(int startNode, char name, int endNode, double cost)
    {
        StartNode = startNode;
        Name = name;
        EndNode = endNode;
        Cost = cost;
    }
    
}