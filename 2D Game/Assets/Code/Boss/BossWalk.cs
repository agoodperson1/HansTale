using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalk : StateMachineBehaviour
{
    public float speed;
    public float attackRange;
    public float shootRange;

    private bool shoot = false;
    Transform player;
    Rigidbody2D rb;

    private float attackTimer = 0.3f;
    private bool attackFlag = true;
    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       Vector2 target = new Vector2(player.position.x, player.position.y);
       Vector2 newPosition = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
       Vector2 ownPosition = new Vector2(animator.transform.position.x, animator.transform.position.y);
       Vector2 direction = GetDirection(target - ownPosition);
       animator.SetFloat("moveX", direction.x);
       animator.SetFloat("moveY", direction.y);
       if (Vector2.Distance(player.position, rb.position) > shootRange && !BossCharging.charging) {
            rb.MovePosition(newPosition); 
       }
       
       if (Vector2.Distance(player.position, rb.position) <= attackRange && attackFlag) {
            attackFlag = false;
            attackTimer = 0.3f;
            shoot = false;
            animator.SetTrigger("Attack");
        }

        if (Vector2.Distance(player.position, rb.position) <= shootRange 
            && Vector2.Distance(player.position, rb.position) > attackRange && !BossCharging.charging) {
            shoot = true;
            animator.SetBool("Shoot", true);

        }

        if (BossCharging.charging) {
            rb.MovePosition(newPosition);
        }

        if (attackFlag == false) {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0.0f) {
                attackFlag = true;
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (shoot == true) {
          animator.SetBool("Shoot", false);
        } else {
            animator.ResetTrigger("Attack");
        }
    }

    Vector2 GetDirection(Vector2 target)
    {
        Vector2 axis = new Vector2(1f, 0f);
        float angle = Vector2.SignedAngle(axis, target);
            //float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;

        if (angle <= 30f && angle >= -30f) {
            return new Vector2(1f, 0f);
        } else if (angle > 30f && angle < 60f) {
            return new Vector2(1f, 1f);
        } else if (angle >= 60f && angle <= 120f) {
            return new Vector2(0f, 1f);
        } else if (angle > 120f && angle < 150f) {
            return new Vector2 (-1f, 1f);
        } else if (angle <-30f && angle > -60f) {
            return new Vector2(1f, -1f);
        } else if (angle <= -60f && angle >= -120f) {
            return new Vector2(0f, -1f);
        } else if (angle < -120f && angle > -150f) {
            return new Vector2(-1f, -1f);
        } else {
            return new Vector2(-1f, 0f);
        } 
    }


}
