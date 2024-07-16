using System;
using UniRx;

namespace Features.Result.Scripts.Delivery
{
    public interface IResultView
    {
        void Show();
        IObservable<Unit> OnNextButtonPressed();
        void Hide();
    }
}
