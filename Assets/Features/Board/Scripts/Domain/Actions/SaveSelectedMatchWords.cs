using Features.Board.Scripts.Infrastructure;
using Features.Words.Scripts.Domain;

namespace Features.Board.Scripts.Domain.Actions
{
    public class SaveSelectedMatchWords : ISaveSelectedMatchWords
    {
        private readonly ICurrentMatchSelectedWordsRepository _currentMatchSelectedWordsRepository;

        public SaveSelectedMatchWords(ICurrentMatchSelectedWordsRepository currentMatchSelectedWordsRepository)
        {
            _currentMatchSelectedWordsRepository = currentMatchSelectedWordsRepository;
        }

        public void Execute(Word word)
        {
            _currentMatchSelectedWordsRepository.Add(word);
        }
    }
}