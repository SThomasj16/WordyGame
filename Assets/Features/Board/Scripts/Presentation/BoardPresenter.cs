using System;
using System.Collections.Generic;
using Features.Board.Scripts.Delivery;
using Features.Board.Scripts.Domain;
using Features.Board.Scripts.Domain.Actions;
using Features.Board.Scripts.Providers;
using Features.Words.Scripts.Domain;
using Features.Words.Scripts.Domain.Actions;
using Features.Words.Scripts.Providers;
using UniRx;

namespace Features.Board.Scripts.Presentation
{
    public class BoardPresenter
    {
        private readonly IBoardView _view;
        private readonly IBoardConfiguration _boardConfig;
        private readonly IGetWord _getWord;
        private readonly IBuildMatrix _matrixBuilder;
        private readonly ISaveCurrentMatchWords _saveCurrentMatchWords;
        private readonly CompositeDisposable _disposable;
        public BoardPresenter(IBoardView view, IBoardConfiguration boardConfig, IGetWord getWord,
            IBuildMatrix matrixBuilder, ISaveCurrentMatchWords saveCurrentMatchWords)
        {
            _view = view;
            _boardConfig = boardConfig;
            _getWord = getWord;
            _matrixBuilder = matrixBuilder;
            _saveCurrentMatchWords = saveCurrentMatchWords;
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
                    _view.SetBoardColumns((int)WordAmountOfCharacters.Five);
                    _view.SetCellSize(120);
                    FillBoardWith(WordAmountOfCharacters.Five, 5);
                    break;
                case BoardMatrix.SixBySix:
                    _view.SetBoardColumns((int)WordAmountOfCharacters.Six);
                    _view.SetCellSize(110);
                    FillBoardWith(WordAmountOfCharacters.Six, 6);
                    break;
                case BoardMatrix.SevenBySeven:
                    _view.SetBoardColumns((int)WordAmountOfCharacters.Seven);
                    _view.SetCellSize(100);
                    FillBoardWith(WordAmountOfCharacters.Seven, 7);
                    break;
                case BoardMatrix.EightByEight:
                    _view.SetBoardColumns((int)WordAmountOfCharacters.Eight);
                    _view.SetCellSize(90);
                    FillBoardWith(WordAmountOfCharacters.Eight, 8);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FillBoardWith(WordAmountOfCharacters wordAmountOfCharacters, int amountOfWords)
        {
            var fiveCharacterWords = GetWords(wordAmountOfCharacters,amountOfWords);
            var matrix = _matrixBuilder.Execute(fiveCharacterWords, (int)wordAmountOfCharacters);
            SaveWords(fiveCharacterWords);
            _view.InstanceLetterItems(matrix);
        }

        private void SaveWords(List<Word> words)
        {
            _saveCurrentMatchWords.Execute(words);
        }

        private List<Word> GetWords(WordAmountOfCharacters amountOfCharactersLimit, int amount)
        {
            var words = new List<Word> {_getWord.Execute(amountOfCharactersLimit)};
            for (var i = 1; i < amount; i++) 
                words.Add(_getWord.Execute(WordAmountOfCharacters.Three));
            return words;
        }

        public static BoardPresenter Present(IBoardView view) =>
            new(view, BoardProvider.GetBoardConfig(), WordsProvider.GetWordAction(), BoardProvider.GetMatrixBuilder(),
                BoardProvider.GetSaveCurrentMatchWordsAction());
    }
}