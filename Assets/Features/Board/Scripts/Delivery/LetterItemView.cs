using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Features.Board.Scripts.Delivery
{
    public class LetterItemView : MonoBehaviour, IPointerMoveHandler
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private Image background;
        [SerializeField] private Color normalColor;
        [SerializeField] private Color selectedColor;
        
        private char _selectedLetter;
        private bool _isSelected;
        private readonly Subject<char> _onSelected = new();
        public IObservable<char> OnSelected() => _onSelected;

        public void Set(char letter)
        {
            _selectedLetter = letter;
            label.text = letter.ToString().ToUpper();
        }
        
        public void OnPointerMove(PointerEventData eventData)
        {
            if(_isSelected) return;
#if UNITY_ANDROID && !UNITY_EDITOR
            if (Input.touchCount > 0)
                Select();
#elif  UNITY_EDITOR
            if (Input.GetKey(KeyCode.Mouse0))
                Select();
#endif
        }

        private void Select()
        {
            _isSelected = true;
            _onSelected.OnNext(_selectedLetter);
            background.color = selectedColor;
        }
    }
}