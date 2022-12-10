using UnityEngine;
using System.Collections;

/**
 *	Rapidly sets a light on/off.
 *	
 *	(c) 2015, Jean Moreno
**/

namespace PC.VFX
{
    /// <summary>
    /// Controls the flickering of the light for the muzzle flash.
    /// </summary>
    [RequireComponent(typeof(Light))]
    public class WFX_LightFlicker : MonoBehaviour
    {
        [SerializeField] private float _flickerTime = 0.05f;
        
        /// <summary>
        /// Flickers the light component of this object. 
        /// Object is meant to be a background light to muzzle flash
        /// </summary>
        /// <returns></returns>
        IEnumerator Flicker()
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;

            yield return new WaitForSeconds(_flickerTime);

            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
        }
    }
}
