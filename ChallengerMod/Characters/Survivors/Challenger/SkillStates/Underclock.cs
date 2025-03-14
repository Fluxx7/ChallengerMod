﻿using EntityStates;
using ChallengerMod.Survivors.Challenger;
using ChallengerMod.Survivors.Challenger.Components;
using ChallengerMod.Survivors.Challenger.SkillStates;
using ChallengerMod.Modules;
using ChallengerMod.Modules.Characters;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using IL.RoR2.Skills;

namespace ChallengerMod.Survivors.Challenger.SkillStates
{
    public class Underclock : BaseSkillState
    {
        private float duration = 0.6f;
        public override void OnEnter()
        {
            ChallengerSystemsController.EndOverclock(this, gameObject);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (isAuthority && fixedAge >= duration)
            {
                outer.SetNextStateToMain();
                return;
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
        
    }
}