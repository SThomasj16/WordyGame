using System.Collections.Generic;
using Newtonsoft.Json;

namespace Features.Words.Scripts.Domain
{
    public class WordsByLetterCount
    {
        [JsonProperty("3")]
        public List<string> threeLetterWords;

        [JsonProperty("4")]
        public List<string> fourLetterWords;

        [JsonProperty("5")]
        public List<string> fiveLetterWords;

        [JsonProperty("6")]
        public List<string> sixLetterWords;

        [JsonProperty("7")]
        public List<string> sevenLetterWords;

        [JsonProperty("8")]
        public List<string> eightLetterWords;
    }
}