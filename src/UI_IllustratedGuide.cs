using System;
using UnityEngine.Events;

// Token: 0x02000337 RID: 823
public class UI_IllustratedGuide : UGUICtrl
{
	// Token: 0x060012D6 RID: 4822 RVA: 0x0007079D File Offset: 0x0006E99D
	public UI_IllustratedGuide()
	{
		this.selfView = new UI_IllustratedGuide_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_illustratedGuide", base.GetType());
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x000707C8 File Offset: 0x0006E9C8
	protected override void ButtonAddClick()
	{
		this.selfView.btn_quit.AddButtonEvent(new UnityAction(this.OnQuitBtnClick));
		this.selfView.btn_skill.AddButtonEvent(delegate
		{
			this.OnQuitBtnClick();
			Game.UI.OpenUI<UI_GuideSkill>(null);
		});
		this.selfView.btn_relic.AddButtonEvent(delegate
		{
			this.OnQuitBtnClick();
			Game.UI.OpenUI<UI_GuideRelic>(null);
		});
		this.selfView.btn_equip.AddButtonEvent(delegate
		{
			this.OnQuitBtnClick();
			Game.UI.OpenUI<UI_GuideEquip>(null);
		});
		this.selfView.btn_hero.AddButtonEvent(delegate
		{
			this.OnQuitBtnClick();
			Game.UI.OpenUI<UI_GuideHero>(null);
		});
		this.selfView.btn_gamedec.AddButtonEvent(delegate
		{
			Util.ShowTips(Game.Language.Get("暂未开放", ""));
		});
		this.selfView.btn_monster.AddButtonEvent(delegate
		{
			Util.ShowTips(Game.Language.Get("暂未开放", ""));
		});
		this.selfView.btn_workshopMod.AddButtonEvent(delegate
		{
			this.OnQuitBtnClick();
			Game.UI.OpenUI<UI_WorkshopMods>(null);
		});
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x000708DB File Offset: 0x0006EADB
	private void OnQuitBtnClick()
	{
		Game.UI.CloseUI<UI_IllustratedGuide>();
	}

	// Token: 0x060012D9 RID: 4825 RVA: 0x0006DDD3 File Offset: 0x0006BFD3
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
	}

	// Token: 0x04001126 RID: 4390
	public UI_IllustratedGuide_View selfView;
}
