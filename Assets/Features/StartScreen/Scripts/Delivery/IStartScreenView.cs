using System;

namespace Features.StartScreen.Scripts.Delivery
{
    public interface IStartScreenView
    {
        IObservable<int> OnDropdownValueChanged();
        void ShowPlayButton();
    }
}
