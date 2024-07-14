using System.Collections.Generic;
using Features.Words.Scripts.Domain;

namespace Features.Words.Scripts.Infrastructure
{
    public class UsedWordsRepository : IUsedWordsRepository
    {
        private readonly List<Word> _usedWords = new List<Word>();
        public void Mark(Word word)
        {
            _usedWords.Add(word);
        }

        public void Clear()
        {
            _usedWords.Clear();
        }

        public List<Word> Get() => _usedWords;
    }
}