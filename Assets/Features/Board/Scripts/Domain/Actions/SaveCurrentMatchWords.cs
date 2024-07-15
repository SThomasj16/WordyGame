using System.Collections.Generic;
using Features.Board.Scripts.Infrastructure;
using Features.Words.Scripts.Domain;

namespace Features.Board.Scripts.Domain.Actions
{
    public class SaveCurrentMatchWords : ISaveCurrentMatchWords
    {
        private readonly ICurrentMatchWordsRepository _repository;

        public SaveCurrentMatchWords(ICurrentMatchWordsRepository repository)
        {
            _repository = repository;
        }

        public void Execute(List<Word> words)
        {
            _repository.Set(words);
        }
    }
}