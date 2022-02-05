using UnityEngine;

public class Blade : Shootable
{
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = float.MaxValue;
        rigidbody.angularVelocity = transform.forward * spinSpeed;
    }
}
