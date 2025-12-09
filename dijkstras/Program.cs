namespace dijkstras
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Toto je test na dijkstras");
            Dijkstra dijkstra = new Dijkstra();
            dijkstra.graph = new List<Edge>[3];
            for(int i = 0; i < dijkstra.graph.Length; i++)
            {
                dijkstra.graph[i] = new List<Edge>();
            }
            int source = 0;

            dijkstra.graph[0].Add(new Edge(1, 2));
            dijkstra.graph[0].Add(new Edge(2, 5));

            dijkstra.graph[1].Add(new Edge(0, 2));
            dijkstra.graph[1].Add(new Edge(2, 2));

            dijkstra.graph[2].Add(new Edge(0, 5));
            dijkstra.graph[2].Add(new Edge(1, 2));

            dijkstra.SolveDijktra(source);
            foreach(int distance in dijkstra.distances)
            {
                Console.WriteLine(distance);
            }
            Console.WriteLine("cesta do dvojky");

            
            int target = 2;
            
            while(source != target)
            {
                target = dijkstra.perviousParent[target];
                Console.WriteLine(target);
            }


        }


    }
    public class Dijkstra
    {
        public List<Edge>[] graph;
        public int[] distances;
        public int[] perviousParent;
        public void SolveDijktra(int startAt)
        {
            distances = new int[graph.Length]; 
            perviousParent = new int[graph.Length];
            bool[] visited = new bool[graph.Length];

            for(int i = 0; i < distances.Length; i++) //nastavim max hodnoty jelikoz neznam vzdalenosti
            {
                distances[i] = int.MaxValue;
            }

            distances[startAt] = 0; // protoze tu zacinam

            PriorityQueue<int, int> priorityQueue = new PriorityQueue<int, int>();
            priorityQueue.Enqueue(startAt, 0);

            while(priorityQueue.Count > 0)
            {
                int current = priorityQueue.Dequeue();

                if (visited[current])
                    continue;

                visited[current] = true;

                foreach(var edge in graph[current])
                {
                    int to = edge.To;
                    int weight = edge.Weight;

                    if (visited[to])
                        continue;

                    int newDistance = distances[current] + weight;

                    if (newDistance < distances[to])
                    {
                        perviousParent[to] = current;
                        distances[to] = newDistance;
                        priorityQueue.Enqueue(to, newDistance);
                    }
                }

            }

            Console.WriteLine("Snad hotovo");



        }
    }
    public class Edge
    {
        public int To;
        public int Weight;

        public Edge(int to, int weight)
        {
            To = to;
            Weight = weight;
        }
    }

}
