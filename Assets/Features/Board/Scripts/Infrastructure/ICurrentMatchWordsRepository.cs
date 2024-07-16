using System.Collections.Generic;
using Features.Words.Scripts.Domain;

namespace Features.Board.Scripts.Infrastructure
{
    public interface ICurrentMatchWordsRepository
    {
        void Set(List<Word> words);
        List<Word> Get();
        void Clear();
    }
}