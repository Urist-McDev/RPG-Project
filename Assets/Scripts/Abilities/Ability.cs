using GameDevTV.Inventories;
using UnityEngine;
using System.Collections.Generic;
using System;
using RPG.Attributes;
using RPG.Core;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "My Ability", menuName = "Abilities/Ability", order = 0)]
    public class Ability : ActionItem
    {
        [SerializeField] TargetingStrategy targetingStrategy;
        [SerializeField] FilterStrategy[] filterStrategies;
        [SerializeField] EffectStrategy[] effectStrategies;
        [SerializeField] float cooldownTime = 0;
        [SerializeField] float focusCost = 0;

        public override void Use(GameObject user)
        {
            PsyFocus psyFocus = user.GetComponent<PsyFocus>();

            if (psyFocus.GetFocus() < focusCost)
            {
                return;
            }

            CooldownStore cooldownStore = user.GetComponent<CooldownStore>();
            if (cooldownStore.GetTimeRemaining(this) > 0)
            {
                return;
            }

            AbilityData data = new AbilityData(user);

            ActionScheduler actionScheduler = user.GetComponent<ActionScheduler>();
            actionScheduler.StartAction(data);

            targetingStrategy.StartTargeting(data, () => TargetAquired(data));
        }

        private void TargetAquired(AbilityData data)
        {
            if (data.IsCancelled()) return;

            PsyFocus psyFocus = data.GetUser().GetComponent<PsyFocus>();
            if (!psyFocus.UseFocus(focusCost)) return;

            CooldownStore cooldownStore = data.GetUser().GetComponent<CooldownStore>();
            cooldownStore.StartCooldown(this, cooldownTime);

            foreach (var filterStrategy in filterStrategies)
            {
                data.SetTargets(filterStrategy.Filter(data.GetTargets()));
            }

            foreach (var effect in effectStrategies)
            {
                effect.StartEffect(data, EffectFinished);
            }
        }

        private void EffectFinished()
        {
            
        }
    }
}