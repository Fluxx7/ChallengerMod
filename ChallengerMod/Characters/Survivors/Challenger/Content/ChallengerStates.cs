using ChallengerMod.Survivors.Challenger.SkillStates;

namespace ChallengerMod.Survivors.Challenger
{
    public static class ChallengerStates
    {
        public static void Init()
        {
            Modules.Content.AddEntityState(typeof(Bisect));

            Modules.Content.AddEntityState(typeof(Disect));

            Modules.Content.AddEntityState(typeof(Ignite));

            Modules.Content.AddEntityState(typeof(Overclock));

            Modules.Content.AddEntityState(typeof(OverclockBisect));

            Modules.Content.AddEntityState(typeof(OverclockDisect));

            Modules.Content.AddEntityState(typeof(Remediate));

            Modules.Content.AddEntityState(typeof(Underclock));
        }
    }
}
