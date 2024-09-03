using ChallengerMod.Survivors.Challenger.SkillStates;

namespace ChallengerMod.Survivors.Challenger
{
    public static class ChallengerStates
    {
        public static void Init()
        {
            Modules.Content.AddEntityState(typeof(Bisect));

            Modules.Content.AddEntityState(typeof(Disect));

            Modules.Content.AddEntityState(typeof(Roll));

            Modules.Content.AddEntityState(typeof(Ignite));
        }
    }
}
