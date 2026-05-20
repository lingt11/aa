using System;
using DG.Tweening;

// Token: 0x020003BF RID: 959
public class UI_UseSkillBookKey : UGUICtrl
{
	// Token: 0x060015E8 RID: 5608 RVA: 0x000880BF File Offset: 0x000862BF
	public UI_UseSkillBookKey()
	{
		this.selfView = new UI_UseSkillBookKey_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_useSkillBookKey", base.GetType());
	}

	// Token: 0x060015E9 RID: 5609 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ButtonAddClick()
	{
	}

	// Token: 0x060015EA RID: 5610 RVA: 0x00079D41 File Offset: 0x00077F41
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.canvasGroup.alpha = 0f;
		this.canvasGroup.DOFade(1f, 1f);
	}

	// Token: 0x060015EB RID: 5611 RVA: 0x00079D70 File Offset: 0x00077F70
	protected override void ClosePanel()
	{
		base.ClosePanel();
		this.canvasGroup.DOKill(false);
	}

	// Token: 0x060015EC RID: 5612 RVA: 0x000880EC File Offset: 0x000862EC
	public void AfterDestroy()
	{
		this.canvasGroup.DOFade(0f, 1f);
		Game.TimerManager.AddTimer(1f, delegate()
		{
			Game.UI.DestroyUI<UI_UseSkillBookKey>();
		});
	}

	// Token: 0x040014A4 RID: 5284
	public UI_UseSkillBookKey_View selfView;
}
