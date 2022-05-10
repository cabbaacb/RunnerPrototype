using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCHecker : MonoBehaviour
{
    public int isGrounded = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
            isGrounded++;
        Debug.Log(isGrounded);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8)
            isGrounded--;
    }
}
