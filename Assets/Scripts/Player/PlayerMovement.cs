using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public Rect screenBoundaries;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var newX = transform.position.x;
        var newY = transform.position.y;

        if(Input.GetKey(KeyCode.S))
        {
            newY = Mathf.Max(screenBoundaries.yMin, transform.position.y - speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            newY = Mathf.Min(screenBoundaries.yMax, transform.position.y + speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            newX = Mathf.Max(screenBoundaries.xMin, transform.position.x - speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            newX = Mathf.Min(screenBoundaries.xMax, transform.position.x + speed * Time.deltaTime);
        }

        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
