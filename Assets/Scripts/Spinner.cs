using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float rotationX = 0.1f;
    [SerializeField] float rotationY = 0.1f;
    [SerializeField] float rotationZ = 0.1f;

    void Update()
    {
        transform.Rotate(rotationX, rotationY, rotationZ);
    }
}
