using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Trigger Animation", menuName = "Abilities/Effects/Trigger Animation", order = 0)]
    public class TriggerAnimation : EffectStrategy
    {
        [SerializeField] string animationTrigger;

        public override void StartEffect(AbilityData data, Action finished)
        {
            Animator animator = data.GetUser().GetComponent<Animator>();
            animator.SetTrigger(animationTrigger);
            finished();
        }
    }
}