using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsBehaviour : MonoBehaviour
{
   
   private void Update()
    {
        
    }

    private IEnumerator HandsWaving()
    {
        while (transform.rotation.x < 90)
        {
            transform.eulerAngles += new Vector3(1, 0, 0);
            yield return null;
        }
        while (transform.rotation.x > -90)
        {
            transform.eulerAngles -= new Vector3(1, 0, 0);
            yield return null;
        }
    }
}
