namespace Features.Board.Scripts.Domain
{
    public interface IBoardConfiguration
    {
        public void SetMatrix(BoardMatrix matrix);
        public BoardMatrix GetMatrix();
    }
}