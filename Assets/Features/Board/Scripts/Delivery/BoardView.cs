using System;
using System.Collections.Generic;
using Features.Board.Scripts.Presentation;
using UniRx;
using UnityEngine;

namespace Features.Board.Scripts.Delivery
{
    public class BoardView : MonoBehaviour, IBoardView
    {
        [SerializeField] private Transform lettersContainer;
        [SerializeField] private GameObject letterItemPrefab;
        
        private readonly ISubject<Unit> _onAppear = new Subject<Unit>();

        private void Awake()
        {
            var presenter = BoardPresenter.Present(this);
        }

        private void Start()
        {
            _onAppear.OnNext(Unit.Default);
        }

        public void InstanceLetterItems(List<char> amount)
        {
            for (var i = 0; i < amount.Count; i++)
            {
               var letterItem = Instantiate(letterItemPrefab, lettersContainer).GetComponent<LetterItemView>();
               letterItem.Set(amount[i]);
            }

        }
        
        public IObservable<Unit> OnViewAppear() => _onAppear;

    }
}