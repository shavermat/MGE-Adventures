using UnityEngine;

public class Infiniterotation : MonoBehaviour
{
    public int rotationSpeed;

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }
}
