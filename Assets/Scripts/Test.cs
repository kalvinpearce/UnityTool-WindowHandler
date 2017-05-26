using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour 
{
	/* Variables */
	Vector2 offset;

	bool isTopmost;
	bool showTicon = true;
	bool clickThrough = false;
	bool isTrans = false;

	public Slider slider;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( Input.GetMouseButtonDown( 1 ) )
		{
			offset = WindowHandler.GetMousePositionInWindow();
		}
		if( Input.GetMouseButton( 1 ) )
		{
			Vector2 mousePos = WindowHandler.GetMousePosition();

			WindowHandler.SetWindowPosition( mousePos.x - offset.x, mousePos.y - offset.y );
		}

		if( Input.GetKeyDown( KeyCode.I ) )
		{
			WindowHandler.GetWindowInfo();
			Debug.Log( "X: " + WindowHandler.position.x + " | Y: " + WindowHandler.position.y );
			Debug.Log( "width: " + WindowHandler.width + " | hight: " + WindowHandler.height );

			Debug.Log( Input.mousePosition );
		}

		if( Input.GetKeyDown( KeyCode.T ) )
		{
			isTopmost = !isTopmost;
			WindowHandler.SetWindowTopmost( isTopmost );
		}

		if( Input.GetKeyDown( KeyCode.R ) )
		{
			showTicon = !showTicon;
			WindowHandler.SetShowTaskbarIcon( showTicon );
		}

		if( Input.GetKeyDown( KeyCode.E ) )
		{
			clickThrough = !clickThrough;
			WindowHandler.SetWindowIgnoreClicks( clickThrough );
		}

		if( Input.GetKeyDown( KeyCode.W ) )
		{
			isTrans = !isTrans;
			WindowHandler.SetWindowTransparency( isTrans );
		}

		if( Input.GetKeyDown( KeyCode.P ) )
		{
			Debug.Log( 0x00000000 | 0x00080000 | 0x00000080 );
			Debug.Log( 0x00080000 | 0x00080000 );
		}

		if( Input.GetKeyDown( KeyCode.Z ) )
		{
			// Under constuction
			//WindowHandler.GetPixelColor( (int)WindowHandler.GetMousePosition().x, (int)WindowHandler.GetMousePosition().y );
		}

		WindowHandler.SetWindowOpacity( slider.value );
	}

	void OnDisable()
	{
		WindowHandler.SetWindowTopmost( false );
	}
}
