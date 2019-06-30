using System;

namespace DieHard
{
    class Program
    {
        private const char CREATE = 'N';
        private const char FILL = 'R';
        private const char EMPTY = 'S';
        private const char MOVE = 'T';
        private const char EXIST = 'e';
        private const char REACHABLE = 'r';
        private const char PRINT = 'v';
        private const char PRINT_GRAPH = 'g';
        private const char PROGRAM_EXIT = 'f';

        private static Graph graph;
        private static Sequence currentSequence;

        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Die Hard simulator");

            string command = string.Empty;
            string[] commandSplit = null;

            char commandCode = ' ';
            int[] commandParams = null;

            while(commandCode != PROGRAM_EXIT)
            {
                Console.Write("Please enter a command: ");
                command = Console.ReadLine();
                commandSplit = command.Split(' ');

                if(commandSplit != null && commandSplit.Length > 0)
                {
                    commandCode = commandSplit[0][0];

                    if(commandSplit.Length > 1)
                    {
                        commandParams = new int[commandSplit.Length - 1];
                        for(int i = 1; i < commandSplit.Length; i++)
                        {
                            commandParams[i - 1] = int.Parse(commandSplit[i]);
                        }
                    }

                    SelectCommand(commandCode, commandParams);
                }
            }
        }

        private static void SelectCommand(char commandCode, int[] commandParams)
        {
            switch(commandCode)
            {
                case CREATE:
                    CreateContainers(commandParams);
                    break;
                case FILL:
                    FillContainer(commandParams[0]);
                    break;
                case EMPTY:
                    EmptyContainer(commandParams[0]);
                    break;
                case MOVE:
                    MoveContent(commandParams[0], commandParams[1]);
                    break;
                case EXIST:
                    Exist(commandParams[0]);
                    break;
                case REACHABLE:
                    Reachable(commandParams);
                    break;
                case PRINT:
                    PrintContainers();
                    break;
                case PRINT_GRAPH:
                    PrintGraph();
                    break;
            }
        }

        private static void CreateContainers(int[] commandParams)
        {
            currentSequence = new Sequence(commandParams);
            graph = NewGraph(currentSequence);
        }

        private static Graph NewGraph(Sequence startSequence)
        {
            Graph newGraph = new Graph();
            newGraph.Generate(startSequence);

            return newGraph;
        }

        private static void FillContainer(int v)
        {
            int result = currentSequence.FillContainer(v - 1);

            if (result < 0)
                Console.WriteLine("OPERATION NOT VALID!");
        }

        private static void EmptyContainer(int v)
        {
            int result = currentSequence.EmptyContainer(v - 1);

            if (result < 0)
                Console.WriteLine("OPERATION NOT VALID!");
        }

        private static void MoveContent(int fromIndex, int toIndex)
        {
            int result = currentSequence.MoveContent(fromIndex - 1, toIndex - 1);

            if (result < 0)
                Console.WriteLine("OPERATION NOT VALID!");
        }

        private static void Exist(int level)
        {
            string result = graph.Exist(level) ? "YES" : "NO";
            Console.WriteLine(result);
        }

        private static void Reachable(int[] commandParams)
        {
            string result = graph.Exist(new Sequence(currentSequence, commandParams)) ? "YES" : "NO";
            Console.WriteLine(result);
        }

        private static void PrintContainers()
        {
            Console.WriteLine(currentSequence);
        }

        private static void PrintGraph()
        {
            Console.WriteLine(graph);
        }
    }
}
