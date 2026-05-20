using System;
using UnityEngine.Events;

// Token: 0x0200038A RID: 906
public class UI_RoleInfoDebug : UGUICtrl
{
	// Token: 0x060014A5 RID: 5285 RVA: 0x0007FF53 File Offset: 0x0007E153
	public UI_RoleInfoDebug()
	{
		this.selfView = new UI_RoleInfoDebug_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_roleInfoDebug", base.GetType());
	}

	// Token: 0x060014A6 RID: 5286 RVA: 0x0007FF7D File Offset: 0x0007E17D
	protected override void ButtonAddClick()
	{
		this.selfView.btn_refresh.AddButtonEvent(new UnityAction(this.RefreshData));
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x0006DDD3 File Offset: 0x0006BFD3
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
	}

	// Token: 0x060014A8 RID: 5288 RVA: 0x0007FF9C File Offset: 0x0007E19C
	private void RefreshData()
	{
		string text = "力量:" + GameHelperClient.localPlayer.STR.ToString();
		text = text + "\n耐力:" + GameHelperClient.localPlayer.STA.ToString();
		text = text + "\n敏捷:" + GameHelperClient.localPlayer.AGI.ToString();
		text = text + "\n闪避:" + GameHelperClient.localPlayer.doge.ToString();
		text = text + "\n等级:" + GameHelperClient.localPlayer.Level.ToString();
		text = text + "\nnowExp:" + GameHelperClient.localPlayer.playerAttribute.NowExp.ToString();
		text = text + "\nmaxExp:" + GameHelperClient.localPlayer.playerAttribute.maxExp.ToString();
		this.selfView.ltext_info.text = text;
	}

	// Token: 0x04001345 RID: 4933
	public UI_RoleInfoDebug_View selfView;
}
