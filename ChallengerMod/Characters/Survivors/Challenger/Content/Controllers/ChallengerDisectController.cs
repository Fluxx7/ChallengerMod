using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using RoR2.UI;
using TMPro;
using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace ChallengerMod.Survivors.Challenger
{
    internal class ChallengerDisectController : NetworkBehaviour
    {
        public CharacterBody victimBody;
        public CharacterBody attackerBody;

        private float stacks;
        private float timer;
        private readonly float stackCoef = 0.25f;
        private float interval;
        public DamageInfo info;
        

        private void Start() 
        {
            victimBody = GetComponent<CharacterBody>();
            stacks = (20/(1 + stackCoef * (victimBody.healthComponent.health/attackerBody.healthComponent.health)));
            interval = 0.33f / stacks;
        }

        private void FixedUpdate() 
        {
            timer += Time.fixedDeltaTime;
            if (timer >= interval)
            {
                info = new DamageInfo()
                {
                    attacker = attackerBody.gameObject,
                    crit = false,
                    damage = ChallengerStaticValues.disectDamageCoefficient * attackerBody.damage,
                    damageColorIndex = DamageColorIndex.Heal,
                    force = Vector3.zero,
                    procCoefficient = 0.5f, //0.5% chance,
                    damageType = DamageType.Generic,
                    position = victimBody.healthComponent.body.corePosition,
                    dotIndex = DotController.DotIndex.None,
                    inflictor = attackerBody.gameObject
                };
                victimBody.healthComponent.TakeDamage(info);
                stacks--;
                timer = 0f;
            }
            if (stacks <= 0)
            {
                Destroy(this);
            }
            
        }

    }
}
