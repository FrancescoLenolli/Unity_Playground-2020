using CoreCharacter;
using UnityEngine;

public class ThrusterMovementComponent : RigidbodySpaceMovement
{
    [SerializeField] private GameObject thrusterParticle = null;

    protected override void SetThrust(bool canThrust)
    {
        base.SetThrust(canThrust);

        if (canThrust && !thrusterParticle.activeSelf)
            thrusterParticle.SetActive(true);
        if (!canThrust && thrusterParticle.activeSelf)
            thrusterParticle.SetActive(false);

    }
}
