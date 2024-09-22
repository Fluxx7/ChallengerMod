using System;
using ChallengerMod.Modules;
using ChallengerMod.Survivors.Challenger.Achievements;

namespace ChallengerMod.Survivors.Challenger
{
    public static class ChallengerTokens
    {
        public static void Init()
        {
            AddHenryTokens();

            ////uncomment this to spit out a lanuage file with all the above tokens that people can translate
            ////make sure you set Language.usingLanguageFolder and printingEnabled to true
            //Language.PrintOutput("Henry.txt");
            ////refer to guide on how to build and distribute your mod with the proper folders
        }

        public static void AddHenryTokens()
        {
            string prefix = ChallengerSurvivor.CHALLENGER_PREFIX;

            string desc = "Challenger is an aggressive survivor focused on extreme damage output<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine
             + "< ! > Running out of energy? Health increases the maximum amount, attack speed increases the recharge speed, and critical strike chance lowers the energy consumption of attacks" + Environment.NewLine + Environment.NewLine
             + "< ! > Disect slashes more times the more current health you have compared to the enemy, so it works well on small or low health enemies. Ignite is better for tanky enemies, but it has a small area of effect so don't use it for crowds" + Environment.NewLine + Environment.NewLine
             + "< ! > Overclocked Disect creates a structure to attack everything in a radius around it. Hit the structure with Ignite to blow up the entire area!" + Environment.NewLine + Environment.NewLine
             + "< ! > Overclocked Bisect takes a while to use, so only use it when you aren't in immediate danger" + Environment.NewLine + Environment.NewLine;

            string outro = "..and so he left, seeking for a stronger opponent.";
            string outroFailure = "..and so he vanished, forever a blank slate.";

            Language.Add(prefix + "NAME", "Challenger");
            Language.Add(prefix + "DESCRIPTION", desc);
            Language.Add(prefix + "SUBTITLE", "The Forsaken");
            Language.Add(prefix + "LORE", "sample lore");
            Language.Add(prefix + "OUTRO_FLAVOR", outro);
            Language.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            Language.Add(prefix + "MASTERY_SKIN_NAME", "Alternate");
            #endregion

            #region Passive
            Language.Add(prefix + "PASSIVE_NAME", "Battery");
            Language.Add(prefix + "PASSIVE_DESCRIPTION", "Challenger's attacks consume energy, which recharges over time. <style=cIsDamage>Challenger's attacks cannot critically strike</style>");
            #endregion

            #region Primary
            Language.Add(prefix + "PRIMARY_BISECT_NAME", "Bisect");
            Language.Add(prefix + "PRIMARY_BISECT_DESCRIPTION", $"Fire a slash of energy through the air for <style=cIsDamage>{100f * ChallengerStaticValues.bisectDamageCoefficient}% damage</style>");
            #endregion

            #region Secondary
            Language.Add(prefix + "SECONDARY_DISECT_NAME", "Disect");
            Language.Add(prefix + "SECONDARY_DISECT_DESCRIPTION", Tokens.agilePrefix + $"Target a nearby enemy, slashing them repeatedly for <style=cIsDamage>{100f * ChallengerStaticValues.disectDamageCoefficient}% damage</style>.");
            #endregion

            #region Utility
            Language.Add(prefix + "UTILITY_IGNITE_NAME", "Incinerate");
            Language.Add(prefix + "UTILITY_IGNITE_DESCRIPTION", Tokens.ignitePrefix + $"Charge and fire a bolt of flame, dealing <style=cIsDamage>{100f * ChallengerStaticValues.igniteBaseDamageCoefficient}% to {100f * ChallengerStaticValues.igniteBaseDamageCoefficient * ChallengerStaticValues.igniteChargeMultiplier}% damage</style> based off charge time");
            #endregion

            #region Special
            Language.Add(prefix + "SPECIAL_OVERCLOCK_NAME", "Overclock");
            Language.Add(prefix + "SPECIAL_OVERCLOCK_DESCRIPTION", Tokens.agilePrefix + "Overclock your next ability, changing the effects but consuming more energy.");
            #endregion

            #region Overclock
            #region Primary
            Language.Add(prefix + "PRIMARY_OVERCLOCK_BISECT_NAME", "Overclocked Bisect");
            Language.Add(prefix + "PRIMARY_OVERCLOCK_BISECT_DESCRIPTION", $"Calibrate your sensors, then unleash a large slash in a targeted area, dealing <style=cIsDamage>{100f * ChallengerStaticValues.overBisectDamageCoefficient}% damage and bypassing armor</style>");
            #endregion

            #region Secondary
            Language.Add(prefix + "SECONDARY_OVERCLOCK_DISECT_NAME", "Overclocked Disect");
            Language.Add(prefix + "SECONDARY_OVERCLOCK_DISECT_DESCRIPTION", $"Charge to designate a region around you. All enemies in this region will continuously be hit by Disect. Once this ability ends, your weapons overheat and <style=cIsDamage>attacks can't be used for {ChallengerStaticValues.overDisectCooldown} seconds</style>");
            #endregion

            #region Utility
            Language.Add(prefix + "UTILITY_REMEDIATE_NAME", "Remediate");
            Language.Add(prefix + "UTILITY_REMEDIATE_DESCRIPTION", Tokens.agilePrefix + $"Activate your repair systems, gaining armor and healing rapidly");
            #endregion

            #region Special
            Language.Add(prefix + "SPECIAL_UNDERCLOCK_NAME", "Cancel Overclock");
            Language.Add(prefix + "SPECIAL_UNDERCLOCK_DESCRIPTION", Tokens.agilePrefix + "Switch back to your normal moves");
            #endregion
            #endregion

            #region Achievements
            Language.Add(Tokens.GetAchievementNameToken(ChallengerMasteryAchievement.identifier), "Challenger: Mastery");
            Language.Add(Tokens.GetAchievementDescriptionToken(ChallengerMasteryAchievement.identifier), "As Challenger, beat the game or obliterate on Monsoon.");
            #endregion
        }
    }
}
