using System;
using DG.Tweening;

// Token: 0x020003AD RID: 941
public class UI_ShopSkillKey : UGUICtrl
{
	// Token: 0x0600158E RID: 5518 RVA: 0x00086553 File Offset: 0x00084753
	public UI_ShopSkillKey()
	{
		this.selfView = new UI_ShopSkillKey_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_shopSkillKey", base.GetType());
	}

	// Token: 0x0600158F RID: 5519 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ButtonAddClick()
	{
	}

	// Token: 0x06001590 RID: 5520 RVA: 0x00079D41 File Offset: 0x00077F41
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.canvasGroup.alpha = 0f;
		this.canvasGroup.DOFade(1f, 1f);
	}

	// Token: 0x06001591 RID: 5521 RVA: 0x00079D70 File Offset: 0x00077F70
	protected override void ClosePanel()
	{
		base.ClosePanel();
		this.canvasGroup.DOKill(false);
	}

	// Token: 0x06001592 RID: 5522 RVA: 0x00086580 File Offset: 0x00084780
	public void AfterDestroy()
	{
		this.canvasGroup.DOFade(0f, 1f);
		Game.TimerManager.AddTimer(1f, delegate()
		{
			Game.UI.DestroyUI<UI_ShopSkillKey>();
		});
	}

	// Token: 0x04001452 RID: 5202
	public UI_ShopSkillKey_View selfView;
}
