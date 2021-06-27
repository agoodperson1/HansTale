using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateHealth : MonoBehaviour
{
	public GameObject prefabHealth;

    // Start is called before the first frame update
    void Start()
    {
    	Instantiate(prefabHealth, new Vector3(0, 10, 0), Quaternion.identity);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
