using Content;
using UnityEngine;

namespace Mechanics
{
    /// <summary>
    /// Manages the health of a block in a game.
    /// </summary>
    public class HealthManager : MonoBehaviour
    {
        /// <summary>
        /// Represents the instance of a block script.
        /// </summary>
        private Block _blockScript;

        /// <summary>
        /// Represents the current health value of an entity.
        /// </summary>
        private float _currentHealth;

        /// <summary>
        /// The maximum health value for a character or entity.
        /// </summary>
        private float _maxHealth;

        /// <summary>
        /// This method is called to start the program.
        /// </summary>
        private void Start()
        {
            Init();
        }

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

        /// <summary>
        /// Applies healing to the character's current health.
        /// </summary>
        /// <param name="incomingHealing">The amount of healing to be applied.</param>
        public void ReceiveHealing(float incomingHealing)
        {
            _currentHealth += incomingHealing;
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }

        /// <summary>
        /// Initializes the current component.
        /// </summary>
        private void Init()
        {
            _blockScript = GetComponent<Block>();
            if (_blockScript == null)
            {
                Debug.LogError(
                    "HealthManager script attached to non-block object");
            }
            else
            {
                _maxHealth = _blockScript.maxHealth;
                _currentHealth = _maxHealth;
            }
        }
    }
}