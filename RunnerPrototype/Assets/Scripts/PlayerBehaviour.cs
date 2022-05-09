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
    //protected int isGrounded;

    

    private float myHealth = 100f;

    [Min(1f)] public float health;
    [Min(5f)] public float maxHealth;
    [SerializeField] private HealthBarScript healthBar;
    [SerializeField] private JumpCHecker jumpChecker;

    [SerializeField] private int _count;
    [SerializeField] private Text _counter;
    private GameObject _previousCountObject;

    private int timer = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        health = myHealth;
        maxHealth = health;
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
            isGrounded++;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8)
            isGrounded--;
    }
    */

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 9)
        {
            SetDamage(10);
            timer = 10;
            speedForward -= 10f;
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
        if(jumpChecker.isGrounded >0)
        {
            rb.AddForce(transform.up * speedJump, ForceMode.Impulse);
        }
    }

    protected void Moving()
    {
        if (timer > 0)
        {
            timer--;
            speedForward += 1f;
        }
        

        if (_count % 10 == 0) speedForward += 2f;
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

        if (health <= 0) UnityEditor.EditorApplication.isPaused = true;
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
