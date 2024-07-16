using System;
using UniRx;

namespace Features.StartScreen.Scripts.Delivery
{
    public interface IStartScreenView
    {
        IObservable<int> OnDropdownValueChanged();
        void ShowPlayButton();
        IObservable<Unit> OnStartButtonPressed();
        void Hide();
        void DisplayBoard();
    }
}
