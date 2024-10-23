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

namespace ChallengerMod.Modules.EnergySystem
{
    public abstract class EnergyController : MonoBehaviour
    {
        public CharacterBody characterBody;

        
        public float baseEnergy = 0f;

        public float baseRecharge = 0f;

        public float energyScalingRatio = 1f;

        public float rechargeScalingRatio = 5f;

        public float rechargeLevelScaling = 0.5f;

        public float efficiencyScalingRatio = 3f;

        protected float currentEnergy;
        protected float currentDrain;
        public float efficiency;
        protected bool debug;
        protected float currentRecharge;

        public virtual void Awake() 
        { 
            characterBody = GetComponent<CharacterBody>();
        }

        public virtual void Start() 
        {
            currentEnergy = CalculateMaxEnergy();
            efficiency = CalculateEnergyEfficiency();
        }

        public virtual void FixedUpdate() 
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
            }
            
        }

        public virtual bool UseEnergy(float amount, bool debug)
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
        public virtual bool UseEnergy(float amount) {  
            return UseEnergy(amount, debug); 
        }

        public virtual bool CheckEnergy(float amount, bool debug)
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
        public virtual bool CheckEnergy(float amount)
        {
            return UseEnergy(amount, debug);
        }

        public virtual void AddEnergyDrain(float amount) {
            currentDrain += amount;
        }

        public virtual void RemoveEnergyDrain(float amount)
        {
            currentDrain -= amount;
        }

        /*
         *                                                    100
         *  Energy Usage = Base Energy Usage * ---------------------------------
         *                                    100 + (crit * efficiencyScalingRatio)
         */
        public virtual float CalculateEnergyEfficiency() {
            return 100 / (100 + ((characterBody.crit - characterBody.baseCrit) * efficiencyScalingRatio));
        }
        public virtual float CalculateEnergyRecharge()
        {
            return baseRecharge + rechargeLevelScaling*(characterBody.level - 1) + (characterBody.attackSpeed * rechargeScalingRatio) - (currentDrain * efficiency);
        }
        public virtual float CalculateMaxEnergy()
        {
            return baseEnergy + (characterBody.maxHealth * energyScalingRatio);
        }

        protected virtual void Debug(float consumption)
        {
            Chat.AddMessage("Current Energy: " + currentEnergy + ", Current Drain: " + currentDrain + ", Current Efficiency: " + efficiency + ", Energy Consumption: " + (consumption) + ", Current Recharge: " + currentRecharge);
        }
    }
}
