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
using EntityStates;
using UnityEngine.UIElements;

namespace ChallengerMod.Survivors.Challenger
{
    internal class ChallengerSystemsController : MonoBehaviour
    {
        public static CharacterBody characterBody;

        // Variables to fine-tune energy balancing
        public static float baseEnergy = 0f;
        public static float baseRecharge = 0f;
        public static float energyScalingRatio = 1f;
        public static float rechargeScalingRatio = 5f;
        public static float rechargeLevelScaling = 1f;
        public static float efficiencyScalingRatio = 3f;

        private static float currentEnergy;
        private static float currentDrain;
        public static float efficiency;
        static bool debug = true;
        private static float currentRecharge;

        private static float healBase = 30f;
        private static bool remediate = false;
        private static float currScale;

        public void Awake() 
        { 
            characterBody = GetComponent<CharacterBody>();
        }

        public void Start() 
        {
            currentEnergy = CalculateMaxEnergy();
            efficiency = CalculateEnergyEfficiency();
        }

        public void FixedUpdate() 
        {
            if (characterBody.hasEffectiveAuthority)
            {
                efficiency = CalculateEnergyEfficiency();
                float recharge = CalculateEnergyRecharge() / 60;
                if (!(currentEnergy >= CalculateMaxEnergy()) || recharge < 0)
                {
                    currentEnergy += CalculateEnergyRecharge()/60;
                }
                if (debug) { 
                    currentRecharge = CalculateEnergyRecharge();
                }

                if (remediate)
                {
                    Debug(ChallengerStaticValues.remediateEnergyCostPerSec * currScale / 60);
                    if (currentEnergy < ChallengerStaticValues.remediateEnergyCostPerSec * currScale / 60) {
                        ToggleRemediate();
                        
                    } else {
                        characterBody.healthComponent.Heal(currScale * healBase / 60, default(ProcChainMask));
                    }
                    
                }
            }
            
        }

        public static bool UseEnergy(float amount)
        {
            
            float cost = amount * efficiency;
            if (debug)
            {
                Debug(cost);
            }
            if (currentEnergy < cost)
            {
                return false;
            }
            else 
            {
                currentEnergy -= cost;
                return true;
            }
        }

        public static bool CheckEnergy(float amount)
        {
            
            
            float cost = amount * efficiency;
            if (debug)
            {
                Debug(cost);
            }
            if (currentEnergy < cost)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void AddEnergyDrain(float amount) {
            currentDrain += amount;
        }

        public static void RemoveEnergyDrain(float amount)
        {
            currentDrain -= amount;
        }

        public static void ToggleRemediate(float scale = 1f) {
            
            float drain = ChallengerStaticValues.remediateEnergyCostPerSec*scale;
            if (remediate)
            {
                characterBody.RemoveBuff(ChallengerBuffs.armorBuff);
                currentDrain -= drain*currScale;
                characterBody.armor -= 300 * currScale;
                remediate = false;
            } else
            {
                characterBody.AddBuff(ChallengerBuffs.armorBuff);
                currentDrain += drain*scale;
                characterBody.armor += 300 * scale;
                currScale = scale;
                remediate = true;
            }
        }

        /*
         *                                                    100
         *  Energy Usage = Base Energy Usage * ---------------------------------
         *                                    100 + (crit * efficiencyScalingRatio)
         */
        public float CalculateEnergyEfficiency() {
            return 100 / (100 + ((characterBody.crit - characterBody.baseCrit) * efficiencyScalingRatio));
        }
        public float CalculateEnergyRecharge()
        {
            return baseRecharge + rechargeLevelScaling*(characterBody.level - 1) + (characterBody.attackSpeed * rechargeScalingRatio) - (currentDrain * efficiency);
        }
        public float CalculateMaxEnergy()
        {
            return baseEnergy + (characterBody.maxHealth * energyScalingRatio);
        }

        private static void Debug(float consumption) {
            Chat.AddMessage("Current Energy: " + currentEnergy + ", Current Drain: " + currentDrain + ", Current Efficiency: " + efficiency + ", Energy Consumption: " + (consumption) + ", Current Recharge: " + currentRecharge);
        }

        public static void StartOverclock(BaseSkillState caller, GameObject gameObject)
        {
            if (caller.isAuthority && caller.skillLocator)
            {
                caller.skillLocator.primary = caller.skillLocator.FindSkill("OverclockPrimary");
                caller.skillLocator.secondary = caller.skillLocator.FindSkill("OverclockSecondary");
                caller.skillLocator.utility = caller.skillLocator.FindSkill("OverclockUtility");
                caller.skillLocator.special = caller.skillLocator.FindSkill("OverclockSpecial");
            }
        }
        public static void EndOverclock(BaseSkillState caller, GameObject gameObject)
        {
            if (caller.isAuthority && caller.skillLocator)
            {
                caller.skillLocator.primary = caller.skillLocator.FindSkill("Primary");
                caller.skillLocator.secondary = caller.skillLocator.FindSkill("Secondary");
                caller.skillLocator.utility = caller.skillLocator.FindSkill("Utility");
                caller.skillLocator.special = caller.skillLocator.FindSkill("Special");
            }
        }
    }
}
