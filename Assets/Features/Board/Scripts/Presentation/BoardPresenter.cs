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
                    _view.SetBoardColumns(5);
                    _view.SetCellSize(120);
                    var fiveCharacterWords = GetWords(WordAmountOfCharacters.Five,5);
                    var matrix = _matrixBuilder.Execute(fiveCharacterWords, 5);
                    SaveWords(fiveCharacterWords);
                    _view.InstanceLetterItems(matrix);
                    break;
                case BoardMatrix.SixBySix:
                    _view.SetBoardColumns(6);
                    _view.SetCellSize(110);
                    var sixCharacterWords = GetWords(WordAmountOfCharacters.Six,6);
                    var sixBySixMatrix = _matrixBuilder.Execute(sixCharacterWords, 6);
                    SaveWords(sixCharacterWords);
                    _view.InstanceLetterItems(sixBySixMatrix);
                    break;
                case BoardMatrix.SevenBySeven:
                    _view.SetBoardColumns(7);
                    _view.SetCellSize(100);
                    var sevenCharacterWords = GetWords(WordAmountOfCharacters.Seven,7);
                    var sevenBySevenMatrix = _matrixBuilder.Execute(sevenCharacterWords, 7);
                    SaveWords(sevenCharacterWords);
                    _view.InstanceLetterItems(sevenBySevenMatrix);
                    break;
                case BoardMatrix.EightByEight:
                    _view.SetBoardColumns(8);
                    _view.SetCellSize(90);
                    var eightCharacterWords = GetWords(WordAmountOfCharacters.Eight,8);
                    var eightByEightMatrix = _matrixBuilder.Execute(eightCharacterWords, 8);
                    SaveWords(eightCharacterWords);
                    _view.InstanceLetterItems(eightByEightMatrix);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SaveWords(List<Word> words)
        {
            _saveCurrentMatchWords.Execute(words);
            for (int i = 0; i < words.Count; i++)
            {
                Debug.Log(words[i].Value);
            }
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