using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
	public Transform BottomPosition;
	public Transform BottomLeftPosition;
	public Transform BottomRightPosition;
	public Transform LeftPosition;
	public Transform RightPosition;
	public Transform TopPosition;
	public Transform TopRightPosition;
	public Transform TopLeftPosition;

	public Transform player;
	private Rigidbody2D rb;
	public float teleportDistance;
    public GameObject sad;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.position, transform.position) > teleportDistance) {
        	Transform positionVector = getPosition(GetDirection(player.position));
        	sad.transform.position = positionVector.position;
            sad.SetActive(true);
            StartCoroutine (ss ());
    		transform.position = positionVector.position;
    	}
    }

	IEnumerator ss() {
        yield return new WaitForSeconds (1);
        sad.SetActive(false);
    }

    Transform getPosition(Vector2 lastFacingDirection) {
		if (lastFacingDirection.x == 0 && lastFacingDirection.y == -1)  {
			return BottomPosition;
		}
		if (lastFacingDirection.x == -1 && lastFacingDirection.y == -1)  {
			return BottomLeftPosition;
		}
		if (lastFacingDirection.x == 1 && lastFacingDirection.y == -1)  {
			return BottomRightPosition;
		}
		if (lastFacingDirection.x == -1 && lastFacingDirection.y == 0)  {
			return LeftPosition;
		}
		if (lastFacingDirection.x == 1 && lastFacingDirection.y == 0)  {
			return RightPosition;
		}
		if (lastFacingDirection.x == 0 && lastFacingDirection.y == 1)  {
			return TopPosition;
		}
		if (lastFacingDirection.x == -1 && lastFacingDirection.y == 1)  {
			return TopLeftPosition;
		}
		if (lastFacingDirection.x == 1 && lastFacingDirection.y == 1)  {
			return TopRightPosition;
		}
		return transform;
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
