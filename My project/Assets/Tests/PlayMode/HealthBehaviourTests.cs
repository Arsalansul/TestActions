using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Game.Tests
{
    public class HealthBehaviourTests
    {
        private GameObject _target;

        [TearDown]
        public void TearDown()
        {
            if (_target != null)
            {
                Object.Destroy(_target);
            }
        }

        [UnityTest]
        public IEnumerator Awake_FillsHealthPoolToMax()
        {
            _target = new GameObject("Target");
            var behaviour = _target.AddComponent<HealthBehaviour>();

            yield return null;

            Assert.AreEqual(100, behaviour.Health.Max);
            Assert.AreEqual(100, behaviour.Health.Current);
            Assert.IsFalse(behaviour.Health.IsDead);
        }

        [UnityTest]
        public IEnumerator FatalDamage_DestroysGameObjectByNextFrame()
        {
            _target = new GameObject("Target");
            var behaviour = _target.AddComponent<HealthBehaviour>();
            yield return null;

            behaviour.TakeDamage(100);

            Assert.IsTrue(behaviour.Health.IsDead);
            Assert.IsTrue(_target != null, "Destroy is deferred to the end of the frame.");

            yield return null;

            Assert.IsTrue(_target == null, "GameObject should be destroyed after death.");
        }
    }
}
