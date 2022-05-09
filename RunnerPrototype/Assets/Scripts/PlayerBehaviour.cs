using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] [Range(0f, 1000f)] private float speedForward = 5f;
    [SerializeField] [Range(1f, 25f)] private float speedRight = 5f;
    [SerializeField] [Range(5f, 50f)] private float speedJump = 10f;

   

    [SerializeField] private Rigidbody rb;

    protected float horizontalInput;
    protected bool isGrounded;

    private float myHealth = 100f;

    [Min(1f)] public float health;
    [Min(5f)] public float maxHealth;
    [SerializeField] private HealthBarScript healthBar;

    [SerializeField] private int _count;
    [SerializeField] private Text _counter;
    private GameObject _previousCountObject;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        health = myHealth;
        maxHealth = health;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8)
            isGrounded = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 9)
        {
            SetDamage(10);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 10)
        {
            if (_previousCountObject == null)
            {
                _previousCountObject = collision.gameObject;
            }
            else
            {
                if (_previousCountObject.transform.position.z < collision.gameObject.transform.position.z)
                {
                    _previousCountObject = collision.gameObject;
                    _count++;
                    _counter.text = "Count: " + _count;
                }
            }
        }
    }

    protected void Jump()
    {      
        //OnCollisionStay()
        if(isGrounded)
        {
            rb.AddForce(transform.up * speedJump, ForceMode.Impulse);
        }
    }

    protected void Moving()
    {
        Vector3 movingForward = transform.forward * speedForward * Time.fixedDeltaTime;
        Vector3 movingHorizontal = transform.right * horizontalInput * speedRight * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movingForward + movingHorizontal);
        FallingDown();
    }



    private void SetDamage(float damage)
    {
        myHealth -= damage;
        health -= damage;
        healthBar.UpdateHealthBar();
    }

    private void FallingDown()
    {
        if (transform.position.y <= -0.5f) SetDamage(1);
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
