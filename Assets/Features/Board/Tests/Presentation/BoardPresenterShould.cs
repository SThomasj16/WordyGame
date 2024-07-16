using System.Collections.Generic;
using Features.Board.Scripts.Delivery;
using Features.Board.Scripts.Domain;
using Features.Board.Scripts.Domain.Actions;
using Features.Board.Scripts.Presentation;
using Features.Words.Scripts.Domain.Actions;
using NSubstitute;
using NUnit.Framework;
using UniRx;

namespace Features.Board.Tests.Presentation
{
    public class BoardPresenterShould
    {
        private BoardPresenter _presenter;
        private IBoardView _view;
        private IBoardConfiguration _config;
        private IGetWord _getWordAction;
        private IBuildMatrix _buildMatrix;
        private ISaveCurrentMatchWords _saveCurrentMatchWordsAction;
        private IIsWordInBoard _isWordInBoard;
        private ISaveSelectedMatchWords _saveSelectedMatchWordsAction;
        private ICheckVictoryStatus _checkVictoryStatusAction;
        private ISubject<Unit> _onAppear;
        private ISubject<Unit> _onMouseUp;
        private ISubject<Unit> _onVictory;
        private ISubject<Unit> _onResetBoard;

        [SetUp]
        public void Setup()
        {
            _onAppear = new Subject<Unit>();
            _onMouseUp = new Subject<Unit>();
            _onVictory = new Subject<Unit>();
            _onResetBoard = new Subject<Unit>();
            _config = Substitute.For<IBoardConfiguration>();
            _view = Substitute.For<IBoardView>();
            _getWordAction = Substitute.For<IGetWord>();
            _buildMatrix = Substitute.For<IBuildMatrix>();
            _saveCurrentMatchWordsAction = Substitute.For<ISaveCurrentMatchWords>();
            _isWordInBoard = Substitute.For<IIsWordInBoard>();
            _saveSelectedMatchWordsAction = Substitute.For<ISaveSelectedMatchWords>();
            _checkVictoryStatusAction = Substitute.For<ICheckVictoryStatus>();
            _view.OnViewAppear().Returns(_onAppear);
            _view.OnViewMouseUp().Returns(_onMouseUp);
            _presenter = new BoardPresenter(_view, _config,_getWordAction,_buildMatrix,_saveCurrentMatchWordsAction,
                _isWordInBoard,_saveSelectedMatchWordsAction,_checkVictoryStatusAction,_onVictory,_onResetBoard);
        }

        [TestCase(BoardMatrix.FiveByFive)]
        [TestCase(BoardMatrix.SixBySix)]
        [TestCase(BoardMatrix.SevenBySeven)]
        [TestCase(BoardMatrix.EightByEight)]
        public void PopulateBoardOnViewAppear(BoardMatrix matrix)
        {
            GivenABoardMatrixConfig(matrix);
            WhenViewAppears();
            ThenPopulateBoard();
        }
        
        
        [TestCase(BoardMatrix.FiveByFive,5)]
        [TestCase(BoardMatrix.SixBySix,6)]
        [TestCase(BoardMatrix.SevenBySeven,7)]
        [TestCase(BoardMatrix.EightByEight,8)]
        public void SetBoardColumnsOnViewAppear(BoardMatrix matrix, int expected)
        {
            GivenABoardMatrixConfig(matrix);
            WhenViewAppears();
            ThenSetBoardColumns(expected);
        }
        
        [TestCase(BoardMatrix.FiveByFive,120)]
        [TestCase(BoardMatrix.SixBySix,110)]
        [TestCase(BoardMatrix.SevenBySeven,100)]
        [TestCase(BoardMatrix.EightByEight,90)]
        public void SetCellSizeOnViewAppear(BoardMatrix matrix, int expected)
        {
            GivenABoardMatrixConfig(matrix);
            WhenViewAppears();
            ThenSetCellSize(expected);
        }

        private void GivenABoardMatrixConfig(BoardMatrix boardMatrix)
        {
            _config.GetMatrix().Returns(boardMatrix);
        }
        
        private void WhenViewAppears()
        {
            _onAppear.OnNext(Unit.Default);
        }
        
        private void ThenPopulateBoard()
        {
            _view.Received(1).InstanceLetterItems(Arg.Any<List<char>>());
        }
        
        private void ThenSetBoardColumns(int amountOfColumns)
        {
            _view.Received(1).SetBoardColumns(amountOfColumns);
        }
        
        private void ThenSetCellSize(int expected)
        {
            _view.Received(1).SetCellSize(expected);
        }
    }
}
