using System;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount;
    public bool isFullHeal;
    public int soundToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(GameManager.instance.pickupEffect, transform.position + new Vector3(0f, 1f, 0f),
                transform.rotation);
            if (isFullHeal)
            {
                HealthManager.instance.ResetHealth();
            }
            else
            {
                HealthManager.instance.AddHealth(healAmount);
            }
            AudioManager.instance.PlaySfx(soundToPlay);
        }
    }
}
