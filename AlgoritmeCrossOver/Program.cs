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
            foreach (int member in parent_2)
            {
                if (!selected_members.Contains(member))
                {
                    left_members[j] = member;
                    j++;
                }
            }

            //Fillimisht populojme pozitat nga 0 deri te indeksi i zgjedhur duke marre antaret perkates nga prindi numer 2
            int final_ones = (length - (start_index + members));
            for (int i = 0; i < start_index; i++)
            {
                child[i] = left_members[final_ones + i];
            }

            //Pastaj populojme edhe antaret pas indeksit te veqse zgjedhur per te perfunduar keshtu formimin e femiut
            for (int i = 0; i < final_ones; i++)
            {
                child[(start_index + members) + i] = left_members[i];
            }

            //Printimi ne konzole i rezultateve dhe rrjedhes se programit
            //Console.WriteLine(String.Format("We have Parent 1: [{0}]", string.Join(",", parent_1)));
            //Console.WriteLine(String.Format("and we have Parent 2: [{0}]", string.Join(",", parent_2)));           
            //onsole.WriteLine(String.Format("We are going to take {0} from Parent 1 starting from index {1}", members, start_index));

            //Console.WriteLine(String.Format("Selected members are: [{0}]", string.Join(",", selected_members)));
            //Console.WriteLine(String.Format("Left members from Parent 2 are: [{0}]", string.Join(",", left_members)));
            //Console.WriteLine(String.Format("The created child is [{0}]", string.Join(",", child)));


            //Detyra per Rank Select Algoritmin
            List<string> cost_keys = new List<string>();
            List<int> cost_values = new List<int>();
            for (int i = 0; i < length; i++)
            {
                for (int k = i + 1; k < length; k++)
                {
                    //Gjenerojme listat me indexa si dhe gjenerojme vlerat random per koston e kalimit nga nje qytet ne tjetrin ne menyre te rastesishme
                    cost_keys.Add(array[i].ToString() + "x" + array[k].ToString());
                    cost_values.Add(_random.Next(1, 30));
                }
            }

            Console.WriteLine(String.Format("Cost Keys: [{0}]", string.Join(",", cost_keys)));
            Console.WriteLine(String.Format("Cost Values: [{0}]", string.Join(",", cost_values)));


            //Gjenrojme Populaten
            int population_size = 5;
            List<int[]> generation = new List<int[]>();
            List<int> generation_evaluation = new List<int>();

            for(int i = 0; i < population_size; i++)
            {
                generation.Add(RandomizeArray(array));
            }

            foreach(var member in generation)
            {
                int res = EvaluateMember(member, cost_keys, cost_values);
                generation_evaluation.Add(res);                
            }

            int[] selected_member = RankSelection(generation, generation_evaluation, length);
            Console.WriteLine(String.Format("Selected member is [{0}]", String.Join(",", selected_member)));
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

        static int EvaluateMember(int[] member, List<string> cost_keys, List<int> cost_values)
        {
            int total_cost = 0;
            int index = -1;
            //Shikojme ne listen cost_keys per indexat si p.sh '1x3' dhe marrim vleren e kostos nga lista cost_values po ashtu kosto '3x1' ekuivalentohet me koston '1x3'
            
            //Unaza vlen per te shikuar elementet nga i pari deri ne te fundit
            for (int i = 0; i < member.Length - 1; i++)
            {                
                index = cost_keys.FindIndex(item => item == member[i].ToString() + "x" + member[i + 1].ToString()) >= 0 ? cost_keys.FindIndex(item => item == member[i].ToString() + "x" + member[i + 1].ToString()) : cost_keys.FindIndex(item => item == member[i+1].ToString() + "x" + member[i].ToString());                
                total_cost += cost_values.ElementAt(index);
            }

            //Sherben per te llogaritur koston nga elementi i fundit per tu kthyer ne fillim
            index = cost_keys.FindIndex(item => item == member[0].ToString() + "x" + member[member.Length-1].ToString()) >= 0 ? cost_keys.FindIndex(item => item == member[0].ToString() + "x" + member[member.Length-1].ToString()) : cost_keys.FindIndex(item => item == member[member.Length - 1].ToString() + "x" + member[0].ToString());
            total_cost += cost_values.ElementAt(index);
            return total_cost;
        }

        static int[] RankSelection(List<int[]> generation, List<int> generation_evaluation, int length)
        {
            List<double> probabilities = new List<double>();            

            int population_size = generation.Count;
            double f = (population_size * (population_size + 1)) / 2;
            
            //Gjenerohet nje dictonary ne menyre qe te mund te ruajm se bashku antarin perkrah rezultatit te funksionit evalues        
            Dictionary<int[], int> gen_member_evaluation = new Dictionary<int[], int>();
            int i = 0;

            //Behet Populimi i dictionarit
            foreach(var item in generation)
            {
                gen_member_evaluation.Add(item, generation_evaluation.ElementAt(i));
                i++;
            }

            //Ne menyre qe ti sortojme ne baze te vleres me te mire (zgjedhja me e mire) sortohen duke filluar nga ata qe kane funksionin evalues me te vogel pasi qe kerkohet rruga me e shkurter
            List<KeyValuePair<int[], int>> tempList = gen_member_evaluation.ToList();
            tempList.Sort(delegate (KeyValuePair<int[], int> pair1, KeyValuePair<int[], int> pair2)
            {
                return pair1.Value.CompareTo(pair2.Value);
            });

            //Gjenerohen probabilitet per secilin nga antaret ne menyre qe te mund te zgjedhim pastaj
            i = 0;       
            foreach(var item in tempList)
            {
                int rank = population_size - i;
                double porbability = (double)rank / f;                
                probabilities.Add(porbability);
                Console.WriteLine(String.Format("Member: [{0}], Fitness: {1}, rank: {2}, probability: {3}", String.Join(",", item.Key), item.Value, rank, porbability));
                i++;
            }

            //Zgjedhet nje probabiltet ne menyre te rastesishme nga probabilitet e gjeneruara per secilin antare
            double random = _random.NextDouble() * (probabilities.Max() - probabilities.Min()) + probabilities.Min();            
            int returned_index = 0;
            double difference = Math.Abs(random - probabilities.First());

            //Zgjedhet elementi qe eshte me i perafert me probabilitetin e gjeneruar
            i = 0;
            foreach(var item in probabilities)
            {
                if(Math.Abs(random - item) < difference)
                {
                    returned_index = i;
                    difference = Math.Abs(random - item);
                }
                i++;
            }            
            
            int[] member = tempList.ElementAt(returned_index).Key;

            return member;
        }
    }
}
