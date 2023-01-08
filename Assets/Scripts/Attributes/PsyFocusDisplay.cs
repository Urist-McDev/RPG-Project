using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class PsyFocusDisplay : MonoBehaviour
    {
        PsyFocus psyFocus;

        private void Awake()
        {
            psyFocus = GameObject.FindWithTag("Player").GetComponent<PsyFocus>();
        }

        private void Update()
        {
            GetComponent<Text>().text = string.Format("{0:0}%", psyFocus.GetFocus());
        }
    }
}