using System.Collections.Generic;
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

        public List<Word> GetWords(int maxAmountOfCharacters) => 
            _wordsRepository.Get().FindAll(word => word.AmountOfCharacters >= maxAmountOfCharacters);
    }
}