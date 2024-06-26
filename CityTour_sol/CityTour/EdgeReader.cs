namespace CityTour;

public static class EdgeReader
{
    private static Edges edges;
    
    public static Edges FromFile(string path)
    {
        edges = new Edges();
        string[] lines = File.ReadAllLines(path);
        DecodeLines(lines);
        return edges;
    }

    private static void DecodeLines(string[] lines)
    {
        foreach (string line in lines)
            DecodeLine(line);
    }
    
    private static void DecodeLine(string line)
    {
        string[] info = line.Split(',');
        int node1 = Convert.ToInt32(info[0]);
        char name = Convert.ToChar(info[1]);
        int node2 = Convert.ToInt32(info[2]);
        double cost = Convert.ToDouble(info[3]);
        AddOneEdgeInEachDirection(node1, name, node2, cost);
    }

    private static void AddOneEdgeInEachDirection(int node1, char name, int node2, double cost)
    {
        AddEdge(node1, name, node2, cost);
        AddEdge(node2, name, node1, cost);
    }

    private static void AddEdge(int startNode, char name, int endNode, double cost)
    {
        Edge edge = new Edge(startNode, name, endNode, cost);
        edges.AddEdge(edge);
    }

}