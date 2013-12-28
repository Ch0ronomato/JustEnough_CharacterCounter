using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustEnough_CharacterCounter
{
    class countingsort
    {
        public List<int> sort(int[] ar, int n, int max, int min, bool returnCount = false)
        {

            int i, idx = 0, k = max;
            int[] B = new int[k];
            for (int j = 0; j < k; j++)
            {
                B[j] = 0;
            }

            for (i = 0; i < n; i++)
            {
                B[ar[i]]++;
            }

            if (returnCount)
            {
                return B.ToList();
            }

            for (i = 0; i < k; i++)
            {
                while (B[i]-- > 0)
                {
                    ar[idx++] = i;
                }
            }
            return ar.ToList();
        }
    }
}
