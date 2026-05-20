using System;
using DG.Tweening;

// Token: 0x0200038C RID: 908
public class UI_RookieMoveKey : UGUICtrl
{
	// Token: 0x060014AB RID: 5291 RVA: 0x000800B8 File Offset: 0x0007E2B8
	public UI_RookieMoveKey()
	{
		this.selfView = new UI_RookieMoveKey_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_rookieMoveKey", base.GetType());
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ButtonAddClick()
	{
	}

	// Token: 0x060014AD RID: 5293 RVA: 0x00079D41 File Offset: 0x00077F41
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.canvasGroup.alpha = 0f;
		this.canvasGroup.DOFade(1f, 1f);
	}

	// Token: 0x060014AE RID: 5294 RVA: 0x00079D70 File Offset: 0x00077F70
	protected override void ClosePanel()
	{
		base.ClosePanel();
		this.canvasGroup.DOKill(false);
	}

	// Token: 0x060014AF RID: 5295 RVA: 0x000800E4 File Offset: 0x0007E2E4
	public void AfterDestroy()
	{
		this.canvasGroup.DOFade(0f, 1f);
		Game.TimerManager.AddTimer(1f, delegate()
		{
			Game.UI.DestroyUI<UI_RookieMoveKey>();
			Game.PlayerManagerClient.StartRookieGuide(RookieGuideManager.RookieGuideMask.ShopKey);
		});
	}

	// Token: 0x04001348 RID: 4936
	public UI_RookieMoveKey_View selfView;
}
