using UnityEngine;
using System.Collections;

public class TargetRaise : MonoBehaviour {
    
	public Material[] materials;
	public Material blank;
	public Transform raise;
	public Transform lower;
    public float speed;
	public int scoreval;
	public GameObject Box;
	public Renderer rend;
	public int toggle;
	public int type;
	
	private GameController gameController;
	
	[SerializeField]
	private AudioClip _bad1 = null;
	[SerializeField]
	private AudioClip _bad2 = null;
	[SerializeField]
	private AudioClip _bad3 = null;
	[SerializeField]
	private AudioClip _good = null;
	[SerializeField]
    protected AudioSource _audioSource = null;
	
	void Start()
	{
		rend = Box.GetComponent<Renderer>();
        rend.enabled = true;
		toggle = 0;
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
		if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();
	}
	
    void Update() 
	{
		if(toggle == 1)
		{
			float step = speed * Time.deltaTime;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, raise.rotation, step);
			if (transform.rotation == raise.rotation)
			{
				toggle = 0;
			}
		}
		if(toggle == -1)
		{
			float step = speed * Time.deltaTime;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, lower.rotation, step);
			if (transform.rotation == lower.rotation)
			{
				toggle = 0;
			}
		}
    }
void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Bullet" && toggle != -1)
		{
			if (type != -1)
			{   
		other.gameObject.transform.parent = gameObject.transform;
				if(type == 0)
				{
					if (_bad1 != null && _audioSource != null)
						_audioSource.PlayOneShot (_bad1);
				}
				else if(type == 1)
				{
					if (_bad2 != null && _audioSource != null)
						_audioSource.PlayOneShot (_bad2);
				}
				else if(type == 2)
				{
					if (_bad3 != null && _audioSource != null)
						_audioSource.PlayOneShot (_bad3);
				}
				else if(type == 3)
				{
					if (_good != null && _audioSource != null)
						_audioSource.PlayOneShot (_good);
				}
			}
			gameController.AddScore(scoreval);
			changeBack();
		}
	}

	public void raiseIt()
	{
		toggle = 1;
	}
	public void lowerIt()
	{
		toggle = -1;
	}
    public void changeType(int value)
	{
		if (value == 9 || value ==10)
		{
			value = 3;
		}
		else if (value == 6||value == 7 || value == 8)
		{
			value = 2;
		}
		else if (value == 3||value == 4 || value == 5)
		{
			value = 1;
		}
		else if (value <= 2)
		{
			value = 0;
		}
		type = value;
		rend.sharedMaterial = materials[value];
		if (value == 3)
			scoreval = -100;
		else
			scoreval = 25;
	}
	public void changeBack()
	{
		toggle = -1;
		rend.sharedMaterial = blank;
		scoreval = 0;
		type = -1;
	}
}
