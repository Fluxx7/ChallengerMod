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

        public static void Init(AssetBundle assetBundle)
        {
            armorBuff = Modules.Content.CreateAndAddBuff("ChallengerRemediate",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/HiddenInvincibility").iconSprite,
                Color.white,
                false,
                false);
        }
  

    }
}
