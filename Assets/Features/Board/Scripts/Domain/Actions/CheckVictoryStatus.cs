using System.Collections.Generic;
using Features.Board.Scripts.Infrastructure;
using Features.Words.Scripts.Domain;

namespace Features.Board.Scripts.Domain.Actions
{
    public class CheckVictoryStatus : ICheckVictoryStatus
    {
        private readonly ICurrentMatchWordsRepository _currentMatchWordsRepository;
        private readonly ICurrentMatchSelectedWordsRepository _currentMatchSelectedWordsRepository;

        public CheckVictoryStatus(ICurrentMatchWordsRepository currentMatchWordsRepository,
            ICurrentMatchSelectedWordsRepository currentMatchSelectedWordsRepository)
        {
            _currentMatchWordsRepository = currentMatchWordsRepository;
            _currentMatchSelectedWordsRepository = currentMatchSelectedWordsRepository;
        }

        public bool Execute()
        {
            var setWordsInGame = new HashSet<Word>(_currentMatchWordsRepository.Get());
            var setWordsSelected = new HashSet<Word>(_currentMatchSelectedWordsRepository.Get());
            return setWordsInGame.SetEquals(setWordsSelected);
        }
    }
}