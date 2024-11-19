using UnityEngine;

namespace Ui
{
    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    public class FloatingJoystick : MonoBehaviour
    {
        [SerializeField] public RectTransform rectTransform;
        [SerializeField] private RectTransform knob;

        public RectTransform RectTransform
        {
            get => rectTransform;
            set => rectTransform = value;
        }
        public RectTransform Knob
        {
            get => knob;
            set => knob = value;
        }
    }
}