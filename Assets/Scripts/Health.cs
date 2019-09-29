using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private GameObject deathVFX;

    public void DealDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            TriggerDeathVFX();
            Destroy(gameObject);
        }
    }

    public void TriggerDeathVFX()
    {
        if (!deathVFX)
            return;

        GameObject deathVFXObject = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(deathVFXObject, 1f);
    }
}
