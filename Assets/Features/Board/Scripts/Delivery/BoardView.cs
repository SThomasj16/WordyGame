﻿using System;
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

        public IObservable<Unit> OnViewAppear() => _onAppear;
        public void InstanceLetterItems(int amount)
        {
            for (var i = 0; i < amount; i++) 
                Instantiate(letterItemPrefab, lettersContainer);
        }
    }
}