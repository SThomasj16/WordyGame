using System.Collections.Generic;
using Features.Words.Scripts.Domain;

namespace Features.Board.Scripts.Infrastructure
{
    public class CurrentMatchSelectedWordsRepository : ICurrentMatchSelectedWordsRepository
    {
        private readonly List<Word> _words = new List<Word>();
        public void Add(Word word)
        {
            _words.Add(word);
        }

        public List<Word> Get() => _words;
        public void Clear()
        {
            _words.Clear();
        }
    }
}