using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    [SerializeField] private float speed = 0;
    [SerializeField] private Camera camera;
    [SerializeField] private float powerUpDuration;
    [SerializeField] private int health;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Transform respawnPoint;

    private Rigidbody playerRb;
    private Coroutine powerUpCoroutine;
    private bool isPowerUpActive = false;

    public void Dead()
    {
        health -= 1;
        if (health > 0)
        {
            transform.position = respawnPoint.position;
        }
        else
        {
            health = 0;
            Debug.Log("Lose.");
        }
        UpdateUI();
    }

    public void PickPowerUp()
    {
        // pastikan stop jika ada coroutine yg sedang berjalan
        if (powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
        }
        powerUpCoroutine = StartCoroutine(StartPowerUp());
    }
    
    private IEnumerator StartPowerUp()
    {
        isPowerUpActive = true;
        if (OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }

        yield return new WaitForSeconds(powerUpDuration);
        isPowerUpActive = false;

        if (OnPowerUpStop != null)
        {
            OnPowerUpStop();
        }
    }

    private void Awake()
    {
        UpdateUI();
        playerRb = GetComponent<Rigidbody>();
        HideLockCursor();
    }

    private void HideLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

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

    private void OnCollisionEnter(Collision other)
    {
        if (isPowerUpActive)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }

    private void UpdateUI()
    {
        healthText.text = "Health: " + health;
    }
}
