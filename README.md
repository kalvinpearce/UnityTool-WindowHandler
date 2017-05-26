# WindowHandler - Unity3D tool
This is a script provide in depth customization of the built game window.

### Note: This system works on windows only

Place the script anywhere in your assets folder and the script will take care of the rest.

### Functions available:
Window Updaters:
```C#
// GetWindowInfo
WindowHandler.GetWindowInfo();
	// Updates all the WindowHandler member variables.
    
// UpdateWindowPosition
WindowHandler.UpdateWindowPosition();
	// Updates the window position if changes are made but not pushed.
```

Window Transformers:
```C#
// SetWindowSize
WindowHandler.SetWindowSize( int width, int height );
	// Resizes the window to the desired width and height.
  
// SetWindowPosition
WindowHandler.SetWindowPosition( int x, int y );
	// Moves the window to the desired position.
```

Mouse Postions:
```C#
// GetMousePosition
WindowHandler.GetMousePosition()
  // Returns a Vector2 of the mouse position in monitor space (works for multiple monitor environments).
  
// GetMousePositionInWindow
WindowHandler.GetMousePositionInWindow()
  // Returns a Vector2 of the mouse position relitave to the window.
```

Window Modifiers
```C#
// SetWindowTopmost
WindowHandler.SetWindowTopmost( bool pinToTop );
	// Sets whether the window should display over the top of all other windows.

// SetWindowTransparency
WindowHandler.SetWindowTransparency( bool isTransparent );
	// Sets whether the window should key out all pixels with a certain color key ( 0x00000000 by default ).

// SetWindowIgnoreClicks
WindowHandler.SetWindowIgnoreClicks( bool clickThrough );
	// Sets whether the window should ignore all clicks and pass them through to windows below it.

// SetShowTaskbarIcon
WindowHandler.SetShowTaskbarIcon( bool showIcon );
	// Sets whether the window should display its icon in the taskbar or not.

// SetWindowOpacity
WindowHandler.SetWindowOpacity( int percentage );
	// Sets the overall opacity of the window ( between 0% & 100% ).
  // Only works if SetWindowIgnoreClicks is true.
```

Planned: 
```C#
// GetPixelColor
WindowHandler.GetPixelColor( int x, int y );
  // Returns a Color value of the pixel at x and y in monitor space.
```
