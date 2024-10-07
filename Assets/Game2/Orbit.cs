using UnityEngine;

public class OrbitingObject2D : MonoBehaviour
{
    public Transform centerObject;

    public float orbitSpeed = 50f;

    public float orbitDistance = 5f;

    private int direction = 1;

    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(orbitDistance, 0, 0);
    }

    void Update()
    {
        Vector3 newPosition = centerObject.position + offset;
        newPosition.z = 0; 
        transform.position = newPosition;

        transform.RotateAround(centerObject.position, Vector3.forward, direction * orbitSpeed * Time.deltaTime);

        offset = (transform.position - centerObject.position).normalized * orbitDistance;

        if (Input.GetMouseButtonDown(0))
        {
            ToggleOrbitDirection();
        }
    }

    void ToggleOrbitDirection()
    {
        direction *= -1; 
    }
}
