using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cakeslice
{
    public class Toggle : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            GetComponent<Outline>().enabled = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void onoff()
        {
            GetComponent<Outline>().enabled = !GetComponent<Outline>().enabled;
        }

        public void LookKeyON()
        {
            GameObject.Find("KEY").GetComponent<Outline>().enabled = true;
        }
        public void LookKeyOFF()
        {
            GameObject.Find("KEY").GetComponent<Outline>().enabled = false;
        }
    }
}