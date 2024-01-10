using Content;
using UnityEngine;

namespace Mechanics
{
    public class HealthManager : MonoBehaviour
    {
        private Block _blockScript;
        private float _currentHealth;
        private float _maxHealth;

        /// <summary>
        /// Takes the specified amount of damage and updates the current health.
        /// If the current health becomes zero or negative, destroy the block.
        /// </summary>
        /// <param name="incomingDamage">The amount of damage to be taken.</param>
        public void TakeDamage(float incomingDamage)
        {
            _currentHealth -= incomingDamage;
            if (_currentHealth <= 0)
            {
                _blockScript.DestroyBlock();
            }
        }
        public void ReceiveHealing(float incomingHealing)
        {
            _currentHealth += incomingHealing;
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }
        
        private void Start()
        {
            Init();
        }

        /// <summary>
        /// Initializes the current component.
        /// </summary>
        private void Init()
        {
            _blockScript = GetComponent<Block>();
            if (_blockScript == null)
            {
                Debug.LogError("HealthManager script attached to non-block object");
            }
            else
            {
                _maxHealth = _blockScript.maxHealth;
                _currentHealth = _maxHealth;
            }
            
        }
    }
}