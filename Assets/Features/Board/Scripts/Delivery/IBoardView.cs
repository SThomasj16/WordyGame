using System;
using System.Collections.Generic;
using Features.Board.Scripts.Domain;
using UniRx;

namespace Features.Board.Scripts.Delivery
{
    public interface IBoardView
    {
        IObservable<Unit> OnViewAppear();
        void InstanceLetterItems(List<char> amount);
        void SetBoardColumns(int matrix);
    }
}