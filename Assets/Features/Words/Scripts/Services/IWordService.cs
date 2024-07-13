using System.Collections.Generic;
using Features.Words.Scripts.Domain;

namespace Features.Words.Scripts.Services
{
    public interface IWordService
    {
        List<Word> GetWords(int maxAmountOfCharacters);
    }
}