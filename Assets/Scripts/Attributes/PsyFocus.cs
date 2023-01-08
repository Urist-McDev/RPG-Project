using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Utils;
using RPG.Stats;
using GameDevTV.Saving;

namespace RPG.Attributes
{
    public class PsyFocus : MonoBehaviour, ISaveable
    {
        LazyValue<float> focus;

        private void Awake() 
        {
            focus = new LazyValue<float>(GetMaxFocus);
        }

        private void Update() 
        {
            if (focus.value < GetMaxFocus())
            {
                focus.value += GetFocusRegen() * Time.deltaTime;
                if (focus.value > GetMaxFocus())
                {
                    focus.value = GetMaxFocus();
                }
            }
        }

        public float GetFocus()
        {
            return focus.value;
        }

        public float GetMaxFocus()
        {
            return GetComponent<BaseStats>().GetStat(Stat.PsyFocus);
        }

        public float GetFocusRegen()
        {
            return GetComponent<BaseStats>().GetStat(Stat.FocusRegenRate);
        }

        public bool UseFocus(float focusToUse)
        {
            if (focusToUse > focus.value)
            {
                return false;
            }
            focus.value -= focusToUse;
            return true;
        }

        public object CaptureState()
        {
            return focus.value;
        }

        public void RestoreState(object state)
        {
            focus.value = (float)state;
        }
    }
}