using RoR2;
using UnityEngine;

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

        }
    }
}
