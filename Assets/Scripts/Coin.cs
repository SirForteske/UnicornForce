using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject CatchFX;
    public float PowerAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerScript>())
        {
            collision.gameObject.GetComponent<PlayerScript>().AddPower(PowerAmount);
            Pick();
        }
    }

    private void Pick()
    {
        GetComponent<Collider2D>().enabled = false;

        if (CatchFX != null)
        {
            var fx = Instantiate(
                CatchFX, 
                new Vector3(transform.position.x, transform.position.y - 1f*transform.localScale.y, transform.position.z),
                Quaternion.Euler(-90f, 0f, 0f));
            Destroy(fx, 1f);
        }

        Destroy(gameObject);
    }
}
