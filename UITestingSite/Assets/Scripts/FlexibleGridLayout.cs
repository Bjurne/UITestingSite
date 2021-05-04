using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public enum FitType
    {
        Uniform = 0,
        Width = 1,
        Height = 2,
        FixedRows = 3,
        FixedColumns = 4
    }

    public enum Alignment
    {
        Left = 0,
        Center = 1,
        Right = 2,
    }

    [SerializeField] FitType fitType = FitType.Uniform;
    [SerializeField] int rows = default;
    [SerializeField] int columns = default;
    [SerializeField] Vector2 cellSize = default;
    [SerializeField] Vector2 spacing = default;
    public bool fitX;
    public bool fitY;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        rows = Mathf.Clamp(rows, 1, 27);
        columns = Mathf.Clamp(columns, 1, 27);

        if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            fitX = true;
            fitY = true;
            float sqrRt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrRt);
            columns = Mathf.CeilToInt(sqrRt);
        }

        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        }
        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = (parentWidth / (float)columns) - ((spacing.x / (float)columns) * 2) - (padding.left / (float)columns) - (padding.right / (float)columns);
        float cellHeight = (parentHeight / (float)rows) - ((spacing.y / (float)rows) * 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows);
        cellWidth = Mathf.Clamp(cellWidth, 0.1f, 100000f);
        cellHeight = Mathf.Clamp(cellHeight, 0.1f, 100000f);

        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            var rowCount = i / columns;
            var columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * (columnCount)) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * (rowCount)) + padding.top;

            //if (childAlignment == TextAnchor.UpperLeft)
            //{
            //    xPos = (cellSize.x * columnCount) + (spacing.x * (columnCount + 1)) + padding.left;
            //    yPos = (cellSize.y * rowCount) + (spacing.y * (rowCount + 1)) + padding.top;
            //}

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    {

    }

    public override void SetLayoutHorizontal()
    {

    }

    public override void SetLayoutVertical()
    {
        
    }
}
