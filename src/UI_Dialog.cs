using System;
using UnityEngine.Events;

// Token: 0x02000317 RID: 791
public class UI_Dialog : UGUICtrl
{
	// Token: 0x06001242 RID: 4674 RVA: 0x0006D44A File Offset: 0x0006B64A
	public UI_Dialog()
	{
		this.selfView = new UI_Dialog_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_dialog", base.GetType());
	}

	// Token: 0x06001243 RID: 4675 RVA: 0x0006D474 File Offset: 0x0006B674
	protected override void ButtonAddClick()
	{
		this.selfView.btn_confirm.AddButtonEvent(new UnityAction(this.OnConfirmBtnClick));
	}

	// Token: 0x06001244 RID: 4676 RVA: 0x0006D492 File Offset: 0x0006B692
	private void OnConfirmBtnClick()
	{
		base.CloseSelfPanel();
		Action action = this.closeAction;
		if (action != null)
		{
			action();
		}
		this.closeAction = null;
	}

	// Token: 0x06001245 RID: 4677 RVA: 0x0006D4B2 File Offset: 0x0006B6B2
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.closeAction = null;
	}

	// Token: 0x06001246 RID: 4678 RVA: 0x0006D4C2 File Offset: 0x0006B6C2
	public override void Update()
	{
		base.CheckAButton(new Action(this.OnConfirmBtnClick));
	}

	// Token: 0x04001087 RID: 4231
	public UI_Dialog_View selfView;

	// Token: 0x04001088 RID: 4232
	public Action closeAction;
}
