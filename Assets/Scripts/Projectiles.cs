using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YS
{
    public class Projectiles : MonoBehaviour
    {
        Animator anim;
        BoxCollider2D boxCollider;

        public float speed;
        public float damage;
        public string blastAnim;

        public float  direction;
        public bool hit=false;

        private void Start()
        {
            anim = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
            direction = FindObjectOfType<PlayerStates>().transform.localScale.x;
            transform.parent = null;
            
        }
        private void FixedUpdate()
        {
            if(hit)
            {
                return;
            }
            gameObject.transform.Translate(Time.deltaTime*speed*direction,0,0);
        }

        public void Destroye()
        {
            Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            Debug.Log("hhh");
            hit=true;
            boxCollider.enabled = false;
            anim.Play(blastAnim);
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("hhh");
            hit = true;
            boxCollider.enabled = false;
            anim.Play(blastAnim);
        }



    }
}