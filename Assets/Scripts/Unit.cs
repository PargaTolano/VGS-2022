using UnityEngine;

public class Unit : MonoBehaviour
{
    public float damage;
    public float currHealth;
    public float maxHealth;
    public void TakeDamage(float damage){
        currHealth -= damage;
        if( currHealth <= 0 ){
            currHealth = 0;
            Debug.Log( name + " is Dead...");
            Destroy(gameObject);
        }
    }
}
