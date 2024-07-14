using System;
using Features.Board.Scripts.Domain;
using UniRx;

namespace Features.Board.Scripts.Delivery
{
    public interface IBoardView
    {
        IObservable<Unit> OnViewAppear();
        void InstanceLetterItems(int amount);
    }
}