using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace Features.Board.Scripts.Delivery
{
    public class LetterItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        private readonly Subject<char> _onSelected = new();

        public void Set(char letter)
        {
            label.text = letter.ToString().ToUpper();
        }

        public IObservable<char> OnSelected() => _onSelected;
    }
}