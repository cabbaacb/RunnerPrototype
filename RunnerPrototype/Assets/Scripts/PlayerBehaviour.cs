using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] [Range(0f, 25f)] private float speedForward = 5f;
    [SerializeField] [Range(0f, 25f)] private float maximumSpeedForward = 15f;
    [SerializeField] [Range(1f, 25f)] private float speedRight = 5f;
    [SerializeField] [Range(5f, 50f)] private float speedJump = 10f;

    [SerializeField] private Rigidbody rb;
    [SerializeField] protected UIScript _UI;

    protected float horizontalInput;

    private float maxHealth = 100f;
    private float health = 100f;

     private int _count;

    private GameObject _previousCountObject;

    private int isGrounded;

    private float time = 0f;
    [SerializeField] private float _endTime = 10f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _UI.EndTime = _endTime; //here we set lenght of our game for UI
    }

    private void Start()
    {
        StartCoroutine(SpeedUp(1));
        
    }

    //if player bumps in any object, which can deal damage, he recieve it, and his speed resets
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 9)
        {
            SetDamage(10);
            speedForward = 5f;
        }
    }

    //if our player leaves a row of tiles, game count increases
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
                    _UI.UpdateCount(_count);
                }
            }
        }
    }

    //if player stays on the ground, he can jump
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

    protected void Jump()
    {      
        if(isGrounded > 0)
        {
            rb.AddForce(transform.up * speedJump, ForceMode.Impulse);
        }
    }

    //constant movement, checking for falling down, and timer, which stops our game when ends
    protected void Moving()
    {
        Vector3 movingForward = transform.forward * speedForward * Time.fixedDeltaTime;
        Vector3 movingHorizontal = transform.right * horizontalInput * speedRight * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movingForward + movingHorizontal);
        FallingDown();
        time = time + 1 * Time.fixedDeltaTime;
        _UI.TimerUpdate(time);
        if (time >= _endTime)
        {
            EndGame();
        }
    }

    //this coroutine increase speed of our player, every second of his existence, unless he moves fast enough
    private IEnumerator SpeedUp(float increase)
    {
        yield return new WaitForSeconds(1f);
        speedForward += increase;
        if (speedForward < 15)
        {
            StartCoroutine(SpeedUp(1));
        }
        else
        {
            StartCoroutine(SpeedUp(0));
        }
    }

    //here we decrease HP of our player, show this in UI, and stop the game when HP reaches zero
    private void SetDamage(float damage)
    {
        health -= damage;
        _UI.UpdateHealth(health, maxHealth);

        if (health <= 0) EndGame();
    }

    //checking for falling down
    private void FallingDown()
    {
        if (transform.position.y <= -0.5f) SetDamage(1);
    }

    //shows the message and stops the game
    private void EndGame()
    {
        _UI.EndGameText();
        UnityEditor.EditorApplication.isPaused = true;
    }
}
