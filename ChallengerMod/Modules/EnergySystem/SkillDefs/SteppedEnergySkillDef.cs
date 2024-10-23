using ChallengerMod.Survivors.Challenger;
using EntityStates;
using JetBrains.Annotations;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ChallengerMod.Modules.EnergySystem
{
    public class SteppedEnergySkillDef : EnergySkillDef
    {

        public override SkillDef.BaseSkillInstanceData OnAssigned([NotNull] GenericSkill skillSlot)
        {
            return new SteppedEnergySkillDef.StepInstanceData
            {
                energyController = skillSlot.characterBody.GetComponent<EnergyController>()
            };
        }

        public override bool IsReady([NotNull] GenericSkill skillSlot)
        {
            return base.IsReady(skillSlot) && GetEnergyController(skillSlot).CheckEnergy(flatEnergyCost, false);
        }

        public override EntityState InstantiateNextState([NotNull] GenericSkill skillSlot)
        {
            EntityState entityState = base.InstantiateNextState(skillSlot);
            SteppedEnergySkillDef.StepInstanceData instanceData = (SteppedEnergySkillDef.StepInstanceData)skillSlot.skillInstanceData;
            SteppedSkillDef.IStepSetter stepSetter = entityState as SteppedSkillDef.IStepSetter;
            if (stepSetter != null)
            {
                stepSetter.SetStep(instanceData.step);
            }
            return entityState;
        }

        public override void OnExecute([NotNull] GenericSkill skillSlot)
        {
            base.OnExecute(skillSlot);
            SteppedEnergySkillDef.StepInstanceData instanceData = (SteppedEnergySkillDef.StepInstanceData)skillSlot.skillInstanceData;
            instanceData.step++;
            if (instanceData.step >= this.stepCount)
            {
                instanceData.step = 0;
            }
        }
        public override void OnFixedUpdate([NotNull] GenericSkill skillSlot, float deltaTime)
        {
            base.OnFixedUpdate(skillSlot, deltaTime);
            if (skillSlot.CanExecute())
            {
                this.stepResetTimer += deltaTime;
            }
            else
            {
                this.stepResetTimer = 0f;
            }
            if (this.stepResetTimer > this.stepGraceDuration)
            {
                ((SteppedEnergySkillDef.StepInstanceData)skillSlot.skillInstanceData).step = 0;
            }
        }

        public int stepCount = 2;

        [Tooltip("The amount of time a step is 'held' before it resets. Only begins to count down when available to execute.")]
        public float stepGraceDuration = 0.1f;

        private float stepResetTimer;

        public class StepInstanceData : EnergySkillDef.InstanceData
        {
            public int step;
        }
    }
}
