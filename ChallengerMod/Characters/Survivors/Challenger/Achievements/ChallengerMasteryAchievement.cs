using RoR2;
using ChallengerMod.Modules.Achievements;

namespace ChallengerMod.Survivors.Challenger.Achievements
{
    //automatically creates language tokens "ACHIEVMENT_{identifier.ToUpper()}_NAME" and "ACHIEVMENT_{identifier.ToUpper()}_DESCRIPTION" 
    [RegisterAchievement(identifier, unlockableIdentifier, null, 10, null)]
    public class ChallengerMasteryAchievement : BaseMasteryAchievement
    {
        public const string identifier = ChallengerSurvivor.CHALLENGER_PREFIX + "masteryAchievement";
        public const string unlockableIdentifier = ChallengerSurvivor.CHALLENGER_PREFIX + "masteryUnlockable";

        public override string RequiredCharacterBody => ChallengerSurvivor.instance.bodyName;

        //difficulty coeff 3 is monsoon. 3.5 is typhoon for grandmastery skins
        public override float RequiredDifficultyCoefficient => 3;
    }
}