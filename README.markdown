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
