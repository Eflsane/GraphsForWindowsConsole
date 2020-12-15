using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsForWindowsConsole
{
    class MathGraph
    {
        private List<LinkedList<int>> graph = new List<LinkedList<int>>();
        private float[,] weight = new float[0, 0];
        public bool IsOriented { get; private set; } = true;

        public void ClearGraph()
        {
            graph = new List<LinkedList<int>>();
            weight = new float[graph.Count, graph.Count];
        }

        public void AddVertex()
        {
            graph.Add(new LinkedList<int>());
            float[,] newWeight = new float[graph.Count, graph.Count];
            for (int i = 0; i < weight.GetLength(0); i++)
            {
                for (int j = 0; j < weight.GetLength(1); j++)
                {
                    newWeight[i, j] = weight[i, j];
                }
            }
            weight = new float[graph.Count, graph.Count];
            for (int i = 0; i < weight.GetLength(0); i++)
            {
                for (int j = 0; j < weight.GetLength(1) - 1; j++)
                {
                    weight[i, j] = newWeight[i, j];
                }
            }
        }

        public bool AddEdge(int x, int y)
        {
            if(x == y)
            {
                return false;
            }
            else if (x < 0 || y < 0)
            {
                return false;
            }
            else if (graph.Count <= 0)
            {
                return false;
            }
            else if (graph.Count <= x || graph.Count <= y)
            {
                return false;
            }
            else if (IsConnected(x, y))
            {
                return false;
            }
            graph[x].AddLast(y);
            graph[x] = new LinkedList<int>(graph[x].OrderBy(vertex => vertex));
            if (!IsOriented)
            {
                graph[y].AddLast(x);
                graph[y] = new LinkedList<int>(graph[y].OrderBy(vertex => vertex));
            }
            SetWeight(x, y, 1);
            return true;
        }

        public bool RemoveVertex(int x)
        {
            if (x < 0)
            {
                return false;
            }
            else if (graph.Count <= x)
            {
                return false;
            }
            for (int i = 0; i < graph.Count; i++)
            {
                if (i == x)
                {
                    continue;
                }
                //graph[i].Remove(x);
                RemoveEdge(i, x);
                if (graph[i].Where<int>(vertex => vertex > x) == null)
                {
                    continue;
                }
                LinkedList<int> vertexConnections = new LinkedList<int>();
                foreach (int connectedVertex in graph[i])
                {
                    int newNumber = connectedVertex;
                    if (connectedVertex > x)
                    {
                        newNumber -= 1;
                    }
                    vertexConnections.AddLast(newNumber);
                }
                graph[i] = new LinkedList<int>();
                foreach (int connectedVertex in vertexConnections)
                {
                    graph[i].AddLast(connectedVertex);
                }
            }
            float[,] newWeight = new float[graph.Count - 1, graph.Count - 1];
            for (int i = 0; i < weight.GetLength(0); i++)
            {
                for (int j = 0; j < weight.GetLength(1); j++)
                {
                    if (i != x && j != x)
                    {
                        int newI = i;
                        int newJ = j;
                        if (i > x)
                        {
                            newI -= 1;
                        }
                        if (j > x)
                        {
                            newJ -= 1;
                        }
                        newWeight[newI, newJ] = weight[i, j];
                    }
                }
            }
            graph.RemoveAt(x);
            weight = new float[graph.Count, graph.Count];
            for (int i = 0; i < weight.GetLength(0); i++)
            {
                for (int j = 0; j < weight.GetLength(1); j++)
                {
                    weight[i, j] = newWeight[i, j];
                }
            }
            return true;
        }

        public bool RemoveEdge(int x, int y)
        {
            if (x == y)
            {
                return false;
            }
            else if (x < 0 || y < 0)
            {
                return false;
            }
            else if (graph.Count <= x || graph.Count <= y)
            {
                return false;
            }
            else if (!IsConnected(x, y))
            {
                return false;
            }
            /*else if (weight[x, y] == 0)
            {
                return false;
            }*/
            SetWeight(x, y, 0, true);
            graph[x].Remove(y);
            if(!IsOriented)
            {
                SetWeight(y, x, 0);
                graph[y].Remove(x);
            }
            return true;
        }

        public bool SetWeight(int x, int y, float weightValue)
        {
            if (x == y)
            {
                return false;
            }
            else if (x < 0 || y < 0)
            {
                return false;
            }
            else if (weightValue == 0)
            {
                return false;
            }
            else if (graph.Count <= x || graph.Count <= y)
            {
                return false;
            }
            else if (!IsConnected(x, y))
            {
                return false;
            }
            /*else if (weight[x, y] == 0)
            {
                return false;
            }*/
            weight[x, y] = weightValue;
            if (!IsOriented) weight[y, x] = weightValue;
            return true;
        }

        private bool SetWeight(int x, int y, float weightValue, bool isRemoving)
        {
            if (x == y)
            {
                return false;
            }
            else if (x < 0 || y < 0)
            {
                return false;
            }
            else if (graph.Count <= x || graph.Count <= y)
            {
                return false;
            }
            else if (!IsConnected(x, y))
            {
                return false;
            }
            /*else if (weight[x, y] == 0)
            {
                return false;
            }*/
            else if (isRemoving)
            {
                if (weightValue != 0)
                {
                    return false;
                }
                weight[x, y] = weightValue;
                if (!IsOriented) weight[y, x] = weightValue;
                return true;
            }
            else if (weightValue == 0)
            {
                return false;
            }
            weight[x, y] = weightValue;
            if (!IsOriented) weight[y, x] = weightValue;
            return true;
        }

        public int CountVertex()
        {
            return graph.Count();
        }

        public int CountEdges()
        {
            int edges = 0;
            for (int i = 0; i < graph.Count; i++)
            {
                edges += graph[i].Count;
            }
            if (!IsOriented) return edges / 2;
            return edges;
        }

        public bool IsConnected(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                return false;
            }
            else if (graph.Count <= x || graph.Count <= y)
            {
                return false;
            }
            var connectedVertex = graph[x].Find(y);
            if (connectedVertex == null)
            {
                return false;
            }
            return true;
        }

        public float GetWeight(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                return 0;
            }
            if (graph.Count <= x && graph.Count <= y)
            {
                return 0;
            }
            return weight[x, y];
        }

        public List<LinkedList<int>> GetGraph()
        {
            List<LinkedList<int>> graphToShow = new List<LinkedList<int>>();
            foreach (LinkedList<int> vertex in graph)
            {
                graphToShow.Add(new LinkedList<int>());

                foreach (int connectedVertex in vertex)
                {
                    graphToShow.Last<LinkedList<int>>().AddLast(connectedVertex);
                }

            }
            return graphToShow;
        }

        public void ChangeType()
        {
            if (!IsOriented)
            {
                IsOriented = true;
                return;
            }
            for (int i = 0; i < graph.Count; i++)
            {
                foreach (int connectedVertex in graph[i])
                {
                    if (!IsConnected(connectedVertex, i))
                    {
                        AddEdge(connectedVertex, i);
                    }
                }
            }
            IsOriented = false;
            return;
        }


        public void ChangeType(bool onIsOriented)
        {
            if (onIsOriented)
            {
                if (!IsOriented)
                {
                    IsOriented = true;
                }
                return;
            }
            if (IsOriented)
            {
                for (int i = 0; i < graph.Count; i++)
                {
                    foreach (int connectedVertex in graph[i])
                    {
                        if (!IsConnected(connectedVertex, i))
                        {
                            AddEdge(connectedVertex, i);
                        }
                    }
                }
                IsOriented = false;
            }
            return;
        }

        public void VerifyType()
        {
            for (int i = 0; i < graph.Count; i++)
            {
                foreach (int connectedVertex in graph[i])
                {
                    if (!IsConnected(connectedVertex, i) || (GetWeight(i, connectedVertex) != GetWeight(connectedVertex, i)))
                    {
                        ChangeType(true);
                        return;
                    }

                }
            }
            ChangeType(false);
            return;
        }

        public List<LinkedList<int>> Show3VertexSubgraphs()
        {
            if (graph.Count <= 3)
            {
                return null;
            }
            if(!IsConnectedGraph())
            {
                return null;
            }
            List<LinkedList<int>> vertex3subgraphs = new List<LinkedList<int>>();
            for(int i = 0; i < graph.Count; i++)
            {
                foreach(int secondVertex in graph[i])
                {
                    foreach (int thirdVertex in graph[secondVertex])
                    {
                        
                        if(IsConnected(thirdVertex, i) || IsConnected(i, thirdVertex))
                        {
                            bool firstVertexIsFoundBefore = false;
                            bool secondVertexIsFoundBefore = false;
                            bool thirdVertexIsFoundBefore = false;
                            for (int j = 0; j < vertex3subgraphs.Count; j++)
                            {
                                foreach (int vertex in vertex3subgraphs[j])
                                {
                                    if(vertex == i || vertex == secondVertex || vertex == thirdVertex)
                                    {
                                        if(firstVertexIsFoundBefore)
                                        {
                                            if(secondVertexIsFoundBefore)
                                            {
                                                thirdVertexIsFoundBefore = true;
                                                continue;
                                            }
                                            secondVertexIsFoundBefore = true;
                                            continue;
                                        }
                                        firstVertexIsFoundBefore = true;
                                    }
                                }
                                if(thirdVertexIsFoundBefore)
                                {
                                    break;
                                }
                                firstVertexIsFoundBefore = false;
                                secondVertexIsFoundBefore = false;
                                thirdVertexIsFoundBefore = false;
                            }
                            if (!thirdVertexIsFoundBefore)
                            {
                                vertex3subgraphs.Add(new LinkedList<int>());
                                vertex3subgraphs.Last().AddLast(i);
                                vertex3subgraphs.Last().AddLast(secondVertex);
                                vertex3subgraphs.Last().AddLast(thirdVertex);
                                break;
                            }
                        }
                    }
                }
            }
            return vertex3subgraphs;
        }

        public bool IsConnectedGraph()
        {
            
            if(graph.Count <= 0)
            {
                return false;
            }
            int vertex = 0;
            int startVertex = 0;
            //int vertexConnected = 0;
            Queue<int> connections = new Queue<int>();
            bool[] isNewVertex = new bool[graph.Count];
            for(int i = 0; i < isNewVertex.Length; i++)
            {
                isNewVertex[i] = true;
            }
            connections.Enqueue(startVertex);
            //vertexConnected++;
            while(connections.Count > 0)
            {
                vertex = connections.Dequeue();
                //vertexConnected++;
                foreach(int connectedVertex in graph[vertex])
                {
                    if(!isNewVertex[connectedVertex])
                    {
                        continue;
                    }
                    connections.Enqueue(connectedVertex);
                    isNewVertex[connectedVertex] = false;
                }
            }
            if(!isNewVertex.All(isNew => isNew == false))
            {
                return false;
            }
            return true;
        }

        public String ReadFromMatrix(String path)
        {
            ClearGraph();
            ChangeType(true);
            try
            {
                StreamReader file = new StreamReader(path);
                String line;
                int lineNumber = 0;
                while ((line = file.ReadLine()) != null)
                {
                    string countString = "";
                    string weightStr = "";
                    int vertexCount = -1;
                    float vertexWeight = -1;
                    int vertexNumber = 0;
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (lineNumber == 0)
                        {
                            if ((line[i] >= '0' && line[i] <= '9')) countString += line[i];
                            if (i == line.Length - 1 || (line[i] < '0' || line[i] > '9')) 
                            {
                                int.TryParse(countString, out vertexCount);
                                countString = "";
                                if (vertexCount == -1)
                                {
                                    file.Close();
                                    ClearGraph();
                                    return "Error wrong file's insides.";
                                }
                                else
                                {
                                    for (int j = 0; j < vertexCount; j++) AddVertex();
                                }
                                break;
                            }
                        }
                        else
                        {
                            if (lineNumber > graph.Count || vertexNumber > graph.Count)
                            {
                                file.Close();
                                ClearGraph();
                                return "Error vertex in matrix more than have been declared.";
                            }
                            if ((line[i] >= '0' && line[i] <= '9') || line[i].Equals('.')) weightStr += line[i];
                            if (i == line.Length - 1 || ((line[i] < '0' || line[i] > '9') && line[i] != '.'))
                            {
                                if (!float.TryParse(weightStr, NumberStyles.Any, CultureInfo.InvariantCulture, out vertexWeight))
                                {
                                    file.Close();
                                    ClearGraph();
                                    return "Error wrong file's insides.";
                                }
                                else
                                {
                                    weightStr = "";
                                    if (vertexWeight > 0)
                                    {
                                        AddEdge(lineNumber - 1, vertexNumber);
                                        SetWeight(lineNumber - 1, vertexNumber, vertexWeight);
                                        vertexWeight = -1;
                                    }
                                    vertexNumber++;
                                }
                            }

                        }


                    }
                    lineNumber++;
                }
                file.Close();
                VerifyType();
                return "OK";
            }
            catch (ArgumentException e)
            {
                ClearGraph();
                return e.Message;
            }
            catch (FileNotFoundException e)
            {
                ClearGraph();
                return e.Message;
            }
            catch (FileLoadException e)
            {
                ClearGraph();
                return e.Message;
            }
            catch (DirectoryNotFoundException e)
            {
                ClearGraph();
                return e.Message;
            }
        }

        public string ReadFromList(string path)
        {
            ClearGraph();
            ChangeType(true);
            try
            {
                StreamReader file = new StreamReader(path);
                String line;
                int lineNumber = 0;
                while ((line = file.ReadLine()) != null)
                {
                    string countString = "";
                    string numericStr = "";
                    int vertexCount = -1;
                    float vertexWeight = -1;
                    int numericNumber = 0;
                    int firstVertex = 0;
                    int secondVertex = 0;
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (lineNumber == 0)
                        {
                            if ((line[i] >= '0' && line[i] <= '9')) countString += line[i];
                            if (i == line.Length - 1 || (line[i] < '0' || line[i] > '9'))
                            {
                                int.TryParse(countString, out vertexCount);
                                countString = "";
                                if (vertexCount == -1)
                                {
                                    file.Close();
                                    ClearGraph();
                                    return "Error wrong file's insides.";
                                }
                                else
                                {
                                    for (int j = 0; j < vertexCount; j++) AddVertex();
                                }
                                break;
                            }
                        }
                        else
                        {
                            if ((line[i] >= '0' && line[i] <= '9') || line[i].Equals('.')) numericStr += line[i];
                            if (i == line.Length - 1 || ((line[i] < '0' || line[i] > '9') && line[i] != '.'))
                            {
                                if (numericNumber == 0)
                                {
                                    if (!int.TryParse(numericStr, out firstVertex))
                                    {

                                        file.Close();
                                        ClearGraph();
                                        return "Error wrong file's insides.";
                                    }
                                    numericStr = "";
                                    numericNumber++;
                                    continue;
                                }
                                else if (numericNumber == 1)
                                {
                                    if (!int.TryParse(numericStr, out secondVertex))
                                    {

                                        file.Close();
                                        ClearGraph();
                                        return "Error wrong file's insides.";
                                    }
                                    else if(firstVertex > graph.Count || secondVertex > graph.Count)
                                    {
                                        file.Close();
                                        ClearGraph();
                                        return "Error vertex in matrix more than have been declared.";
                                    }
                                    numericStr = "";
                                    numericNumber++;
                                    continue;
                                }
                                else
                                {
                                    if (!float.TryParse(numericStr, NumberStyles.Any, CultureInfo.InvariantCulture, out vertexWeight))
                                    {
                                        file.Close();
                                        ClearGraph();
                                        return "Error wrong file's insides.";
                                    }
                                    numericStr = "";
                                    AddEdge(firstVertex - 1, secondVertex - 1);
                                    SetWeight(firstVertex - 1, secondVertex - 1, vertexWeight);
                                    vertexWeight = -1;
                                    numericNumber++;
                                }
                            }
                        }
                    }
                    lineNumber++;
                }
                file.Close();
                VerifyType();
                return "OK";
            }
            catch (ArgumentException e)
            {
                ClearGraph();
                return e.Message;
            }
            catch (FileNotFoundException e)
            {
                ClearGraph();
                return e.Message;
            }
            catch (FileLoadException e)
            {
                ClearGraph();
                return e.Message;
            }
            catch (DirectoryNotFoundException e)
            {
                ClearGraph();
                return e.Message;
            }
        }

        public string SaveToMatrix(string path)
        {
            try
            {
                FileStream file = File.Create(path);
                StreamWriter fileWriter = new StreamWriter(file);
                fileWriter.WriteLine(graph.Count);
                for (int i = 0; i < weight.GetLength(0); i++)
                {
                    String saveString = "";
                    for (int j = 0; j < weight.GetLength(1); j++)
                    {
                        saveString += weight[i, j];
                        if (j < weight.GetLength(1) - 1) saveString += " ";
                    }
                    saveString = saveString.Replace(",", ".");
                    fileWriter.WriteLine(saveString);
                }
                fileWriter.Close();
                file.Close();
                return "OK";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
            catch (FileLoadException e)
            {
                return e.Message;
            }
            catch (DirectoryNotFoundException e)
            {
                return e.Message;
            }
        }

        public string SaveToList(string path)
        {
            try
            {
                FileStream file = File.Create(path);
                StreamWriter fileWriter = new StreamWriter(file);
                fileWriter.WriteLine(graph.Count);
                for (int i = 0; i < weight.GetLength(0); i++)
                {
                    string saveString;
                    for (int j = 0; j < weight.GetLength(1); j++)
                    {
                        saveString = "";
                        if (IsConnected(i, j))
                        {
                            saveString += (i + 1) + " " + (j + 1) + " " + weight[i, j];
                            saveString = saveString.Replace(",", ".");
                            fileWriter.WriteLine(saveString);
                        }
                    }
                }
                fileWriter.Close();
                file.Close();
                return "OK";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
            catch (FileLoadException e)
            {
                return e.Message;
            }
            catch (DirectoryNotFoundException e)
            {
                return e.Message;
            }
        }
    }
}
