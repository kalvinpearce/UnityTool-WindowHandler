using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rotate : MonoBehaviour 
{
	/* Variables */
	[SerializeField]
	float speed = 5;

	// Use this for initialization
	void Start () 
	{
		Application.runInBackground = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate( new Vector3( ( speed * Time.deltaTime ),
									   ( speed * Time.deltaTime ),
									   ( speed * Time.deltaTime ) ) );
	}
}
