using Features.Words.Scripts.Domain;

namespace Features.Board.Scripts.Domain.Actions
{
    public interface ISaveSelectedMatchWords
    {
        void Execute(Word word);
    }
}