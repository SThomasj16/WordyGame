using System;
using System.Linq;
using Features.Words.Scripts.Domain;
using Features.Words.Scripts.Infrastructure;

namespace Features.Words.Scripts.Services
{
    public class WordService : IWordService
    {
        private readonly IWordsRepository _wordsRepository;
        private readonly IUsedWordsRepository _usedWordsRepository;

        public WordService(IWordsRepository wordsRepository, IUsedWordsRepository usedWordsRepository)
        {
            _wordsRepository = wordsRepository;
            _usedWordsRepository = usedWordsRepository;
        }

        public void SetAsUsed(Word word)
        {
            _usedWordsRepository.Mark(word);
        }

        public Word GetWord(WordAmountOfCharacters amountOfCharacters)
        {
            var word = GetNewWord(amountOfCharacters);
            
            if (word.Value == null)
            {
                _usedWordsRepository.Clear();
                word = GetNewWord(amountOfCharacters);
            }

            _usedWordsRepository.Mark(word);
            return word;
        }

        private Word GetNewWord(WordAmountOfCharacters amountOfCharacters)
        {
            return _wordsRepository.Get()
                .Where(FilterUsedWords())
                .FirstOrDefault(IsWordWithCorrectAmountOfCharacters(amountOfCharacters));
        }

        private static Func<Word, bool> IsWordWithCorrectAmountOfCharacters(WordAmountOfCharacters amountOfCharacters) => 
            word => word.AmountOfCharacters == (int)amountOfCharacters;

        private Func<Word, bool> FilterUsedWords() => 
            word => !_usedWordsRepository.Get().Contains(word);
        
    }
}