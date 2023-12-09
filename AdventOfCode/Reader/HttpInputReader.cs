namespace AdventOfCode.Reader
{
    internal class HttpInputReader : IInputProvider
    {
        /// <summary>
        /// Read the AOC token from this location. 
        /// </summary>
        readonly string secretsFilePath = "token.txt";

        /// <summary>
        /// Used to cache quiz inputs.
        /// </summary>
        readonly QuizzCache quizzCache = new();
        readonly CacheKey cacheKey;

        public HttpInputReader(int year, int day)
        {
            try
            {
                var token = File.ReadAllText(secretsFilePath);
                cacheKey = new CacheKey(year, day, token);
            }
            catch (Exception ex)
            {
                throw new AggregateException("Could not read token.", ex);
            }
        }

        public IList<string> GetInput()
        {
            if (quizzCache.TryGet(cacheKey, out string cachedValue))
            {
                return cachedValue.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            };

            using HttpClient client = new();
            client.DefaultRequestHeaders.Add("cookie", cacheKey.Token);

            var response = client.GetAsync($"https://adventofcode.com/{cacheKey.Year}/day/{cacheKey.Day}/input").Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result.TrimEnd();
                quizzCache.Set(cacheKey, data);
                return data.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            }

            // Handle the error or throw an exception.
            throw new HttpRequestException($"Failed to retrieve data. Status code: {response.StatusCode}");
        }
    }
}
