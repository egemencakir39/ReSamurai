using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSoundSprite : MonoBehaviour
{
    
        public Sprite openSprite;
        public Sprite closedSprite;
    
        private Image buttonImage;
        private bool isOpen = true;
    
        void Start()
        {
            buttonImage = GetComponent<Image>();
            isOpen = true;
            buttonImage.sprite = openSprite;
    
            GetComponent<Button>().onClick.AddListener(ToggleSprite);
        }
    
        void ToggleSprite()
        {
            isOpen = !isOpen;
    
            if (isOpen)
                buttonImage.sprite = openSprite;
            else
                buttonImage.sprite = closedSprite;
        }
}
