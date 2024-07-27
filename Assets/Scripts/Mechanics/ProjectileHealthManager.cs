namespace Mechanics
{
    public class ProjectileHealthManager : HealthManager
    {
        public void SetMaxHealth(float maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
        }

        public override void Heal(float amount) { } // Projectiles can't be healed
    }
}