using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.Dialogue
{
    public class AIConversant : MonoBehaviour, IRayCastable
    {
        [SerializeField] Dialogue dialogue = null;
        [SerializeField] string AIName;

        public CursorType GetCursorType()
        {
            return CursorType.Dialogue;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (dialogue == null)
            {
                return false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
            }
            return true;
        }

        public string GetAIName()
        {
            return AIName;
        }
    }
}