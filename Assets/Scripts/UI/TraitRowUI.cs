using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RPG.Stats;

namespace RPG.UI
{
    public class TraitRowUI : MonoBehaviour
    {
        [SerializeField] Trait trait;
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] Button minusButton;
        [SerializeField] Button plusButton;

        Traits playerTraits = null;

        private void Start() 
        {
            playerTraits = GameObject.FindGameObjectWithTag("Player").GetComponent<Traits>();
            minusButton.onClick.AddListener(() => Allocate(-1));
            plusButton.onClick.AddListener(() => Allocate(+1));
        }

        private void Update() 
        {
            minusButton.interactable = playerTraits.CanAssignPoints(trait, -1);
            plusButton.interactable = playerTraits.CanAssignPoints(trait, +1);

            scoreText.text = playerTraits.GetProposedPoints(trait).ToString();
        }

        public void Allocate(int points)
        {
            playerTraits.AssignPoints(trait, points);
        }
    }
}