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

namespace ChallengerMod.Survivors.Challenger.SkillStates
{
    public class Bisect : BaseSkillState, RoR2.Skills.SteppedSkillDef.IStepSetter
    {
        public int swingIndex;

        protected string hitboxGroupName = "SwordGroup";

        protected DamageType damageType = DamageType.Generic;
        protected float damageCoefficient = ChallengerStaticValues.bisectDamageCoefficient;
        protected float procCoefficient = 1f;
        protected float pushForce = 300f;
        protected Vector3 bonusForce = Vector3.zero;
        protected float baseDuration = 2f;

        protected float attackStartPercentTime = 0.2f;
        protected float attackEndPercentTime = 0.4f;

        protected float earlyExitPercentTime = 0.4f;

        protected float attackRecoil = 0.75f;
        private float range = 256f;
        private float force = 800f;

        protected float hitHopVelocity = 4f;

        protected string swingSoundString = "";
        protected string hitSoundString = "";
        protected string muzzleString = "SwingCenter";
        protected string playbackRateParam = "Slash.playbackRate";
        protected GameObject swingEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/TracerGoldGat");
        protected GameObject hitEffectPrefab;
        protected NetworkSoundEventIndex impactSound = NetworkSoundEventIndex.Invalid;

        public float duration;
        private bool hasFired;
        protected bool inHitPause;
        private bool hasHopped;
        protected float stopwatch;
        protected Animator animator;

        public override void OnEnter()
        {
            base.OnEnter();
            duration = baseDuration / attackSpeedStat;
            animator = GetModelAnimator();
            StartAimMode(0.5f + duration, false);

            PlayAttackAnimation();

            
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
                new BulletAttack
                {
                    bulletCount = 1,
                    aimVector = aimRay.direction,
                    origin = aimRay.origin,
                    damage = damageCoefficient * damageStat,
                    damageColorIndex = DamageColorIndex.Default,
                    damageType = DamageType.Generic,
                    falloffModel = BulletAttack.FalloffModel.None,
                    maxDistance = range,
                    force = force,
                    hitMask = LayerIndex.CommonMasks.bullet,
                    minSpread = 0f,
                    maxSpread = 0f,
                    isCrit = false,
                    owner = gameObject,
                    muzzleName = muzzleString,
                    smartCollision = true,
                    procChainMask = default,
                    procCoefficient = procCoefficient,
                    radius = 0.75f,
                    sniper = false,
                    stopperMask = LayerIndex.CommonMasks.bullet,
                    weapon = null,
                    tracerEffectPrefab = swingEffectPrefab,
                    spreadPitchScale = 1f,
                    spreadYawScale = 1f,
                    queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                    hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FirePistol2.hitEffectPrefab,
                }.Fire();
                hasFired = true;
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