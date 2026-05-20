using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x0200012D RID: 301
public class CheckInput
{
	// Token: 0x060005A7 RID: 1447 RVA: 0x00020C80 File Offset: 0x0001EE80
	private void SetInputType(InputType inputType)
	{
		if (this.curInputType == inputType)
		{
			return;
		}
		Debug.Log(inputType);
		this.curInputType = inputType;
		if (this.curInputType == InputType.Gamepad)
		{
			GameHelperClient.IsJoyStick = true;
		}
		else
		{
			GameHelperClient.IsJoyStick = false;
		}
		MySystemEvent.Instance.DispatchMessage<InputType>(2, this.curInputType);
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.RefreshPlayerSkill();
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x00020CE8 File Offset: 0x0001EEE8
	public InputType Update()
	{
		if (Gamepad.current == null)
		{
			return InputType.Keyboard;
		}
		Vector2 vector = Gamepad.current.dpad.ReadValue();
		if (vector != Vector2.zero)
		{
			this.SetInputType(InputType.Gamepad);
		}
		if (vector.x < 0f)
		{
			this.CheckJoyButtonPress(5, vector.x, -0.5f, false);
		}
		else if (vector.x > 0f)
		{
			this.CheckJoyButtonPress(6, vector.x, 0.5f, true);
		}
		else if (vector.y < 0f)
		{
			this.CheckJoyButtonPress(4, vector.y, -0.5f, false);
		}
		else if (vector.y > 0f)
		{
			this.CheckJoyButtonPress(3, vector.y, 0.5f, true);
		}
		if (vector == Vector2.zero)
		{
			this.joyDictionary[5] = false;
			this.joyDictionary[6] = false;
			this.joyDictionary[4] = false;
			this.joyDictionary[3] = false;
		}
		Vector2 lhs = Gamepad.current.leftStick.ReadValue();
		Vector2 vector2 = Gamepad.current.rightStick.ReadValue();
		if (lhs != Vector2.zero)
		{
			this.SetInputType(InputType.Gamepad);
		}
		if (vector2 != Vector2.zero)
		{
			this.SetInputType(InputType.Gamepad);
		}
		if (vector2.x < 0f)
		{
			this.CheckJoyButtonPress(14, vector2.x, -0.5f, false);
		}
		else if (vector2.x > 0f)
		{
			this.CheckJoyButtonPress(13, vector2.x, 0.5f, true);
		}
		if (vector2.y < 0f)
		{
			this.CheckJoyButtonPress(16, vector2.y, -0.5f, false);
		}
		else if (vector2.y > 0f)
		{
			this.CheckJoyButtonPress(15, vector2.y, 0.5f, true);
		}
		if (Mathf.Abs(vector2.x) < 0.1f && Mathf.Abs(vector2.y) < 0.1f)
		{
			this.joyDictionary[14] = false;
			this.joyDictionary[13] = false;
			this.joyDictionary[16] = false;
			this.joyDictionary[15] = false;
		}
		this.CheckJoyButtonPress(19, Gamepad.current.leftTrigger.ReadValue());
		this.CheckJoyButtonPress(22, Gamepad.current.rightTrigger.ReadValue());
		this.CheckJoyButtonPress(7, Gamepad.current.leftShoulder.ReadValue());
		this.CheckJoyButtonPress(8, Gamepad.current.rightShoulder.ReadValue());
		this.CheckJoyButtonPress(1, Gamepad.current.aButton.ReadValue());
		this.CheckJoyButtonPress(12, Gamepad.current.bButton.ReadValue());
		this.CheckJoyButtonPress(23, Gamepad.current.xButton.ReadValue());
		this.CheckJoyButtonPress(11, Gamepad.current.yButton.ReadValue());
		this.CheckJoyButtonPress(24, Gamepad.current.selectButton.ReadValue());
		this.CheckJoyButtonPress(25, Gamepad.current.startButton.ReadValue());
		if (Gamepad.current != null)
		{
			if (Gamepad.current.xButton.wasPressedThisFrame)
			{
				MySystemEvent.Instance.DispatchMessage(9);
			}
			if (Gamepad.current.xButton.wasReleasedThisFrame)
			{
				MySystemEvent.Instance.DispatchMessage(10);
			}
			if (Gamepad.current.rightTrigger.wasPressedThisFrame)
			{
				MySystemEvent.Instance.DispatchMessage(17);
			}
			if (Gamepad.current.rightTrigger.wasReleasedThisFrame)
			{
				MySystemEvent.Instance.DispatchMessage(18);
			}
		}
		Gamepad.current.aButton.ReadValue();
		Gamepad.current.bButton.ReadValue();
		Gamepad.current.xButton.ReadValue();
		Gamepad.current.yButton.ReadValue();
		Gamepad.current.selectButton.ReadValue();
		Gamepad.current.startButton.ReadValue();
		Gamepad.current.circleButton.ReadValue();
		Gamepad.current.triangleButton.ReadValue();
		Gamepad.current.crossButton.ReadValue();
		Gamepad.current.squareButton.ReadValue();
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			this.SetInputType(InputType.Touch);
			return InputType.Touch;
		}
		if (Input.GetJoystickNames().Length == 0)
		{
			this.SetInputType(InputType.Keyboard);
			return InputType.Keyboard;
		}
		if (Input.anyKeyDown)
		{
			foreach (object obj in Enum.GetValues(typeof(KeyCode)))
			{
				KeyCode key = (KeyCode)obj;
				if (Input.GetKeyDown(key))
				{
					key.ToString();
				}
			}
			this.SetInputType(this.CheckKeyDown());
			return this.curInputType;
		}
		return this.curInputType;
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x00021200 File Offset: 0x0001F400
	private void CheckJoyButtonPress(int name, float v)
	{
		this.joyDictionary.TryAdd(name, false);
		if (v > 0.6f && !this.joyDictionary[name])
		{
			this.joyDictionary[name] = true;
			MySystemEvent.Instance.DispatchMessage(name);
			Debug.Log(name);
			this.SetInputType(InputType.Gamepad);
			return;
		}
		if (v < 0.5f && this.joyDictionary[name])
		{
			this.joyDictionary[name] = false;
		}
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x00021280 File Offset: 0x0001F480
	private void CheckJoyButtonPress(int name, float now, float target, bool dayu)
	{
		this.joyDictionary.TryAdd(name, false);
		if (!this.joyDictionary[name])
		{
			if (dayu)
			{
				if (now > target)
				{
					this.joyDictionary[name] = true;
					MySystemEvent.Instance.DispatchMessage(name);
					Debug.Log(name);
					return;
				}
			}
			else if (now < target)
			{
				this.joyDictionary[name] = true;
				MySystemEvent.Instance.DispatchMessage(name);
				Debug.Log(name);
			}
		}
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x000212FC File Offset: 0x0001F4FC
	private InputType CheckKeyDown()
	{
		if (Input.GetKeyDown(KeyCode.Joystick1Button0))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button1))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button2))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button3))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button4))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button5))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button6))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button7))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button8))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button9))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button10))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button11))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button12))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button13))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button14))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button15))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button16))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button17))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button18))
		{
			return InputType.Gamepad;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button19))
		{
			return InputType.Gamepad;
		}
		return InputType.Keyboard;
	}

	// Token: 0x0400081E RID: 2078
	public InputType curInputType;

	// Token: 0x0400081F RID: 2079
	private Dictionary<int, bool> joyDictionary = new Dictionary<int, bool>();
}
