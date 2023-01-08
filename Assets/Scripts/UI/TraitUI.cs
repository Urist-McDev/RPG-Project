using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RPG.Stats;

namespace RPG.UI
{
    public class TraitUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI unassignedPointsText;
        [SerializeField] Button commitButton;

        Traits playerTraits = null;

        private void Start()
        {
            playerTraits = GameObject.FindGameObjectWithTag("Player").GetComponent<Traits>();
            commitButton.onClick.AddListener(playerTraits.Commit);
        }

        private void Update() 
        {
            unassignedPointsText.text = playerTraits.GetUnassignedPoints().ToString();
        }
    }
}