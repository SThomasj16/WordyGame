using Features.Board.Scripts.Domain;
using Features.Board.Scripts.Providers;
using Features.StartScreen.Scripts.Delivery;
using UniRx;

namespace Features.StartScreen.Scripts.Presentation
{
    public class StartScreenPresenter
    {
        private readonly IStartScreenView _view;
        private readonly BoardConfiguration _boardConfiguration;
        private readonly CompositeDisposable _disposable;

        private StartScreenPresenter(IStartScreenView view, BoardConfiguration boardConfiguration)
        {
            _view = view;
            _boardConfiguration = boardConfiguration;
            _disposable = new CompositeDisposable();
            SubscribeToViewEvents();
        }

        private void SubscribeToViewEvents()
        {
            _view.OnDropdownValueChanged()
                .Do(HandleDropdownValueChanged)
                .Subscribe()
                .AddTo(_disposable);

            _view.OnStartButtonPressed()
                .Do(_ => HandleStartButtonPressed())
                .Subscribe()
                .AddTo(_disposable);
        }

        private void HandleStartButtonPressed()
        {
            _view.Hide();
            _view.DisplayBoard();
        }

        private void HandleDropdownValueChanged(int value)
        {
            SetBoardConfiguration(value);
            _view.ShowPlayButton();
        }

        private void SetBoardConfiguration(int value)
        {
            switch (value)
            {
                case 0:
                    _boardConfiguration.SetMatrix(BoardMatrix.FiveByFive);
                    break;
                case 1:
                    _boardConfiguration.SetMatrix(BoardMatrix.SixBySix);
                    break;
                case 2:
                    _boardConfiguration.SetMatrix(BoardMatrix.SevenBySeven);
                    break;
                case 3:
                    _boardConfiguration.SetMatrix(BoardMatrix.EightByEight);
                    break;
            }
        }

        public static StartScreenPresenter Present(IStartScreenView view) => 
            new(view, BoardProvider.GetBoardConfig());

        public void Dispose()
        {
            _disposable?.Clear();
        }
    }
}