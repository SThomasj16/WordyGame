using System;
using System.Collections.Generic;
using System.Linq;
using Features.Board.Scripts.Delivery;
using Features.Board.Scripts.Domain;
using Features.Board.Scripts.Providers;
using Features.Words.Scripts.Domain;
using Features.Words.Scripts.Domain.Actions;
using Features.Words.Scripts.Providers;
using UniRx;
using Random = UnityEngine.Random;

namespace Features.Board.Scripts.Presentation
{
    public class BoardPresenter
    {
        private readonly IBoardView _view;
        private readonly IBoardConfiguration _boardConfig;
        private readonly IGetWord _getWord;
        private readonly IBuildMatrix _matrixBuilder;
        private readonly CompositeDisposable _disposable;
        public BoardPresenter(IBoardView view, IBoardConfiguration boardConfig, IGetWord getWord,
            IBuildMatrix matrixBuilder)
        {
            _view = view;
            _boardConfig = boardConfig;
            _getWord = getWord;
            _matrixBuilder = matrixBuilder;
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
                    var words = GetWords(WordAmountOfCharacters.Five,5);
                    var matrix = _matrixBuilder.Execute(words, 5);
                    _view.InstanceLetterItems(matrix);
                    break;
                case BoardMatrix.SixBySix:
                    //_view.InstanceLetterItems(36);
                    break;
                case BoardMatrix.SevenBySeven:
                    //_view.InstanceLetterItems(49);
                    break;
                case BoardMatrix.EightByEight:
                    //_view.InstanceLetterItems(64);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private List<Word> GetWords(WordAmountOfCharacters amountOfCharactersLimit, int amount)
        {
            var words = new List<Word>();
            words.Add(_getWord.Execute(WordAmountOfCharacters.Five));
            for (var i = 1; i < amount; i++)
            {
                words.Add(_getWord.Execute(WordAmountOfCharacters.Three));
            }
            return words;
        }

        public static BoardPresenter Present(IBoardView view) =>
            new(view, BoardProvider.GetBoardConfig(), WordsProvider.GetWordAction(), BoardProvider.GetMatrixBuilder());
    }
}