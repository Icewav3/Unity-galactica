using Mechanics;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] protected float maxHealth = 100f;
    protected float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        if (currentHealth == 0)
            OnDeath();
    }

    public virtual void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    protected virtual void OnDeath()
    {
        Destroy(gameObject);
    }
}