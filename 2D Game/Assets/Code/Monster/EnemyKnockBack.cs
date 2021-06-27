using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour
{
    public float damage;
    public float thrust;
    public MonsterHealthBar monsterHealthBar;
    public HealthBar healthBar;
    private Animator attackAnimator;

    private float attackTimer = 0.5f;
    private bool attackFlag = true;

    void Start()
    {
        //InvokeRepeating("UpdateAI", 1f, 1f);
        attackAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (monsterHealthBar.GetHealthPercent() <= 0)
        {
            Destroy(gameObject);
        }

        if (attackFlag == false) {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f) {
                attackFlag = true;
            }
        }
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && attackFlag)
        {
            attackFlag = false;
            attackTimer = 0.5f;
            Rigidbody2D player = other.GetComponent<Rigidbody2D>();
            if (player != null)
            {
                ApplyForce(player, thrust);
            }
            Vector2 target = player.transform.position - transform.position;
            Vector2 direction = GetDirection(target.normalized);
            if (attackAnimator != null) {
                StartCoroutine(monsterAttack(direction));    
            }
            
            
            healthBar.Damage(damage);
        }

        /* if (other.gameObject.CompareTag("Monster")) {
			Rigidbody2D monster = other.GetComponent<Rigidbody2D>();
			if (monster != null) {
				ApplyForce(monster, 30);
			}
			} */
        }

        private IEnumerator monsterAttack(Vector2 direction)
        {

            attackAnimator.SetBool("Attacking", true);
            attackAnimator.SetFloat("AttackX", direction.x);
            attackAnimator.SetFloat("AttackY", direction.y);
            yield return new WaitForSeconds(0.1f);
            attackAnimator.SetBool("Attacking", false);
        }

        void ApplyForce(Rigidbody2D body, float thrustApplied)
        {
            Vector2 difference = body.transform.position - transform.position;
            difference = difference.normalized * thrustApplied;
            body.AddForce(difference, ForceMode2D.Impulse);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        void UpdateAI()
        {
        //gameObject.GetComponent<Pathfinding.AIPath>().enabled = true;

        }

        Vector2 GetDirection(Vector2 target)
        {
            Vector2 axis = new Vector2(1f, 0f);
            float angle = Vector2.Angle(axis, target);
            if (angle <= 45f && angle >= -45f)
            {
                return new Vector2(1f, 0f);
            }
            else if (angle > 45f && angle <= 135f)
            {
                return new Vector2(0f, 1f);
            }
            else if (angle < -45f && angle >= -135f)
            {
                return new Vector2(0f, -1f);
            }
            else
            {
                return new Vector2(-1f, 0f);
            }
        }


    }
