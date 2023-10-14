using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Scripts.Car
{
    public class ChangerSpriteCar: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer; 
        [SerializeField] private Sprite _magnet;
        [SerializeField] private Sprite _nitro;
        [SerializeField] private Sprite _shield;
        [SerializeField] private Sprite _clineCar;

        public void Magnet() => 
            _spriteRenderer.sprite = _magnet;
        
        public void Nitro() => 
            _spriteRenderer.sprite = _nitro;
        
        public void Shield() => 
            _spriteRenderer.sprite = _shield;
        
        public void Cline() => 
            _spriteRenderer.sprite = _clineCar;
    }
}