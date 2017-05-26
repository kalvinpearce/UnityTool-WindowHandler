using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowHandler 
{
	/* Variables */
#region Enums
	enum WindowStyle : uint
	{
		BORDER			= 0x00800000,
		CAPTION			= 0x00C00000,
		CHILD			= 0x40000000,
		CHILDWINDOW		= 0x40000000,
		CLIPCHILDREN	= 0x02000000,
		CLIPSIBLINGS	= 0x04000000,
		DISABLED		= 0x08000000,
		DLGFRAME		= 0x00400000,
		GROUP			= 0x00020000,
		HSCROLL			= 0x00100000,
		ICONIC			= 0x20000000,
		MAXIMIZE		= 0x01000000,
		MAXIMIZEBOX		= 0x00010000,
		MINIMIZE		= 0x20000000,
		MINIMIZEBOX		= 0x00020000,
		OVERLAPPED		= 0x00000000,
		POPUP			= 0x80000000,
		SIZEBOX			= 0x00040000,
		SYSMENU			= 0x00080000,
		TABSTOP			= 0x00010000,
		THICKFRAME		= 0x00040000,
		TILED			= 0x00000000,
		VISIBLE			= 0x10000000,
		VSCROLL			= 0x00200000
	}

	enum ExtendedWindowStyle : uint
	{
		ACCEPTFILES			= 0x00000010,
		APPWINDOW			= 0x00040000,
		CLIENTEDGE			= 0x00000200,
		COMPOSITED			= 0x02000000,
		CONTEXTHELP			= 0x00000400,
		CONTROLPARENT		= 0x00010000,
		DLGMODALFRAME		= 0x00000001,
		LAYERED				= 0x00080000,
		LAYOUTRTL			= 0x00400000,
		LEFT				= 0x00000000,
		LEFTSCROLLBAR		= 0x00004000,
		LTRREADING			= 0x00000000,
		MDICHILD			= 0x00000040,
		NOACTIVATE			= 0x08000000,
		NOINHERITLAYOUT		= 0x00100000,
		NOPARENTNOTIFY		= 0x00000004,
		NOREDIRECTIONBITMAP	= 0x00200000,
		RIGHT				= 0x00001000,
		RIGHTSCROLLBAR		= 0x00000000,
		RTLREADING			= 0x00002000,
		STATICEDGE			= 0x00020000,
		TOOLWINDOW			= 0x00000080,
		TOPMOST				= 0x00000008,
		TRANSPARENT			= 0x00000020,
		WINDOWEDGE			= 0x00000100
	}

	enum SwpFlag : uint
	{
		ASYNCWINDOWPOS	= 0x4000,
		DEFERERASE		= 0x2000,
		DRAWFRAME		= 0x0020,
		FRAMECHANGED	= 0x0020,
		HIDEWINDOW		= 0x0080,
		NOACTIVATE		= 0x0010,
		NOCOPYBITS		= 0x0100,
		NOMOVE			= 0x0002,
		NOOWNERZORDER	= 0x0200,
		NOREDRAW		= 0x0008,
		NOREPOSITION	= 0x0200,
		NOSENDCHANGING	= 0x0400,
		NOSIZE			= 0x0001,
		NOZORDER		= 0x0004,
		SHOWWINDOW		= 0x0040
	}
#endregion

	/// <summary>
		/// The window's position.
		/// </summary>
		/// <param name="X">The x position of the window.</param>
		/// <param name="Y">The y position of the window.</param>
	public static Vector2 position;
	/// <summary>
		/// The window's width.
		/// </summary>
	public static int width;
	/// <summary>
		/// The window's height.
		/// </summary>
	public static int height;
	/// <summary>
		/// The color of pixels to cull if trancparency is enabled.
		/// </summary>
	public static int colorkey = 0x00000000;


	static int windowOpacity = 100;
	static bool stayOnTop = false;
	static bool showTaskbarIcon = true;
	static bool canClickThrough = false;
	static bool transparentWindow = false;

    static int windowHandle;
    static uint swpFlags;

    static long lWsStyle, lExWsStyle;

	// Use this for initialization
	static WindowHandler() 
	{
		// Grab window handle
        windowHandle  = GetActiveWindow();

		// Grab windows styles
        lWsStyle  = GetWindowLong( windowHandle, -16 );

		// Grab windows styles
        lExWsStyle  = 0;

		GetWindowInfo();
		UpdateWindowPosition();
	}

	/// <summary>
		/// Grabs all the window data and stores it in variables for the WindowHandler to use
		/// </summary>
	public static void GetWindowInfo()
	{
		windowRect windowRect = new windowRect();
		GetWindowRect( windowHandle, ref windowRect );

		position.x	= windowRect.left;
		position.y	= windowRect.top;
		width		= windowRect.right - windowRect.left;
		height		= windowRect.bottom - windowRect.top;
	}

	/// <summary>
		/// Updates the position of the window.
		/// </summary>
	public static void UpdateWindowPosition()
	{
		SetWindowPosition( position );
	}

#region SetWindowsPosition
	/// <summary>
		/// Sets the position of the window
		/// </summary>
		/// <param name="X">The x position to change the window to.</param>
		/// <param name="Y">The y position to change the window to.</param>
	public static void SetWindowPosition( int x, int y )
	{
		GetWindowInfo();
		swpFlags = ( (uint)SwpFlag.SHOWWINDOW | (uint)SwpFlag.FRAMECHANGED ) ;

		if( stayOnTop )
			SetWindowPos( windowHandle, -1, x, y, width, height, swpFlags );
		else
			SetWindowPos( windowHandle, -2, x, y, width, height, swpFlags );

		GetWindowInfo();
	}

	/// <summary>
		/// Sets the position of the window
		/// </summary>
		/// <param name="Vector2 position">The position to change the window to.</param>
	public static void SetWindowPosition( Vector2 position )
	{
		SetWindowPosition( (int)position.x, (int)position.y );
	}

	/// <summary>
		/// Sets the position of the window
		/// </summary>
		/// <param name="Vector3 position">The position to change the window to.</param>
	public static void SetWindowPosition( Vector3 position )
	{
		SetWindowPosition( (int)position.x, (int)position.y );
	}

	/// <summary>
		/// Sets the position of the window
		/// </summary>
		/// <param name="X">The x position to change the window to.</param>
		/// <param name="Y">The y position to change the window to.</param>
	public static void SetWindowPosition( float x, float y )
	{
		SetWindowPosition( (int)x, (int)y );
	}
#endregion

#region SetWindowSize
	/// <summary>
		/// Sets the size of the window
		/// </summary>
		/// <param name="Width">The width to change the window to.</param>
		/// <param name="Height">The height to change the window to.</param>
	public static void SetWindowSize( int windowWidth, int windowHeight )
	{
		width = windowWidth;
		height = windowHeight;
		SetWindowPosition( position );
	}

	/// <summary>
		/// Sets the size of the window
		/// </summary>
		/// <param name="Vector2 Size">The width and height to change the window to.</param>
	public static void SetWindowSize( Vector2 size )
	{
		SetWindowSize( (int)size.x, (int)size.y );
	}

	/// <summary>
		/// Sets the size of the window
		/// </summary>
		/// <param name="Vector3 Size">The width and height to change the window to.</param>
	public static void SetWindowSize( Vector3 size )
	{
		SetWindowSize( (int)size.x, (int)size.y );
	}
	
	/// <summary>
		/// Sets the size of the window
		/// </summary>
		/// <param name="Width">The width to change the window to.</param>
		/// <param name="Height">The height to change the window to.</param>
	public static void SetWindowSize( float windowWidth, float windowHeight )
	{
		SetWindowSize( (int)windowWidth, (int)windowHeight );
	}
#endregion

#region Mouse Functions
	/// <summary>
		/// Returns the position of the mouse in monitor space.
		/// </summary>
		/// <returns>Returns a Vector2 of the x and y positions of the mouse in monitor space.</returns>
	public static Vector2 GetMousePosition()
	{
		point tempPoint = new point();
		Vector2 mousepos;
		GetPhysicalCursorPos( ref tempPoint );
		mousepos.x = tempPoint.x;
		mousepos.y = tempPoint.y;

		return mousepos;
	}

	/// <summary>
		/// Returns the position of the mouse in window space.
		/// </summary>
		/// <returns>Returns a Vector2 of the x and y positions of the mouse in window space.</returns>
	public static Vector2 GetMousePositionInWindow()
	{
		Vector2 mouseInWinPos = new Vector2();
		Vector2 mousePos = GetMousePosition();
		GetWindowInfo();
		mouseInWinPos.x = mousePos.x - position.x;
		mouseInWinPos.y = mousePos.y - position.y;

		return mouseInWinPos;
	}
#endregion

	/* Currently under Constuction */
	/*
	static Color GetPixelColor( int x, int y )
	{
		Color temp = Color.black;
		uint colorData = GetPixel( windowHandle, x, y );
		Debug.Log( "Full val: " + colorData );
		Debug.Log( "B val: " + ((colorData>>16) & 0xFF) );
		Debug.Log( "G val: " + ((colorData>>8) & 0xFF) );
		Debug.Log( "R val: " + ((colorData) & 0xFF) );
		return temp;
	}
	*/


#region Style Modifiers
	/// <summary>
		/// Pins the window to display over the top of all other windows.
		/// </summary>
		/// <param name="bool pinToTop">The value of if the window should be pinned to the top.</param>
	public static void SetWindowTopmost( bool pinToTop )
	{
		stayOnTop = pinToTop;
		UpdateWindowPosition();
	}

	/// <summary>
		/// Sets the window to be transparent. All pixels with a color matching the WindowHandler.colorKey value will be transparent and able to be clicked through.
		/// </summary>
		/// <param name="bool isTransparent">The value of if the window should be transparent.</param>
	public static void SetWindowTransparency( bool isTransparent )
	{
		transparentWindow = isTransparent;
		PushExStyles();
	}

	/// <summary>
		/// Sets the whole window to ignore clicks.
		/// </summary>
		/// <param name="bool clickThrough">The value of if the window should ignore clicks.</param>
	public static void SetWindowIgnoreClicks( bool clickThrough )
	{
		canClickThrough = clickThrough;
		PushExStyles();
	}

	/// <summary>
		/// Sets the window's taskbar icon.
		/// </summary>
		/// <param name="bool showIcon">The value of if the taskbar icon should appear in the taskbar or not.</param>
	public static void SetShowTaskbarIcon( bool showIcon )
	{
		showTaskbarIcon = showIcon;
		PushExStyles();
	}

	/// <summary>
		/// Sets the entire window's opacity.
		/// </summary>
		/// <param name="Percentage">Between 0% and 100% of the windows opacity</param>
	public static void SetWindowOpacity( int percentage )
	{
		windowOpacity = percentage;
		windowOpacity = Mathf.Clamp( windowOpacity, 0, 100 );

		PushExStyles();
	}
	/// <summary>
		/// Sets the entire window's opacity.
		/// </summary>
		/// <param name="Percentage">Between 0% and 100% of the windows opacity</param>
	public static void SetWindowOpacity( float percentage )
	{
		SetWindowOpacity( (int)percentage );
	}

	/* Push the Extended window styles */
	static void PushExStyles()
	{
		lExWsStyle = 0x0000;
		int typeFlag = 0x0002;
		
		if( transparentWindow )
		{
			lExWsStyle |= (long)ExtendedWindowStyle.LAYERED;
			typeFlag = 0x0001;
		}
		if( canClickThrough )
		{
			lExWsStyle |= (long)ExtendedWindowStyle.LAYERED;
			lExWsStyle |= (long)ExtendedWindowStyle.TRANSPARENT;
		}
		if( !showTaskbarIcon )
		{
			lExWsStyle |= (long)ExtendedWindowStyle.TOOLWINDOW;
		}

		// Set the windows extended style
		SetWindowLong( windowHandle, -20, lExWsStyle );

		SetLayeredWindowAttributes( windowHandle, colorkey, (byte)((255 * windowOpacity) /100), typeFlag );

		UpdateWindowPosition();
	}
#endregion

#region EXTERNAL FUNCTIONS
	[DllImport( "user32.dll", EntryPoint = "SetWindowLongA" )]
    static extern int SetWindowLong( int hwnd, int nIndex, long dwNewLong );

    [DllImport( "user32.dll" )]
    static extern bool ShowWindowAsync( int hWnd, int nCmdShow );

    [DllImport( "user32.dll", EntryPoint = "SetLayeredWindowAttributes" )]
    static extern int SetLayeredWindowAttributes( int hwnd, int crKey, byte bAlpha, int dwFlags );

    [DllImport( "user32.dll", EntryPoint = "GetActiveWindow" )]
    private static extern int GetActiveWindow();

    [DllImport( "user32.dll", EntryPoint = "GetWindowLong" )]
    private static extern long GetWindowLong( int hwnd, int nIndex );

    [DllImport( "user32.dll", EntryPoint = "SetWindowPos" )]
    private static extern int SetWindowPos( int hwnd, int hwndInsertAfter, int x, int y, int cx, int cy, uint uFlags );

	[DllImport( "user32.dll", EntryPoint = "GetCursorPos" )]
	static extern bool GetPhysicalCursorPos( ref point refPoint );

	[DllImport( "user32.dll", EntryPoint = "GetWindowRect" )]
	static extern bool GetWindowRect( int hwnd, ref windowRect windowRect );

	[DllImport( "Gdi32.dll", EntryPoint = "GetPixel" )]
	static extern uint GetPixel( int hwnd, int xPos, int yPos );
#endregion

	struct point
	{
		public int x;
		public int y;
	}

	struct windowRect
	{
		public int left;
		public int top;
		public int right;
		public int bottom;
	}
}
