using System;

// Token: 0x0200039D RID: 925
public class UI_Set : UGUICtrl
{
	// Token: 0x0600152D RID: 5421 RVA: 0x00083135 File Offset: 0x00081335
	public UI_Set()
	{
		this.selfView = new UI_Set_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_set", base.GetType());
	}

	// Token: 0x0600152E RID: 5422 RVA: 0x0008315F File Offset: 0x0008135F
	protected override void ButtonAddClick()
	{
		this.selfView.btn_close.AddButtonEvent(delegate
		{
			base.CloseSelfPanel();
		});
	}

	// Token: 0x0600152F RID: 5423 RVA: 0x0006DDD3 File Offset: 0x0006BFD3
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
	}

	// Token: 0x040013D9 RID: 5081
	public UI_Set_View selfView;
}
