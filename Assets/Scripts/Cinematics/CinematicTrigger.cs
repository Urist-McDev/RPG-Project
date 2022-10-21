using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private bool hasBeenSeen = true;

        private void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.tag == "Player" && hasBeenSeen == true)
            {
                GetComponent<PlayableDirector>().Play();
                hasBeenSeen = false;
            }
        }
    }
}