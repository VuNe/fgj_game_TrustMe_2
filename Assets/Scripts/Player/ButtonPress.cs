using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour 
{
	private RaycastHit hit;

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			PaintingCheck();
		}
	}

//========================================================================================================================

	void PaintingCheck ()
	{
		if (Physics.Raycast(transform.position, transform.forward, out hit, 2.0f))
		{
			if(hit.transform.tag == "CorrectPainting")
			{
				Debug.Log("AMAZING! YOU WON THE GAME!!! I'M REALLY PROUD OF YOU!!11!1!111");
			}
		}
	}

}
