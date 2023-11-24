 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private Camera camera;

    private Rigidbody playerRb;

    private void Awake() {
        playerRb = GetComponent<Rigidbody>();
        HideLockCursor();
    }

    private void HideLockCursor() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        // Input axis
        // a left (-),  d right (+)
        float horizontal = Input.GetAxis("Horizontal");
        // w up (+), s down (-)
        float vertical = Input.GetAxis("Vertical");

        // input * sumbu x camera
        Vector3 horizontalDirection = horizontal * camera.transform.right;
        // input * sumbu z camera
        Vector3 verticalDirection = vertical * camera.transform.forward;
        horizontalDirection.y = 0;
        verticalDirection.y = 0;

        // movement direction for third person camera
        Vector3 movementDirection = horizontalDirection + verticalDirection;

        // movement
        playerRb.velocity = movementDirection * speed * Time.fixedDeltaTime;
    }
}
