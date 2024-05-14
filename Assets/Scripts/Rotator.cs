using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotationX = 0.5f;

    void Update()
    {
        transform.Rotate(rotationX, 0, 0);
    }
}
