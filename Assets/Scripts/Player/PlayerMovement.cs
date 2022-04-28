using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputMaster;

public class PlayerMovement : MonoBehaviour
{
    [Header("Animation")]
    public Animator bodyAnimatorController;

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

        if (bodyAnimatorController == null)
            bodyAnimatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var v = movement.ReadValue<Vector2>();
        var newPosition = new Vector2(
            Mathf.Max(screenBoundaries.xMin, Mathf.Min(screenBoundaries.xMax, transform.position.x + v.normalized.x * speed * Time.deltaTime)),
            Mathf.Max(screenBoundaries.yMin, Mathf.Min(screenBoundaries.yMax, transform.position.y + v.normalized.y * speed * Time.deltaTime)));

        transform.position = newPosition;

        if (bodyAnimatorController)
            bodyAnimatorController.SetInteger("yMovement", v.y == 0 ? 0 : (int)Mathf.Sign(v.y));
    }
}
