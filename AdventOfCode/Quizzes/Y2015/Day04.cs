﻿using AdventOfCode.Reader;
using System.Security.Cryptography;
using System.Text;

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

            while (true)
            {
                var answer = $"{key}{iteration}";
                byte[] inputBytes = Encoding.ASCII.GetBytes(answer);
                byte[] hashBytes = MD5.HashData(inputBytes);

                var result = Convert.ToHexString(hashBytes);
                if (result.StartsWith(condition))
                    return iteration;

                iteration++;
            }
        }
    }
}
