using System;
using Features.Result.Scripts.Presentation;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Result.Scripts.Delivery
{
    public class ResultView : MonoBehaviour, IResultView
    {
        [SerializeField] private GameObject victoryPopup;
        [SerializeField] private Button nextButton;
        private ResultViewPresenter _presenter;
        private void Awake()
        {
            _presenter = ResultViewPresenter.Present(this);
        }

        public void Show()
        {
            victoryPopup.SetActive(true);
        }

        public void Hide()
        {
            victoryPopup.SetActive(false);
        }
        public IObservable<Unit> OnNextButtonPressed() => nextButton.OnClickAsObservable();

        private void OnDestroy()
        {
            _presenter.Dispose();
        }
    }
}