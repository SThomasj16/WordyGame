using System.Collections.Generic;

namespace Features.Board.Scripts.Domain
{
    public interface IBuildMatrix
    {
        List<char> Execute(string input, int amountOfRowsAndColumns);
    }
}