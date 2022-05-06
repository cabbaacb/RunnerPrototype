using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerInputSystem : PlayerBehaviour
{
    private void FixedUpdate()
    {
        if(Input.GetButtonDown("Jump")) Jump();
        

    }
}
