using Features.Words.Scripts.Infrastructure;
using static Utils.Provider.Scripts.Provider;

namespace Features.Words.Scripts.Providers
{
    public static class WordsProvider
    {
        public static IWordsRepository GetWordsRepository() =>
            GetOrInstanciate(() => new WordsRepository(), "WordsProvider.WordsRepository");
    }
}