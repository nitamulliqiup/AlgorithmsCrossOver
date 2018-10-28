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
            //Deklarimi i vargut me vlerat qe do te kene prinderit
            int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int length = array.Length;
            int members = 3;
        
            //Gejnerimi i prindit te pare duke i perzier vlerat e vargut te dhene
            int[] parent_1 = RandomizeArray(array);
            //Gejnerimi i prindit te dyte duke i perzier vlerat e vargut te dhene
            int[] parent_2 = RandomizeArray(array);

            //Variabel nihmese per te ruajtur antaret e zgjedhur nga prindi i pare
            int[] selected_members = new int[members];

            //Variabel nihmese per te ruajtur qe mbesin pas largimit te antareve te zgjedhur nga prindi i dyte
            int[] left_members = new int[length - members];

            //Variabel nihmese per te ruajtur femiun
            int[] child = new int[length];

            //Ne menyre te rastesishme zgjedhim se ku do te fillojme te marrim antaret per te formuar femiun
            int start_index = _random.Next(0, length - members);

            //Zgjedhim antaret si dhe vendosim vlerat perkatese te femiu
            for (int i = 0; i < members; i++)
            {
                selected_members[i] = parent_1[start_index + i];
                child[start_index + i] = parent_1[start_index + i];
            }
            
            //Gjejme antaret e mbetur nga prindi numer 2 qe duhet te vendosen te femiu
            int j = 0;
            foreach(int member in parent_2)
            {
                if(!selected_members.Contains(member))
                {
                    left_members[j] = member;
                    j++;
                }              
            }

            //Fillimisht populojme pozitat nga 0 deri te indeksi i zgjedhur duke marre antaret perkates nga prindi numer 2
            int final_ones = (length - (start_index + members));
            for (int i= 0; i < start_index; i++ )
            {
                child[i] = left_members[final_ones + i];
            }

            //Pastaj populojme edhe antaret pas indeksit te veqse zgjedhur per te perfunduar keshtu formimin e femiut
            for (int i = 0; i < final_ones; i++)
            {
                child[(start_index + members) + i] = left_members[i];
            }

            //Printimi ne konzole i rezultateve dhe rrjedhes se programit
            Console.WriteLine(String.Format("We have Parent 1: [{0}]", string.Join(",", parent_1)));
            Console.WriteLine(String.Format("and we have Parent 2: [{0}]", string.Join(",", parent_2)));           
            Console.WriteLine(String.Format("We are going to take {0} from Parent 1 starting from index {1}", members, start_index));

            Console.WriteLine(String.Format("Selected members are: [{0}]", string.Join(",", selected_members)));
            Console.WriteLine(String.Format("Left members from Parent 2 are: [{0}]", string.Join(",", left_members)));
            Console.WriteLine(String.Format("The created child is [{0}]", string.Join(",", child)));           
            Console.ReadLine();

        }

        //Variable qe na ndihmon ne gjenerimin e numrave te rastesishim
        static Random _random = new Random();

        //Funksion ndihmes qe na mundeson perzierjen e vargjeve me permbajtje te numrave te tipit int
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
