using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float distance;
    public int damage;
    public float lifeTime;
    public LayerMask whatIsSolid;

    public GameObject destroyEffect;
    
    // Start is called before the first frame update
    private void Start()
    {
        Invoke("DestroyProjectile",lifeTime);
    }

    
   private void Update()
   {
       RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, transform.up, distance ,whatIsSolid);
       if (hitinfo.collider != null)
       {
           if (hitinfo.collider.CompareTag("Enemy"))
               
           {
               Debug.Log("Enrmy Must Take damage");
               hitinfo.collider.GetComponent<Enemy>().TakeDamage(damage);
           }
           DestroyProjectile();
       }
        transform.Translate(transform.up * speed * Time.deltaTime);
    }

   void DestroyProjectile()
   { 
       Destroy(gameObject);
   }
}
