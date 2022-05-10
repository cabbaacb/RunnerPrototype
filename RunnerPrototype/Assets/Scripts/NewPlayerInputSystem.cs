using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerInputSystem : PlayerBehaviour
{
    private MovingControls movingcontrols;

    private void Awake()
    {
        movingcontrols = new MovingControls();
        maxHealth = health;

        movingcontrols.Player.Jumping.performed += _ => Jump();
    }

    private void FixedUpdate()
    {
        horizontalInput = movingcontrols.Player.Moving.ReadValue<float>();
        Moving();
    }

    private void OnEnable()
    {
        movingcontrols.Enable();
    }

    private void OnDisable()
    {
        movingcontrols.Disable();
    }

    private void OnDestroy()
    {
        movingcontrols.Dispose();
    }

}
