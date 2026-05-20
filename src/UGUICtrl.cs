using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// Token: 0x0200005F RID: 95
public class UGUICtrl
{
	// Token: 0x060001AF RID: 431 RVA: 0x0000A444 File Offset: 0x00008644
	protected void OnCreate(UGUIView t, string path, Type _panelName)
	{
		GameObject gameObject = AssetManager.LoadPrefab(path, Game.UI.UIRoot, true);
		gameObject.SetZero();
		this.canvasGroup = gameObject.GetComponent<CanvasGroup>();
		if (this.canvasGroup == null)
		{
			Debug.LogError(((_panelName != null) ? _panelName.ToString() : null) + " 无CanvasGroup");
		}
		t.gameObject = gameObject;
		t.transform = gameObject.transform;
		t.Init(t.transform);
		this.mainView = t;
		this.OnRegisterEvent();
		this.ButtonAddClick();
		this.panelName = _panelName;
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected virtual void Init()
	{
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected virtual void ButtonAddClick()
	{
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected virtual void OnRegisterEvent()
	{
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x0000A4D9 File Offset: 0x000086D9
	protected void Back()
	{
		Game.UI.BackUIPanel();
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x0000A4E5 File Offset: 0x000086E5
	public void OpenSelfPanel(object data)
	{
		this.canvasGroup.alpha = 1f;
		this.canvasGroup.interactable = true;
		this.canvasGroup.blocksRaycasts = true;
		this.OpenPanel(data);
		this.isOpen = true;
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected virtual void OpenPanel(object data)
	{
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x0000A51D File Offset: 0x0000871D
	public void CloseSelfPanel()
	{
		this.ClosePanel();
		this.canvasGroup.alpha = 0f;
		this.canvasGroup.interactable = false;
		this.canvasGroup.blocksRaycasts = false;
		this.isOpen = false;
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected virtual void ClosePanel()
	{
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x0000A554 File Offset: 0x00008754
	public void ViewActive(bool _b)
	{
		this.mainView.gameObject.SetActive(_b);
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x0000A568 File Offset: 0x00008768
	public virtual void Dispose()
	{
		if (this.canvasGroup != null)
		{
			this.canvasGroup.DOKill(false);
		}
		UGUIView uguiview = this.mainView;
		if (((uguiview != null) ? uguiview.gameObject : null) != null)
		{
			Object.Destroy(this.mainView.gameObject);
		}
	}

	// Token: 0x060001BA RID: 442 RVA: 0x0000A5BA File Offset: 0x000087BA
	public virtual void Update()
	{
		this.CheckInput();
	}

	// Token: 0x060001BB RID: 443 RVA: 0x0000A5C4 File Offset: 0x000087C4
	private void CheckInput()
	{
		if (this.isOpen)
		{
			if (this.buttonList != null && this.buttonList.Count > 0)
			{
				this.CheckUpButton(new Action(this.SelectPre));
				this.CheckDownButton(new Action(this.SelectNext));
				this.CheckLeftButton(new Action(this.SelectPre));
				this.CheckRightButton(new Action(this.SelectNext));
				this.CheckAButton(new Action(this.CallSelect));
			}
			if (!GameHelperClient.IsJoyStick)
			{
				return;
			}
			if (Gamepad.current == null)
			{
				return;
			}
			Vector2 lhs = Gamepad.current.leftStick.ReadValue();
			if (this.joyStickNotCheck && lhs == Vector2.zero)
			{
				this.joyStickNotCheck = false;
			}
			if (this.aButtonNotCheck && Gamepad.current.aButton.ReadValue() == 0f)
			{
				this.aButtonNotCheck = false;
			}
			if (this.bButtonNotCheck && Gamepad.current.bButton.ReadValue() == 0f)
			{
				this.bButtonNotCheck = false;
			}
		}
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000A6D4 File Offset: 0x000088D4
	protected void CheckUpButton(Action ac)
	{
		if (!GameHelperClient.IsJoyStick)
		{
			return;
		}
		if (Gamepad.current == null)
		{
			return;
		}
		Vector2 vector = Gamepad.current.leftStick.ReadValue();
		if (Input.GetKeyDown(KeyCode.UpArrow) || (vector.y > 0.5f && !this.joyStickNotCheck))
		{
			ac();
			this.joyStickNotCheck = true;
		}
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0000A730 File Offset: 0x00008930
	protected void CheckDownButton(Action ac)
	{
		if (!GameHelperClient.IsJoyStick)
		{
			return;
		}
		if (Gamepad.current == null)
		{
			return;
		}
		Vector2 vector = Gamepad.current.leftStick.ReadValue();
		if (Input.GetKeyDown(KeyCode.DownArrow) || (vector.y < -0.5f && !this.joyStickNotCheck))
		{
			ac();
			this.joyStickNotCheck = true;
		}
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000A78C File Offset: 0x0000898C
	protected void CheckLeftButton(Action ac)
	{
		if (!GameHelperClient.IsJoyStick)
		{
			return;
		}
		if (Gamepad.current == null)
		{
			return;
		}
		Vector2 vector = Gamepad.current.leftStick.ReadValue();
		if (Input.GetKeyDown(KeyCode.LeftArrow) || (vector.x < -0.5f && !this.joyStickNotCheck))
		{
			ac();
			this.joyStickNotCheck = true;
		}
	}

	// Token: 0x060001BF RID: 447 RVA: 0x0000A7E8 File Offset: 0x000089E8
	protected void CheckRightButton(Action ac)
	{
		if (!GameHelperClient.IsJoyStick)
		{
			return;
		}
		if (Gamepad.current == null)
		{
			return;
		}
		Vector2 vector = Gamepad.current.leftStick.ReadValue();
		if (Input.GetKeyDown(KeyCode.RightArrow) || (vector.x > 0.5f && !this.joyStickNotCheck))
		{
			ac();
			this.joyStickNotCheck = true;
		}
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0000A844 File Offset: 0x00008A44
	protected void CheckAButton(Action ac)
	{
		if (!GameHelperClient.IsJoyStick)
		{
			return;
		}
		if (Gamepad.current == null)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Return) || (Gamepad.current.aButton.isPressed && !this.aButtonNotCheck))
		{
			ac();
			this.aButtonNotCheck = true;
		}
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x0000A890 File Offset: 0x00008A90
	protected void ClearNavigation()
	{
		this.buttonList.Clear();
		this.btnIndex = 0;
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0000A8A4 File Offset: 0x00008AA4
	protected void ActiveFirstNavigation()
	{
		UGUICtrl.<ActiveFirstNavigation>d__29 <ActiveFirstNavigation>d__;
		<ActiveFirstNavigation>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<ActiveFirstNavigation>d__.<>4__this = this;
		<ActiveFirstNavigation>d__.<>1__state = -1;
		<ActiveFirstNavigation>d__.<>t__builder.Start<UGUICtrl.<ActiveFirstNavigation>d__29>(ref <ActiveFirstNavigation>d__);
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0000A8DB File Offset: 0x00008ADB
	protected void AddNavigation(UIBehaviour trans)
	{
		this.buttonList.Add(trans.GetComponent<UINavigation>());
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x0000A8F0 File Offset: 0x00008AF0
	private void SelectPre()
	{
		this.buttonList[this.btnIndex].Normal();
		this.btnIndex--;
		if (this.btnIndex < 0)
		{
			this.btnIndex = 0;
		}
		this.buttonList[this.btnIndex].Selected();
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x0000A948 File Offset: 0x00008B48
	private void SelectNext()
	{
		this.buttonList[this.btnIndex].Normal();
		this.btnIndex++;
		if (this.btnIndex >= this.buttonList.Count - 1)
		{
			this.btnIndex = this.buttonList.Count - 1;
		}
		this.buttonList[this.btnIndex].Selected();
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000A9B7 File Offset: 0x00008BB7
	private void CallSelect()
	{
		this.buttonList[this.btnIndex].Pressed();
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0000A9CF File Offset: 0x00008BCF
	public bool IsOpen()
	{
		return this.alwaysUpdate || this.canvasGroup.alpha == 1f;
	}

	// Token: 0x040001FC RID: 508
	public Type panelName;

	// Token: 0x040001FD RID: 509
	public UGUIView mainView;

	// Token: 0x040001FE RID: 510
	protected CanvasGroup canvasGroup;

	// Token: 0x040001FF RID: 511
	public bool isOpen;

	// Token: 0x04000200 RID: 512
	public bool alwaysUpdate;

	// Token: 0x04000201 RID: 513
	protected List<UINavigation> buttonList = new List<UINavigation>();

	// Token: 0x04000202 RID: 514
	private int btnIndex;

	// Token: 0x04000203 RID: 515
	protected bool joyStickNotCheck;

	// Token: 0x04000204 RID: 516
	protected bool aButtonNotCheck;

	// Token: 0x04000205 RID: 517
	protected bool bButtonNotCheck;
}
