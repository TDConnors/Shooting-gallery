using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

public GameObject[] targets;
public int score;
public int ammo;
public int waveLimit;
public int numWaves;
public float startWait;
public float waveWait;
public bool gameover;
public bool restart;
public Text scoreText;
public Text waveText;
public Text gameOverText;
public Text ammoText;
bool CursorLockedVar;


private TargetRaise [] targetScripts;


	void Start () 
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = (false);
		CursorLockedVar = (true);
		gameover = false;
		restart = false;
		gameOverText.text = "";
		numWaves = 0;
		score = 0;
		ammo = 0;
		UpdateScore();
		UpdateAmmo();
		targetScripts = new TargetRaise[16];
		for (int i = 0; i < 16; i++)
		{
			targetScripts[i] = targets[i].GetComponent <TargetRaise>();		
		}
		StartCoroutine (targetWaves ());
	}
	
	void Update () 
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		
if (Input.GetKeyDown ("escape") && !CursorLockedVar) {
Cursor.lockState = CursorLockMode.Locked;
Cursor.visible = (false);
CursorLockedVar = (true);
}
else if(Input.GetKeyDown("escape") && CursorLockedVar){
Cursor.lockState = CursorLockMode.None;
Cursor.visible = (true);
CursorLockedVar = (false);
}
	}
	
	IEnumerator targetWaves ()
	{
		int tempTar = 0;
		int tempMat = 0;
		int numTar = 0;
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			numWaves++;
			if (numWaves == waveLimit)
				waveText.text = "Final Wave";
			else	
				waveText.text = "Wave " + numWaves;
			
			numTar = Random.Range (5, 8);
			for (int i = 0;i < numTar; i++)
			{
				tempTar = Random.Range (0, targets.Length);
				tempMat = Random.Range (0, 10);
				targetScripts[tempTar].changeType(tempMat);
				targetScripts[tempTar].raiseIt();
			}
			yield return new WaitForSeconds (waveWait);
			for (int i = 0;i < targets.Length ; i++)
			{
				targetScripts[i].changeBack();
			}
			yield return new WaitForSeconds (startWait);
			if (numWaves >= waveLimit)
			{
				break;
			}
		}
		gameoverScreen();
	}
	
	public void AddScore (int newScoreValue)
	{
		if(gameover == false)
		{
		score += newScoreValue;
		UpdateScore ();
		}
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void setAmmo (int newAmmo)
	{
		if(gameover == false)
		{
		ammo = newAmmo;
		UpdateAmmo ();
		}
	}
	
	void UpdateAmmo ()
	{
		ammoText.text = "Ammo: " + ammo;
	}
	
	void gameoverScreen()
	{
		gameover = true;
		restart = true;
		ammoText.text ="";
		scoreText.text ="";
		waveText.text ="Press R to Restart";
		gameOverText.text = "    Game Over \nYour Score is " + score;	
	}
}
