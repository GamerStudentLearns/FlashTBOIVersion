using UnityEngine;
public class RoomCameraController : MonoBehaviour
{
    public float moveSpeed = 5f;         // speed of camera movement
    public Vector3 offset = new Vector3(0, 1f, 0); // shift camera slightly up
    private Transform targetRoom;
    void LateUpdate()
    {
        if (targetRoom == null) return;
        Vector3 targetPosition = new Vector3(
            targetRoom.position.x + offset.x,
            targetRoom.position.y + offset.y,
            transform.position.z + offset.z // usually keep z the same
        );
        // Smoothly move camera toward the room
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
    public void FocusRoom(Transform roomTransform)
    {
        targetRoom = roomTransform;
    }

    public void SnapToTarget(Transform newTarget)
    {
        targetRoom = newTarget;
        transform.position = new Vector3(newTarget.position.x, newTarget.position.y, transform.position.z);

    }
}