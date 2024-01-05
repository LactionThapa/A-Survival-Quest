using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 userInput;
    private Transform playerCamera;
    private int health;
    [SerializeField] public Weapon Weapon;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField, Range(0, 1000)] int maxHealth;
    [SerializeField] private GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main.transform;
        slider.maxValue = maxHealth;
        health = 10;
        slider.value = health;
        text.text = $"{health}";
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forwardVector = playerCamera.forward;
        forwardVector.y = 0.0f;

        Vector3 rightVector = playerCamera.right;
        rightVector.y = 0.0f;

        Vector3 moveDirection = forwardVector * userInput.y + rightVector * userInput.x;

        rb.velocity = moveDirection * 8.0f;
    }

    public void OnMove(InputValue value)
    {
        userInput = value.Get<Vector2>();
    }

    public void Heal(int healthRecoveryAmount)
    {
        if (healthRecoveryAmount + health > maxHealth)
            health = maxHealth;
        else
            health += healthRecoveryAmount;
        slider.value = health;
        text.text = $"{health}";
    }

    public void DealDamage(int damage)
    {
        if (health - damage <= 0)
        {
            health = 0;
            gameOverPanel.SetActive(true);
        }
        else
            health -= damage;
        slider.value = health;
        text.text = $"{health}";
    }

}
