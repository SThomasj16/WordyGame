using Features.Words.Scripts.Domain;

namespace Features.Words.Scripts.Services
{
    public interface IWordService
    {
        Word GetWord(WordAmountOfCharacters amountOfCharacters);
    }
}