using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class LevelPageController : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private int totalPages = 3;
    [SerializeField] private float pageWidth = 1920f; 

    private int currentPage = 0;

    private void Start()
    {
        UpdatePage();
    }

    public void GoLeft()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
        }
    }

    public void GoRight()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            UpdatePage();
        }
    }

    private void UpdatePage()
    {
        Vector2 targetPos = new Vector2(-pageWidth * currentPage, 0);
        content.anchoredPosition = targetPos;
    }
}
