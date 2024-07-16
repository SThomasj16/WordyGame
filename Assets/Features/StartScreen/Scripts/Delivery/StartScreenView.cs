using System;
using Features.StartScreen.Scripts.Presentation;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Features.StartScreen.Scripts.Delivery
{
    public class StartScreenView : MonoBehaviour, IStartScreenView
    {
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private Button playButton;
        [SerializeField] private GameObject board;
        private readonly Subject<int> _onDropdownValueChanged = new();
        private StartScreenPresenter _presenter;
        private void Awake()
        {
            _presenter = StartScreenPresenter.Present(this);
        }

        private void Start()
        {
            dropdown.onValueChanged.AddListener(HandleOnValueChanged);
        }

        private void HandleOnValueChanged(int value)
        {
            _onDropdownValueChanged.OnNext(value);
        }
        public void ShowPlayButton()
        {
            playButton.gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void DisplayBoard()
        {
            board.SetActive(true);
        }
        public IObservable<int> OnDropdownValueChanged() => _onDropdownValueChanged;
        public IObservable<Unit> OnStartButtonPressed() => playButton.OnClickAsObservable();
      

        public void OnDestroy()
        {
            _presenter.Dispose();
        }
    }
}