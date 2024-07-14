using TMPro;
using UnityEngine;

namespace Features.Board.Scripts.Delivery
{
    public class LetterItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;

        public void Set(char letter)
        {
            label.text = letter.ToString().ToUpper();
        }

    }
}