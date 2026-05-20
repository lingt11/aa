using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Token: 0x02000394 RID: 916
public class UI_InputPC : MonoBehaviour
{
	// Token: 0x060014DB RID: 5339 RVA: 0x00080A16 File Offset: 0x0007EC16
	private void Awake()
	{
		UI_InputPC.Instance = this;
		this.resetButton.AddButtonEvent(new UnityAction(this.RestoreDefaults));
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x00080A35 File Offset: 0x0007EC35
	private void RestoreDefaults()
	{
		InputManager inputManager = EntityStatic.Get<InputManager>();
		if (inputManager == null)
		{
			return;
		}
		inputManager.RestoreDefaults();
	}

	// Token: 0x060014DD RID: 5341 RVA: 0x00080A48 File Offset: 0x0007EC48
	public bool CheckConflict(InputAction targetAction, int targetBindingIndex, string newPath, out string conflictInfo)
	{
		conflictInfo = "";
		foreach (InputActionMap inputActionMap in EntityStatic.Get<InputManager>().controls.asset.actionMaps)
		{
			foreach (InputAction inputAction in inputActionMap.actions)
			{
				for (int i = 0; i < inputAction.bindings.Count; i++)
				{
					if (!string.IsNullOrEmpty(inputAction.bindings[i].effectivePath) && inputAction.bindings[i].effectivePath == newPath)
					{
						if (inputAction != targetAction)
						{
							conflictInfo = InputManager.GetKeyReadableName(inputAction, i);
							return true;
						}
						if (inputAction == targetAction && i != targetBindingIndex)
						{
							conflictInfo = InputManager.GetKeyReadableName(inputAction, i);
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x00080B94 File Offset: 0x0007ED94
	public void StartRebinding(InputAction action, int bindingIndex, Action<string> onComplete, Action<string> onConflict)
	{
		action.Disable();
		string oldPath = action.bindings[bindingIndex].effectivePath;
		this._rebindingOperation = action.PerformInteractiveRebinding(bindingIndex).WithControlsExcluding("Mouse").OnMatchWaitForAnother(0.1f).OnComplete(delegate(InputActionRebindingExtensions.RebindingOperation operation)
		{
			string effectivePath = action.bindings[bindingIndex].effectivePath;
			string obj;
			if (this.CheckConflict(action, bindingIndex, effectivePath, out obj))
			{
				action.ApplyBindingOverride(bindingIndex, oldPath);
				Action<string> onConflict2 = onConflict;
				if (onConflict2 != null)
				{
					onConflict2(obj);
				}
				Action<string> onComplete2 = onComplete;
				if (onComplete2 != null)
				{
					onComplete2(InputManager.GetKeyReadableName(action, bindingIndex));
				}
			}
			else
			{
				InputManager inputManager = EntityStatic.Get<InputManager>();
				if (inputManager != null)
				{
					inputManager.SaveBindingOverrides();
				}
				Action<string> onComplete3 = onComplete;
				if (onComplete3 != null)
				{
					onComplete3(InputManager.GetKeyReadableName(action, bindingIndex));
				}
			}
			this.RebindComplete();
		}).OnCancel(delegate(InputActionRebindingExtensions.RebindingOperation operation)
		{
			Action<string> onComplete2 = onComplete;
			if (onComplete2 != null)
			{
				onComplete2(InputManager.GetKeyReadableName(action, bindingIndex));
			}
			this.RebindComplete();
		}).Start();
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x00080C4F File Offset: 0x0007EE4F
	public void RebindComplete()
	{
		if (this._rebindingOperation != null)
		{
			this._rebindingOperation.Dispose();
			this._rebindingOperation = null;
		}
		EntityStatic.Get<InputManager>().controls.Enable();
	}

	// Token: 0x04001361 RID: 4961
	public static UI_InputPC Instance;

	// Token: 0x04001362 RID: 4962
	private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;

	// Token: 0x04001363 RID: 4963
	public Button resetButton;
}
