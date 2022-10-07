using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Grid : ScriptableObject
{
    private int width;
    private int height;
    private int cellSize;
    private Cell cellPrefab;
    private Cell[,] gridArray;
    private ExternalWall externalWall;
    private Transform parentTransform;


    public Grid(int width, int height, int cellSize, Cell cellPrefab, Transform parentTransform, ExternalWall externalWall)
    {
        
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.cellPrefab = cellPrefab;
        this.parentTransform = parentTransform;
        this.externalWall = externalWall;

        generateBoard();
    }

    private void generateBoard()
    {
        Cell cell;
        gridArray = new Cell[width, height];
        ExternalWall ew;
        GameObject parent = new GameObject("WallParent");
        parent.transform.SetParent(parentTransform);

        for (int i = 0; i < width; i++)
        {
            
            for (int j = 0; j < height; j++)
            {
                var p = new Vector2(i+parentTransform.position.x, j+parentTransform.position.y) * cellSize;
                cell = Instantiate(cellPrefab, p, Quaternion.identity);
                
                cell.Init(this, i, j, true);

                cell.transform.SetParent(parentTransform);

                gridArray[i, j] = cell;

                if (i == 0) { 
                    ew = Instantiate(externalWall, new Vector2(-1 + parentTransform.position.x, j  + parentTransform.position.y) * cellSize, Quaternion.identity);
                    ew.gameObject.transform.SetParent(parent.gameObject.transform);
                }

                if (i == width - 1) { 
                    ew = Instantiate(externalWall, new Vector2(width  + parentTransform.position.x, j  + parentTransform.position.y) * cellSize, Quaternion.identity);
                    ew.gameObject.transform.SetParent(parent.gameObject.transform);
                }
            }

            ew = Instantiate(externalWall, new Vector2(i + parentTransform.position.x, -1 + parentTransform.position.y) * cellSize, Quaternion.identity);
            ew.gameObject.transform.SetParent(parent.gameObject.transform);
            ew = Instantiate(externalWall, new Vector2(i + parentTransform.position.x,height + parentTransform.position.y) * cellSize, Quaternion.identity);
            ew.gameObject.transform.SetParent(parent.gameObject.transform);

        }

        //var center = new Vector2((float)height / 2 - 0.5f, (float)width / 2 - 0.5f);

        //Camera.main.transform.position = new Vector3(center.y, center.x, -5);
    }

    internal int GetHeight()
    {
        return height;
    }

    internal int GetWidth()
    {
        return width;
    }

    public void SetWalkable(Vector3 position, bool value)
    {
        //Debug.Log("SetWalkable " + position.x + " " + position.y);
        gridArray[(int)position.x, (int)position.y].SetWalkable(value);
    }

    public void setBusyCell(int initialX,int initialY, int newX, int newY)
    {
        gridArray[initialX, initialY].SetWalkable(true);
        gridArray[newX, newY].SetWalkable(false);
    }

    public bool isWalkable(int x, int y)
    {
        return gridArray[x, y].isWalkable;
    }

    public Cell GetGridObject(int x, int y)
    {
        //Debug.Log("GetGridObject " + x + " " + y);
        return gridArray[x, y];
    }

    internal float GetCellSize()
    {
        return cellSize;
    }
}
