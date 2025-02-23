using RoR2;
using UnityEngine;
using ChallengerMod.Modules;
using System;
using RoR2.Projectile;
using static R2API.DamageAPI;

namespace ChallengerMod.Survivors.Challenger
{
    public static class ChallengerAssets
    {
        // particle effects
        public static GameObject swordSwingEffect;
        public static GameObject swordHitImpactEffect;

        public static GameObject bombExplosionEffect;

        // networked hit sounds
        public static NetworkSoundEventDef swordHitSoundEvent;

        //projectiles
        public static GameObject arrowProjectilePrefab;
        public static GameObject slashProjectilePrefab;

        private static AssetBundle _assetBundle;
        public static ModdedDamageType disectDmgType = ReserveDamageType();

        public static void Init(AssetBundle assetBundle)
        {

            _assetBundle = assetBundle;

            swordHitSoundEvent = Content.CreateAndAddNetworkSoundEventDef("HenrySwordHit");

            CreateEffects();

            CreateProjectiles();
        }

        #region effects
        private static void CreateEffects()
        {
            CreateBombExplosionEffect();

            swordSwingEffect = _assetBundle.LoadEffect("HenrySwordSwingEffect", true);
            swordHitImpactEffect = _assetBundle.LoadEffect("ImpactHenrySlash");
        }

        private static void CreateBombExplosionEffect()
        {
            bombExplosionEffect = _assetBundle.LoadEffect("BombExplosionEffect", "HenryBombExplosion");

            if (!bombExplosionEffect)
                return;

            ShakeEmitter shakeEmitter = bombExplosionEffect.AddComponent<ShakeEmitter>();
            shakeEmitter.amplitudeTimeDecay = true;
            shakeEmitter.duration = 0.5f;
            shakeEmitter.radius = 200f;
            shakeEmitter.scaleShakeRadiusWithLocalScale = false;

            shakeEmitter.wave = new Wave
            {
                amplitude = 1f,
                frequency = 40f,
                cycleOffset = 0f
            };

        }
        #endregion effects

        #region projectiles
        private static void CreateProjectiles()
        {
            CreateArrowProjectile();
            slashProjectilePrefab = Asset.LoadAndAddProjectilePrefab(_assetBundle, "primaryBisectSlash");
            slashProjectilePrefab.AddComponent<BisectBehaviour>();
            slashProjectilePrefab.GetComponent<ProjectileController>().ghostPrefab.AddComponent<BisectGhostBehaviour>();
        }
        private static void CreateArrowProjectile()
        {
            arrowProjectilePrefab = Asset.LoadAndAddProjectilePrefab(_assetBundle, "utilityIgniteArrow");
        }

        
        #endregion projectiles
    }
}
