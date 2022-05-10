using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is needed to say LevelManager how far player traveled
public class ColliderExitScript : MonoBehaviour
{
    private LevelManager _levelManager;

    private void Start()
    {
        _levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (_levelManager.RowDistance < Mathf.RoundToInt(gameObject.transform.position.z))
        {
            _levelManager.RowDistance = Mathf.RoundToInt(gameObject.transform.position.z);
        }
    }
}
