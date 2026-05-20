using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000321 RID: 801
public class UI_GuideEquip : UGUICtrl
{
	// Token: 0x06001271 RID: 4721 RVA: 0x0006DE5C File Offset: 0x0006C05C
	public UI_GuideEquip()
	{
		this.selfView = new UI_GuideEquip_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_guideEquip", base.GetType());
		this.InitData();
		UI_GuideEquip.ShowListUI showListUI = default(UI_GuideEquip.ShowListUI);
		showListUI.listNameGo = this.selfView.trans_skillName.gameObject;
		showListUI.listNameTex = showListUI.listNameGo.transform.GetChild(0).GetComponent<Text>();
		showListUI.poolView = this.selfView.pool_skillList;
		this.ShowListUIAry.Add(showListUI);
		this.guideDecItem = this.selfView.trans_Dec.GetComponent<UI_GuideDecItem>();
		this.guideDecItem.gameObject.SetActive(false);
		this.InitEquipTypeToggle();
	}

	// Token: 0x06001272 RID: 4722 RVA: 0x0006DF34 File Offset: 0x0006C134
	private void InitData()
	{
		this.allEquipData.Clear();
		this.allEquipData.Add(UI_GuideEquip.EquipType.Normal, new List<object>());
		this.allEquipData.Add(UI_GuideEquip.EquipType.Myth, new List<object>());
		foreach (KeyValuePair<string, object> keyValuePair in ((Dictionary<string, object>)ExcelManager.allExcelData["equipment"]))
		{
			int num = int.Parse(keyValuePair.Value.DIC("id"));
			if (num <= 2000)
			{
				if (num < 100)
				{
					this.allEquipData[UI_GuideEquip.EquipType.Normal].Add(keyValuePair.Value);
				}
				else
				{
					this.allEquipData[UI_GuideEquip.EquipType.Myth].Add(keyValuePair.Value);
				}
			}
		}
	}

	// Token: 0x06001273 RID: 4723 RVA: 0x0006E014 File Offset: 0x0006C214
	private void InitEquipTypeToggle()
	{
		Toggle component = this.selfView.trans_Normal.GetComponent<Toggle>();
		Toggle component2 = this.selfView.trans_Myth.GetComponent<Toggle>();
		component.isOn = true;
		component2.isOn = true;
		this.isNormalOn = component.isOn;
		this.isMythOn = component2.isOn;
		component.onValueChanged.AddListener(new UnityAction<bool>(this.OnNormalToggleChanged));
		component2.onValueChanged.AddListener(new UnityAction<bool>(this.OnMythToggleChanged));
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x0006E097 File Offset: 0x0006C297
	protected override void ButtonAddClick()
	{
		this.selfView.btn_back.AddButtonEvent(new UnityAction(this.OnQuitBtnClick));
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x0006E0B5 File Offset: 0x0006C2B5
	private void OnQuitBtnClick()
	{
		Game.UI.CloseUI<UI_GuideEquip>();
		Game.UI.OpenUI<UI_IllustratedGuide>(null);
	}

	// Token: 0x06001276 RID: 4726 RVA: 0x0006E0CD File Offset: 0x0006C2CD
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.UpdateEquipView();
	}

	// Token: 0x06001277 RID: 4727 RVA: 0x0006E0DC File Offset: 0x0006C2DC
	private void UpdateEquipView()
	{
		int count = this.ShowListUIAry.Count;
		for (int i = 0; i < count; i++)
		{
			UI_GuideEquip.ShowListUI showListUI = this.ShowListUIAry[i];
			showListUI.listNameGo.SetActive(false);
			showListUI.poolView.RemoveAllView();
			showListUI.poolView.gameObject.SetActive(false);
		}
		int num = 0;
		if (this.isNormalOn)
		{
			UI_GuideEquip.ShowListUI showListUI2 = this.GetShowListUI(num);
			showListUI2.listNameTex.text = this.GetEquipTypeName(UI_GuideEquip.EquipType.Normal);
			this.ShowEquipList(this.allEquipData[UI_GuideEquip.EquipType.Normal], showListUI2.poolView);
			num++;
		}
		if (this.isMythOn)
		{
			UI_GuideEquip.ShowListUI showListUI3 = this.GetShowListUI(num);
			showListUI3.listNameTex.text = this.GetEquipTypeName(UI_GuideEquip.EquipType.Myth);
			this.ShowEquipList(this.allEquipData[UI_GuideEquip.EquipType.Myth], showListUI3.poolView);
		}
	}

	// Token: 0x06001278 RID: 4728 RVA: 0x0006E1B1 File Offset: 0x0006C3B1
	private void OnNormalToggleChanged(bool isOn)
	{
		this.OnEquipTypeToggleChanged(UI_GuideEquip.EquipType.Normal, isOn);
	}

	// Token: 0x06001279 RID: 4729 RVA: 0x0006E1BB File Offset: 0x0006C3BB
	private void OnMythToggleChanged(bool isOn)
	{
		this.OnEquipTypeToggleChanged(UI_GuideEquip.EquipType.Myth, isOn);
	}

	// Token: 0x0600127A RID: 4730 RVA: 0x0006E1C5 File Offset: 0x0006C3C5
	private void OnEquipTypeToggleChanged(UI_GuideEquip.EquipType equipType, bool isOn)
	{
		if (equipType == UI_GuideEquip.EquipType.Normal)
		{
			this.isNormalOn = isOn;
		}
		else
		{
			this.isMythOn = isOn;
		}
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		this.UpdateEquipView();
	}

	// Token: 0x0600127B RID: 4731 RVA: 0x0006E201 File Offset: 0x0006C401
	private string GetEquipTypeName(UI_GuideEquip.EquipType equipType)
	{
		return Game.Language.Get((equipType == UI_GuideEquip.EquipType.Normal) ? "商店装备" : "神器", "");
	}

	// Token: 0x0600127C RID: 4732 RVA: 0x0006E224 File Offset: 0x0006C424
	private void ShowEquipList(List<object> list, PoolView poolView)
	{
		for (int i = 0; i < list.Count; i++)
		{
			poolView.AddView().transform.GetComponent<UI_GuideEquipItem>().SetEquip((Dictionary<string, object>)list[i]);
		}
	}

	// Token: 0x0600127D RID: 4733 RVA: 0x0006E264 File Offset: 0x0006C464
	private UI_GuideEquip.ShowListUI GetShowListUI(int listIndex)
	{
		if (this.ShowListUIAry.Count > listIndex)
		{
			UI_GuideEquip.ShowListUI showListUI = this.ShowListUIAry[listIndex];
			showListUI.listNameGo.SetActive(true);
			showListUI.poolView.gameObject.SetActive(true);
			return showListUI;
		}
		UI_GuideEquip.ShowListUI showListUI2 = default(UI_GuideEquip.ShowListUI);
		GameObject listNameGo = Object.Instantiate<GameObject>(this.selfView.trans_skillName.gameObject, this.selfView.trans_skillName.parent);
		showListUI2.listNameGo = listNameGo;
		showListUI2.listNameTex = showListUI2.listNameGo.transform.GetChild(0).GetComponent<Text>();
		PoolView poolView = Object.Instantiate<PoolView>(this.selfView.pool_skillList, this.selfView.trans_skillName.parent);
		showListUI2.poolView = poolView;
		showListUI2.poolView.RemoveAllView();
		this.ShowListUIAry.Add(showListUI2);
		return showListUI2;
	}

	// Token: 0x0600127E RID: 4734 RVA: 0x0006E33D File Offset: 0x0006C53D
	public void ShowDec(string skillName, string skillInfo, Sprite sprite)
	{
		if (!this.guideDecItem.gameObject.activeSelf)
		{
			this.guideDecItem.gameObject.SetActive(true);
		}
		this.guideDecItem.SetSkill(skillName, skillInfo, sprite);
	}

	// Token: 0x040010AE RID: 4270
	public UI_GuideEquip_View selfView;

	// Token: 0x040010AF RID: 4271
	private List<UI_GuideEquip.ShowListUI> ShowListUIAry = new List<UI_GuideEquip.ShowListUI>();

	// Token: 0x040010B0 RID: 4272
	private Dictionary<UI_GuideEquip.EquipType, List<object>> allEquipData = new Dictionary<UI_GuideEquip.EquipType, List<object>>();

	// Token: 0x040010B1 RID: 4273
	private bool isNormalOn;

	// Token: 0x040010B2 RID: 4274
	private bool isMythOn;

	// Token: 0x040010B3 RID: 4275
	private UI_GuideDecItem guideDecItem;

	// Token: 0x02000322 RID: 802
	private enum EquipType
	{
		// Token: 0x040010B5 RID: 4277
		Normal,
		// Token: 0x040010B6 RID: 4278
		Myth
	}

	// Token: 0x02000323 RID: 803
	private struct ShowListUI
	{
		// Token: 0x040010B7 RID: 4279
		public GameObject listNameGo;

		// Token: 0x040010B8 RID: 4280
		public Text listNameTex;

		// Token: 0x040010B9 RID: 4281
		public PoolView poolView;
	}
}
