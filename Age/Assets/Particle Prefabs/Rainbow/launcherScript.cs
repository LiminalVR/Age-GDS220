using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launcherScript : MonoBehaviour {

    public GameObject rainbow;
    public float shootForce = 0f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectile = (GameObject)Instantiate(
                rainbow, transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * shootForce);
        }
	}
}
