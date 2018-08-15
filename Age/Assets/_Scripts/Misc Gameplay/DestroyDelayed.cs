using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelayed : MonoBehaviour {

	void Start ()
    {
        Invoke("DestroyMe", 9f);
	}
	
	void DestroyMe()
    {
        Destroy(gameObject);
    }
}
