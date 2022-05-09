using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSphereScript : MonoBehaviour
{
    private float speed;

    private void Start()
    {
        speed = Random.Range(2f, 5f);
        int direction = Random.Range(0, 2);
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
