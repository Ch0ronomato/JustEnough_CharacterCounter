using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace JustEnough_CharacterCounter
{
    // don't fear the heapier....
   
    /**
     * My tatic here was too utilize heapsort. Heapsort has very nice
     * properties too it: 
     *      1. Our best and worse case complexities are O(nlog(n)).
     *      2. A heap is similar to a tree, so we get a tree structure, which means we have 2^(k-1) nodes for a k-1 sized tree
     *         Where any partially filled level is from left to right. This is a shape property
     *      3. A heap also indicates that the parent node will have a higher value then that of it's children.
     * This is an attractive property for the input we are given in the prompt.
     */
    class Heapier
    {
        Dictionary<char, int> count = new Dictionary<char, int>();
        public Dictionary<char, int> sort(string[] words, int length)
        {
            // treat each word as a heap, then after fully "heaping" 
            // all the individual words, we have a partially sorted array, so
            // now heap again.
            List<string> sortedWords = new List<string>(length);
            for (int j = 0; j < length; j++)
            {
                char[] word = words[j].ToCharArray();
                int n = words[j].Length;
                this.buildHeap(word, n);
                for (int i = n - 1; i >= 1; i--)
                {
                    char temp = word[0];
                    word[0] = word[i];
                    word[i] = temp;
                    this.heapify(word, 0, i);
                }
                Console.WriteLine(word);
                sortedWords.Add(new string(word));
            }

            string final = String.Join("", sortedWords.ToArray());
            int finalLength = final.Length;
            char[] sentence = final.ToCharArray();
            this.buildHeap(sentence, finalLength);
            for (int i = finalLength - 1; i >= 1; i--)
            {
                char temp = sentence[0];
                sentence[0] = sentence[i];
                sentence[i] = temp;
                this.heapify(sentence, 0, 1);
            }
            Console.WriteLine(sentence);
            return this.count;
        }

        private void buildHeap(char[] chunk, int n)
        {
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                this.heapify(chunk, i, n);
            }
        }

        private void heapify(char[] chunk, int idx, int max)
        {
            int left = 2 * idx + 1;
            int right = 2 * idx + 2;
            int largest = Int32.MaxValue;

            // find the largest element
            if (left < max && (int)chunk[left] < (int)chunk[idx])
            {
                largest = left;
            }
            else
            {
                largest = idx;
            }

            if (right < max && (int)chunk[right] < (int)chunk[idx])
            {
                largest = right;
            }
            

            // if largest isn't center, then make it center and propagate
            if (largest != idx)
            {
                char temp = chunk[idx];
                chunk[idx] = chunk[largest];
                chunk[largest] = temp;
                this.heapify(chunk, largest, max);
            }
        }

        
    }
}
