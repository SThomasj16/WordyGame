using System;
using System.Collections.Generic;
using Features.Board.Scripts.Domain;
using Features.Board.Scripts.Presentation;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Board.Scripts.Delivery
{
    public class BoardView : MonoBehaviour, IBoardView
    {
        [SerializeField] private Transform lettersContainer;
        [SerializeField] private GameObject letterItemPrefab;
        [SerializeField] private GridLayoutGroup layoutGroup;
        
        private readonly ISubject<Unit> _onAppear = new Subject<Unit>();

        private void Awake()
        {
            var presenter = BoardPresenter.Present(this);
        }

        private void Start()
        {
            _onAppear.OnNext(Unit.Default);
        }

        public void InstanceLetterItems(List<char> word)
        {
            foreach (var character in word)
            {
                var letterItem = Instantiate(letterItemPrefab, lettersContainer).GetComponent<LetterItemView>();
                letterItem.Set(character);
            }
        }

        public void SetBoardColumns(int matrix)
        {
            layoutGroup.constraintCount = matrix;
        }

        public void SetCellSize(int expected)
        {
            layoutGroup.cellSize = new Vector2(expected, expected);
        }

        public IObservable<Unit> OnViewAppear() => _onAppear;

    }
}