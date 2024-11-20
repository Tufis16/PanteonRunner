using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FinishWall : MonoBehaviour
{
    public TexturePainter texturePainter;
    public TMP_Text text_Percentage;
    public Slider slider_BrushSize;
    public Button buttonYellow, buttonRed, buttonBlue;

    public Color32 colorYellow, colorRed, colorBlue;

    private void Start()
    {
        buttonYellow.onClick.AddListener(ColorPickYellow);
        buttonRed.onClick.AddListener(ColorPickRed);
        buttonBlue.onClick.AddListener(ColorPickBlue);

    }

    private void Update()
    {
        if(texturePainter.paintPercent <= 100)
        {
            text_Percentage.text = "%" + texturePainter.paintPercent.ToString("F0");
        }

        texturePainter.brushSize = slider_BrushSize.value;
    }

    public void ColorPickYellow()
    {
        texturePainter.brushColor = colorYellow;
    }

    public void ColorPickRed()
    {
        texturePainter.brushColor = colorRed;
    }

    public void ColorPickBlue()
    {
        texturePainter.brushColor = colorBlue;
    }

    public void BrushSizeControl()
    {
        texturePainter.brushSize = slider_BrushSize.value;
    }
}
