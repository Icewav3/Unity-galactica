using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSnap : MonoBehaviour
{
    public Color color = Color.red;
    public float width = 0.1f;
    private int _gridX = 32;
    private int _gridY = 32;
    private Material _defaultMat;
    private GameObject _player;
    private GameObject _grid;
    private GameObject[,] _gridArray;
    private void Start()
    {
        //Initialize
        _defaultMat = new Material(Shader.Find("Sprites/Default"));
        _player = GameObject.Find("PlayerCore");
        _grid = GameObject.Find("SnapGrid");
        _gridArray = new GameObject[_gridX, _gridY];
        
        //Generate Grid
        for (int x = 0; x < _gridX; x++)
        {
            for (int y = 0; y < _gridY; y++)
            {
                //Creating GameObject and setting parent
                GameObject gridCell = new GameObject("Grid Cell");
                gridCell.transform.parent = _grid.transform;
                gridCell.transform.position = new Vector3(x, y);
                
                //Add to GameObject array
                _gridArray[x, y] = gridCell;
                
                //draw lines
                Vector3 start = new Vector3(-0.5f, -0.5f, 0f);
                Vector3 end = new Vector3(0.5f, -0.5f, 0f);
                DrawLine(gridCell, start, end, color, width);
                start = new Vector3(-0.5f, -0.5f, 0f);
                end = new Vector3(-0.5f, 0.5f, 0f);
                DrawLine(gridCell, start, end, color, width);
            }
        }
    }
    
    private void DrawLine(GameObject parent, Vector3 start, Vector3 end, Color color, float lineWidth)
    {
        GameObject lineObj = new GameObject("Grid Line");
        lineObj.transform.parent = parent.transform;

        LineRenderer line = lineObj.AddComponent<LineRenderer>();
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.useWorldSpace = false;
        line.material = _defaultMat;
        line.startColor = color;
        line.endColor = color;
        line.positionCount = 2;
        line.SetPosition(0, parent.transform.TransformPoint(start));
        line.SetPosition(1, parent.transform.TransformPoint(end));
        
    }

    
    public GameObject Getlist(int x, int y) //public method for getting the block in the selected position
    {
        return _gridArray[x, y];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
