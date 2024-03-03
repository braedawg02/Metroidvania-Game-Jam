using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Yapper : MonoBehaviour
{
    public GameObject spritePrefab;
    private GameObject spriteInstance;
    private bool isPlayerInside;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInside)
        {
                  if (Input.GetButtonDown("Interact") && spriteInstance != null)
            {
                //Destroys the sprite once the player presses the interact button
                Destroy(spriteInstance);

            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            // Calculate the position above this person
            Vector2 spritePosition = (Vector2)transform.position + Vector2.up * GetComponent<Collider2D>().bounds.size.y + Vector2.right * 0.5f + Vector2.down * 0.5f;

            // Instantiate the sprite GameObject at the calculated position
            spriteInstance = Instantiate(spritePrefab, spritePosition, Quaternion.identity);

            // Enable the Renderer of the sprite instance to make it visible
            spriteInstance.GetComponent<SpriteRenderer>().enabled = true;
      
            isPlayerInside = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            if (spriteInstance != null)
            {
                //Destroys the sprite once the player leaves the trigger
                Destroy(spriteInstance);
            }
            isPlayerInside = false;

        }
    }
}

