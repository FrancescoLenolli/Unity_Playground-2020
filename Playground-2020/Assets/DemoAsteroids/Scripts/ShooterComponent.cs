using CoreCharacter;
using System.Collections;
using UnityEngine;

public class ShooterComponent : CharacterComponent
{
    [SerializeField] private float fireRate = .5f;
    [SerializeField] private Bullet bulletPrefab = null;
    [SerializeField] private Transform muzzle = null;
    private float cooldown = 0f;
    private bool canShoot = false;

    private void Update()
    {
        Ray ray = new Ray(muzzle.position, muzzle.forward);
        Debug.DrawLine(muzzle.position, ray.GetPoint(3));
    }

    public override void SetUp(CharacterControl character)
    {
        base.SetUp(character);
        character.inputComponent.OnContinuousAction1 += Shoot;
        StartCoroutine(ShootRoutine());
    }

    private void Shoot(bool canShoot)
    {
        this.canShoot = canShoot;
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            cooldown = Mathf.Clamp(cooldown - Time.deltaTime, 0f, fireRate);

            if (canShoot)
            {
                if (cooldown == 0f)
                {
                    Ray ray = new Ray(muzzle.position, muzzle.forward);
                    Vector3 direction = ray.GetPoint(3) - muzzle.position;

                    Bullet bullet = Instantiate(bulletPrefab, muzzle.position, bulletPrefab.transform.rotation, transform);
                    bullet.Init(direction);
                    cooldown = fireRate;
                }
            }

            yield return null;
        }
    }
}
