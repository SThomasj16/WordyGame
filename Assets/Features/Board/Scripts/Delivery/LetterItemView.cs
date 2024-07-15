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
        
        private readonly Subject<LetterItemView> _onSelected = new();
        private char _selectedLetter;
        private bool _isSelected;
        private bool _isLocked;
        public IObservable<LetterItemView> OnSelected() => _onSelected;

        public void Set(char letter)
        {
            _selectedLetter = letter;
            label.text = letter.ToString().ToUpper();
        }

        public char GetLetter() => _selectedLetter;
        
        public void OnPointerMove(PointerEventData eventData)
        {
            if(_isSelected || _isLocked) return;
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
            _onSelected.OnNext(this);
            background.color = selectedColor;
        }

        public void Deselect()
        {
            _isSelected = false;
            background.color = normalColor;
        }

        public void Lock()
        {
            _isLocked = true;
        }
    }
}