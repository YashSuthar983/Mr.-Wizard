using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YS
{
    public class Damager : MonoBehaviour
    {
        public float obstaleDamage;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag=="Player")
            {
                PlayerStates playerstats= collision.GetComponent<PlayerStates>();
                playerstats.TakeDamage(obstaleDamage);
            }
        }
    }

}