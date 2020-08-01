using System;
using System.Collections;
using UnityEngine;

public class TestingGrid : MonoBehaviour{

    [SerializeField]
    private HeatMapVisual heatMapVisual;
    [SerializeField]
    private HeatMapBoolVisual heatMapBoolVisual;
    [SerializeField]
    private HeatMapGenericVisual heatMapGenericVisual;

    private Grid<HeatMapGridObject> grid;
    private Grid<StringGridObject> stringGrid;

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    private void Start()
    {
        //grid = new Grid<HeatMapGridObject>(20, 10, 10f, new Vector3(-100, -50, 0), (Grid<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y));
        stringGrid = new Grid<StringGridObject>(20, 10, 5f, new Vector3(-50, -25, 0), (Grid<StringGridObject> g, int x, int y) => new StringGridObject(g, x, y));

        //heatMapVisual.SetGrid(grid);
        //heatMapBoolVisual.SetGrid(grid);
        //heatMapGenericVisual.SetGrid(grid);
    }

    private void Update()
    {
        Vector3 position = TestingGrid.GetMouseWorldPosition();

        /*if (Input.GetMouseButtonDown(0))
        {
            //grid.AddValue(position, 100, 2, 20);
            //grid.SetValue(position, true);
            HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);
            if (heatMapGridObject != null)
            {
                heatMapGridObject.AddValue(5);
            }
        }*/

        if (Input.GetKeyDown(KeyCode.A))  stringGrid.GetGridObject(position).AddLetter("A");
        if (Input.GetKeyDown(KeyCode.B)) stringGrid.GetGridObject(position).AddLetter("B");
        if (Input.GetKeyDown(KeyCode.C)) stringGrid.GetGridObject(position).AddLetter("C");

        if (Input.GetKeyDown(KeyCode.Alpha1)) stringGrid.GetGridObject(position).AddNumber("1");
        if (Input.GetKeyDown(KeyCode.Alpha2)) stringGrid.GetGridObject(position).AddNumber("2");
        if (Input.GetKeyDown(KeyCode.Alpha3)) stringGrid.GetGridObject(position).AddNumber("3");
    }
}

public class HeatMapGridObject{

    private int MIN = 0;
    private int MAX = 100;

    private Grid<HeatMapGridObject> grid;
    private int x;
    private int y;
    private int value;

    public HeatMapGridObject(Grid<HeatMapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue)
    {
        value += addValue;
        value = Mathf.Clamp(value, MIN, MAX);
        grid.TriggerGridObjectChanged(x, y);
    }

    public float GetValueNormalized()
    {
        return (float)value / MAX;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}

public class StringGridObject
{
    private Grid<StringGridObject> grid;
    private string letters;
    private string numbers;
    private int x;
    private int y;
    

    public StringGridObject(Grid<StringGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        letters = "";
        numbers = "";
    }

    public void AddLetter(string letter)
    {
        letters += letter;
        grid.TriggerGridObjectChanged(x, y);
    }

    public void AddNumber(string number)
    {
        numbers += number;
        grid.TriggerGridObjectChanged(x, y);
    }

    public override string ToString()
    {
        return letters + "\n" + numbers;
    }
}
