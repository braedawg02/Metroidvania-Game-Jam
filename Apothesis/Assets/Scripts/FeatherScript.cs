using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFeather : MonoBehaviour
{
    public static float fAmount = 0; // Global feather amount
}

public class FeatherScript : MonoBehaviour
{
    [SerializeField] public GameObject player; // Reference to the player object

    // Awake is called before the first frame update
    void Awake()
    {
        Debug.Log(GlobalFeather.fAmount); // Log the global feather amount
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            Debug.Log("Feather collected"); // Log that the feather is collected
            GlobalFeather.fAmount++; // Increase the global feather amount
            gameObject.SetActive(false); // Deactivate the feather game object
        }
    }
}
