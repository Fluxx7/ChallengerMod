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
    internal class BisectBehaviour : MonoBehaviour
    {

        public float scalar = 0.6f;

        private void Start()
        {
            transform.localScale = new Vector3(0.1f, 1f, 0.1f);

        }
        private void OnEnable()
        {
            transform.localScale = new Vector3(0.1f, 1f, 0.1f);

        }
        private void FixedUpdate()
        {

            transform.localScale += new Vector3(scalar, 0, scalar);
        }

    }
}
