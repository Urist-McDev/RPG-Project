using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Stats;
using GameDevTV.Inventories;

namespace RPG.Inventories
{
    public class StatsEquipment : Equipment, IModifierProvider
    {
        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            foreach (var slot in GetAllPopulatedSlots())
            {
                var Item = GetItemInSlot(slot) as IModifierProvider;
                if (Item == null) continue;

                foreach (float modifier in Item.GetAdditiveModifiers(stat))
                {
                    yield return modifier;
                }
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            foreach (var slot in GetAllPopulatedSlots())
            {
                var Item = GetItemInSlot(slot) as IModifierProvider;
                if (Item == null) continue;

                foreach (float modifier in Item.GetPercentageModifiers(stat))
                {
                    yield return modifier;
                }
            }
        }
    }
}