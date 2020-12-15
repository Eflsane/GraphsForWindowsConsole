using System;
using System.Globalization;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsForWindowsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MathGraph mathGraph = new MathGraph();
            ShowBasicInterface(mathGraph);
        }

        public static void ShowBasicInterface(MathGraph mathGraph)
        {
            ShowGraph(mathGraph);
            Console.WriteLine("1. Add Vertex");
            Console.WriteLine("2. Add Edge");
            Console.WriteLine("3. Set Weight");
            Console.WriteLine("4. Remove Vertex");
            Console.WriteLine("5. Remove Edge");
            Console.WriteLine("6. Count Vertex");
            Console.WriteLine("7. Count Edges");
            Console.WriteLine("8. Check is vertex connected");
            Console.WriteLine("9. Show Weight");
            Console.WriteLine("10. Clear Graph");
            Console.WriteLine("11. Change type");
            Console.WriteLine("12. Open Graph");
            Console.WriteLine("13. Save Graph");
            Console.WriteLine("14. Show all three vertex subgraphs");
            Console.WriteLine("15. Exit");
            Console.WriteLine("1010. Easy");
            Console.WriteLine("Choose youre destiny:");
            processImput(mathGraph);
        }

        public static void processImput(MathGraph mathGraph)
        {
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    {
                        mathGraph.AddVertex();
                        Console.Clear();
                        ShowBasicInterface(mathGraph);
                        break;
                    }
                case "2":
                    {
                        Console.WriteLine("Enter first vertex:");
                        int firstVertex = -1;
                        if (!int.TryParse(Console.ReadLine(), out firstVertex))
                        {
                            Console.WriteLine("Wrong input \nPress any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        Console.WriteLine("Enter second vertex:");
                        int secondVertex = -1;
                        if (!int.TryParse(Console.ReadLine(), out secondVertex))
                        {
                            Console.WriteLine("Wrong input \nPress any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        bool result = mathGraph.AddEdge(firstVertex, secondVertex);
                        if (!result)
                        {
                            Console.WriteLine("Edge was not created \nPress any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        Console.WriteLine("Edge created \nPress any key");
                        Console.ReadKey();
                        Console.Clear();
                        ShowBasicInterface(mathGraph);
                        break;
                    }
                case "3":
                    {
                        Console.WriteLine("Enter first vertex:");
                        int firstVertex = -1;
                        if (!int.TryParse(Console.ReadLine(), out firstVertex))
                        {
                            Console.WriteLine("Wrong input \nPress any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        Console.WriteLine("Enter second vertex:");
                        int secondVertex = -1;
                        if (!int.TryParse(Console.ReadLine(), out secondVertex))
                        {
                            Console.WriteLine("Wrong input \nPress any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        Console.WriteLine("Enter weight:");
                        float weight = 0;
                        if (!float.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out weight))
                        {
                            Console.WriteLine("Wrong input \nPress any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        bool result = mathGraph.SetWeight(firstVertex, secondVertex, weight);
                        if (!result)
                        {
                            Console.WriteLine("Weight was not set \nPress any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        Console.WriteLine("Weight was set \nPress any key");
                        Console.ReadKey();
                        Console.Clear();
                        ShowBasicInterface(mathGraph);
                        break;
                    }
                case "4":
                    {
                        Console.WriteLine("Enter vertex:");
                        int vertex = -1;
                        if (!int.TryParse(Console.ReadLine(), out vertex))
                        {
                            Console.WriteLine("Wrong input \nPress any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        bool result = mathGraph.RemoveVertex(vertex);
                        if (!result)
                        {
                            Console.WriteLine("Vertex was not removed \nPress any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        Console.WriteLine("Vertex was removed \nPress any key");
                        Console.ReadKey();
                        Console.Clear();
                        ShowBasicInterface(mathGraph);
                        break;
                    }
                case "5":
                    {
                        Console.WriteLine("Enter first vertex:");
                        int firstVertex = -1;
                        if (!int.TryParse(Console.ReadLine(), out firstVertex))
                        {
                            Console.WriteLine("Wrong input \nPress any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        Console.WriteLine("Enter second vertex:");
                        int secondVertex = -1;
                        if (!int.TryParse(Console.ReadLine(), out secondVertex))
                        {
                            Console.WriteLine("Wrong input \nPress any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        bool result = mathGraph.RemoveEdge(firstVertex, secondVertex);
                        if (!result)
                        {
                            Console.WriteLine("Edge was not removed \nPress any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        Console.WriteLine("Edge removed \nPress any key");
                        Console.ReadKey();
                        Console.Clear();
                        ShowBasicInterface(mathGraph);
                        break;
                    }
                case "6":
                    {
                        Console.WriteLine("In graph " + mathGraph.CountVertex() + " vertex \nPress any key");
                        Console.ReadKey();
                        Console.Clear();
                        ShowBasicInterface(mathGraph);
                        break;
                    }
                case "7":
                    {
                        Console.WriteLine("In graph " + mathGraph.CountEdges() + " edges \nPress any key");
                        Console.ReadKey();
                        Console.Clear();
                        ShowBasicInterface(mathGraph);
                        break;
                    }
                case "8":
                    {
                        Console.WriteLine("Enter first vertex:");
                        int firstVertex = -1;
                        if (!int.TryParse(Console.ReadLine(), out firstVertex))
                        {
                            Console.WriteLine("Wrong input \n Press any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        Console.WriteLine("Enter second vertex:");
                        int secondVertex = -1;
                        if (!int.TryParse(Console.ReadLine(), out secondVertex))
                        {
                            Console.WriteLine("Wrong input \n Press any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        bool result = mathGraph.IsConnected(firstVertex, secondVertex);
                        if (!result)
                        {
                            Console.WriteLine("Vertex are not connected \nPress any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        Console.WriteLine("Vertex are connected \nPress any key");
                        Console.ReadKey();
                        Console.Clear();
                        ShowBasicInterface(mathGraph);
                        break;
                    }
                case "9":
                    {
                        Console.WriteLine("Enter first vertex:");
                        int firstVertex = -1;
                        if (!int.TryParse(Console.ReadLine(), out firstVertex))
                        {
                            Console.WriteLine("Wrong input \nPress any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        Console.WriteLine("Enter second vertex:");
                        int secondVertex = -1;
                        if (!int.TryParse(Console.ReadLine(), out secondVertex))
                        {
                            Console.WriteLine("Wrong input \nPress any key");
                            Console.ReadKey();
                            Console.Clear();
                            ShowBasicInterface(mathGraph);
                            break;
                        }
                        float result = mathGraph.GetWeight(firstVertex, secondVertex);
                        Console.WriteLine("Weight equals \n" + result + "\nPress any key");
                        Console.ReadKey();
                        Console.Clear();
                        ShowBasicInterface(mathGraph);
                        break;
                    }
                case "10":
                    {
                        mathGraph.ClearGraph();
                        Console.Clear();
                        ShowBasicInterface(mathGraph);
                        break;
                    }
                case "11":
                    {
                        mathGraph.ChangeType();
                        Console.Clear();
                        ShowBasicInterface(mathGraph);
                        break;
                    }
                case "12":
                    {
                        Console.WriteLine("Which type of graph representation use");
                        Console.WriteLine("1. Adjacency matrix");
                        Console.WriteLine("2. Adjacency list");
                        string representationType = Console.ReadLine();
                        switch (representationType)
                        {
                            case "1":
                                {
                                    Console.WriteLine("Enter path to file with matrix:");
                                    String result = mathGraph.ReadFromMatrix(Console.ReadLine());
                                    Console.WriteLine(result);
                                    Console.ReadKey();
                                    Console.Clear();
                                    ShowBasicInterface(mathGraph);
                                    break;
                                }
                            case "2":
                                {
                                    Console.WriteLine("Enter path to file with list:");
                                    String result = mathGraph.ReadFromList(Console.ReadLine());
                                    Console.WriteLine(result);
                                    Console.ReadKey();
                                    Console.Clear();
                                    ShowBasicInterface(mathGraph);
                                    break;
                                }
                        }
                        break;
                    }
                case "13":
                    {
                        Console.WriteLine("Which type of graph representation use");
                        Console.WriteLine("1. Adjacency matrix");
                        Console.WriteLine("2. Adjacency list");
                        string representationType = Console.ReadLine();
                        switch (representationType)
                        {
                            case "1":
                                {
                                    Console.WriteLine("Enter path to file with matrix or where create it:");
                                    String result = mathGraph.SaveToMatrix(Console.ReadLine());
                                    Console.WriteLine(result);
                                    Console.ReadKey();
                                    Console.Clear();
                                    ShowBasicInterface(mathGraph);
                                    break;
                                }
                            case "2":
                                {
                                    Console.WriteLine("Enter path to file with list or where create it:");
                                    String result = mathGraph.SaveToList(Console.ReadLine());
                                    Console.WriteLine(result);
                                    Console.ReadKey();
                                    Console.Clear();
                                    ShowBasicInterface(mathGraph);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                case "14":
                    {
                        var vertex3subgraphs = mathGraph.Show3VertexSubgraphs();
                        if (vertex3subgraphs.Equals(null))
                        {
                            Console.WriteLine("There is no three vertex subgraphs");
                            
                        }
                        else
                        {
                            string result = "Found " + vertex3subgraphs.Count + " three vertex subgraphs" + ":";
                            for (int i = 0; i < vertex3subgraphs.Count; i++)
                            {
                                result += "\n";
                                foreach (int vertex in vertex3subgraphs[i])
                                {
                                    if (vertex == vertex3subgraphs[i].Last.Value) result += vertex;
                                    else result += vertex + ", ";
                                }
                            }
                            result += "\nPress any key";
                            Console.WriteLine(result);
                        }
                        Console.ReadKey();
                        Console.Clear();
                        ShowBasicInterface(mathGraph);
                        break;
                    }
                case "15":
                    {
                        Environment.Exit(0);
                        break;
                    }
                case "1010":
                    {
                        MediaPlayer player = new MediaPlayer();
                      //  player.Open();
                      //  player.Play();
                        Console.Clear();
                        ShowBasicInterface(mathGraph);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Wrong input \nPress any key");
                        Console.ReadKey();
                        Console.Clear();
                        ShowBasicInterface(mathGraph);
                        break;
                    }
            }
        }

        public static void ShowGraph(MathGraph mathGraph)
        {
            int consoleWidth = Console.WindowWidth;
            if (mathGraph.IsOriented)
            {
                string isOrientedStr = "Oriented";
                Console.WriteLine("{0," + (consoleWidth - isOrientedStr.Length - 3) + "}", isOrientedStr);
            }
            else
            {
                string isOrientedStr = "Not Oriented";
                Console.WriteLine("{0," + (consoleWidth - isOrientedStr.Length - 3) + "}", isOrientedStr);
            }
            var graph = mathGraph.GetGraph();
            for (int i = 0; i < graph.Count; i++)
            {
                Console.Write(i + " => ");
                foreach (int connectedVertex in graph[i])
                {
                    Console.Write(connectedVertex + " ");
                }
                Console.Write("\n");
            }
        }
    }
}
