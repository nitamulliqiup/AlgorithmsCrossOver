using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmeCrossOver
{
    class Program
    {
        static void Main(string[] args)
        {
            //int maximum_values = 8;
            int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int length = array.Length;
            int members = 3;

            int[] parent_1 = RandomizeArray(array);
            int[] parent_2 = RandomizeArray(array);
            int[] selected_members = new int[members];
            int[] left_members = new int[length - members];
            int[] child = new int[length];

            int start_index = _random.Next(0, length - members);
            for (int i = 0; i < members; i++)
            {
                selected_members[i] = parent_1[start_index + i];
                child[start_index + i] = parent_1[start_index + i];
            }
            
            int j = 0;
            foreach(int member in parent_2)
            {
                if(!selected_members.Contains(member))
                {
                    left_members[j] = member;
                    j++;
                }              
            }

            for(int i= 0; i < start_index; i++ )
            {
                child[i] = left_members[i];
            }

            int final_ones = (length - (start_index + members));
            for(int i = 0; i < final_ones; i++)
            {
                child[(start_index + members) + i] = left_members[start_index + i];
            }
            Console.WriteLine(String.Format("We have Parent 1: [{0}]", string.Join(",", parent_1)));
            Console.WriteLine(String.Format("and we have Parent 2: [{0}]", string.Join(",", parent_2)));           
            Console.WriteLine(String.Format("We are going to take {0} from Parent 1 starting from index {1}", members, start_index));

            Console.WriteLine(String.Format("Selected members are: [{0}]", string.Join(",", selected_members)));
            Console.WriteLine(String.Format("Left members from Parent 2 are: [{0}]", string.Join(",", left_members)));
            Console.WriteLine(String.Format("The created child is [{0}]", string.Join(",", child)));           
            Console.ReadLine();

        }

        static Random _random = new Random();

        static int[] RandomizeArray(int[] arr)
        {
            List<KeyValuePair<int, int>> list =
                new List<KeyValuePair<int, int>>();
            
            foreach (int s in arr)
            {
                list.Add(new KeyValuePair<int, int>(_random.Next(), s));
            }
            
            var sorted = from item in list
                         orderby item.Key
                         select item;
            
            int[] result = new int[arr.Length];
            
            int index = 0;
            foreach (KeyValuePair<int, int> pair in sorted)
            {
                result[index] = pair.Value;
                index++;
            }
            
            return result;
        }
    }
}
