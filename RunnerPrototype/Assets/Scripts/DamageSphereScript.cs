using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script moves damaging spheres
public class DamageSphereScript : MonoBehaviour
{
    private float speed;

    private void Start()
    {
        speed = Random.Range(2f, 5f);   //we set random speed
        int direction = Random.Range(0, 2); //and random direction of movement
        if (direction == 0)
        {
            MoveFromTo(gameObject.transform.position, new Vector3(0f, gameObject.transform.position.y, gameObject.transform.position.z), speed);

            StartCoroutine(PingPongWithDelay());
        }
        else
        {
            MoveFromTo(gameObject.transform.position, new Vector3(8f, gameObject.transform.position.y, gameObject.transform.position.z), speed);

            StartCoroutine(PingPongWithDelayReversed());
        }
    }

    private IEnumerator MoveFromTo(Vector3 startPosition, Vector3 endPosition, float time)
    {
        var currentTime = 0f;
        while (currentTime < time)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, 1 - (time - currentTime) / time);
            currentTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPosition;
    }

    //i was a bit lazy, so i just copypasted coroutine and reversed movement points to change direction
    private IEnumerator PingPongWithDelay()
    {
        while (true)
        {
            yield return MoveFromTo(new Vector3(0f, gameObject.transform.position.y, gameObject.transform.position.z), new Vector3(8f, gameObject.transform.position.y, gameObject.transform.position.z), speed);
            yield return new WaitForSeconds(0.5f);
            yield return MoveFromTo(new Vector3(8f, gameObject.transform.position.y, gameObject.transform.position.z), new Vector3(0f, gameObject.transform.position.y, gameObject.transform.position.z), speed);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator PingPongWithDelayReversed()
    {
        while (true)
        {
            yield return MoveFromTo(new Vector3(8f, gameObject.transform.position.y, gameObject.transform.position.z), new Vector3(0f, gameObject.transform.position.y, gameObject.transform.position.z), speed);
            yield return new WaitForSeconds(0.5f);
            yield return MoveFromTo(new Vector3(0f, gameObject.transform.position.y, gameObject.transform.position.z), new Vector3(8f, gameObject.transform.position.y, gameObject.transform.position.z), speed);
            yield return new WaitForSeconds(0.5f);
        }
    }

}
