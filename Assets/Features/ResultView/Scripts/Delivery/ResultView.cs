using Features.ResultView.Scripts.Presentation;
using UnityEngine;

namespace Features.ResultView.Scripts.Delivery
{
    public class ResultView : MonoBehaviour, IResultView
    {
        [SerializeField] private GameObject victoryPopup;
        private ResultViewPresenter _presenter;
        private void Awake()
        {
            _presenter = ResultViewPresenter.Present(this);
        }

        public void ShowVictoryPopUp()
        {
            victoryPopup.SetActive(true);
        }

        private void OnDestroy()
        {
            _presenter.Dispose();
        }
    }
}