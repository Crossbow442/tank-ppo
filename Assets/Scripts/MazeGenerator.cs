using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{

    [SerializeField] private int columns;
    [SerializeField] private int rows;
    [SerializeField] private int knockDownChance;
    [SerializeField] private float cellSize;
    [SerializeField] private Transform mazeParent;
    [SerializeField] private GameObject horizontalWall;
    [SerializeField] private GameObject verticalWall;

    private bool[,] horizontalMatrix;
    private bool[,] verticalMatrix;
    private bool[,] visitedMatrix;
    private Stack<Vector2Int> pastMoves;
    private Vector2Int backtrackPos; 
    private bool up;
    private bool down;
    private bool left;
    private bool right;
    private bool carl;
    private float leftEdge;
    private float bottomEdge;
    private int cellsVisited;
    private int chColumn;
    private int chRow;
    private int rand;
    private int count;
    private Vector2 wallPosVector;

    public void wallGen()
    {
        horizontalMatrix = new bool[columns, (rows - 1)];
        verticalMatrix = new bool[(columns - 1), rows];
        visitedMatrix = new bool[columns, rows];
        pastMoves = new Stack<Vector2Int>();

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

        //Randomly Generates the Maze Pattern
        chColumn = Random.Range(0, columns);
        chRow = Random.Range(0, rows);
        pastMoves.Push(new Vector2Int(chColumn, chRow));
        cellsVisited = 0;

        while (cellsVisited + 1 < columns * rows)
        {
            visitedMatrix[chColumn, chRow] = true;

            count = 0;
            up = false;
            down = false;
            left = false;
            right = false;

            //Checks Whether the Adjacent Cells Have Been Traveled To
            if (chColumn + 1 < columns && !visitedMatrix[chColumn + 1, chRow])
            {
                right = true;
                count += 1;
            }
            if (chColumn > 0 && !visitedMatrix[chColumn - 1, chRow])
            {
                left = true;
                count += 1;
            }
            if (chRow + 1 < rows && !visitedMatrix[chColumn, chRow + 1])
            {
                up = true;
                count += 1;
            }
            if (chRow > 0 && !visitedMatrix[chColumn, chRow - 1])
            {
                down = true;
                count += 1;
            }
            //
            
            if (count == 0)
            {
                pastMoves.Pop();
                backtrackPos = pastMoves.Peek();
                chColumn = backtrackPos.x;
                chRow = backtrackPos.y;
                continue;
            }

            carl = true;
            
            //Picks Next Cell
            while (carl)
            {
                rand = Random.Range(0, 4);

                if (rand == 0 && right)
                {
                    verticalMatrix[chColumn, chRow] = false;
                    chColumn += 1;
                    carl = false;
                }
                if (rand == 1 && left)
                {
                    verticalMatrix[chColumn - 1, chRow]= false;
                    chColumn -= 1;
                    carl = false;
                }
                if (rand == 2 && up)
                {
                    horizontalMatrix[chColumn, chRow] = false;
                    chRow += 1;
                    carl = false;
                }
                if (rand == 3 && down)
                {
                    horizontalMatrix[chColumn, chRow - 1] = false;
                    chRow -= 1;
                    carl = false;
                }
                //
            }

            pastMoves.Push(new Vector2Int(chColumn, chRow));
            cellsVisited += 1;
        }
        //

        //Knocks Out a Couple More Walls
        for (int x = 0; x < horizontalMatrix.GetLength(0); x++)
        {
            for (int y = 0; y < horizontalMatrix.GetLength(1); y++)
            {
                if (horizontalMatrix[x, y])
                {
                    rand = Random.Range(0, knockDownChance);

                    if (rand == 0)
                    {
                        horizontalMatrix[x, y] = false;
                    }
                }
            }
        }

        for (int x = 0; x < verticalMatrix.GetLength(0); x++)
        {
            for (int y = 0; y < verticalMatrix.GetLength(1); y++)
            {
                if (verticalMatrix[x, y])
                {
                    rand = Random.Range(1, knockDownChance);

                    if (rand == 1)
                    {
                        verticalMatrix[x, y] = false;
                    }
                }
            }
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
}