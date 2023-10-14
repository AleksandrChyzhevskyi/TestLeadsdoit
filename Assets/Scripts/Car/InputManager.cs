using SimpleInputNamespace;
using UnityEngine;

namespace Scripts.Car
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private ButtonInputUI _gas;
        [SerializeField] private ButtonInputUI _brake;
        [SerializeField] private SteeringWheel _steeringWheel;

        public bool OnGas => _gas.button.value;
        public bool OnBrake => _brake.button.value;
        public float Rotate => _steeringWheel.Value;
    }
}