﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Grid {

    public const int sortingOrderDefault = 5000;

    public const int HEAT_MAP_MAX_VALUE = 100;
    public const int HEAT_MAP_MIN_VALUE = 0;

    private int width;
    private int height;
    private float cellsize;
    private Vector3 originPosition;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;

    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    // Create Text in the World
    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault)
    {
        if (color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }

    // Create Text in the World
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

    public Grid(int width, int height, float cellsize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellsize = cellsize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];

        bool showDebug = true;
        if (showDebug)
        {
            debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    debugTextArray[x, y] = Grid.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellsize, cellsize) * .5f, 20, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

            OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>
            {
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y].ToString();
            };
        }
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellsize + originPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellsize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellsize);
    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = Mathf.Clamp(value, HEAT_MAP_MIN_VALUE, HEAT_MAP_MAX_VALUE);
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellsize;
    }

    public Vector3 GetWorldPositionEins(int x, int y)
    {
        return new Vector3(x, y) * cellsize + originPosition;
    }

    public void AddValue(int x, int y, int value)
    {
        SetValue(x, y, GetValue(x, y) + value);
    }

    public void AddValue(Vector3 worldPosition, int value, int fullValueRange, int totalRange)
    {
        int lowerValueAmount = Mathf.RoundToInt((float)value / (totalRange - fullValueRange));

        GetXY(worldPosition, out int originX, out int originY);
        for (int x = 0; x < totalRange; x++)
        {
            for (int y = 0; y < totalRange - x; y++)
            {
                int radius = x + y;
                int addValueAmount = value;
                if (radius > fullValueRange)
                {
                    addValueAmount -= lowerValueAmount * (radius - fullValueRange);
                }

                AddValue(originX + x, originY + y, addValueAmount);

                if (x != 0)
                {
                    AddValue(originX - x, originY + y, addValueAmount);
                }
                if (y != 0)
                {
                    AddValue(originX + x, originY - y, addValueAmount);
                    if (x != 0)
                    {
                        AddValue(originX - x, originY - y, addValueAmount);
                    }
                }
            }
        }
    }       
    
    //public void AddValue(Vector3 worldPosition, int value, int range)
    //{
    //    int lowerValueAmount = Mathf.RoundToInt((float)value / (totalRange - fullValueRange));

    //    GetXY(worldPosition, out int originX, out int originY);
    //    for (int x = 0; x < range; x++)
    //    {
    //        for (int y = 0; y < range - x; y++)
    //        {
    //            int radius = x + y;
    //            int addValueAmount = value;
    //            if (radius >= fullValueRange)
    //            {
    //                addValueAmount -= lowerValueAmount * (radius - fullValueRange);
    //            }

    //            AddValue(originX + x, originY + y, value);

    //            if (x != 0)
    //            {
    //                AddValue(originX - x, originY + y, addValueAmount);
    //            }
    //            if (y != 0)
    //            {
    //                AddValue(originX + x, originY - y, addValueAmount);
    //                if (x != 0)
    //                {
    //                    AddValue(originX - x, originY - y, addValueAmount);
    //                }
    //            }
    //        }
    //    }
    //}
}
