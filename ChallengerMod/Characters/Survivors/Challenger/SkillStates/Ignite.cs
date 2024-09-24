using EntityStates;
using ChallengerMod.Survivors.Challenger;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace ChallengerMod.Survivors.Challenger.SkillStates
{
    public class Ignite : GenericProjectileBaseState
    {
        public static float BaseDuration = 0.65f;
        //delays for projectiles feel absolute ass so only do this if you know what you're doing, otherwise it's best to keep it at 0
        public static float BaseDelayDuration = 0.0f;

        public static float DamageCoefficient = 16f;
        private bool canFire;

        public override void OnEnter()
        {
            canFire = ChallengerSystemsController.UseEnergy(ChallengerStaticValues.igniteEnergyCost);
            if (canFire)
            {
                this.skillLocator.FindSkill("Utility").stock--;
                projectilePrefab = ChallengerAssets.bombProjectilePrefab;
                //base.effectPrefab = Modules.Assets.SomeMuzzleEffect;
                //targetmuzzle = "muzzleThrow"

                attackSoundString = "HenryBombThrow";

                baseDuration = BaseDuration;
                baseDelayBeforeFiringProjectile = BaseDelayDuration;

                damageCoefficient = DamageCoefficient;
                //proc coefficient is set on the components of the projectile prefab
                force = 80f;

                //base.projectilePitchBonus = 0;
                //base.minSpread = 0;
                //base.maxSpread = 0;

                recoilAmplitude = 0.1f;
                bloom = 10;

            }
            base.OnEnter();
        }

        public override void FixedUpdate()
        {
            outer.SetNextStateToMain();
            base.FixedUpdate();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

        public override void PlayAnimation(float duration)
        {

            if (GetModelAnimator())
            {
                PlayAnimation("Gesture, Override", "ThrowBomb", "ThrowBomb.playbackRate", this.duration);
            }
        }
    }
}