using System;
using System.Collections.Generic;
using Features.Board.Scripts.Domain;
using UniRx;

namespace Features.Board.Scripts.Delivery
{
    public interface IBoardView
    {
        IObservable<Unit> OnViewAppear();
        void InstanceLetterItems(List<char> word);
        void SetBoardColumns(int matrix);
        void SetCellSize(int expected);
    }
}