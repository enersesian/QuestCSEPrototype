using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Transform startTransform, endTransform;
    private float animTime, waitTime;

    public void Move(Transform start, Transform end, float holdTime, float moveTime)
    {
        startTransform = start;
        endTransform = end;
        animTime = moveTime;
        waitTime = holdTime;
        StartCoroutine("Moving");
    }

    private IEnumerator Moving()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        float elapsedTime = 0f;
        while (Vector3.Distance(transform.position, endTransform.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(startTransform.position, endTransform.position, elapsedTime / animTime);
            transform.rotation = Quaternion.Lerp(startTransform.rotation, endTransform.rotation, elapsedTime / animTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
