using System.Collections.Generic;
using Features.Words.Scripts.Domain;

namespace Features.Words.Scripts.Infrastructure
{
    public interface IWordsRepository
    {
        List<Word> Get();
    }
}