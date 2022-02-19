using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroAttack : MonoBehaviour
{
    [System.Serializable]
    public class HitboxData{
        public Vector2 size;
        public float offset;
    }
    private Animator        animator;
    [SerializeField]
    public List<HitboxData> hitboxes;
    public LayerMask        whatIsEnemy;

    private bool isInAttack1, isInAttack2;
    private Unit unit;
    void Start()
    {
        animator = GetComponent<Animator>();
        unit = GetComponent<Unit>();
        isInAttack1 = isInAttack2 = false;
    }
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.R)){
            if( !isInAttack1 )
                animator.SetTrigger("Attack1");
            else if( !isInAttack2 )
                animator.SetBool("Attack2", true);
        }
    }
    private void OnDrawGizmos() {
        foreach( var hb in hitboxes ){
            Gizmos.DrawWireCube( transform.position + transform.right * hb.offset, hb.size );
        }
    }
    private void Attack(HitboxData hb){
        var hits = Physics2D.BoxCastAll(transform.position, hb.size, 0f, transform.right, hb.offset, whatIsEnemy.value);
        foreach( var hit in hits ){
            var enemyUnit  = hit.collider.GetComponent<Unit>();
            enemyUnit.TakeDamage(unit.damage);
        }
    }
    public void OnAttack1Animation(){
        Attack(hitboxes[0]);
    }
    public void OnAttack1Start(){
        isInAttack1 = true;
    }
    public void OnAttack1End(){
        isInAttack1 = false;
    }
    public void OnAttack2Animation(){
        Attack(hitboxes[1]);
    }
    public void OnAttack2Start(){
        isInAttack2 = true;
    }
    public void OnAttack2End(){
        animator.SetBool("Attack2", false);
        isInAttack1 = false;
        isInAttack2 = false;
    }
}
