using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _groundTile;

    [SerializeField] private int _numberOfLines = 5;
    private Vector3 _spawnRow;

    private int _rowDistance = 2;

    public int RowDistance
    {
        get { return _rowDistance; }
        set { _rowDistance = value; ExitFromCollider(); }
    }


    private void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            SpawnRow();
        }
        
    }

    private void SpawnRow()
    {
        Vector3 spawnPoint = _spawnRow;
        for (int i = 0; i < _numberOfLines; i++)
        {
            if (i == 0)
            {
                SpawnTile(spawnPoint, true);
            }
            else
            {
                SpawnTile(spawnPoint, false);
            }
            spawnPoint.x += 2;
        }
    }

    private void SpawnTile(Vector3 position, bool setNextRow)
    {
        GameObject tile = Instantiate(_groundTile, position, Quaternion.identity, gameObject.transform);
        if (setNextRow)
        {
            _spawnRow = tile.transform.GetChild(1).transform.position;
        }
    }

    private void ExitFromCollider()
    {
        SpawnRow();
    }
}
