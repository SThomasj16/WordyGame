using System;
using Features.Board.Scripts.Providers;
using Features.Result.Scripts.Delivery;
using UniRx;

namespace Features.Result.Scripts.Presentation
{
    public class ResultViewPresenter
    {
        private readonly IResultView _view;
        private readonly ISubject<Unit> _onResetBoardEvent;
        private readonly CompositeDisposable _disposable;
        private ResultViewPresenter(IResultView view, ISubject<Unit> onVictoryEvent,
            ISubject<Unit> onResetBoardEvent)
        {
            _view = view;
            _onResetBoardEvent = onResetBoardEvent;
            _disposable = new CompositeDisposable();
            SubscribeToVictoryEvent(onVictoryEvent);
            SubscribeToViewEvents();
        }

        private void SubscribeToViewEvents()
        {
            _view.OnNextButtonPressed()
                .Do(_ => HandleNextButtonPressed())
                .Subscribe()
                .AddTo(_disposable);
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
            _view.Show();
        }
        
        private void HandleNextButtonPressed()
        {
            _view.Hide();
            _onResetBoardEvent.OnNext(Unit.Default);
        }
        
        public static ResultViewPresenter Present(IResultView view)
        {
            return new ResultViewPresenter(view, BoardProvider.GetOnVictoryEvent(), 
                BoardProvider.GetOnResetBoardEvent());
        }

        public void Dispose()
        {
            _disposable?.Clear();
        }
    }
}