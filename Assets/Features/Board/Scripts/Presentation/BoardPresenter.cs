﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ISaveSelectedMatchWords _saveSelectedMatchWords;
        private readonly ICheckVictoryStatus _checkVictoryStatus;
        private readonly ISubject<Unit> _onVictoryEvent;
        private readonly IResetMatchRepositories _resetMatchRepositories;
        private readonly CompositeDisposable _disposable;
        private readonly List<LetterItemView> _wordItemsSelected = new();

        public BoardPresenter(IBoardView view, IBoardConfiguration boardConfig, IGetWord getWord,
            IBuildMatrix matrixBuilder, ISaveCurrentMatchWords saveCurrentMatchWords,
            IIsWordInBoard isWordInBoard, ISaveSelectedMatchWords saveSelectedMatchWords,
            ICheckVictoryStatus checkVictoryStatus, ISubject<Unit> onVictoryEvent, IObservable<Unit> onResetBoardEvent,
            IResetMatchRepositories resetMatchRepositories)
        {
            _view = view;
            _boardConfig = boardConfig;
            _getWord = getWord;
            _matrixBuilder = matrixBuilder;
            _saveCurrentMatchWords = saveCurrentMatchWords;
            _isWordInBoard = isWordInBoard;
            _saveSelectedMatchWords = saveSelectedMatchWords;
            _checkVictoryStatus = checkVictoryStatus;
            _onVictoryEvent = onVictoryEvent;
            _resetMatchRepositories = resetMatchRepositories;
            _disposable = new CompositeDisposable();
            SubscribeToViewEvents();
            SubscribeToResetBoardEvent(onResetBoardEvent);
        }

        private void SubscribeToResetBoardEvent(IObservable<Unit> onResetBoardEvent)
        {
            onResetBoardEvent
                .Do(_ => HandleBoardReset())
                .Subscribe()
                .AddTo(_disposable);
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

            _view.OnViewMouseUp()
                .Do(_ => HandleMouseUp())
                .Subscribe()
                .AddTo(_disposable);
        }

        private void HandleBoardReset()
        {
            _view.ClearBoard();
            _resetMatchRepositories.Execute();
            PopulateBoard();
        }

        private void HandleMouseUp()
        {
            if (IsAVictory()) return;
            var word = _wordItemsSelected.Select(word => word.GetLetter()).ToArray();
            var selectedWord = new Word(new string(word));
            Array.Reverse(word);
            var invertedWord = new Word(new string(word));
            if (_isWordInBoard.Execute(selectedWord))
            {
                _wordItemsSelected.ForEach(item => item.Lock());
                _saveSelectedMatchWords.Execute(selectedWord);
            }
            else if (_isWordInBoard.Execute(invertedWord))
            {
                _wordItemsSelected.ForEach(item => item.Lock());
                _saveSelectedMatchWords.Execute(invertedWord);
            }
            else
                _wordItemsSelected.ForEach(item => item.Deselect());

            _wordItemsSelected.Clear();
            if (IsAVictory())
                _onVictoryEvent.OnNext(Unit.Default);
        }

        private bool IsAVictory() => _checkVictoryStatus.Execute();

        private void HandleLetterSelected(LetterItemView letterItem)
        {
            if (IsAVictory()) return;

            _wordItemsSelected.Add(letterItem);
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
                    FillBoardWithWords(WordAmountOfCharacters.Five);
                    break;
                case BoardMatrix.SixBySix:
                    ConfigBoard(WordAmountOfCharacters.Six);
                    FillBoardWithWords(WordAmountOfCharacters.Six);
                    break;
                case BoardMatrix.SevenBySeven:
                    ConfigBoard(WordAmountOfCharacters.Seven);
                    FillBoardWithWords(WordAmountOfCharacters.Seven);
                    break;
                case BoardMatrix.EightByEight:
                    ConfigBoard(WordAmountOfCharacters.Eight);
                    FillBoardWithWords(WordAmountOfCharacters.Eight);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ConfigBoard(WordAmountOfCharacters wordAmountOfCharacters)
        {
            _view.SetBoardColumns((int) wordAmountOfCharacters);
            _view.SetCellSize(GetCellSizeFor(wordAmountOfCharacters));
        }

        private void FillBoardWithWords(WordAmountOfCharacters wordAmountOfCharacters)
        {
            var selectedWords = GetWords(wordAmountOfCharacters, (int) wordAmountOfCharacters);
            var matrix = _matrixBuilder.Execute(selectedWords, (int) wordAmountOfCharacters);
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
                    return 180;
                case WordAmountOfCharacters.Six:
                    return 160;
                case WordAmountOfCharacters.Seven:
                    return 140;
                case WordAmountOfCharacters.Eight:
                    return 120;
                default:
                    throw new ArgumentOutOfRangeException(nameof(wordAmountOfCharacters), wordAmountOfCharacters, null);
            }
        }

        public static BoardPresenter Present(IBoardView view) =>
            new(view, BoardProvider.GetBoardConfig(), WordsProvider.GetWordAction(),
                BoardProvider.GetMatrixBuilder(), BoardProvider.GetSaveCurrentMatchWordsAction(),
                BoardProvider.GetIsWordInBoardAction(), BoardProvider.GetSaveSelectedMatchWords(),
                BoardProvider.GetCheckVictoryStatusAction(), BoardProvider.GetOnVictoryEvent(),
                BoardProvider.GetOnResetBoardEvent(), BoardProvider.GetResetMatchRepositoriesAction());

        public void Dispose()
        {
            _disposable?.Clear();
        }
    }
}