namespace Features.Words.Scripts.Domain.Actions
{
    public interface IGetWord
    {
        Word Execute(WordAmountOfCharacters amountOfCharacters);
    }
}