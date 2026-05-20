using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x02000391 RID: 913
public class CustomInputUI : MonoBehaviour
{
	// Token: 0x060014C9 RID: 5321 RVA: 0x000806FD File Offset: 0x0007E8FD
	private void Start()
	{
		if (this.actionReference != null)
		{
			this._action = EntityStatic.Get<InputManager>().controls.asset.FindAction(this.actionReference.action.id);
		}
		this.UpdateUI();
	}

	// Token: 0x060014CA RID: 5322 RVA: 0x0008073D File Offset: 0x0007E93D
	private void Awake()
	{
		MySystemEvent.Instance.RegisterMessage(36, new Action<Body>(this.NeedUpdateUI));
	}

	// Token: 0x060014CB RID: 5323 RVA: 0x00080757 File Offset: 0x0007E957
	private void OnDestroy()
	{
		MySystemEvent.Instance.UnregisterMessage(36, new Action<Body>(this.NeedUpdateUI));
	}

	// Token: 0x060014CC RID: 5324 RVA: 0x00080771 File Offset: 0x0007E971
	private void NeedUpdateUI(Body body)
	{
		this.UpdateUI();
	}

	// Token: 0x060014CD RID: 5325 RVA: 0x00080779 File Offset: 0x0007E979
	private void UpdateUI()
	{
		if (this._action != null)
		{
			this.keyNameText.text = InputManager.GetKeyReadableName(this._action, this.bindingIndex);
		}
	}

	// Token: 0x060014CE RID: 5326 RVA: 0x0008079F File Offset: 0x0007E99F
	public void SetInputActionReference(InputAction inputAction)
	{
		this._action = inputAction;
		this.UpdateUI();
	}

	// Token: 0x04001355 RID: 4949
	[Header("设置")]
	public InputActionReference actionReference;

	// Token: 0x04001356 RID: 4950
	public int bindingIndex;

	// Token: 0x04001357 RID: 4951
	[Header("UI 组件")]
	public TextMeshProUGUI keyNameText;

	// Token: 0x04001358 RID: 4952
	private InputAction _action;
}
