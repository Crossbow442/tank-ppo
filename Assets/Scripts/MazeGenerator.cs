using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{

    [SerializeField] private int columns;
    [SerializeField] private int rows;
    [SerializeField] private float cellSize;
    [SerializeField] private Transform mazeParent;
    [SerializeField] private GameObject horizontalWall;
    [SerializeField] private GameObject verticalWall;

    private bool[,] horizontalMatrix;
    private bool[,] verticalMatrix;
    private bool[,] visitedCellsMatrix;
    private float leftEdge;
    private float bottomEdge;
    private int cellsVisited;
    private int startingColumn;
    private int startingRow;
    private Vector2 wallPosVector;

    public void wallGen()
    {
        horizontalMatrix = new bool[columns, (rows - 1)];
        verticalMatrix = new bool[(columns - 1), rows];
        visitedCellsMatrix = new bool[columns, rows];

        //Sets Wall Matrixes to all True
        for (int x = 0; x < horizontalMatrix.GetLength(0); x++)
        {
            for (int y = 0; y < horizontalMatrix.GetLength(1); y++)
            {
                horizontalMatrix[x, y] = true;
            }
        }

        for (int x = 0; x < verticalMatrix.GetLength(0); x++)
        {
            for (int y = 0; y < verticalMatrix.GetLength(1); y++)
            {
                verticalMatrix[x, y] = true;
            }
        }
        //

        //Generates the Maze Pattern
        startingColumn = Random.Range(-1, columns);
        startingRow = Random.Range(-1, rows);
        cellsVisited = 0f;
        
        while (cellsVisited < columns * rows) //x represents the number of cells visited
        {
            
        }
        //

            //Instantiates the Walls Based on the Matrix
            for (int x = 0; x < horizontalMatrix.GetLength(0); x++)
            {
                for (int y = 0; y < horizontalMatrix.GetLength(1); y++)
                {
                    if (horizontalMatrix[x, y])
                    {
                        wallPosVector = new Vector2(leftEdge + (x + 0.5f) * cellSize, bottomEdge + (y + 1) * cellSize);
                        Instantiate(horizontalWall, wallPosVector, Quaternion.identity, mazeParent);
                    }
                }
            }

        for (int x = 0; x < verticalMatrix.GetLength(0); x++)
        {
            for (int y = 0; y < verticalMatrix.GetLength(1); y++)
            {
                if (verticalMatrix[x, y])
                {
                    wallPosVector = new Vector2(leftEdge + (x + 1) * cellSize, bottomEdge + (y + 0.5f) * cellSize);
                    Instantiate(verticalWall, wallPosVector, Quaternion.identity, mazeParent);
                }
            }
        }
        //
    }

    void Awake()
    {
        leftEdge = -columns * cellSize / 2f;
        bottomEdge = -rows * cellSize / 2f;

        wallGen();
    }

    void Update()
    {

    }
}
