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
        private readonly float stackCoef = 0.5f;
        private float interval;
        public DamageInfo info;
        private GameObject attackerObject;
        

        private void Start() 
        {
            victimBody = GetComponent<CharacterBody>();
            stacks = (40/(1 + stackCoef * (victimBody.healthComponent.health/attackerBody.healthComponent.health)));
            interval = 0.66f / stacks;
            attackerObject = attackerBody.gameObject;
        }

        private void FixedUpdate() 
        {
            timer += Time.fixedDeltaTime;
            if (timer >= interval)
            {
                if (attackerBody != null && attackerObject != null)
                {
                    info = new DamageInfo()
                    {
                        attacker = attackerObject,
                        crit = false,
                        damage = ChallengerStaticValues.disectDamageCoefficient * attackerBody.damage,
                        damageColorIndex = DamageColorIndex.WeakPoint,
                        force = Vector3.zero,
                        procCoefficient = 0.5f,
                        procChainMask = default,
                        damageType = DamageType.Generic,
                        position = victimBody.corePosition,
                        dotIndex = DotController.DotIndex.None,
                        inflictor = attackerObject
                    };

                    victimBody.healthComponent.TakeDamageProcess(info);
                    GlobalEventManager.instance.OnHitEnemy(info, victimBody.gameObject);
                }
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
