using Features.Board.Scripts.Delivery;
using Features.Board.Scripts.Domain;
using Features.Board.Scripts.Presentation;
using NSubstitute;
using NUnit.Framework;
using UniRx;

namespace Features.Board.Tests.Presentation
{
    public class BoardPresenterShould
    {
        private BoardPresenter _presenter;
        private IBoardView _view;
        private ISubject<Unit> _onAppear;

        [SetUp]
        public void Setup()
        {
            _onAppear = new Subject<Unit>();
            _view = Substitute.For<IBoardView>();
            _view.OnViewAppear().Returns(_onAppear);
            _presenter = new BoardPresenter(_view);
        }

        [TestCase(BoardMatrix.FiveByFive, 25)]
        [TestCase(BoardMatrix.SixBySix, 36)]
        [TestCase(BoardMatrix.SevenBySeven,49)]
        [TestCase(BoardMatrix.EightByEight, 64)]
        public void PopulateBoardOnViewAppear(BoardMatrix matrix, int expectedAmountOfItems)
        {
            GivenABoardMatrixConfig(matrix);
            WhenViewAppears();
            ThenPopulateBoardWith(expectedAmountOfItems);
        }
        
        private void GivenABoardMatrixConfig(BoardMatrix boardMatrix)
        {
            _view.GetMatrixType().Returns(boardMatrix);
        }
        
        private void WhenViewAppears()
        {
            _onAppear.OnNext(Unit.Default);
        }
        
        private void ThenPopulateBoardWith(int expectedAmountOfItems)
        {
            _view.Received(1).InstanceLetterItems(expectedAmountOfItems);
        }
    }
}
