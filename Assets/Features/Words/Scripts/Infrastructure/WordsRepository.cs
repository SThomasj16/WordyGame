using System.Collections.Generic;
using Features.Words.Scripts.Domain;

namespace Features.Words.Scripts.Infrastructure
{
    public class WordsRepository : IWordsRepository
    {
        private List<Word> saveWords = new();
        public List<Word> Get() => saveWords;

        public void Set(List<Word> words)
        {
            saveWords = words;
        }
    }
}