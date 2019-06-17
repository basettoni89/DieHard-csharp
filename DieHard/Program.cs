using System;

namespace DieHard
{
    class Program
    {
        private const char CREATE = 'N';
        private const char FILL = 'R';
        private const char EMPTY = 'S';
        private const char MOVE = 'T';
        private const char PRINT = 'v';
        private const char PROGRAM_EXIT = 'f';

        private static Container[] containers;

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
                case PRINT:
                    PrintContainers();
                    break;
            }
        }

        private static void CreateContainers(int[] commandParams)
        {
            containers = new Container[commandParams.Length];

            for(int i = 0; i < commandParams.Length; i++)
            {
                Container container = new Container(commandParams[i]);
                containers[i] = container;
            }
        }

        private static void FillContainer(int v)
        {
            if (v > containers.Length)
                return;

            int index = v - 1;
            if (containers[index].IsFull)
                Console.WriteLine("OPERATION NOT VALID!");
            else
                containers[index].Fill();
        }

        private static void EmptyContainer(int v)
        {
            if (v > containers.Length)
                return;

            int index = v - 1;
            if (containers[index].IsEmpty)
                Console.WriteLine("OPERATION NOT VALID!");
            else
                containers[index].Empty();
        }

        private static void MoveContent(int fromIndex, int toIndex)
        {
            if (fromIndex > containers.Length || toIndex > containers.Length)
                return;

            Container from = containers[fromIndex - 1];
            Container to = containers[toIndex - 1];

            if(from.IsEmpty || to.IsFull)
                Console.WriteLine("OPERATION NOT VALID!");

            from.Level = to.Increase(from.Level);
        }

        private static void PrintContainers()
        {
            string output = "(";

            for (int i = 0; i < containers.Length; i++)
            {
                output += containers[i];

                if (i < containers.Length - 1)
                    output += ",";
            }

            output += ")";

            Console.WriteLine(output);
        }
    }
}
