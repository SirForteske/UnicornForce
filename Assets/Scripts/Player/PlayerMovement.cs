using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputMaster;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public Rect screenBoundaries;

    private InputMaster inputMaster;
    private InputAction movement;

    private void Awake()
    {
        inputMaster = new InputMaster();
    }
    // Start is called before the first frame update
    void Start()
    {
        movement = inputMaster.Player.Movement;
        movement.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        var v = movement.ReadValue<Vector2>();
        var newPosition = new Vector2(
            Mathf.Max(screenBoundaries.xMin, Mathf.Min(screenBoundaries.xMax, transform.position.x + v.normalized.x * speed * Time.deltaTime)),
            Mathf.Max(screenBoundaries.yMin, Mathf.Min(screenBoundaries.yMax, transform.position.y + v.normalized.y * speed * Time.deltaTime)));

        transform.position = newPosition;
        /*
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
       
        transform.position = new Vector3(newX, newY, transform.position.z); */
    }
}
