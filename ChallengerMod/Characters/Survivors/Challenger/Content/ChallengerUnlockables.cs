using ChallengerMod.Survivors.Challenger.Achievements;
using RoR2;
using UnityEngine;

namespace ChallengerMod.Survivors.Challenger
{
    public static class ChallengerUnlockables
    {
        public static UnlockableDef characterUnlockableDef = null;
        public static UnlockableDef masterySkinUnlockableDef = null;

        public static void Init()
        {
            masterySkinUnlockableDef = Modules.Content.CreateAndAddUnlockbleDef(
                ChallengerMasteryAchievement.unlockableIdentifier,
                Modules.Tokens.GetAchievementNameToken(ChallengerMasteryAchievement.identifier),
                ChallengerSurvivor.instance.assetBundle.LoadAsset<Sprite>("texMasteryAchievement"));
        }
    }
}
