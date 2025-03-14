﻿ using ChallengerMod.Modules.BaseStates;
using EntityStates;
using IL.RoR2.Skills;
using RoR2;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using RoR2.Audio;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using static UnityEngine.ParticleSystem.PlaybackState;
using RoR2.Projectile;
using System.Numerics;
using TMPro;

namespace ChallengerMod.Survivors.Challenger.SkillStates
{
    public class Bisect : BaseSkillState, RoR2.Skills.SteppedSkillDef.IStepSetter
    {
        public int swingIndex;
        
        private GameObject prefab = ChallengerAssets.slashProjectilePrefab;

        private bool hasFired = false;
        private float baseDuration = 0.8f;
        private float baseDelay = 0.1f;
        private float duration;
        private float force = 100f;
        private float damageCoef = ChallengerStaticValues.bisectDamageCoefficient;
        private Animator animator;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            base.characterBody.SetAimTimer(2f);
            this.animator = base.GetModelAnimator();
            PlayCrossfade("Gesture, Override", "Slash" + (swingIndex == 1 ? 2 : 1), "Slash.playbackRate", duration, 0.1f * duration); 
            
            

        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!this.hasFired && base.fixedAge >= this.duration * baseDelay)
            {
                //Util.PlayAttackSpeedSound(this.attackSoundString, base.gameObject, this.attackSoundPitch);
                this.FireSlash();
                this.hasFired = true;
            }
            if (base.isAuthority && base.fixedAge >= this.duration)
            {
                this.outer.SetNextStateToMain();
            }
                
            

            
        }

        public override void OnExit() 
        { 
            base.OnExit(); 
        }

        private void FireSlash()
        {
            if (this.hasFired)
            {
                return;
            } 
            Ray aimRay = base.GetAimRay();
            UnityEngine.Quaternion rotation = Util.QuaternionSafeLookRotation(aimRay.direction);
            switch (swingIndex)
            {
                case 0:
                    UnityEngine.Quaternion temp = UnityEngine.Quaternion.Euler(0, 0, -35);
                    rotation = rotation*temp;
                    break;
                case 1:
                    UnityEngine.Quaternion temp2 = UnityEngine.Quaternion.Euler(0, 0, 35);
                    rotation = rotation * temp2; 
                    break;
                default:
                    break;
            }
            if (base.isAuthority)
            {
                float num = this.damageStat * this.damageCoef;
                ProjectileManager.instance.FireProjectile(this.prefab, aimRay.origin, rotation, base.gameObject, num, this.force, RollCrit(), DamageColorIndex.Default, null, 250f);
            }
        }

        public void SetStep(int i)
        {
            swingIndex = i;
        }
        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write(swingIndex);
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            swingIndex = reader.ReadInt32();
        }
    }
}