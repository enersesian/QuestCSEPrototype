using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour {

    // "Bobbing" animation from 1D Perlin noise.

    // Range over which height varies.
    public float heightScale = 1.0f;

    // Distance covered per second along X axis of Perlin plane.
    public float xScale = 1.0f;

    public float min = -1f, max = 1f;

    public bool isSent = false;

    private float height;

    private void Start()
    {
        xScale *= Random.Range(0.8f, 1.2f);
    }

    void Update()
    {
        if (!isSent) height = (heightScale * Mathf.PerlinNoise(Time.time * xScale, 0.0f) - 1f) / 2f;
        else height = transform.localPosition.y + 0.01f;

        Vector3 pos = transform.localPosition;
        pos.y = height;
        transform.localPosition = pos;

    }
}
