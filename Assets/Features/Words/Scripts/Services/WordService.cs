using System.Linq;
using Features.Words.Scripts.Domain;
using Features.Words.Scripts.Infrastructure;

namespace Features.Words.Scripts.Services
{
    public class WordService : IWordService
    {
        private readonly IWordsRepository _wordsRepository;

        public WordService(IWordsRepository wordsRepository)
        {
            _wordsRepository = wordsRepository;
        }

        public Word GetWord(WordAmountOfCharacters amountOfCharacters) => 
            _wordsRepository.Get().First(word => word.AmountOfCharacters == (int)amountOfCharacters);
    }

}