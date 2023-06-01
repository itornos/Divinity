using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class DamageablePJ : MonoBehaviour
    {
        [Tooltip("Multiplier to apply to the received damage")]
        public float DamageMultiplier = 1f;

        [Range(0, 1)]
        [Tooltip("Multiplier to apply to self damage")]
        public float SensibilityToSelfdamage = 0.5f;

        public HealthPj Health { get; private set; }

        void Awake()
        {
            // find the health component either at the same level, or higher in the hierarchy
            Health = GetComponent<HealthPj>();
            if (!Health)
            {
                Health = GetComponentInParent<HealthPj>();
            }
        }

        public void InflictDamage(float damage, bool isExplosionDamage, GameObject damageSource)
        {
            if (Health)
            {
                var totalDamage = damage;

                // skip the crit multiplier if it's from an explosion
                if (!isExplosionDamage)
                {
                    totalDamage *= DamageMultiplier;
                }

                // potentially reduce damages if inflicted by self
                if (Health.gameObject == damageSource)
                {
                    totalDamage *= SensibilityToSelfdamage;
                }

                // apply the damages
                Health.TakeDamage(totalDamage, damageSource);
            }
        }
    }
}
