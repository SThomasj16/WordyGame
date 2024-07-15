using System;
using Features.Board.Scripts.Providers;
using Features.ResultView.Scripts.Delivery;
using UniRx;

namespace Features.ResultView.Scripts.Presentation
{
    public class ResultViewPresenter
    {
        private readonly IResultView _view;
        private readonly CompositeDisposable _disposable;
        private ResultViewPresenter(IResultView view, IObservable<Unit> onVictoryEvent)
        {
            _view = view;
            _disposable = new CompositeDisposable();
            SubscribeToVictoryEvent(onVictoryEvent);
        }

        private void SubscribeToVictoryEvent(IObservable<Unit> onVictoryEvent)
        {
            onVictoryEvent
                .Do(_ => HandleVictoryEvent())
                .Subscribe()
                .AddTo(_disposable);
        }

        private void HandleVictoryEvent()
        {
            _view.ShowVictoryPopUp();
        }
        
        public static ResultViewPresenter Present(IResultView view)
        {
            return new ResultViewPresenter(view, BoardProvider.GetOnVictoryEvent());
        }

        public void Dispose()
        {
            _disposable?.Clear();
        }
    }
}