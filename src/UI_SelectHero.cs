using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200038F RID: 911
public class UI_SelectHero : UGUICtrl
{
	// Token: 0x060014B5 RID: 5301 RVA: 0x00080185 File Offset: 0x0007E385
	public UI_SelectHero()
	{
		this.selfView = new UI_SelectHero_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_selectHero", base.GetType());
	}

	// Token: 0x060014B6 RID: 5302 RVA: 0x000801AF File Offset: 0x0007E3AF
	protected override void ButtonAddClick()
	{
		this.selfView.btn_backMenu.AddButtonEvent(new UnityAction(this.OnQuitBtnClick));
		this.selfView.btn_card.AddButtonEvent(new UnityAction(this.OnCardBtnClick));
	}

	// Token: 0x060014B7 RID: 5303 RVA: 0x000801E9 File Offset: 0x0007E3E9
	private void OnQuitBtnClick()
	{
		(Game.UI.OpenUI<UI_Confirm>(null) as UI_Confirm).SetConfirmText(Game.Language.Get("是否返回主菜单", ""), new Action(this.OnQuitCallBack), null, null, "");
	}

	// Token: 0x060014B8 RID: 5304 RVA: 0x00080227 File Offset: 0x0007E427
	private void OnQuitCallBack()
	{
		GameHelperClient.OnGameReset();
	}

	// Token: 0x060014B9 RID: 5305 RVA: 0x0008022E File Offset: 0x0007E42E
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.selfView.trans_info.gameObject.SetActive(false);
		new List<string>();
		this.selfView.dd_selectHero.gameObject.SetActive(false);
	}

	// Token: 0x060014BA RID: 5306 RVA: 0x00080269 File Offset: 0x0007E469
	protected override void ClosePanel()
	{
		this.selfView.dd_selectHero.onValueChanged.RemoveListener(new UnityAction<int>(this.OnSelectHero));
	}

	// Token: 0x060014BB RID: 5307 RVA: 0x0008028C File Offset: 0x0007E48C
	private void OnSelectHero(int index)
	{
		NetworkClient.connection.Send<ServerNetMessage>(new ServerNetMessage
		{
			serverNetOperation = ServerNetOperation.SelectHero,
			datas = new int[]
			{
				index
			}
		}, 0);
	}

	// Token: 0x060014BC RID: 5308 RVA: 0x000802C8 File Offset: 0x0007E4C8
	public void ShowHeroInfo(int index)
	{
		if (index == -1)
		{
			this.selfView.trans_info.gameObject.SetActive(false);
			return;
		}
		this.selfView.trans_info.gameObject.SetActive(true);
		string heroName = Util.GetHeroName((HeroType)index);
		this.selfView.ltext_heroInfo.text = heroName + "\n\n" + UI_SelectHero.GetHeroInfo((HeroType)index);
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x00080330 File Offset: 0x0007E530
	public static string GetHeroInfo(HeroType heroType)
	{
		object dic = ExcelManager.allExcelData["hero"];
		int num = (int)heroType;
		object dic2 = dic.DIC(num.ToString());
		string text = Game.Language.Get("skill", "");
		string text2 = Game.Language.Get("hero_" + dic2.DIC("id") + "_info", "");
		dic2.DIC("skill");
		string text3;
		if (!dic2.DIC("zhuDong"))
		{
			int num2 = dic2.DIC("skill");
			text3 = Game.Language.Get("p_" + num2.ToString(), "");
		}
		else
		{
			int num3 = dic2.DIC("skill");
			text3 = Game.Language.Get("a_" + num3.ToString(), "");
		}
		string text4 = string.Concat(new string[]
		{
			Game.Language.Get("力量成长", ""),
			":",
			dic2.DIC("STRadd"),
			"\n",
			Game.Language.Get("敏捷成长", ""),
			":",
			dic2.DIC("AGIadd"),
			"\n",
			Game.Language.Get("耐力成长", ""),
			":",
			dic2.DIC("STAadd")
		});
		return string.Concat(new string[]
		{
			text,
			":",
			text3,
			"\n\n",
			text2,
			"\n\n",
			text4
		});
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x000804F3 File Offset: 0x0007E6F3
	public void ShowAllHero(List<GameObject> list)
	{
		if (GameHelperClient.IsJoyStick)
		{
			this.heroList = list;
			this.selectIndex = 0;
			this.ShowSelectHero(0);
		}
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x00080514 File Offset: 0x0007E714
	private void ShowSelectHero(int add)
	{
		this.heroList[this.selectIndex].transform.localScale = Vector3.one;
		this.selectIndex += add;
		if (this.selectIndex < 0)
		{
			this.selectIndex = 0;
		}
		if (this.selectIndex > 7)
		{
			this.selectIndex = 7;
		}
		this.heroList[this.selectIndex].transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
		int index = this.heroList[this.selectIndex].name.Split('_', StringSplitOptions.None)[1].ToInt32();
		UI_SelectHero ui = Game.UI.GetUI<UI_SelectHero>();
		if (ui == null)
		{
			return;
		}
		ui.ShowHeroInfo(index);
	}

	// Token: 0x060014C0 RID: 5312 RVA: 0x000805DC File Offset: 0x0007E7DC
	public override void Update()
	{
		base.Update();
		if (this.isOpen)
		{
			base.CheckUpButton(delegate
			{
				this.ShowSelectHero(-4);
			});
			base.CheckDownButton(delegate
			{
				this.ShowSelectHero(4);
			});
			base.CheckLeftButton(delegate
			{
				this.ShowSelectHero(-1);
			});
			base.CheckRightButton(delegate
			{
				this.ShowSelectHero(1);
			});
			base.CheckAButton(delegate
			{
				(NetworkManager.singleton as MyServerNetworkManager).OnSelectHero(this.selectIndex);
			});
		}
	}

	// Token: 0x060014C1 RID: 5313 RVA: 0x0002B0F5 File Offset: 0x000292F5
	private void OnCardBtnClick()
	{
		Game.UI.OpenUI<UI_MyCard>(null);
	}

	// Token: 0x0400134D RID: 4941
	public UI_SelectHero_View selfView;

	// Token: 0x0400134E RID: 4942
	private int selectIndex;

	// Token: 0x0400134F RID: 4943
	private List<GameObject> heroList;
}
