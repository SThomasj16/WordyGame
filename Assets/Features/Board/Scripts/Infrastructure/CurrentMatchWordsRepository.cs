using System.Collections.Generic;
using Features.Words.Scripts.Domain;

namespace Features.Board.Scripts.Infrastructure
{
    public class CurrentMatchWordsRepository : ICurrentMatchWordsRepository
    {
        private List<Word> _words = new();
        
        public void Set(List<Word> words)
        {
            _words = words;
        }

        public List<Word> Get() => _words;
        public void Clear()
        {
            _words.Clear();
        }
    }
}