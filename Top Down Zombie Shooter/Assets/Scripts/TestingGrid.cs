using System;
using System.Collections;
using UnityEngine;

public class TestingGrid : MonoBehaviour{

    private Grid grid;
    private Grid grid1;
    private Grid grid2;

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
        grid = new Grid(4, 2, 10f, new Vector3(20, 0));
        grid1 = new Grid(5, 3, 5f, new Vector3(0, -25f));
        grid2 = new Grid(4, 4, 5f, new Vector3(-35f, -15f));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(TestingGrid.GetMouseWorldPosition(), 2);
            grid1.SetValue(TestingGrid.GetMouseWorldPosition(), 9);
            grid2.SetValue(TestingGrid.GetMouseWorldPosition(), 72);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(TestingGrid.GetMouseWorldPosition()));
        }
    }
}
