using AdventOfCode.Reader;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2015
{
    public class Day04 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day04(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var key = inputProvider.GetInput().Single();
            return FindAnswer(key, "00000");
        }

        public long Part2()
        {
            var key = inputProvider.GetInput().Single();
            return FindAnswer(key, "000000");
        }

        private long FindAnswer(string key, string condition)
        {
            int iteration = 0;
            using MD5 md5 = MD5.Create();

            while (true)
            {
                var answer = $"{key}{iteration}";
                byte[] inputBytes = Encoding.ASCII.GetBytes(answer);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                var result = Convert.ToHexString(hashBytes);
                if (result.StartsWith(condition))
                    return iteration;

                iteration++;
            }
        }
    }
}
