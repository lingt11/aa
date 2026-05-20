using System;
using DG.Tweening;

// Token: 0x0200035D RID: 861
public class UI_OpenShopKey : UGUICtrl
{
	// Token: 0x060013AF RID: 5039 RVA: 0x00079D17 File Offset: 0x00077F17
	public UI_OpenShopKey()
	{
		this.selfView = new UI_OpenShopKey_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_openShopKey", base.GetType());
	}

	// Token: 0x060013B0 RID: 5040 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ButtonAddClick()
	{
	}

	// Token: 0x060013B1 RID: 5041 RVA: 0x00079D41 File Offset: 0x00077F41
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.canvasGroup.alpha = 0f;
		this.canvasGroup.DOFade(1f, 1f);
	}

	// Token: 0x060013B2 RID: 5042 RVA: 0x00079D70 File Offset: 0x00077F70
	protected override void ClosePanel()
	{
		base.ClosePanel();
		this.canvasGroup.DOKill(false);
	}

	// Token: 0x060013B3 RID: 5043 RVA: 0x00079D88 File Offset: 0x00077F88
	public void AfterDestroy()
	{
		this.canvasGroup.DOFade(0f, 1f);
		Game.TimerManager.AddTimer(1f, delegate()
		{
			Game.UI.DestroyUI<UI_OpenShopKey>();
			Game.PlayerManagerClient.StartRookieGuide(RookieGuideManager.RookieGuideMask.ShopSkillKey);
		});
	}

	// Token: 0x0400124D RID: 4685
	public UI_OpenShopKey_View selfView;
}
