using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour
{
    public GameController GameController;
    public float scrollSpeed;
	public float tileSizeZ;
    public GameObject ps;
    public GameObject psd;
    public GameObject ps1;
    public GameObject psd1;
    public GameObject ps2;
    public GameObject psd2;

    private Vector3 startPosition;

	void Start ()
	{
		startPosition = transform.position;
        GameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

	void Update ()
	{
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition;
        if (GameController.gameOver == true)
        {
            scrollSpeed = 0;
            ps.SetActive(false);
            psd.SetActive(false);
            ps2.SetActive(true);
            psd2.SetActive(true);

        }
        if (GameController.gameWon == true)
        {
            scrollSpeed = -25;
            ps.SetActive(false);
            psd.SetActive(false);
            ps1.SetActive(true);
            psd1.SetActive(true);

        }
    }   
}