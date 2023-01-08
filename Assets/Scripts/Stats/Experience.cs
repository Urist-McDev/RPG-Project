using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Saving;
using System;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;
        
        public event Action onXPGained;

        private void Update() 
        {
            if (Input.GetKey(KeyCode.E))
            {
                GainExperience(Time.deltaTime * 10);
            }
        }

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onXPGained();
        }

        public float GetPoints()
        {
            return experiencePoints;
        }

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }
}