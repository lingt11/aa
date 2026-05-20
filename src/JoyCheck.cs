using System;
using UnityEngine;

// Token: 0x02000285 RID: 645
public class JoyCheck : MonoBehaviour
{
	// Token: 0x06000C0D RID: 3085 RVA: 0x00042C54 File Offset: 0x00040E54
	private void OnEnable()
	{
		MySystemEvent.Instance.RegisterMessage<InputType>(2, new Action<Body, InputType>(this.InputTypeChange));
		this.InputTypeChange(default(Body), GameHelperClient.IsJoyStick ? InputType.Gamepad : InputType.Keyboard);
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x00042C92 File Offset: 0x00040E92
	private void OnDisable()
	{
		MySystemEvent.Instance.UnregisterMessage<InputType>(2, new Action<Body, InputType>(this.InputTypeChange));
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x00042CAC File Offset: 0x00040EAC
	private void InputTypeChange(Body body, InputType inputType)
	{
		if (inputType == InputType.Gamepad)
		{
			this.keyboard.gameObject.SetActive(false);
			this.joy.gameObject.SetActive(true);
			return;
		}
		if (inputType == InputType.Keyboard)
		{
			this.keyboard.gameObject.SetActive(true);
			this.joy.gameObject.SetActive(false);
		}
	}

	// Token: 0x04000CDB RID: 3291
	public GameObject keyboard;

	// Token: 0x04000CDC RID: 3292
	public GameObject joy;
}
