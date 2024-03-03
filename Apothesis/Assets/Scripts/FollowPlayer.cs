using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float smoothness = 0.5f; // Adjust this value to control the smoothness of the camera movement
    private Vector3 offset;
    private bool isLookingUp = false;
    private bool isLookingDown = false;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the velocity offset based on the player's horizontal velocity
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

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            isLookingUp = true;
            isLookingDown = false;
        }
        else
        {
            isLookingUp = false;
        }

        if (isLookingUp)
        {
            // Move the camera up smoothly by 0.1 units
            Vector3 targetPositionUp = transform.position + Vector3.up * 2f;
            transform.position = Vector3.Lerp(transform.position, targetPositionUp, smoothness * Time.deltaTime);
        }
        else
        {
            // Move the camera back down smoothly to its original position
            Vector3 targetPositionUp = player.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPositionUp, smoothness * Time.deltaTime);
        }
          if (Input.GetAxisRaw("Vertical") < 0)
        {
            isLookingDown = true;
            isLookingUp = false;
        }
        else
        {
            isLookingDown = false;
        }

        if (isLookingDown)
        {
            // Move the camera down smoothly by 0.1 units
            Vector3 targetPositionDown = transform.position + Vector3.down * 2f;
            transform.position = Vector3.Lerp(transform.position, targetPositionDown, smoothness * Time.deltaTime);
        }
        else
        {
            // Move the camera back up smoothly to its original position
            Vector3 targetPositionDown = player.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPositionDown, smoothness * Time.deltaTime);
        }
       
    
    }
}
