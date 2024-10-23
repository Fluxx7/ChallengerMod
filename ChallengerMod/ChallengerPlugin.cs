using BepInEx;
using ChallengerMod.Survivors.Challenger;
using R2API;
using R2API.Utils;
using RoR2;
using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

//rename this namespace
namespace ChallengerMod
{
    //[BepInDependency("com.rune580.riskofoptions", BepInDependency.DependencyFlags.SoftDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, MODNAME, MODVERSION)]
    public class ChallengerPlugin : BaseUnityPlugin
    {
        public const string MODUID = "com.fluxxi.challengermod";
        public const string MODNAME = "ChallengerMod";
        public const string MODVERSION = "0.0.1";
        public const string DEVELOPER_PREFIX = "FLUXXI";

        public static ChallengerPlugin instance;

        void Awake()
        {
            instance = this;

            //easy to use logger
            Log.Init(Logger);

            // used when you want to properly set up language folders
            Modules.Language.Init();

            // character initialization
            new ChallengerSurvivor().Initialize();

            Hook();

            // make a content pack and add it. this has to be last
            new Modules.ContentPacks().Initialize();
        }

        void Hook() {
            GlobalEventManager.onServerDamageDealt += GlobalEventManager_ServerDamageDealt;
        }

        private void GlobalEventManager_ServerDamageDealt(DamageReport damageReport)
        {
            if (DamageAPI.HasModdedDamageType(damageReport.damageInfo, ChallengerAssets.disectDmgType) && damageReport.victimBody != null)
            {
                damageReport.victimBody.gameObject.AddComponent<ChallengerDisectController>().attackerBody = damageReport.attackerBody;

            }
        }
    }
}
