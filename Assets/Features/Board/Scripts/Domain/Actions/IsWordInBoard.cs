using Features.Board.Scripts.Infrastructure;
using Features.Words.Scripts.Domain;

namespace Features.Board.Scripts.Domain.Actions
{
    public class IsWordInBoard : IIsWordInBoard
    {
        private readonly ICurrentMatchWordsRepository _currentMatchWordsRepository;

        public IsWordInBoard(ICurrentMatchWordsRepository currentMatchWordsRepository)
        {
            _currentMatchWordsRepository = currentMatchWordsRepository;
        }

        public bool Execute(Word word)
        {
            var words = _currentMatchWordsRepository.Get();
            return words.Contains(word);
        }
    }
}