
Graph graph = new Graph(15);  // Vytvoření instance grafu se 15 vrcholy

// Source, Destination, Weight
graph.AddEdge(0, 1, 5);
graph.AddEdge(1, 2, 3);
graph.AddEdge(2, 3, -2);
graph.AddEdge(1, 9, 7);
graph.AddEdge(3, 4, 2);
graph.AddEdge(4, 5, -2);
graph.AddEdge(5, 6, 2);
graph.AddEdge(6, 7, 5);
graph.AddEdge(6, 8, -7);
graph.AddEdge(6, 11, 1);
graph.AddEdge(6, 10, 6);
graph.AddEdge(10, 12, -3);
graph.AddEdge(13, 14, -1);
graph.AddEdge(12, 14, 4);
//graph.AddEdge(10, 13, 3); // Vrchol 13 nemá k sobě žádnou hranu, vytvoříme ji z vrcholu 10

graph.BellmanFord(0);  // Spuštění algoritmu z uzlu 0


public class Edge
{
    public int Source { get; set; }  // Zdrojový vrchol hrany
    public int Destination { get; set; }  // Cílový vrchol hrany
    public int Weight { get; set; }  // Hodnota hrany
}

public class Graph
{
    public int VerticesCount { get; set; }  // Počet vrcholů grafu
    public List<Edge> Edges { get; set; }  // Seznam hran grafu

    // Konstruktor třídy Graph
    public Graph(int verticesCount)
    {
        VerticesCount = verticesCount;
        Edges = new List<Edge>();
    }

    // Metoda pro přidání hrany do grafu
    public void AddEdge(int source, int destination, int weight)
    {
        Edges.Add(new Edge() { Source = source, Destination = destination, Weight = weight });
    }

    // Hledání nejkratších cest
    public void BellmanFord(int source)
    {
        int[] distance = new int[VerticesCount];  // Pole pro ukládání vzdáleností od zadaného zdroje
        for (int i = 0; i < VerticesCount; i++)
            distance[i] = int.MaxValue;  // Inicializace vzdáleností na nekonečno

        distance[source] = 0; 

        // Relaxace se opakuje V-1 krát, hledá se nejkratší cesta do src
        for (int i = 1; i < VerticesCount; i++)
        {
            foreach (var edge in Edges)
            {
                if (distance[edge.Source] != int.MaxValue && distance[edge.Source] + edge.Weight < distance[edge.Destination])
                {
                    distance[edge.Destination] = distance[edge.Source] + edge.Weight;
                }
            }
        }

        // Pokud po provedeni relaxace V-1 krát dokážeme najít ještě lepší řešení, znamená to že graf obsahuje negative weight cycle
        foreach (var edge in Edges)
        {
            if (distance[edge.Source] != int.MaxValue && distance[edge.Source] + edge.Weight < distance[edge.Destination])
            {
                Console.WriteLine("Graf obsahuje cyklus s negativní vahou");
                return;
            }
        }

        // Výpis
        Console.WriteLine("Vzdálenost uzlů od zdroje");
        for (int i = 0; i < VerticesCount; i++)
            Console.WriteLine($"{i}\t {distance[i]}");
    }
}