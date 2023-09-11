using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject cut;
    [SerializeField] private GameObject bullet;
    private Rigidbody2D rb;
    private PlayerControls input;
    private Boolean haveBomb;
    private Boolean haveSword;
    private Boolean havePistol;
    private int activeWeapon;
    private PlayerMovement playerMovement;
    private GameObject ultimoataque;
    private void Awake()
    {
        haveSword = false;
        havePistol = false;
        haveBomb = false;
        rb = GetComponent<Rigidbody2D>();
        input = new PlayerControls();
        activeWeapon = 0;
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Attack.performed += OnAttack;
    }
    private void OnDisable()
    {
        input.Disable();
        input.Player.Attack.performed -= OnAttack;
    }
    void OnAttack(InputAction.CallbackContext context)
    {
        if (haveBomb)
        {
            ultimoataque = Instantiate(bomb, rb.transform.position + playerMovement.GetLastMove(), Quaternion.identity);
            ultimoataque.GetComponent<ThrowBomb>().SetMovimento(playerMovement.GetLastMove());
        }
        if (haveSword)
        {
            Instantiate(cut, rb.transform.position + playerMovement.GetLastMove(), Quaternion.identity);
        }
        if (havePistol)
        {
            ultimoataque = Instantiate(bullet, rb.transform.position + playerMovement.GetLastMove(), Quaternion.identity);
            ultimoataque.GetComponent<Bullet>().SetMovimento(playerMovement.GetLastMove());
        }
    }
    public void ResetWeapons()
    {
        haveSword = false;
        havePistol = false;
        haveBomb = false;
    }
    public int GetWeapon()
    {
        return activeWeapon;
    }
    public void SetWeapon(int w)
    {
        activeWeapon = w;
        if (w == 0)
        {
            ResetWeapons();
        }
        activeWeapon = w;
        if (w == 1)
        {
            ResetWeapons();
            haveSword = true;
        }
        activeWeapon = w;
        if (w == 2)
        {
            ResetWeapons();
            havePistol = true;
        }
        activeWeapon = w;
        if (w == 3)
        {
            ResetWeapons();
            haveBomb = true;
        }
    }
    public Rigidbody2D GetRigidbody()
    {
        return rb;
    }
}
