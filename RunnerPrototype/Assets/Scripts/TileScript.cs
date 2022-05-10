using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script simply destroys our tiles (and walls) after certain amount of time
public class TileScript : MonoBehaviour
{
    [SerializeField] private float time = 5f;
    private void Update()
    {
        time = time - 1 * Time.deltaTime;
        if (time < 0) SelfDestruction();
    }

    private void SelfDestruction()
    {
        Destroy(gameObject);
    }
}
