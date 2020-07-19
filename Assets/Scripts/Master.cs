using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Master : MonoBehaviour
{
    public Canvas canvas;
    public GameObject grid;
    public GameObject cell;
    public Text rowText;
    public Text colText;


    int row = 40;
    int col = 40;
    int i;
    int j;


    int[,] oldGrid = new int[50, 50];//Random array
    int[,] newGrid = new int[50, 50];//Result Array

    void Start()
    {
        Instantiate(grid);//Instantiating the Grid Parent
        randomGridGen();
    }

    void Update()
    {
        KeyboardValue();
        statusCheck();
    }

    void randomGridGen()//Generating a random input 2d array for the grid
    {
        System.Random random = new System.Random();

        for (i = 1; i < row + 1; i++)//Ignoring the edges, keping them Zero
        {
            for (j = 1; j < col + 1; j++)
            {
                oldGrid[i, j] = random.Next(0, 2);//setting values from 0 or 1 in the grid 
                newGrid[i, j] = oldGrid[i, j];
            }
        }

        gridGen();
    }

    void gridGen()//Generating the Grid in the screen
    {
        Vector3 newPos = new Vector3();
        for (i = 1; i < row + 1; i++)
        {
            for (j = 1; j < col + 1; j++)
            {
                GameObject gridfind = GameObject.Find("Grid(Clone)");
                GameObject captureAlive = Instantiate(cell, gridfind.transform);//putting the cells in the grid
                captureAlive.transform.position = new Vector3((newPos.x + 20 * j) - 400f, (newPos.y + 20 * i) - 250f, newPos.z);//Manupulating the position of the grid and the spacing between the cells.

                if (newGrid[i, j] == 1)
                {
                    captureAlive.GetComponent<SpriteRenderer>().color = Color.blue;//Living cells will be "Blue"
                }
                else
                {
                    captureAlive.GetComponent<SpriteRenderer>().color = Color.black;//Living cells will be "Black"
                }
            }
        }
    }

    void statusCheck()//Check if the cell is alive or dead!
    {
        int p = 0;
        int q = 0;
        for (i = 1; i < row + 1; i++)
        {
            for (j = 1; j < col + 1; j++)
            {
                if (oldGrid[i, j] == 1)//For alive cells("1")
                {
                    int sum = 0;//to calculate the number of neighbors
                    for (p = i - 1; p < i + 2; p++)
                    {
                        for (q = j - 1; q < j + 2; q++)
                        {
                            sum += oldGrid[p, q];
                        }
                    }

                    if ((sum - 1) < 2 || (sum - 1) > 3)//(sum-1) to eliminate the "1" itself.Also putting condition checks.
                    {
                        newGrid[i, j] = 0;
                    }
                    else
                    {
                        newGrid[i, j] = 1;
                    }
                }
                else if (oldGrid[i, j] == 0)//For dead cells("0")
                {
                    int sum = 0;
                    for (p = i - 1; p < i + 2; p++)
                    {
                        for (q = j - 1; q < j + 2; q++)
                        {
                            sum += oldGrid[p, q];
                        }
                    }

                    if (sum == 3)//condition check and change
                    {
                        newGrid[i, j] = 1;
                    }
                    else
                    {
                        newGrid[i, j] = 0;
                    }
                }
            }
        }

        gridGen();

        for (i = 1; i < row + 1; i++)//setting the newGrid values to the oldOne to check again!
        {
            for (j = 1; j < col + 1; j++)
            {
                oldGrid[i, j] = newGrid[i, j];
            }
        }
    }


    static string Value;
    public void SetIt(GameObject value)
    {
        Value = value.GetComponent<Text>().text;
    }
    public void KeyboardValue()//for getting values from keyboard
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b')
            {
                Value = Value.Substring(0, Value.Length - 1);
            }
            else if ((c == '\n') || (c == '\r'))//on enter
            {
                GameObject gridfind = GameObject.Find("Grid(Clone)");
                Destroy(gridfind);//Old grid destroy 

                string rin = rowText.GetComponent<Text>().text.ToString();//getting the values from the text fields
                row = Int32.Parse(rin);

                string cil = colText.GetComponent<Text>().text.ToString();
                col = Int32.Parse(cil);

                Instantiate(grid);//create a new grid parent for the new array

                randomGridGen();//gets the input row and coloumn
            }
        }
    }
}
