using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using RoR2.UI;
using TMPro;
using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.UI;

namespace ChallengerMod.Survivors.Challenger
{
    internal class ChallengerEnergyController : MonoBehaviour
    {
        public CharacterBody characterBody;

        // Variables to fine-tune energy balancing
        public static float baseEnergy = 0f;
        public static float baseRecharge = 0f;
        public static float energyScalingRatio = 1f;
        public static float rechargeScalingRatio = 5f;
        public static float efficiencyScalingRatio = 1f;

        public static float currentEnergy;
        public static float currentDrain;
        public static float efficiency;
        static bool debug = true;
        private static float currentRecharge;

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
                if (!(currentEnergy >= CalculateMaxEnergy()))
                {
                    currentEnergy += CalculateEnergyRecharge()/60;
                }
                if (debug) { 
                    currentRecharge = CalculateEnergyRecharge();
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
            return baseRecharge + (characterBody.attackSpeed * rechargeScalingRatio) - (currentDrain * efficiency);
        }
        public float CalculateMaxEnergy()
        {
            return baseEnergy + (characterBody.maxHealth * energyScalingRatio);
        }

        private static void Debug(float consumption) {
            Chat.AddMessage("Current Energy: " + currentEnergy + ", Current Drain: " + currentDrain + ", Current Efficiency: " + efficiency + ", Energy after Usage: " + (currentEnergy - consumption) + ", Current Recharge: " + currentRecharge);
        }
    }
}
