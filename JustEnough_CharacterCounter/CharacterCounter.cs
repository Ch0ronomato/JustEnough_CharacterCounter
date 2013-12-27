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
        List<string> parsed = new List<string>();
        Heapier heapier = new Heapier();
        public static void Main()
        {
            // instantiate counter
            CharacterCounter main = new CharacterCounter();

            // get input
            main.getInput();

            // sort
            main.heapier.sort(main.parsed.ToArray(), main.parsed.Count);
        }

        /**
         * Function gets input from file
         */
        public void getInput()
        {
            // get file from project
            using (StreamReader io = new StreamReader("../../autogram.txt"))
            {
                this.rawInput = io.ReadToEnd();
                io.Close();
            }

            // turn strings into list
            this.parsed = this.rawInput.Trim()
                .Split(new char[] { ' ' })
                .Select(x =>
                {
                    x.Replace("\n", "");
                    return x;
                }).ToList();
        }
    }
}
