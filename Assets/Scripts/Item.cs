using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject CatchFX;
    public float value = 1;

    public Action<PlayerScript, float> OnPick;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerScript>())
        {
            OnPick?.Invoke(collision.gameObject.GetComponent<PlayerScript>(), value);
            Pick();
        }
    }

    protected virtual void Pick()
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
