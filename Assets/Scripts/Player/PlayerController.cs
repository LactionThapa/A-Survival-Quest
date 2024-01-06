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
    public bool glockIsAvailable = false;
    public bool mp5IsAvailable = false;
    [SerializeField] public Weapon Weapon;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField, Range(0, 1000)] int maxHealth;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private List<Weapon> WeaponSlots = new List<Weapon>();
    [field: SerializeField] public AudioSource AudioSource { get; private set; }
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip healingClip;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main.transform;
        slider.maxValue = maxHealth;
        health = 10;
        slider.value = health;
        text.text = $"{health}";
        FirstPersonController.JumpAction += Jump;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forwardVector = playerCamera.forward;
        forwardVector.y = 0.0f;

        Vector3 rightVector = playerCamera.right;
        rightVector.y = 0.0f;

        Vector3 moveDirection = forwardVector * userInput.y + rightVector * userInput.x;
        Vector3 velocity = moveDirection * 8.0f;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (mp5IsAvailable)
                ChangeWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (glockIsAvailable)
                ChangeWeapon(1);
        }
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
        AudioSource.clip = healingClip;
        AudioSource.Play();
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

    private void ChangeWeapon(int slotNumber)
    {
        Weapon?.gameObject.SetActive(false);
        Weapon = WeaponSlots[slotNumber];
        Weapon.gameObject.SetActive(true);
    }

    private void Jump()
    {
        AudioSource.clip = jumpClip;
        AudioSource.Play();
    }

    internal void AddWeapon()
    {
        AudioSource.clip = healingClip;
        AudioSource.Play();
    }
}
