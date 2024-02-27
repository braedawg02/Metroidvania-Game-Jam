using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GlobalFeather : MonoBehaviour
{
    public static float fAmount = 0;
}
public class FeatherScript : MonoBehaviour
{
    
    [SerializeField] public GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
       Debug.Log(GlobalFeather.fAmount); 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            Debug.Log("Feather collected");
            GlobalFeather.fAmount ++;
            gameObject.SetActive(false);
        }
    }
}
