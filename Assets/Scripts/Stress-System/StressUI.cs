using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TrojanMouse.StressSystem
{
    public class StressUI : MonoBehaviour
    {
        [SerializeField] Slider fillbar;
        [SerializeField] float smoothingSpeed;
        float velocity;

        private void Awake()
        {
            fillbar = (!fillbar) ? GetComponent<Slider>() : fillbar;
        }

        void Update()
        {
            fillbar.value = Mathf.SmoothDamp(fillbar.value, Stress.current.average / 100, ref velocity, smoothingSpeed);
        }
    }
}