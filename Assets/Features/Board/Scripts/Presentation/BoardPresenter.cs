using System;
using Features.Board.Scripts.Delivery;
using Features.Board.Scripts.Domain;
using Features.Board.Scripts.Providers;
using UniRx;

namespace Features.Board.Scripts.Presentation
{
    public class BoardPresenter
    {
        private readonly IBoardView _view;
        private readonly IBoardConfiguration _boardConfig;
        private readonly CompositeDisposable _disposable;
        public BoardPresenter(IBoardView view, IBoardConfiguration boardConfig)
        {
            _view = view;
            _boardConfig = boardConfig;
            _disposable = new CompositeDisposable();
            SubscribeToViewEvents();
        }

        private void SubscribeToViewEvents()
        {
            _view.OnViewAppear()
                .Do(_ => HandleOnAppear())
                .Subscribe()
                .AddTo(_disposable);
        }

        private void HandleOnAppear()
        {
            PopulateBoard();
        }

        private void PopulateBoard()
        {
            var matrixType = _boardConfig.GetMatrix();
            switch (matrixType)
            {
                case BoardMatrix.FiveByFive:
                    _view.InstanceLetterItems(25);
                    break;
                case BoardMatrix.SixBySix:
                    _view.InstanceLetterItems(36);
                    break;
                case BoardMatrix.SevenBySeven:
                    _view.InstanceLetterItems(49);
                    break;
                case BoardMatrix.EightByEight:
                    _view.InstanceLetterItems(64);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static BoardPresenter Present(IBoardView view) =>
            new(view, BoardProvider.GetBoardConfig());
    }
}