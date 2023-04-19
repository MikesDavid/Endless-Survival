using UnityEngine;

public class DealDmg : MonoBehaviour
{
    public Rigidbody rb;
    public LayerMask whatIsPlayer;

    public int damage;
    public float maxLifietime;
    private void Update()
    {
        maxLifietime -= Time.deltaTime;
        if(maxLifietime <= 0) Destroy(gameObject);
        DealDamage();
    }

    public void DealDamage()
    {
        
        Collider[] enemies = Physics.OverlapSphere(transform.position, 0.1f, whatIsPlayer); ;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetComponent<Player>())
            {
                enemies[i].GetComponent<Player>().TakeDamage(damage);
                enemies[i].GetComponent<Player>().damaged = true;
                Destroy(gameObject);
            }
            
        }
        
    }

}
