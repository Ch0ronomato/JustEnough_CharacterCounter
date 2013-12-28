using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustEnough_CharacterCounter
{
    class CharacterCounter
    {
        string rawInput = "";
        Heapier heapier = new Heapier();
        public static void Main()
        {
            // instantiate counter
            CharacterCounter main = new CharacterCounter();

            // get input
            main.getInput();

            // convert to integer
            List<int> parsedAsInt = main.rawInput.Select(x =>
            {
                return (int)x;
            }).ToList();
            List<Counted> final = new List<Counted>();
            
            // instantiate counting sorter
            countingsort sorter = new countingsort();
            final.Add(new Counted() {
                min = parsedAsInt.Min(),
                sorted = sorter.sort(parsedAsInt.ToArray(), parsedAsInt.Count, parsedAsInt.Max() + 1, parsedAsInt.Min()),
                count = sorter.sort(parsedAsInt.ToArray(), parsedAsInt.Count, parsedAsInt.Max() + 1, parsedAsInt.Min(), true)
            });

            // tally up the total.
            Dictionary<char, int> count = new Dictionary<char, int>();
            final.ForEach(x =>
            {
                x.sorted.RemoveAll(y => y < 33);
                for (int i = 0; i< x.sorted.Count; i++)
                {
                    if (!count.ContainsKey((char)x.sorted[i]))
                    {
                        count[(char)x.sorted[i]] = 0;
                    }
                    count[(char)x.sorted[i]]++;
                }
            });

            // print
            foreach (KeyValuePair<char, int> entry in count)
            {
                Console.WriteLine(entry.Key + " appears " + entry.Value + " times");
            }
        }

        /**
         * Function gets input from file
         */
        public void getInput()
        {
            // get file from project
            using (StreamReader io = new StreamReader("../../autogram.txt"))
            {
                this.rawInput = io.ReadToEnd().ToLower();
                io.Close();
            }
        }
    }
}
