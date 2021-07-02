using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayTriggerH2F1 : MonoBehaviour
{
    private PolygonCollider2D trigger;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<PolygonCollider2D>();
        trigger.enabled = false;
        Invoke("resetTrigger", 1f);
    }

    void resetTrigger() {
        trigger.enabled = true;
    }
}
