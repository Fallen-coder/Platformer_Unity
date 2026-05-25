using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = new Vector3(
                target.position.x,
                target.position.y,
                -10f
            );

            transform.position = Vector3.Lerp(
                transform.position,
                newPosition,
                smoothSpeed * Time.deltaTime
            );
        }
    }
}