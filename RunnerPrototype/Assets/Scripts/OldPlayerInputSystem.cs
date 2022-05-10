using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Here we get commands from old input system
public class OldPlayerInputSystem : PlayerBehaviour
{   
    private void FixedUpdate()
    {
        if(Input.GetButtonDown("Jump")) Jump();
        horizontalInput = Input.GetAxis("Horizontal");
        Moving();       

    }
}
