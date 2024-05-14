using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OscillatorVertical : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    Vector3 offset;
    [SerializeField] float period = 2f;
    float cycles, rawSinWave;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon)
            return;
        cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinWave + 1) / 2f;

        offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
