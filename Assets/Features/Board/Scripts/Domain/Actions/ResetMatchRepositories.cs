using Features.Board.Scripts.Infrastructure;

namespace Features.Board.Scripts.Domain.Actions
{
    public class ResetMatchRepositories : IResetMatchRepositories
    {
        private readonly ICurrentMatchWordsRepository _currentMatchWordsRepository;
        private readonly ICurrentMatchSelectedWordsRepository _currentMatchSelectedWordsRepository;

        public ResetMatchRepositories(ICurrentMatchWordsRepository currentMatchWordsRepository,
            ICurrentMatchSelectedWordsRepository currentMatchSelectedWordsRepository)
        {
            _currentMatchWordsRepository = currentMatchWordsRepository;
            _currentMatchSelectedWordsRepository = currentMatchSelectedWordsRepository;
        }

        public void Execute()
        {
            _currentMatchWordsRepository.Clear();
            _currentMatchSelectedWordsRepository.Clear();
        }
    }
}