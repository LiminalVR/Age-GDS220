using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherScript : MonoBehaviour
{
    public GameObject rainbow;
    public float shootForce = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectile = (GameObject)Instantiate(
            rainbow, transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * shootForce);
        }
    }
}