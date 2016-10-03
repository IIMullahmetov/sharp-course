using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сombinatorics
{
    class TasksManager
    {
        static int students = 12;
        static int tasks = 8;
        int allTasks = 24;
        int[,] array = new int[students, tasks];

        public TasksManager()
        {
            Start();
            Show();
        }

        public void Start()
        {
            Random random = new Random();

            for (int i = 0; i < students; i++)
            {
                for (int j = 0; j < tasks; j++)
                {
                    Random(random, i, j);

                    // terrible?
                    while (CompareColumn(array[i, j], i, j) || (CompareLine(array[i, j], i, j)))
                    {
                        Random(random, i, j);
                    }
                } 
            } 
        }

        public void Random(Random random, int i, int j)
        {
            array[i, j] = random.Next(1, allTasks);
        }

        public bool CompareColumn(int element, int i, int j)
        {
            for (int h = 0; h < students; h++)
            {
                if (i != 0 && h != i && element == array[h, j])
                {
                    return true;
                }
            }
            return false;
        }

        public bool CompareLine(int element, int i, int j)
        {
            for (int u = 0; u < tasks; u++)
            {
                if (j != 0 && u != j && element == array[i, u])
                {
                    return true;
                }
            }
            return false;
        }

        public void Show()
        {
            for (int i = 0; i < students; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < tasks; j++)
                {
                    if (array[i, j] < 10)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(array[i, j] + " ");
                }
            }
        }
    }
}
