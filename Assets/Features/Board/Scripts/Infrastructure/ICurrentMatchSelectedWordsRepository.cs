using System.Collections.Generic;
using Features.Words.Scripts.Domain;

namespace Features.Board.Scripts.Infrastructure
{
    public interface ICurrentMatchSelectedWordsRepository
    {
        void Add(Word word);
        List<Word> Get();
        void Clear();
    }
}