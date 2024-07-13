using System.Collections.Generic;
using Features.Words.Scripts.Domain;

namespace Features.Words.Scripts.Infrastructure
{
    public interface IUsedWordsRepository
    {
        void Mark(Word word);
        void Clear();
        List<Word> Get();
    }
}