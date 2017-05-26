using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOnQuad : MonoBehaviour 
{
	/* Variables */
	Renderer rend;
	Texture2D texture;
	public int brushRadius = 6;

	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<Renderer>();
		texture = new Texture2D( (int)transform.localScale.x * 87, (int)transform.localScale.y * 87 );
		rend.material.mainTexture = texture;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( Input.GetKey( KeyCode.Q ) ) 
		{
			for( int y = -(int)(brushRadius * 0.5f ); y < (int)(brushRadius * 0.5f); ++y )
			{
				for( int x = -(int)(brushRadius * 0.5f ); x < (int)(brushRadius * 0.5f); ++x )
				{
					Vector2 xypos = new Vector2( (int)Input.mousePosition.x + x, (int)Input.mousePosition.y + y );
					if( Vector3.Distance( (Vector2)Input.mousePosition, xypos ) <= ( brushRadius * 0.5f ) )
						texture.SetPixel( (int)Input.mousePosition.x + x, (int)Input.mousePosition.y + y, new Color(0,0,0,0) );
				}
			}
		}
		texture.Apply();
	}
}
