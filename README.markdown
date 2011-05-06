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

Some Common Methods
-------------------

*   `KeyWasReleased(Keys) : bool`
    Returns `true` if the specified key was recently (during the last `Update()`) released.

*   `GetElapsedHeldTime(MouseButtons) : TimeSpan`
    Returns a `TimeSpan` containing the time the specified mouse button has been held for.