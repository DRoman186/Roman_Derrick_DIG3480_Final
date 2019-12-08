using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
    public GameObject Gatling;
    public Transform shotSpawn;
    public Transform shotSpawn1;
    public Transform shotSpawn2;
    public float fireRate;
    public bool isPowered;

    private float nextFire;

    private void Start()
    {
        isPowered = false;
    }

    void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire && !isPowered) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}
        if (Input.GetButton("Fire1") && Time.time > nextFire && isPowered)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn1.position, shotSpawn1.rotation);
            Instantiate(shot, shotSpawn2.position, shotSpawn2.rotation);
            GetComponent<AudioSource>().Play();
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Powerup")
        {
            Destroy(collision.gameObject);
            Debug.Log("Gatling Start");
            StartCoroutine(Powerup());
        }
    }

    void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}

    IEnumerator Powerup()
    {
        isPowered = true;
        fireRate = .15f;
        yield return new WaitForSeconds(5);
        isPowered = false;
        fireRate = .25f;
        Debug.Log("Gatling Over");
    }
}
