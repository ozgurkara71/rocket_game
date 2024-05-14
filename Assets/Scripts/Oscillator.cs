using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    // movementFactor is restricted between 0 and 1
    [SerializeField] [Range(0, 1)] float movementFactor;

    Vector3 offset;

    float cycles;
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPosition = transform.position;
        movementVector.x = 8;
    }

    void Update()
    {
        // Use epsilon compare instead of zero compare. Floats may not be exactly equal at deep levels.
        if (period <= Mathf.Epsilon)
            return;
        // continually growing over time
        cycles = Time.time / period;
        // constant value of 6.283
        const float tau = Mathf.PI * 2;
        // going from -1 to 1
        float rawSinWave = Mathf.Sin(cycles * tau);
        // recalculated to go from 0 to 1 so its cleaner
        movementFactor = (rawSinWave + 1f) / 2f; 

        offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
