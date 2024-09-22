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
            var poisonDef = new DotController.DotDef
            {
                interval = 0.1f,
                damageCoefficient = AssassinStaticValues.poisonDOTDamageCoef,
                damageColorIndex = DamageColorIndex.Poison,
                associatedBuff = poisonDebuff,
                terminalTimedBuffDuration = 10f,
                resetTimerOnAdd = true
            };

            poisonDoT = DotAPI.RegisterDotDef(poisonDef);
        }
    }
}
