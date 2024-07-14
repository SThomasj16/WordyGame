using System.Collections.Generic;
using Features.Words.Scripts.Domain;

namespace Features.Board.Scripts.Domain
{
    public interface IBuildMatrix
    {
        List<char> Execute(List<Word> input, int amountOfRowsAndColumns);
    }
}