XNA Input Manager
=================

Usage
-----

	private InputManager _inputManager;
	
	protected override void Initialize()
	{
		_inputManager = new InputManager();
		Components.Add(_inputManager);
	}
	
	protected override void Update(GameTime gameTime)
	{
		if (_inputManager.KeyWasPressed(Keys.Enter))
		{
			// TODO: Use your newfound input awesomeness!
		}
	}

Methods
-------

*   `KeyWasPressed(Keys) : bool`
    Returns whether the specified key was pressed and released (not held) since the last `Update()` call; `false` otherwise.

*   `KeyWasPressedFor(Keys, TimeSpan) : bool`
    Returns whether the specified key has been held for the specified duration.

*   `KeyWasPressedWithModifiers(Keys, Keys[]) : bool`
    Returns whether the specified key was pressed and released (not held) while the specified modifiers were held.

*   `KeyWasReleased(Keys) : bool`
    Returns `true` if the specified key was recently released (during the last `Update()` call); `false` otherwise.

*   `GetElapsedHeldTime(Keys) : TimeSpan`
    Returns the duration for which the specified key has been held.

*   `GetElapsedHeldTime(MouseButtons) : TimeSpan`
    Returns the duration for which the specified mouse button has been held.

*   `MouseButtonIsDown(MouseButtons) : bool`

*   `MouseButtonIsUp(MouseButtons) : bool`

*   `MouseButtonWasClicked(MouseButtons) : bool`

*   `ButtonWasClickedWithKeyModifiers(MouseButtons, Keys[]) : bool`

*   `ButtonWasReleased(MouseButtons) : bool`

*   `GetDistanceScrolled() : int`

*   `MouseIsScrollingUp() : bool`

*   `MouseIsScrollingDown() : bool`