using System;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200030D RID: 781
public class UI_Confirm : UGUICtrl
{
	// Token: 0x0600121B RID: 4635 RVA: 0x0006B678 File Offset: 0x00069878
	public UI_Confirm()
	{
		this.selfView = new UI_Confirm_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_confirm", base.GetType());
		this.inputField = this.selfView.trans_inputMessage.gameObject.GetComponent<InputField>();
	}

	// Token: 0x0600121C RID: 4636 RVA: 0x0006B6C8 File Offset: 0x000698C8
	protected override void ButtonAddClick()
	{
		this.selfView.btn_confirm.AddButtonEvent(new UnityAction(this.OnBtnConfirmClick));
		this.selfView.btn_cancel.AddButtonEvent(new UnityAction(this.OnBtnCancelClick));
	}

	// Token: 0x0600121D RID: 4637 RVA: 0x0006B702 File Offset: 0x00069902
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.onConfirmAction = null;
		this.onCancelAction = null;
		this.onInputFieldAction = null;
	}

	// Token: 0x0600121E RID: 4638 RVA: 0x0006B720 File Offset: 0x00069920
	public void SetConfirmText(string text, Action confirmAction, Action cancelAction = null, Action<string> inputFieldAction = null, string inputFieldText = "")
	{
		this.selfView.ltext_dec.text = text;
		this.onConfirmAction = (Action)Delegate.Combine(this.onConfirmAction, confirmAction);
		this.onCancelAction = (Action)Delegate.Combine(this.onCancelAction, cancelAction);
		this.onInputFieldAction = (Action<string>)Delegate.Combine(this.onInputFieldAction, inputFieldAction);
		this.inputField.gameObject.SetActive(inputFieldAction != null);
		if (inputFieldAction != null)
		{
			this.inputField.placeholder.GetComponent<Text>().text = inputFieldText;
			this.inputField.text = "";
		}
	}

	// Token: 0x0600121F RID: 4639 RVA: 0x0006B7C4 File Offset: 0x000699C4
	private void OnBtnConfirmClick()
	{
		Action<string> action = this.onInputFieldAction;
		if (action != null)
		{
			action(this.inputField.text);
		}
		Game.UI.CloseUI<UI_Confirm>();
		Action action2 = this.onConfirmAction;
		if (action2 == null)
		{
			return;
		}
		action2();
	}

	// Token: 0x06001220 RID: 4640 RVA: 0x0006B7FC File Offset: 0x000699FC
	private void OnBtnCancelClick()
	{
		Game.UI.CloseUI<UI_Confirm>();
		Action action = this.onCancelAction;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x04001046 RID: 4166
	public UI_Confirm_View selfView;

	// Token: 0x04001047 RID: 4167
	private Action onConfirmAction;

	// Token: 0x04001048 RID: 4168
	private Action onCancelAction;

	// Token: 0x04001049 RID: 4169
	private InputField inputField;

	// Token: 0x0400104A RID: 4170
	private Action<string> onInputFieldAction;
}
