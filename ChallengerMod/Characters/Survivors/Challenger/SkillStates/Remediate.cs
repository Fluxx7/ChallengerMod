 using ChallengerMod.Modules.BaseStates;
using EntityStates;
using RoR2;
using UnityEngine;

namespace ChallengerMod.Survivors.Challenger.SkillStates
{
    public class Remediate : BaseSkillState
    {


        public override void OnEnter()
        {
            base.OnEnter();
            ChallengerSystemsController.EndOverclock(this, gameObject);
            ChallengerSystemsController.ToggleRemediate();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            outer.SetNextStateToMain();
            
        }

        
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}