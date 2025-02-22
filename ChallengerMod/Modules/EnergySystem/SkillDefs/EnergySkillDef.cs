using ChallengerMod.Survivors.Challenger;
using JetBrains.Annotations;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ChallengerMod.Modules.EnergySystem
{
    public class EnergySkillDef : SkillDef
    {
        public float flatEnergyCost = 0;

        public override SkillDef.BaseSkillInstanceData OnAssigned([NotNull] GenericSkill skillSlot)
        {
            return new EnergySkillDef.InstanceData
            {
                energyController = skillSlot.characterBody.GetComponent<EnergyController>()
            };
        }

        public override bool IsReady([NotNull] GenericSkill skillSlot)
        {
            return base.IsReady(skillSlot) && GetEnergyController(skillSlot).CheckEnergy(flatEnergyCost, false) ;
        }

        public override void OnExecute([NotNull] GenericSkill skillSlot)
        {
            base.OnExecute(skillSlot);
            GetEnergyController(skillSlot).UseEnergy(flatEnergyCost);
        }

        protected static EnergyController GetEnergyController([NotNull] GenericSkill skillSlot)
        {
            EnergyController energyController = ((EnergySkillDef.InstanceData)skillSlot.skillInstanceData).energyController;
            return energyController;
        }

        public class InstanceData : SkillDef.BaseSkillInstanceData
        {
            public EnergyController energyController { get; set; }
        }
        #region Helper Methods
        internal static T CreateEnergySkillDef<T>(SkillDefInfo skillDefInfo, float flatEnergyCost) where T : EnergySkillDef
        {
            //pass in a type for a custom skilldef, e.g. HuntressTrackingSkillDef
            T skillDef = ScriptableObject.CreateInstance<T>();

            skillDef.skillName = skillDefInfo.skillName;
            (skillDef as ScriptableObject).name = skillDefInfo.skillName;
            skillDef.skillNameToken = skillDefInfo.skillNameToken;
            skillDef.skillDescriptionToken = skillDefInfo.skillDescriptionToken;
            skillDef.icon = skillDefInfo.skillIcon;

            skillDef.activationState = skillDefInfo.activationState;
            skillDef.activationStateMachineName = skillDefInfo.activationStateMachineName;
            skillDef.interruptPriority = skillDefInfo.interruptPriority;

            skillDef.baseMaxStock = skillDefInfo.baseMaxStock;
            skillDef.baseRechargeInterval = skillDefInfo.baseRechargeInterval;

            skillDef.rechargeStock = skillDefInfo.rechargeStock;
            skillDef.requiredStock = skillDefInfo.requiredStock;
            skillDef.stockToConsume = skillDefInfo.stockToConsume;

            skillDef.dontAllowPastMaxStocks = skillDefInfo.dontAllowPastMaxStocks;
            skillDef.beginSkillCooldownOnSkillEnd = skillDefInfo.beginSkillCooldownOnSkillEnd;
            skillDef.canceledFromSprinting = skillDefInfo.canceledFromSprinting;
            skillDef.forceSprintDuringState = skillDefInfo.forceSprintDuringState;
            skillDef.fullRestockOnAssign = skillDefInfo.fullRestockOnAssign;
            skillDef.resetCooldownTimerOnUse = skillDefInfo.resetCooldownTimerOnUse;
            skillDef.isCombatSkill = skillDefInfo.isCombatSkill;
            skillDef.mustKeyPress = skillDefInfo.mustKeyPress;
            skillDef.cancelSprintingOnActivation = skillDefInfo.cancelSprintingOnActivation;
            skillDef.flatEnergyCost = flatEnergyCost;

            skillDef.keywordTokens = skillDefInfo.keywordTokens;

            ChallengerMod.Modules.Content.AddSkillDef(skillDef);


            return skillDef;
        }
        internal static EnergySkillDef CreateEnergySkillDef(SkillDefInfo skillDefInfo, float flatEnergyCost)
        {
            return CreateEnergySkillDef<EnergySkillDef>(skillDefInfo, flatEnergyCost);
        }
        #endregion
    }
}
