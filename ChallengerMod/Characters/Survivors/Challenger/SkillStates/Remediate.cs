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
            ChallengerSystemsController.AddEnergyDrain(ChallengerStaticValues.remediateEnergyCostPerSec);
            characterBody.AddBuff(ChallengerBuffs.armorBuff);
        }

        public override void OnExit()
        {
            ChallengerSystemsController.RemoveEnergyDrain(ChallengerStaticValues.remediateEnergyCostPerSec);
            characterBody.RemoveBuff(ChallengerBuffs.armorBuff);
            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!ChallengerSystemsController.CheckEnergy(ChallengerStaticValues.remediateEnergyCostPerSec/60)){
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