using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Token: 0x02000392 RID: 914
public class RebindUIButton : MonoBehaviour
{
	// Token: 0x060014D0 RID: 5328 RVA: 0x000807B0 File Offset: 0x0007E9B0
	private void Start()
	{
		this._action = EntityStatic.Get<InputManager>().controls.asset.FindAction(this.actionReference.action.id);
		this.UpdateUI();
		this.rebindButton.AddButtonEvent(new UnityAction(this.OnClickRebind));
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x00080804 File Offset: 0x0007EA04
	private void OnEnable()
	{
		this.UpdateUI();
		MySystemEvent.Instance.RegisterMessage(35, new Action<Body>(this.NeedUpdateUI));
		MySystemEvent.Instance.RegisterMessage(36, new Action<Body>(this.NeedUpdateUI));
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x0008083C File Offset: 0x0007EA3C
	private void OnDisable()
	{
		MySystemEvent.Instance.UnregisterMessage(35, new Action<Body>(this.NeedUpdateUI));
		MySystemEvent.Instance.UnregisterMessage(36, new Action<Body>(this.NeedUpdateUI));
	}

	// Token: 0x060014D3 RID: 5331 RVA: 0x0008086E File Offset: 0x0007EA6E
	private void NeedUpdateUI(Body body)
	{
		this.UpdateUI();
	}

	// Token: 0x060014D4 RID: 5332 RVA: 0x00080878 File Offset: 0x0007EA78
	private void UpdateUI()
	{
		if (this._action != null)
		{
			this.rebindButton.interactable = true;
			this.keyNameText.gameObject.SetActive(true);
			this.statusText.gameObject.SetActive(false);
			this.keyNameText.text = InputManager.GetKeyReadableName(this._action, this.bindingIndex);
		}
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x000808D8 File Offset: 0x0007EAD8
	private void OnClickRebind()
	{
		UI_InputPC.Instance.RebindComplete();
		MySystemEvent.Instance.DispatchMessage(35);
		this.statusText.text = Game.Language.Get("请输入按键", "");
		this.keyNameText.gameObject.SetActive(false);
		this.statusText.gameObject.SetActive(true);
		this.rebindButton.interactable = false;
		UI_InputPC.Instance.StartRebinding(this._action, this.bindingIndex, delegate(string newName)
		{
			this.keyNameText.text = newName;
			this.rebindButton.interactable = true;
			this.keyNameText.gameObject.SetActive(true);
			this.statusText.gameObject.SetActive(false);
			MySystemEvent.Instance.DispatchMessage(36);
		}, delegate(string conflictName)
		{
			Util.ShowTipsNoLanguage(string.Format(ColorDefine.NormalColor, string.Format(Game.Language.Get("按键冲突", ""), conflictName)));
		});
	}

	// Token: 0x04001359 RID: 4953
	[Header("设置")]
	public InputActionReference actionReference;

	// Token: 0x0400135A RID: 4954
	public int bindingIndex;

	// Token: 0x0400135B RID: 4955
	[Header("UI 组件")]
	public TextMeshProUGUI keyNameText;

	// Token: 0x0400135C RID: 4956
	public Button rebindButton;

	// Token: 0x0400135D RID: 4957
	public Text statusText;

	// Token: 0x0400135E RID: 4958
	private InputAction _action;
}
