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
                caller.skillLocator.primary = caller.skillLocator.FindSkill("OverclockPrimary");
                caller.skillLocator.secondary = caller.skillLocator.FindSkill("OverclockSecondary");
                caller.skillLocator.utility = caller.skillLocator.FindSkill("OverclockUtility");
                caller.skillLocator.special = caller.skillLocator.FindSkill("OverclockSpecial");
            }
        }
        public static void EndOverclock(BaseSkillState caller, GameObject gameObject)
        {
            new_state = 0;
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
