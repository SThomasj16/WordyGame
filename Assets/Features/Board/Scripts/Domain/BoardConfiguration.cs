using UnityEngine;

namespace Features.Board.Scripts.Domain
{
    [CreateAssetMenu(fileName = "WordyGame", menuName = "Board/CreateConfig", order = 1)]

    public class BoardConfiguration : ScriptableObject
    {
        private BoardMatrix _matrixType;
        public void SetMatrix(BoardMatrix matrix) => _matrixType = matrix;
        public BoardMatrix GetMatrix() => _matrixType;
    }
}


