using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootInput : MonoBehaviour
{
    public Shoot shoot;
    public InputActionReference toggleRef = null;
    public void Awake()
    {
        toggleRef.action.started += Shoot;
    }

    private void OnDestroy()
    {
        toggleRef.action.started -= Shoot;
    }
    
    private void Shoot(InputAction.CallbackContext context)
    {
        shoot.Launch();
    }
}
