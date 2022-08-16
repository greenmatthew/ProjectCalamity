using UnityEngine;
using System.Collections;

/**
 *	Rapidly sets a light on/off.
 *	
 *	(c) 2015, Jean Moreno
**/

namespace PC.VFX
{
    [RequireComponent(typeof(Light))]
    public class WFX_LightFlicker : MonoBehaviour
    {
        public float time = 0.05f;
        
        private float _timer;

        void OnEnable ()
        {
            _timer = time;
            //StartCoroutine("Flicker");
        }
        
        public IEnumerator Flicker()
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
            
            do
            {
                _timer -= Time.deltaTime;
                yield return null;
            }
            while(_timer > 0);
            _timer = time;

            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
        }
    }
}
