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
using UnityEngine;

namespace Features.Board.Scripts.Presentation
{
    public class BoardPresenter
    {
        private readonly IBoardView _view;
        private readonly IBoardConfiguration _boardConfig;
        private readonly IGetWord _getWord;
        private readonly IBuildMatrix _matrixBuilder;
        private readonly ISaveCurrentMatchWords _saveCurrentMatchWords;
        private readonly IIsWordInBoard _isWordInBoard;
        private readonly CompositeDisposable _disposable;
        private readonly List<char> _charactersSelected = new();
        public BoardPresenter(IBoardView view, IBoardConfiguration boardConfig, IGetWord getWord,
            IBuildMatrix matrixBuilder, ISaveCurrentMatchWords saveCurrentMatchWords,
            IIsWordInBoard isWordInBoard)
        {
            _view = view;
            _boardConfig = boardConfig;
            _getWord = getWord;
            _matrixBuilder = matrixBuilder;
            _saveCurrentMatchWords = saveCurrentMatchWords;
            _isWordInBoard = isWordInBoard;
            _disposable = new CompositeDisposable();
            SubscribeToViewEvents();
        }

        private void SubscribeToViewEvents()
        {
            _view.OnViewAppear()
                .Do(_ => HandleOnAppear())
                .Subscribe()
                .AddTo(_disposable);

            _view.OnLetterSelected()
                .Do(HandleLetterSelected)
                .Subscribe()
                .AddTo(_disposable);
            
            _view.OnMouseUp()
                .Do(_ => HandleMouseUp())
                .Subscribe()
                .AddTo(_disposable);        }

        private void HandleMouseUp()
        {
            var selectedWord = new Word(new string(_charactersSelected.ToArray()), WordTheme.Animals);
            if(_isWordInBoard.Execute(selectedWord))
                Debug.Log("EXITO");
            _charactersSelected.Clear();
        }

        private void HandleLetterSelected(char character)
        {
            _charactersSelected.Add(character);
            Debug.Log(character);
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
                    FillBoardWith(WordAmountOfCharacters.Five, (int)WordAmountOfCharacters.Five);
                    break;
                case BoardMatrix.SixBySix:
                    ConfigBoard(WordAmountOfCharacters.Six);
                    FillBoardWith(WordAmountOfCharacters.Six, (int)WordAmountOfCharacters.Six);
                    break;
                case BoardMatrix.SevenBySeven:
                    ConfigBoard(WordAmountOfCharacters.Seven);
                    FillBoardWith(WordAmountOfCharacters.Seven, (int)WordAmountOfCharacters.Seven);
                    break;
                case BoardMatrix.EightByEight:
                    ConfigBoard(WordAmountOfCharacters.Eight);
                    FillBoardWith(WordAmountOfCharacters.Eight,  (int)WordAmountOfCharacters.Eight);
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
            var selectedWords = GetWords(wordAmountOfCharacters,amountOfWords);
            var matrix = _matrixBuilder.Execute(selectedWords, (int)wordAmountOfCharacters);
            SaveWords(selectedWords);
            DebugSelectedWords(selectedWords);
            _view.InstanceLetterItems(matrix);
        }

        private void DebugSelectedWords(List<Word> selectedWords)
        {
            Debug.Log("Selected words are:");
            foreach (var word in selectedWords)
            {
                Debug.Log(word.Value);
            }
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
                BoardProvider.GetSaveCurrentMatchWordsAction(), BoardProvider.GetIsWordInBoardAction());
    }
}