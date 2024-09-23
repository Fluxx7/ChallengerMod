using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using RoR2.UI;
using TMPro;
using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace ChallengerMod.Survivors.Challenger
{
    internal class ChallengerOverlayController : NetworkBehaviour
    {
        private HUD hud = null;

        private void Awake() {
            On.RoR2.UI.HUD.Awake += HookDisplay;
        }

        private void HookDisplay(On.RoR2.UI.HUD.orig_Awake orig, RoR2.UI.HUD self) {
            orig(self);
            hud = self;
            GameObject energyHUD = new GameObject("EnergyHUD");
            energyHUD.transform.SetParent(hud.mainContainer.transform);
            RectTransform rectTransform = energyHUD.AddComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.sizeDelta = Vector2.zero;
            rectTransform.anchoredPosition = Vector2.zero;
        }

        private void OnDestroy()
        {
            On.RoR2.UI.HUD.Awake -= HookDisplay;
        }
    }
}
