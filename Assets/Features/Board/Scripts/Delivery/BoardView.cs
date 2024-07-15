using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly List<LetterItemView> _createdLetters = new();
        
        private readonly ISubject<Unit> _onAppear = new Subject<Unit>();
        private readonly ISubject<Unit> _onMouseUp = new Subject<Unit>();
        private readonly ISubject<char> _onLetterSelected = new Subject<char>();

        private void Awake()
        {
            var presenter = BoardPresenter.Present(this);
        }

        private void Start()
        {
            _onAppear.OnNext(Unit.Default);
        }

        private void Update()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (Input.touchCount == 0)
                _onMouseUp.OnNext(Unit.Default);
#elif  UNITY_EDITOR
            if (Input.GetKeyUp(KeyCode.Mouse0)) 
                _onMouseUp.OnNext(Unit.Default);
#endif
        }

        public void InstanceLetterItems(List<char> word)
        {
            foreach (var character in word)
            {
                var letterItem = Instantiate(letterItemPrefab, lettersContainer).GetComponent<LetterItemView>();
                _createdLetters.Add(letterItem);
                letterItem.Set(character);
            }

            SubscribeToLetterItemEmissions();
        }

        private void SubscribeToLetterItemEmissions()
        {
            _createdLetters.Select(letter => letter.OnSelected())
                .Merge()
                .Do(_onLetterSelected.OnNext)
                .Subscribe()
                .AddTo(this);
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
        public IObservable<char> OnLetterSelected() => _onLetterSelected;

        public IObservable<Unit> OnMouseUp() => _onMouseUp;
    }
}