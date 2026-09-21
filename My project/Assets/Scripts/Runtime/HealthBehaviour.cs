using UnityEngine;

namespace Game
{
    /// <summary>
    /// Scene-facing wrapper around <see cref="Health"/> that despawns its
    /// GameObject once the pool is empty.
    /// </summary>
    public class HealthBehaviour : MonoBehaviour
    {
        [SerializeField]
        private int maxHealth = 100;

        public Health Health { get; private set; }

        private void Awake()
        {
            Health = new Health(maxHealth);
            Health.Died += OnDied;
        }

        private void OnDestroy()
        {
            if (Health != null)
            {
                Health.Died -= OnDied;
            }
        }

        public void TakeDamage(int amount)
        {
            Health.TakeDamage(amount);
        }

        private void OnDied()
        {
            Destroy(gameObject);
        }
    }
}
