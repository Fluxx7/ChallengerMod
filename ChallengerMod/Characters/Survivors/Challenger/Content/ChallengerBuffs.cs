using RoR2;
using R2API;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System;

namespace ChallengerMod.Survivors.Challenger
{
    public static class ChallengerBuffs
    {
        // armor buff gained during remediate
        public static BuffDef armorBuff;
        public static BuffDef disectDebuff;

        public static DotController.DotIndex disectDoT;

        public static void Init(AssetBundle assetBundle)
        {
            armorBuff = Modules.Content.CreateAndAddBuff("ChallengerRemediate",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/HiddenInvincibility").iconSprite,
                Color.white,
                false,
                false);
            disectDebuff = Modules.Content.CreateAndAddBuff("ChallengerDisect",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/HiddenInvincibility").iconSprite,
                Color.white,
                true,
                true);
            AddDoTs();
        }

        private static void AddDoTs() {
            var disectDef = new DotController.DotDef
            {
                interval = 0.1f,
                damageCoefficient = ChallengerStaticValues.disectDamageCoefficient,
                damageColorIndex = DamageColorIndex.WeakPoint,
                associatedBuff = disectDebuff,
                terminalTimedBuffDuration = 10f,
                resetTimerOnAdd = true
            };

            disectDoT = DotAPI.RegisterDotDef(disectDef, DisectBehavior, DisectVisual);
        }

        public static void DisectBehavior(RoR2.DotController self, RoR2.DotController.DotStack dotStack) { 

        }

        public static void DisectVisual(RoR2.DotController self)
        {

        }
    }
}
