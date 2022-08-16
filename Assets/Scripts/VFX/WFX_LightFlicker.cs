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
        
        /// <summary>
        /// Flickers the light component of this object. 
        /// Object is meant to be a background light to muzzle flash
        /// </summary>
        /// <returns> Iterator interface. Function is a coroutine. </returns>
        IEnumerator Flicker()
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;

            yield return new WaitForSeconds(time);

            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
        }
    }
}
