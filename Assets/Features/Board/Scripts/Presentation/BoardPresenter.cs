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
                    ConfigBoard(WordAmountOfCharacters.Five);
                    FillBoardWith(WordAmountOfCharacters.Five, (int)matrixType);
                    break;
                case BoardMatrix.SixBySix:
                    ConfigBoard(WordAmountOfCharacters.Six);
                    FillBoardWith(WordAmountOfCharacters.Six, (int)matrixType);
                    break;
                case BoardMatrix.SevenBySeven:
                    ConfigBoard(WordAmountOfCharacters.Seven);
                    FillBoardWith(WordAmountOfCharacters.Seven, (int)matrixType);
                    break;
                case BoardMatrix.EightByEight:
                    ConfigBoard(WordAmountOfCharacters.Eight);
                    FillBoardWith(WordAmountOfCharacters.Eight,  (int)matrixType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ConfigBoard(WordAmountOfCharacters wordAmountOfCharacters)
        {
            _view.SetBoardColumns((int)wordAmountOfCharacters);
            _view.SetCellSize(GetCellSizeFor(wordAmountOfCharacters));
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
        
        private int GetCellSizeFor(WordAmountOfCharacters wordAmountOfCharacters)
        {
            switch (wordAmountOfCharacters)
            {
                case WordAmountOfCharacters.Three:
                case WordAmountOfCharacters.Four:
                case WordAmountOfCharacters.Five:
                    return 120;
                case WordAmountOfCharacters.Six:
                    return 110;
                case WordAmountOfCharacters.Seven:
                    return 100;
                case WordAmountOfCharacters.Eight:
                    return 90;
                default:
                    throw new ArgumentOutOfRangeException(nameof(wordAmountOfCharacters), wordAmountOfCharacters, null);
            }
        }

        public static BoardPresenter Present(IBoardView view) =>
            new(view, BoardProvider.GetBoardConfig(), WordsProvider.GetWordAction(), BoardProvider.GetMatrixBuilder(),
                BoardProvider.GetSaveCurrentMatchWordsAction());
    }
}