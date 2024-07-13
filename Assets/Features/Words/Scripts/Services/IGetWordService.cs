using System.Collections.Generic;
using Features.Words.Scripts.Domain;

namespace Features.Words.Scripts.Services
{
    public interface IGetWordService
    {
        List<Word> GetWords(int maxAmountOfCharacters, int amountOfWords);
    }
}