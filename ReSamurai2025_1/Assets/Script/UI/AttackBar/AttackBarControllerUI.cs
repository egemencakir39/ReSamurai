using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackBarControllerUI : MonoBehaviour
{
    [Header("Prefab & UI")]
    [SerializeField] private GameObject barSegmentPrefab;
    [SerializeField] private Transform barContainer;

    [Header("Full Sprites")]
    [SerializeField] private Sprite spriteFirst;
    [SerializeField] private Sprite spriteMid;
    [SerializeField] private Sprite spriteEnd;

    [Header("Empty Sprites")]
    [SerializeField] private Sprite spriteFirstEmpty;
    [SerializeField] private Sprite spriteMidEmpty;
    [SerializeField] private Sprite spriteEndEmpty;
    
    [Header("Icon Sprites")]
    [SerializeField] private Sprite iconFull;
    [SerializeField] private Sprite iconEmpty;

    private List<Image> barSegments = new List<Image>();
    private List<string> barTypes = new List<string>(); 
    private List<Image> barIcons = new List<Image>();

    public void InitBar(int maxAttack)
    {
        foreach (Transform child in barContainer)
            Destroy(child.gameObject);

        barSegments.Clear();
        barTypes.Clear();
        barIcons.Clear();

        for (int i = 0; i < maxAttack; i++)
        {
            GameObject bar = Instantiate(barSegmentPrefab, barContainer);
        
            Image barImage = bar.transform.Find("barImage").GetComponent<Image>();  
            Image iconImage = bar.transform.Find("barIcon").GetComponent<Image>();

            string type;

            if (maxAttack == 1)
            {
                barImage.sprite = spriteFirst;
                type = "first";
            }
            else if (maxAttack == 2)
            {
                barImage.sprite = (i == 0) ? spriteFirst : spriteEnd;
                type = (i == 0) ? "first" : "end";
            }
            else
            {
                if (i == 0)
                {
                    barImage.sprite = spriteFirst;
                    type = "first";
                }
                else if (i == maxAttack - 1)
                {
                    barImage.sprite = spriteEnd;
                    type = "end";
                }
                else
                {
                    barImage.sprite = spriteMid;
                    type = "mid";
                }
            }
            barSegments.Add(barImage);
            barTypes.Add(type);
            barIcons.Add(iconImage);
        }
    }
    public void UpdateBar(int currentAttack)
    {
        for (int i = 0; i < barSegments.Count; i++)
        {
            bool isFilled = i < currentAttack;

            switch (barTypes[i])
            {
                case "first":
                    barSegments[i].sprite = isFilled ? spriteFirst : spriteFirstEmpty;
                    break;
                case "mid":
                    barSegments[i].sprite = isFilled ? spriteMid : spriteMidEmpty;
                    break;
                case "end":
                    barSegments[i].sprite = isFilled ? spriteEnd : spriteEndEmpty;
                    break;
            }
            barIcons[i].sprite = isFilled ? iconFull : iconEmpty;
        }
    }
}
