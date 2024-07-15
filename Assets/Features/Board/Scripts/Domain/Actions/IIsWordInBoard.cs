using Features.Words.Scripts.Domain;

namespace Features.Board.Scripts.Domain.Actions
{
    public interface IIsWordInBoard
    {
       bool Execute(Word word);
    }
}