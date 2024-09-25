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
using ChallengerMod.Modules.EnergySystem;

namespace ChallengerMod.Survivors.Challenger
{
    internal class ChallengerSystemsController : EnergyController
    {

        private float healBase = 30f;
        private bool remediate = false;
        private float currScale;

        public override void Start()
        {
            base.Start();
            debug = true;
        }

        public override void FixedUpdate() 
        {
            base.FixedUpdate();
            if (characterBody.hasEffectiveAuthority && remediate)
            {
                Debug(ChallengerStaticValues.remediateEnergyCostPerSec * currScale / 60);
                if (currentEnergy < ChallengerStaticValues.remediateEnergyCostPerSec * currScale / 60) {
                    ToggleRemediate();
                        
                } else {
                    characterBody.healthComponent.Heal(currScale * healBase / 60, default(ProcChainMask));
                }
            }
            
        }

        public void ToggleRemediate(float scale = 1f)
        {
            float drain = ChallengerStaticValues.remediateEnergyCostPerSec * scale;
            if (remediate)
            {
                characterBody.RemoveBuff(ChallengerBuffs.armorBuff);
                currentDrain -= drain * currScale;
                characterBody.armor -= 300 * currScale;
                remediate = false;
            }
            else
            {
                characterBody.AddBuff(ChallengerBuffs.armorBuff);
                currentDrain += drain * scale;
                characterBody.armor += 300 * scale;
                currScale = scale;
                remediate = true;
            }
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
