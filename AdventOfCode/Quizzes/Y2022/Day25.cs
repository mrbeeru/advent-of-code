using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2022
{
    [Aoc(year: 2022, day: 25)]
    public class Day25(IInputProvider inputProvider) : IPartOne<string>
    {
        public string Part1()
        {
            var input = inputProvider.GetInput();
            var sum = input.Aggregate(0L, (x, next) => x + SnafuToDecimal(next));
            return DecimalToSnafu(sum);
        }

        public long SnafuToDecimal(string snafu)
        {
            var dec = 0L;

            for (int i = 0; i < snafu.Length; i++)
            {
                var digit = snafu[snafu.Length - 1 - i] switch
                {
                    '=' => -2,
                    '-' => -1,
                    '0' => 0,
                    '1' => 1,
                    '2' => 2,
                    _ => throw new Exception("Illegal snafu digit.")
                };

                dec += digit * (long)Math.Pow(5, i);
            }

            return dec;
        }

        public string DecimalToSnafu(long dec)
        {
            var snafu = "";
            int carry = 0;

            while (dec > 0)
            {
                var mod = (dec % 5);
                mod += carry;
                dec /= 5;

                var snafuDigit = mod switch
                {
                    0 => '0',
                    1 => '1',
                    2 => '2',
                    3 => '=',
                    4 => '-',
                    5 => '0'
                };

                snafu += snafuDigit;
                carry = mod >= 3 ? 1 : 0;
            }

            return string.Join("", snafu.Reverse());
        }
    }
}
