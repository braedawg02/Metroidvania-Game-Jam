using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float smoothness = 0.5f; // Adjust this value to control the smoothness of the camera movement
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
void Update()
{
    Vector3 velocityOffset = new Vector3(player.GetComponent<Rigidbody2D>().velocity.x, 0, 0);
    Vector3 targetPosition = player.transform.position + offset + velocityOffset;

    // Calculate the distance between the current camera position and the target position
    float distance = Vector3.Distance(transform.position, targetPosition);

    // Check if the player is moving
    if (distance > 0.01f)
    {
        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
    }
    else
    {
        // If the player is not moving, snap the camera to the target position
        transform.position = targetPosition;
    }
}
}
