using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreedyAlgorithm
{
    public class PrimsAlgorithmCalculator
    {
        class Graph
        {
            Dictionary<int, Dictionary<int, int>> nodeNodeWeightGraph =
                new Dictionary<int, Dictionary<int, int>>();

            HashSet<int> nodes = new HashSet<int>();

            public HashSet<int> Nodes
            {
                get { return nodes; }
            }

            public void Add(int i, int j, int weight)
            {
                if (!nodes.Contains(i))
                    nodes.Add(i);

                if (!nodes.Contains(j))
                    nodes.Add(j);

                if (!nodeNodeWeightGraph.ContainsKey(i))
                    nodeNodeWeightGraph.Add(i, new Dictionary<int, int>());

                if (!nodeNodeWeightGraph.ContainsKey(j))
                    nodeNodeWeightGraph.Add(j, new Dictionary<int, int>());

                nodeNodeWeightGraph[i].Add(j, weight);
                nodeNodeWeightGraph[j].Add(i, weight);
            }

            public Dictionary<int, int> GetConnection(int node)
            {
                return nodeNodeWeightGraph[node];
            }
            
            public int GetWeight(int i, int j)
            {
                if (nodeNodeWeightGraph[i].ContainsKey(j))
                    return nodeNodeWeightGraph[i][j];
                else
                    return int.MaxValue;
            }
        }

        class MinSpanningTree
        {
            HashSet<int> nodes = new HashSet<int>();

            public List<Tuple<int,int,int>> Edges
            {
                get; private set;
            }

            public MinSpanningTree(int initialNode)
            {
                nodes.Add(initialNode);
                this.Edges = new List<Tuple<int, int, int>>();
            }

            public void Add(Tuple<int, int, int> edge)
            {
                this.Edges.Add(edge);
                this.nodes.Add(edge.Item2); 
            }

            public bool ContainsNode(int node)
            {
                return nodes.Contains(node);
            }


        }

        class Frontier
        {
            SortedDictionary<int, Dictionary<int, int?>> minCutTree = new SortedDictionary<int, Dictionary<int, int?>>();
            Dictionary<int, int> nodeWeightDict = new Dictionary<int, int>();

            public void Add(int weight, int node, int? mstNode)
            {
                if (!minCutTree.ContainsKey(weight))
                    minCutTree.Add(weight, new Dictionary<int, int?>());

                minCutTree[weight].Add(node, mstNode);

                nodeWeightDict.Add(node, weight);
            }

            public void Remove(int weight, int node)
            {
                if (minCutTree.ContainsKey(weight))
                {
                    if (minCutTree[weight].Count <= 1)
                        minCutTree.Remove(weight);
                    else
                        minCutTree[weight].Remove(node);
                }

                nodeWeightDict.Remove(node);
            }

            public void AdjustWeight(int node, int mstNode, int weight)
            {
                if (!nodeWeightDict.ContainsKey(node))
                    return;

                int oldWeight = nodeWeightDict[node];

                if (oldWeight < weight)
                    return;

                if (minCutTree[oldWeight].Count <= 1)
                    minCutTree.Remove(oldWeight);
                else
                     minCutTree[oldWeight].Remove(node);

                if (!minCutTree.ContainsKey(weight))
                    minCutTree.Add(weight, new Dictionary<int, int?>());

                minCutTree[weight].Add(node, mstNode);
                nodeWeightDict[node] = weight;
            }

            public Tuple<int, int, int> GetMinimumCut()
            {
                if (minCutTree.Count <= 0)
                    return null;

                KeyValuePair<int, Dictionary<int, int?>> minWeightNodes = minCutTree.First();
                KeyValuePair<int, int?> edge = minWeightNodes.Value.First();

                if (edge.Value == null)
                    Debug.Assert(false);

                return new Tuple<int, int, int>(minWeightNodes.Key, edge.Key, (int)edge.Value);
            }
        }
        
        string dataFilePath;
        PrimsData data = new PrimsData();
        Graph graph = new Graph();

        public PrimsAlgorithmCalculator(string dataFilePath)
        {
            this.dataFilePath = dataFilePath;
            ParseData();
            CreateGraph();
        }

        private void ParseData()
        {
            PrimsDataParser dp = new PrimsDataParser(dataFilePath);
            dp.Parse(data);
        }

        private void CreateGraph()
        {
            IList<Tuple<int, int, int>> edges = data.GetGraphList();

            foreach(Tuple<int, int, int> edge in edges)
            {
                graph.Add(edge.Item1, edge.Item2, edge.Item3);
            }
        }

        public int RunAlgorithm()
        {
            bool firstNode = true;

            // initialize the min spanning tree and frontier
            // Pick first node to put into the min spanning tree
            // add the other nodes into the frontier

            MinSpanningTree mst = null;
            Frontier frontier = new Frontier();
            int initialNode = -1;

            foreach (int node in graph.Nodes)
            {
                if (firstNode)
                {
                    initialNode = node;
                    mst = new MinSpanningTree(node);
                    firstNode = false;
                }
                else
                {
                    int weight = graph.GetWeight(node, initialNode);
                    frontier.Add(weight, node, initialNode);
                }
            }

            while (frontier.GetMinimumCut() != null)
            {
                // pick the node with the lowest weight and add it to the mst
                // remove node from frontier
                Tuple<int, int, int> nextEdge = frontier.GetMinimumCut();
                frontier.Remove(nextEdge.Item1, nextEdge.Item2);
                mst.Add(nextEdge);

                // adjust connecting nodes
                foreach (KeyValuePair<int, int> nodeWeightPair in graph.GetConnection(nextEdge.Item2))
                {
                    frontier.AdjustWeight(nodeWeightPair.Key, nextEdge.Item2, nodeWeightPair.Value);
                }
            }

            // cost of the mst
            int cost = 0;
            foreach(Tuple<int,int,int> triplet in mst.Edges)
            {
                cost += triplet.Item1;
            }

            return cost;  //-3612829
        }
    }

    public class PrimsDataParser
    {
        string filepath;

        public PrimsDataParser(string filepath)
        {
            this.filepath = filepath;
        }

        public void Parse(PrimsData data)
        {
            string[] parsedLines = File.ReadAllLines(filepath);

            for (int i = 1; i < parsedLines.Length; i++)
            {
                string parsedLine = parsedLines[i];

                string[] tokens = parsedLine.Split();

                if (tokens.Length < 3)
                    continue;

                int nodeOneId = int.Parse(tokens[0]);
                int nodeTwoId = int.Parse(tokens[1]);
                int nodeThreeId = int.Parse(tokens[2]);

                data.Add(new Tuple<int, int, int>(nodeOneId, nodeTwoId, nodeThreeId));
            }
        }
    }

    public class PrimsData
    {
        List<Tuple<int, int, int>> graphCollection = new List<Tuple<int, int, int>>();

        public void Add(Tuple<int, int, int> item)
        {
            graphCollection.Add(item);
        }

        public IList<Tuple<int, int, int>> GetGraphList()
        {
            return graphCollection;
        }
    }
}
