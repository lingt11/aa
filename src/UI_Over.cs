using System;

// Token: 0x02000360 RID: 864
public class UI_Over : UGUICtrl
{
	// Token: 0x060013B9 RID: 5049 RVA: 0x00079E11 File Offset: 0x00078011
	public UI_Over()
	{
		this.selfView = new UI_Over_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_over", base.GetType());
	}

	// Token: 0x060013BA RID: 5050 RVA: 0x00079E3B File Offset: 0x0007803B
	protected override void ButtonAddClick()
	{
		this.selfView.btn_backmenu.AddButtonEvent(delegate
		{
			base.CloseSelfPanel();
			GameHelperClient.OnGameReset();
		});
	}

	// Token: 0x060013BB RID: 5051 RVA: 0x00079E5C File Offset: 0x0007805C
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		if (data.ToString().Equals("win"))
		{
			this.selfView.ltext_info.text = Game.Language.Get("tip_win", "");
			Game.UI.OpenUI<UI_Over>("win");
			return;
		}
		this.selfView.ltext_info.text = Game.Language.Get("tip_failed", "");
		Game.UI.OpenUI<UI_Over>("failed");
	}

	// Token: 0x04001251 RID: 4689
	public UI_Over_View selfView;
}
