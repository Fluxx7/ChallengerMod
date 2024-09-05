 using ChallengerMod.Modules.BaseStates;
using EntityStates;
using RoR2;
using UnityEngine;

namespace ChallengerMod.Survivors.Challenger.SkillStates
{
    public class Remediate : BaseSkillState
    {
        public static float baseDuration = 10f;

        private float duration;

        public override void OnEnter()
        {
            base.OnEnter();
            ChallengerOverclockController.EndOverclock(this, gameObject);
            duration = baseDuration / attackSpeedStat;
            characterBody.AddBuff(ChallengerBuffs.armorBuff);
        }

        public override void OnExit()
        {
            characterBody.RemoveBuff(ChallengerBuffs.armorBuff);
            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (fixedAge >= duration && isAuthority)
            {
                outer.SetNextStateToMain();
                return;
            }
        }

        
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}