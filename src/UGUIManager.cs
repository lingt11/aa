using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class UGUIManager : IApplicationQuit, IUpdate
{
	// Token: 0x060001CB RID: 459 RVA: 0x0000AAF0 File Offset: 0x00008CF0
	public UGUIManager()
	{
		this.uiRootObj = AssetManager.LoadPrefab("UI/Prefabs/UIRoot", null, true);
		this.uiRootObj.SetZero();
		this.UIRoot = this.uiRootObj.transform.GetChild(0);
		this.canvasRectTransform = this.UIRoot.GetComponent<RectTransform>();
		Canvas component = this.UIRoot.GetComponent<Canvas>();
		if (component != null && component.renderMode == RenderMode.ScreenSpaceCamera)
		{
			this.uiCamera = component.worldCamera;
		}
	}

	// Token: 0x060001CC RID: 460 RVA: 0x0000ABA8 File Offset: 0x00008DA8
	public void OpenUIPanel<T>(object data = null) where T : UGUICtrl, new()
	{
		Type typeFromHandle = typeof(T);
		if (this.curPanelName == typeFromHandle)
		{
			return;
		}
		this.curPanelName = typeFromHandle;
		if (!this.uiPanelCtrl.ContainsKey(typeFromHandle))
		{
			this.uiPanelCtrl.Add(typeFromHandle, Activator.CreateInstance<T>());
		}
		if (this.uiPanelStack.Count > 0)
		{
			this.uiPanelStack.Peek().CloseSelfPanel();
		}
		this.uiPanelStack.Push(this.uiPanelCtrl[typeFromHandle]);
		this.uiPanelCtrl[typeFromHandle].OpenSelfPanel(data);
	}

	// Token: 0x060001CD RID: 461 RVA: 0x0000AC44 File Offset: 0x00008E44
	public UGUICtrl OpenUI<T>(object data = null) where T : UGUICtrl, new()
	{
		Type typeFromHandle = typeof(T);
		if (!this.uiWindowCtrl.ContainsKey(typeFromHandle))
		{
			this.uiWindowCtrl.Add(typeFromHandle, Activator.CreateInstance<T>());
		}
		this.uiWindowCtrl[typeFromHandle].mainView.transform.SetAsLastSibling();
		this.uiWindowCtrl[typeFromHandle].OpenSelfPanel(data);
		return this.uiWindowCtrl[typeFromHandle];
	}

	// Token: 0x060001CE RID: 462 RVA: 0x0000ACBC File Offset: 0x00008EBC
	public void OpenUI<T>(Vector3 v3, object data = null) where T : UGUICtrl, new()
	{
		Type typeFromHandle = typeof(T);
		this.OpenUI<T>(data);
		this.uiWindowCtrl[typeFromHandle].mainView.transform.position = v3;
	}

	// Token: 0x060001CF RID: 463 RVA: 0x0000ACF8 File Offset: 0x00008EF8
	public void OpenUIArchPos<T>(Vector3 v3, object data = null) where T : UGUICtrl, new()
	{
		Type typeFromHandle = typeof(T);
		this.OpenUI<T>(data);
		this.uiWindowCtrl[typeFromHandle].mainView.transform.GetComponent<RectTransform>().anchoredPosition = v3;
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0000AD40 File Offset: 0x00008F40
	public void CloseUI<T>() where T : UGUICtrl, new()
	{
		Type typeFromHandle = typeof(T);
		if (this.uiWindowCtrl.ContainsKey(typeFromHandle))
		{
			this.uiWindowCtrl[typeFromHandle].CloseSelfPanel();
		}
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0000AD78 File Offset: 0x00008F78
	public void DestroyUI<T>() where T : UGUICtrl, new()
	{
		Type typeFromHandle = typeof(T);
		if (this.uiWindowCtrl.ContainsKey(typeFromHandle))
		{
			this.uiWindowCtrl[typeFromHandle].CloseSelfPanel();
			Object.Destroy(this.uiWindowCtrl[typeFromHandle].mainView.gameObject);
			this.uiWindowCtrl.Remove(typeFromHandle);
		}
		if (this.uiPanelCtrl.ContainsKey(typeFromHandle))
		{
			this.uiPanelCtrl[typeFromHandle].CloseSelfPanel();
			Object.Destroy(this.uiPanelCtrl[typeFromHandle].mainView.gameObject);
			this.uiPanelCtrl.Remove(typeFromHandle);
		}
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0000AE20 File Offset: 0x00009020
	public void CloseAllWindowUI()
	{
		foreach (KeyValuePair<Type, UGUICtrl> keyValuePair in this.uiWindowCtrl)
		{
			keyValuePair.Value.CloseSelfPanel();
		}
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0000AE78 File Offset: 0x00009078
	public T GetUI<T>()
	{
		Type typeFromHandle = typeof(T);
		if (this.uiPanelCtrl.ContainsKey(typeFromHandle))
		{
			return (T)((object)this.uiPanelCtrl[typeFromHandle]);
		}
		if (this.uiWindowCtrl.ContainsKey(typeFromHandle))
		{
			return (T)((object)this.uiWindowCtrl[typeFromHandle]);
		}
		return default(T);
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x0000AEDC File Offset: 0x000090DC
	public void BackUIPanel()
	{
		if (this.uiPanelStack.Count > 0)
		{
			UGUICtrl uguictrl = this.uiPanelStack.Peek();
			if (this.uiWindowCtrl.ContainsKey(uguictrl.panelName))
			{
				this.uiWindowCtrl.Remove(uguictrl.panelName);
			}
			uguictrl.CloseSelfPanel();
			this.uiPanelStack.Pop();
			if (this.uiPanelStack.Count > 0)
			{
				uguictrl = this.uiPanelStack.Peek();
				this.curPanelName = uguictrl.panelName;
				uguictrl.OpenSelfPanel(null);
			}
		}
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0000AF67 File Offset: 0x00009167
	public void OnApplicationQuit()
	{
		Debug.Log("UI重置");
		Object.Destroy(this.uiRootObj);
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x0000AF80 File Offset: 0x00009180
	public void Update()
	{
		this.keys = this.uiWindowCtrl.Keys.ToList<Type>();
		for (int i = this.keys.Count - 1; i >= 0; i--)
		{
			if (this.uiWindowCtrl[this.keys[i]].IsOpen())
			{
				this.uiWindowCtrl[this.keys[i]].Update();
			}
		}
		if (this.uiPanelStack.Count > 0)
		{
			this.uiPanelStack.Peek().Update();
		}
	}

	// Token: 0x0400020A RID: 522
	public Stack<UGUICtrl> uiPanelStack = new Stack<UGUICtrl>(32);

	// Token: 0x0400020B RID: 523
	public Dictionary<Type, UGUICtrl> uiPanelCtrl = new Dictionary<Type, UGUICtrl>(32);

	// Token: 0x0400020C RID: 524
	public Dictionary<Type, UGUICtrl> uiWindowCtrl = new Dictionary<Type, UGUICtrl>(32);

	// Token: 0x0400020D RID: 525
	private Type curPanelName;

	// Token: 0x0400020E RID: 526
	public Camera uiCamera;

	// Token: 0x0400020F RID: 527
	public RectTransform canvasRectTransform;

	// Token: 0x04000210 RID: 528
	public Transform UIRoot;

	// Token: 0x04000211 RID: 529
	private GameObject uiRootObj;

	// Token: 0x04000212 RID: 530
	private List<Type> keys = new List<Type>(32);
}
