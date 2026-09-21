using System;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Plain C# health pool: clamps to [0, max] and reports death.
    /// </summary>
    public class Health
    {
        public int Max { get; }
        public int Current { get; private set; }

        public bool IsDead => Current == 0;

        public event Action Died;

        public Health(int max)
        {
            if (max <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(max), "Max health must be positive.");
            }

            Max = max;
            Current = max;
        }

        public void TakeDamage(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Damage must not be negative.");
            }

            if (IsDead)
            {
                return;
            }

            Current = Mathf.Max(0, Current - amount);

            if (IsDead)
            {
                Died?.Invoke();
            }
        }

        public void Heal(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Heal must not be negative.");
            }

            if (IsDead)
            {
                return;
            }

            Current = Mathf.Min(Max, Current + amount);
        }
    }
}
