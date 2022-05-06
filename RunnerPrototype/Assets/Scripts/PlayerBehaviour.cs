using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] [Min(1f)] private float speedForward = 5f;
    [SerializeField] [Min(5f)] private float speedJump = 10f;

    [SerializeField] private Rigidbody rb;

    private void FixedUpdate()
    {
        Vector3 movingForward = transform.forward * speedForward * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movingForward);
    }

    protected void Jump()
    {
        rb.AddForce(transform.up * speedJump, ForceMode.Impulse);
    }









    /*
    [SerializeField][Min(1f)] private float speedForward = 5f;
    [SerializeField][Min(1f)] private float speedJump = 1f;

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();

        StartCoroutine(MovingForward());
    }

    protected void Jump()
    {
        rigidBody.AddForce(transform.up * speedJump, ForceMode.Impulse);
    }
    private IEnumerator MovingForward()
    {
        while(true)
        {
            rigidBody.velocity += Vector3.forward * speedForward * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
            
            //transform.position += Vector3.forward * speedForward * Time.deltaTime;

            //yield return null;
        }
    }*/
}
