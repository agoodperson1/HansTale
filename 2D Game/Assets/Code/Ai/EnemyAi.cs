using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAi : MonoBehaviour
{

	public Transform target;

	public float speed = 600f;
	public float nextWaypointDistance = .5f;

	Path path;
	int currentWaypoint = 0;
	bool reachedEndOfPath = false;

	public Transform enemy;

	Seeker seeker;
	Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }


    void UpdatePath() {
    	if (seeker.IsDone()) {
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    	}
    }


    void OnPathComplete(Path p) {
    	if (!p.error) {
    		path = p;
    		currentWaypoint = 0;
    	}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null) {
        	return;
        }


        if (currentWaypoint >= path.vectorPath.Count) {
        	reachedEndOfPath = true;
        	return;
        } else {
        	reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        
        if (distance < nextWaypointDistance) {
        	currentWaypoint++;
        }

         if(force.x >= 0.01f) {

        	enemy.localScale = new Vector3(-1f, 1f, 1f);

        } else if (force.x <= -0.01f) {

        	enemy.localScale = new Vector3(1f, 1f, 1f);
        	
    }
        }
    }
    

