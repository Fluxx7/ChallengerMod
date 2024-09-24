 using ChallengerMod.Modules.BaseStates;
using EntityStates;
using IL.RoR2.Skills;
using RoR2;
using UnityEngine;
using RoR2.Audio;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using UnityEngine.Networking;
using static UnityEngine.ParticleSystem.PlaybackState;
using RoR2.Projectile;

namespace ChallengerMod.Survivors.Challenger.SkillStates
{
    public class Bisect : GenericProjectileBaseState, RoR2.Skills.SteppedSkillDef.IStepSetter
    {
        public int swingIndex;

        protected string hitboxGroupName = "SwordGroup";

        protected DamageType damageType = DamageType.Generic;
        protected float DamageCoefficient = ChallengerStaticValues.bisectDamageCoefficient;
        protected float procCoefficient = 1f;
        protected float pushForce = 300f;
        protected float BaseDuration = 2f;

        protected float attackStartPercentTime = 0.2f;
        protected float attackEndPercentTime = 0.4f;

        protected float earlyExitPercentTime = 0.4f;

        protected float attackRecoil = 0.75f;
        private float Force = 200f;

        protected float hitHopVelocity = 4f;

        protected string swingSoundString = "";
        protected string hitSoundString = "";
        protected string muzzleString = "SwingCenter";
        protected string playbackRateParam = "Slash.playbackRate";
        protected GameObject swingEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/TracerGoldGat");
        protected GameObject slashPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/TracerGoldGat");
        protected GameObject hitEffectPrefab;
        protected NetworkSoundEventIndex impactSound = NetworkSoundEventIndex.Invalid;

        public float Furation;
        private bool hasFired;
        private bool canFire;
        protected bool inHitPause;
        protected float Stopwatch;
        protected Animator animator;

        public override void OnEnter()
        {
            base.OnEnter();
            duration = baseDuration / attackSpeedStat;
            canFire = ChallengerSystemsController.UseEnergy(ChallengerStaticValues.bisectEnergyCost);
            animator = GetModelAnimator();
            if (canFire)
            {
                StartAimMode(0.5f + duration, false);

                PlayAttackAnimation();
            }
            
        }

        protected virtual void PlayAttackAnimation()
        {
            PlayCrossfade("Gesture, Override", "Slash" + (1 + swingIndex), playbackRateParam, duration, 0.05f);
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        protected virtual void PlaySwingEffect()
        {
            EffectManager.SimpleMuzzleFlash(swingEffectPrefab, gameObject, muzzleString, false);
        }

        private void FireAttack()
        {
            if (isAuthority)
            {

                Ray aimRay = GetAimRay();
                projectilePrefab = ChallengerAssets.slashProjectilePrefab;
                baseDuration = BaseDuration;
                baseDelayBeforeFiringProjectile = 0;

                damageCoefficient = DamageCoefficient;
                //proc coefficient is set on the components of the projectile prefab
                force = Force;

                //base.projectilePitchBonus = 0;
                //base.minSpread = 0;
                //base.maxSpread = 0;

                recoilAmplitude = 0.1f;
                bloom = 10;
            }
        }

        private void EnterAttack()
        {
            hasFired = true;
            Util.PlayAttackSpeedSound(swingSoundString, gameObject, attackSpeedStat);

            PlaySwingEffect();

            if (isAuthority)
            {
                AddRecoil(-1f * attackRecoil, -2f * attackRecoil, -0.5f * attackRecoil, 0.5f * attackRecoil);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!canFire) {
                outer.SetNextStateToMain();
                return;
            }

            stopwatch += Time.deltaTime;

            bool fireStarted = stopwatch >= duration * attackStartPercentTime;
            bool fireEnded = stopwatch >= duration * attackEndPercentTime;

            //to guarantee attack comes out if at high attack speed the stopwatch skips past the firing duration between frames
            if (fireStarted && !fireEnded || fireStarted && fireEnded && !hasFired)
            {
                if (!hasFired)
                {
                    EnterAttack();
                    FireAttack();
                }
            }

            if (stopwatch >= duration && isAuthority)
            {
                outer.SetNextStateToMain();
                return;
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            if (stopwatch >= duration * earlyExitPercentTime)
            {
                return InterruptPriority.Any;
            }
            return InterruptPriority.Skill;
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

        public void SetStep(int i)
        {
            swingIndex = i;
        }
    }
}