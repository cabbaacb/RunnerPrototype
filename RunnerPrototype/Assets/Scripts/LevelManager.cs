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

    private int _rowDistance = 2;

    private int[,] _arrayOfRoadObjects;

    public int RowDistance
    {
        get { return _rowDistance; }
        set { _rowDistance = value; ExitFromCollider(); }
    }


    private void Start()
    {
        _arrayOfRoadObjects = new int[5, 2];
        for (int i = 0; i < 10; i++)
        {
            SpawnRow();
        }
    }

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

    private void SpawnTile(Vector3 position, bool setNextRow, int numberInRow)
    {
        GameObject tile = Instantiate(_groundTile, position, Quaternion.identity, gameObject.transform);
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

    private void ExitFromCollider()
    {
        SpawnRow();
    }
}
