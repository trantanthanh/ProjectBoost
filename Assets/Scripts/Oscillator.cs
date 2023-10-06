using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField][Range(0, 1)] float movementFactor;
    [SerializeField] float speed = 20f;
    float degree = 0;
    float radians = 0;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoving();
    }

    private void UpdateMoving()
    {
        degree += speed * Time.deltaTime;
        degree %= 360;
        radians = degree * Mathf.PI / 180;
        Vector3 offset = movementVector * movementFactor * Mathf.Sin(radians);
        transform.position = startingPosition + offset;
    }
}
