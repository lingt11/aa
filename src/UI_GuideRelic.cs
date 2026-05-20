using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200032E RID: 814
public class UI_GuideRelic : UGUICtrl
{
	// Token: 0x060012B1 RID: 4785 RVA: 0x0006F77C File Offset: 0x0006D97C
	public UI_GuideRelic()
	{
		this.selfView = new UI_GuideRelic_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_guideRelic", base.GetType());
		UI_GuideRelic.ShowListUI showListUI = default(UI_GuideRelic.ShowListUI);
		showListUI.listNameGo = this.selfView.trans_skillName.gameObject;
		showListUI.listNameTex = showListUI.listNameGo.transform.GetChild(0).GetComponent<Text>();
		showListUI.poolView = this.selfView.pool_skillList;
		this.ShowListUIAry.Add(showListUI);
		this.tipInfoRect = this.selfView.trans_tipInfo.GetComponent<RectTransform>();
		this.selfView.trans_tipInfo.gameObject.SetActive(false);
	}

	// Token: 0x060012B2 RID: 4786 RVA: 0x0006F842 File Offset: 0x0006DA42
	protected override void ButtonAddClick()
	{
		this.selfView.btn_back.AddButtonEvent(new UnityAction(this.OnQuitBtnClick));
	}

	// Token: 0x060012B3 RID: 4787 RVA: 0x0006F860 File Offset: 0x0006DA60
	private void OnQuitBtnClick()
	{
		Game.UI.CloseUI<UI_GuideRelic>();
		Game.UI.OpenUI<UI_IllustratedGuide>(null);
	}

	// Token: 0x060012B4 RID: 4788 RVA: 0x0006F878 File Offset: 0x0006DA78
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.UpdateRelicView();
	}

	// Token: 0x060012B5 RID: 4789 RVA: 0x0006F888 File Offset: 0x0006DA88
	private void UpdateRelicView()
	{
		if (this.isInit)
		{
			return;
		}
		this.isInit = true;
		Dictionary<int, List<ItemType>> dictionary = new Dictionary<int, List<ItemType>>();
		int num = 0;
		foreach (KeyValuePair<ItemType, RemainsData> keyValuePair in Game.GameData.RemainsDataDic)
		{
			if (!dictionary.ContainsKey(keyValuePair.Value.grade))
			{
				dictionary[keyValuePair.Value.grade] = new List<ItemType>();
				this.GetShowListUI(num);
				num++;
			}
			dictionary[keyValuePair.Value.grade].Add(keyValuePair.Key);
		}
		num = 0;
		foreach (KeyValuePair<int, List<ItemType>> keyValuePair2 in dictionary)
		{
			UI_GuideRelic.ShowListUI showListUI = this.ShowListUIAry[num];
			showListUI.listNameTex.text = Game.Language.Get(PathDefine.Concat("quality_", keyValuePair2.Key), "");
			foreach (ItemType itemTypeValue in keyValuePair2.Value)
			{
				showListUI.poolView.AddView().transform.GetComponent<UI_GuideRelicItem>().SetRelic(itemTypeValue, keyValuePair2.Key);
			}
			num++;
		}
	}

	// Token: 0x060012B6 RID: 4790 RVA: 0x0006FA2C File Offset: 0x0006DC2C
	private UI_GuideRelic.ShowListUI GetShowListUI(int listIndex)
	{
		if (this.ShowListUIAry.Count > listIndex)
		{
			UI_GuideRelic.ShowListUI showListUI = this.ShowListUIAry[listIndex];
			showListUI.listNameGo.SetActive(true);
			showListUI.poolView.gameObject.SetActive(true);
			return showListUI;
		}
		UI_GuideRelic.ShowListUI showListUI2 = default(UI_GuideRelic.ShowListUI);
		GameObject listNameGo = Object.Instantiate<GameObject>(this.selfView.trans_skillName.gameObject, this.selfView.trans_skillName.parent);
		showListUI2.listNameGo = listNameGo;
		showListUI2.listNameTex = showListUI2.listNameGo.transform.GetChild(0).GetComponent<Text>();
		PoolView poolView = Object.Instantiate<PoolView>(this.selfView.pool_skillList, this.selfView.trans_skillName.parent);
		showListUI2.poolView = poolView;
		showListUI2.poolView.RemoveAllView();
		this.ShowListUIAry.Add(showListUI2);
		return showListUI2;
	}

	// Token: 0x060012B7 RID: 4791 RVA: 0x0006FB08 File Offset: 0x0006DD08
	public void ShowRelicInfo(Vector3 cardPosition, string relicName, string relicInfo)
	{
		this.tipInfoRect.gameObject.SetActive(true);
		this.tipInfoRect.position = cardPosition;
		this.tipInfoRect.anchoredPosition += new Vector2(45f, -65f);
		this.selfView.ltext_tipTitle.text = relicName;
		this.selfView.ltext_tipInfo.text = relicInfo;
	}

	// Token: 0x060012B8 RID: 4792 RVA: 0x0006FB79 File Offset: 0x0006DD79
	public void HideRelicInfo()
	{
		this.tipInfoRect.gameObject.SetActive(false);
	}

	// Token: 0x040010FB RID: 4347
	private List<UI_GuideRelic.ShowListUI> ShowListUIAry = new List<UI_GuideRelic.ShowListUI>();

	// Token: 0x040010FC RID: 4348
	public UI_GuideRelic_View selfView;

	// Token: 0x040010FD RID: 4349
	private bool isInit;

	// Token: 0x040010FE RID: 4350
	private RectTransform tipInfoRect;

	// Token: 0x0200032F RID: 815
	private struct ShowListUI
	{
		// Token: 0x040010FF RID: 4351
		public GameObject listNameGo;

		// Token: 0x04001100 RID: 4352
		public Text listNameTex;

		// Token: 0x04001101 RID: 4353
		public PoolView poolView;
	}
}
