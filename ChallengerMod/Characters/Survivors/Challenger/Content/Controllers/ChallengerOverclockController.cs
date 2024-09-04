using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using RoR2.UI;
using TMPro;
using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.UI;
using EntityStates;
using ChallengerMod.Survivors.Challenger;
using ChallengerMod.Survivors.Challenger.Components;
using ChallengerMod.Survivors.Challenger.SkillStates;
using ChallengerMod.Modules;
using ChallengerMod.Modules.Characters;
using UnityEngine.Networking;
using IL.RoR2.Skills;

namespace ChallengerMod.Survivors.Challenger
{
    internal class ChallengerOverclockController : MonoBehaviour
    {
        private static int state = 0;
        private static int new_state = 0;
        public void Awake() 
        {
        }
        //public void FixedUpdate()
        //{
        //    if (new_state != state) {
        //        switch (state) {
        //            case 0:
        //                Upclock();
        //                break;
        //            case 1:
        //                Downclock();
        //                break;
        //        }
                
        //    }
        //}
        //private void Upclock()
        //{

        //}

        
        //private void Downclock()
        //{
            
        //}

        public static void StartOverclock(BaseSkillState caller, GameObject gameObject)
        {
            new_state = 1;
            if (caller.isAuthority && caller.skillLocator)
            {
                caller.skillLocator.primary.SetSkillOverride(gameObject, ChallengerSurvivor.primaryOverclockSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                caller.skillLocator.secondary.SetSkillOverride(gameObject, ChallengerSurvivor.secondaryOverclockSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                caller.skillLocator.utility.SetSkillOverride(gameObject, ChallengerSurvivor.utilityOverclockSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                caller.skillLocator.special.SetSkillOverride(gameObject, ChallengerSurvivor.specialOverclockSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            }
        }
        public static void EndOverclock(BaseSkillState caller, GameObject gameObject)
        {
            new_state = 0;
            if (caller.isAuthority && caller.skillLocator)
            {
                caller.skillLocator.primary.UnsetSkillOverride(gameObject, ChallengerSurvivor.primaryOverclockSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                caller.skillLocator.secondary.UnsetSkillOverride(gameObject, ChallengerSurvivor.secondaryOverclockSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                caller.skillLocator.utility.UnsetSkillOverride(gameObject, ChallengerSurvivor.utilityOverclockSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                caller.skillLocator.special.UnsetSkillOverride(gameObject, ChallengerSurvivor.specialOverclockSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            }
        }
    }
}
