using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000326 RID: 806
public class UI_GuideHero : UGUICtrl
{
	// Token: 0x06001285 RID: 4741 RVA: 0x0006E540 File Offset: 0x0006C740
	public UI_GuideHero()
	{
		this.selfView = new UI_GuideHero_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_guideHero", base.GetType());
		this.guideDecItem = this.selfView.trans_Dec.GetComponent<UI_GuideDecItem>();
		this.guideDecItem.gameObject.SetActive(false);
	}

	// Token: 0x06001286 RID: 4742 RVA: 0x0006E59C File Offset: 0x0006C79C
	protected override void ButtonAddClick()
	{
		this.selfView.btn_back.AddButtonEvent(new UnityAction(this.OnQuitBtnClick));
	}

	// Token: 0x06001287 RID: 4743 RVA: 0x0006E5BA File Offset: 0x0006C7BA
	private void OnQuitBtnClick()
	{
		Game.UI.CloseUI<UI_GuideHero>();
		Game.UI.OpenUI<UI_IllustratedGuide>(null);
	}

	// Token: 0x06001288 RID: 4744 RVA: 0x0006E5D2 File Offset: 0x0006C7D2
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.UpdateHeroView();
	}

	// Token: 0x06001289 RID: 4745 RVA: 0x0006E5E4 File Offset: 0x0006C7E4
	private void UpdateHeroView()
	{
		if (this.isInit)
		{
			return;
		}
		this.isInit = true;
		List<HeroType> list = new List<HeroType>();
		foreach (object obj in Enum.GetValues(typeof(HeroType)))
		{
			HeroType heroType = (HeroType)obj;
			if (heroType != HeroType.None)
			{
				if (GameHelperClient.isSaveHero)
				{
					Dictionary<string, RoleAttribute> heroAttributeDic = Game.GameData.HeroAttributeDic;
					int num = (int)heroType;
					if (heroAttributeDic[num.ToString()].isSave)
					{
						Dictionary<string, RoleAttribute> heroAttributeDic2 = Game.GameData.HeroAttributeDic;
						num = (int)heroType;
						if (!heroAttributeDic2[num.ToString()].isSaveMode)
						{
							continue;
						}
					}
				}
				list.Add(heroType);
			}
		}
		foreach (HeroType hero in list)
		{
			this.selfView.pool_skillList.AddView().transform.GetComponent<UI_GuideHeroItem>().SetHero(hero);
		}
	}

	// Token: 0x0600128A RID: 4746 RVA: 0x0006E700 File Offset: 0x0006C900
	public void ShowDec(string skillName, string skillInfo, Sprite sprite)
	{
		if (!this.guideDecItem.gameObject.activeSelf)
		{
			this.guideDecItem.gameObject.SetActive(true);
		}
		this.guideDecItem.SetSkill(skillName, skillInfo, sprite);
	}

	// Token: 0x040010C5 RID: 4293
	private bool isInit;

	// Token: 0x040010C6 RID: 4294
	public UI_GuideHero_View selfView;

	// Token: 0x040010C7 RID: 4295
	private UI_GuideDecItem guideDecItem;
}
