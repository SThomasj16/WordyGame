using Features.Words.Scripts.Domain.Actions;
using Features.Words.Scripts.Infrastructure;
using Features.Words.Scripts.Services;
using static Utils.Provider.Scripts.Provider;

namespace Features.Words.Scripts.Providers
{
    public static class WordsProvider
    {
        public static IGetWord GetWordAction() =>
            GetOrInstanciate(() => new GetWord(GetWordService()), "WordsProvider.GetWord");
        
        private static IWordService GetWordService() =>
            GetOrInstanciate(() => new WordService(GetWordsRepository(), GetUsedWordsRepository()),
                "WordsProvider.WordService");
        
        public static IWordsRepository GetWordsRepository() =>
            GetOrInstanciate(() => new WordsRepository(), "WordsProvider.WordsRepository");
        
        private static IUsedWordsRepository GetUsedWordsRepository() =>
            GetOrInstanciate(() => new UsedWordsRepository(), "WordsProvider.GetUsedWordsRepository");
    }
}