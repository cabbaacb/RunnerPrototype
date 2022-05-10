using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _groundTile;
    [SerializeField] private GameObject _walls;

    [Tooltip("This determines how wide our road will be")]
    private int _numberOfLines = 5;
    [Tooltip("The bigger the number, the less objects will be")]
    [SerializeField] private int _objectsSpawnRate = 5;

    private Vector3 _spawnRow;

    private int[,] _arrayOfRoadObjects;

    private int _rowDistance = 2;
    public int RowDistance  //we use public property to determine where and when spawn next row
    {
        get { return _rowDistance; }
        set { _rowDistance = value; SpawnRow(); }
    }


    private void Start()
    {
        _arrayOfRoadObjects = new int[5, 2];
        for (int i = 0; i < 10; i++)    //here we spawn a first few rows of the road
        {
            SpawnRow();
        }
    }

    //this is a simple procedural generation of a road
    //here we determine elements of one row of it
    private void DetermineRow()
    {
        for (int i = 0; i < _arrayOfRoadObjects.GetLength(0); i++)
        {
            for (int k = 0; k < _arrayOfRoadObjects.GetLength(1); k++)
            {
                int value = Random.Range(0, _objectsSpawnRate);
                _arrayOfRoadObjects.SetValue(value, i, k);
            }
        }
    }

    //here we spawn walls and row of tiles, at coordinates in the end of previous row
    private void SpawnRow()
    {
        DetermineRow();
        Vector3 spawnPoint = _spawnRow;
        Instantiate(_walls, spawnPoint, Quaternion.identity, gameObject.transform);
        for (int i = 0; i < _numberOfLines; i++)
        {
            if (i == 0)
            {
                SpawnTile(spawnPoint, true, i);
            }
            else
            {
                SpawnTile(spawnPoint, false, i);
            }
            spawnPoint.x += 2;
        }
    }

    //here we spawn individual tiles
    private void SpawnTile(Vector3 position, bool setNextRow, int numberInRow)
    {
        GameObject tile = Instantiate(_groundTile, position, Quaternion.identity, gameObject.transform);

        //and set what objects they will has
        if (_arrayOfRoadObjects[numberInRow, 0] == 0)
        {
            tile.transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (_arrayOfRoadObjects[numberInRow, 1] == 0)
        {
            tile.transform.GetChild(3).gameObject.SetActive(true);
        }

        if (_arrayOfRoadObjects[numberInRow, 1] == 1)
        {
            tile.transform.GetChild(4).gameObject.SetActive(true);
        }
        else if (_arrayOfRoadObjects[numberInRow, 1] == 2)
        {
            tile.transform.GetChild(5).gameObject.SetActive(true);
        }

        if (setNextRow) //this is needed to determine next row spawn start position
        {
            _spawnRow = tile.transform.GetChild(0).transform.position;
        }
    }
}
