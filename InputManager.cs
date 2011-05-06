using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace XnaInputManager
{
	public class InputManager : GameComponent
	{
		private KeyboardState _priorKeyboardState;
		private KeyboardState _currentKeyboardState;

		private MouseState _priorMouseState;
		private MouseState _currentMouseState;

		private readonly IDictionary<Keys, TimeSpan> _keyHeldTimes;
		private readonly IDictionary<MouseButtons, TimeSpan> _mouseButtonHeldTimes;
		private readonly IDictionary<MouseButtons, Func<MouseState, ButtonState>> _mouseButtonMaps;

		public InputManager() : base(null)
		{
			_mouseButtonMaps = new Dictionary<MouseButtons, Func<MouseState, ButtonState>>
			{
				{ MouseButtons.Left, s => s.LeftButton },
				{ MouseButtons.Right, s => s.RightButton },
				{ MouseButtons.Middle, s => s.MiddleButton },
				{ MouseButtons.Extra1, s => s.XButton1 },
				{ MouseButtons.Extra2, s => s.XButton2 }
			};

			_keyHeldTimes = new Dictionary<Keys, TimeSpan>();
			foreach (var key in Enum.GetValues(typeof(Keys)))
			{
				_keyHeldTimes.Add((Keys)key, TimeSpan.Zero);
			}

			_mouseButtonHeldTimes = new Dictionary<MouseButtons, TimeSpan>();
			foreach (var mouseButton in Enum.GetValues(typeof(MouseButtons)))
			{
				_mouseButtonHeldTimes.Add((MouseButtons)mouseButton, TimeSpan.Zero);
			}
		}

		public bool KeyWasPressed(Keys key)
		{
			return _currentKeyboardState.IsKeyDown(key) && _priorKeyboardState.IsKeyUp(key);
		}

		public bool KeyWasPressedFor(Keys key, TimeSpan timeSpan)
		{
			return GetElapsedHeldTime(key).CompareTo(timeSpan) >= 0;
		}

		public bool KeyWasPressedWithModifiers(Keys key, params Keys[] modifiers)
		{
			return KeyWasPressed(key) && modifiers.All(k => _currentKeyboardState.IsKeyDown(k));
		}

		public bool KeyWasReleased(Keys key)
		{
			return _currentKeyboardState.IsKeyUp(key) && _priorKeyboardState.IsKeyDown(key);
		}

		public TimeSpan GetElapsedHeldTime(Keys key)
		{
			return _keyHeldTimes[key];
		}

		public TimeSpan GetElapsedHeldTime(MouseButtons mouseButton)
		{
			return _mouseButtonHeldTimes[mouseButton];
		}

		public bool MouseButtonIsDown(MouseButtons button)
		{
			return _mouseButtonMaps[button](_currentMouseState) == ButtonState.Pressed;
		}

		public bool MouseButtonIsUp(MouseButtons button)
		{
			return _mouseButtonMaps[button](_currentMouseState) == ButtonState.Released;
		}

		public bool MouseButtonWasClicked(MouseButtons button)
		{
			return
				_mouseButtonMaps[button](_currentMouseState) == ButtonState.Released &&
				_mouseButtonMaps[button](_priorMouseState) == ButtonState.Pressed;
		}

		public bool ButtonWasClickedWithKeyModifiers(MouseButtons button, params Keys[] modifierKeys)
		{
			return MouseButtonWasClicked(button) && modifierKeys.All(k => _currentKeyboardState.IsKeyDown(k));
		}

		public bool ButtonWasReleased(MouseButtons button)
		{
			return
				_mouseButtonMaps[button](_currentMouseState) == ButtonState.Released &&
				_mouseButtonMaps[button](_priorMouseState) == ButtonState.Pressed;
		}

		public int GetDistanceScrolled()
		{
			return _currentMouseState.ScrollWheelValue - _priorMouseState.ScrollWheelValue;
		}

		public bool MouseIsScrollingUp()
		{
			return _currentMouseState.ScrollWheelValue > _priorMouseState.ScrollWheelValue;
		}

		public bool MouseIsScrollingDown()
		{
			return _currentMouseState.ScrollWheelValue < _priorMouseState.ScrollWheelValue;
		}

		public override void Initialize()
		{
			_priorKeyboardState = _currentKeyboardState = Keyboard.GetState();
			_priorMouseState = _currentMouseState = Mouse.GetState();
		}

		public override void Update(GameTime gameTime)
		{
			// Keyboard
			_priorKeyboardState = _currentKeyboardState;
			_currentKeyboardState = Keyboard.GetState();

			foreach (var key in Enum.GetValues(typeof(Keys)))
			{
				if (_currentKeyboardState[(Keys)key] == KeyState.Down)
					_keyHeldTimes[(Keys)key] = _keyHeldTimes[(Keys)key] + gameTime.ElapsedGameTime;
				else
					_keyHeldTimes[(Keys)key] = TimeSpan.Zero;
			}

			// Mouse
			_priorMouseState = _currentMouseState;
			_currentMouseState = Mouse.GetState();

			foreach (var mouseButton in Enum.GetValues(typeof(MouseButtons)))
			{
				if (_mouseButtonMaps[(MouseButtons)mouseButton](_currentMouseState) == ButtonState.Pressed)
					_mouseButtonHeldTimes[(MouseButtons)mouseButton] += gameTime.ElapsedGameTime;
				else
					_mouseButtonHeldTimes[(MouseButtons)mouseButton] = TimeSpan.Zero;
			}

			base.Update(gameTime);
		}
	}

	public enum MouseButtons
	{
		Left,
		Middle,
		Right,
		Extra1,
		Extra2
	}
}