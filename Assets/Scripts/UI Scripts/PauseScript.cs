using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaskedMischiefNamespace
{
    public class PauseScript : MonoBehaviour
    {
        private GameObject canvas;
        // Start is called before the first frame update
        void Start()
        {
            canvas = GameObject.Find("canvas");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void togglePause()
        {

        }
    }
}