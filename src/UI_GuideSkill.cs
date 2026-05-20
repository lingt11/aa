using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000333 RID: 819
public class UI_GuideSkill : UGUICtrl
{
	// Token: 0x060012C2 RID: 4802 RVA: 0x0006FE44 File Offset: 0x0006E044
	public UI_GuideSkill()
	{
		bool[] array = new bool[6];
		array[0] = true;
		this.sortToggles = array;
		this.sortKeys = new List<string>
		{
			"D",
			"C",
			"B",
			"A",
			"S",
			"H"
		};
		base..ctor();
		this.selfView = new UI_GuideSkill_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_guideSkill", base.GetType());
		this.InitData();
		UI_GuideSkill.ShowListUI showListUI = default(UI_GuideSkill.ShowListUI);
		showListUI.listNameGo = this.selfView.trans_skillName.gameObject;
		showListUI.listNameTex = showListUI.listNameGo.transform.GetChild(0).GetComponent<Text>();
		showListUI.poolView = this.selfView.pool_skillList;
		this.ShowListUIAry.Add(showListUI);
		this.guideDecItem = this.selfView.trans_Dec.GetComponent<UI_GuideDecItem>();
		this.guideDecItem.gameObject.SetActive(false);
		this.InitSortBtn();
	}

	// Token: 0x060012C3 RID: 4803 RVA: 0x0006FF7C File Offset: 0x0006E17C
	private void InitData()
	{
		foreach (KeyValuePair<string, object> keyValuePair in ((Dictionary<string, object>)ExcelManager.allExcelData["activeSkill"]))
		{
			string key = keyValuePair.Value.DIC("quality") + "_a";
			if (!this.allSkillData.ContainsKey(key))
			{
				this.allSkillData[key] = new List<object>();
			}
			this.allSkillData[key].Add(keyValuePair.Value);
		}
		foreach (KeyValuePair<string, object> keyValuePair2 in ((Dictionary<string, object>)ExcelManager.allExcelData["passsiveSkill"]))
		{
			string key2 = keyValuePair2.Value.DIC("quality") + "_p";
			if (!this.allSkillData.ContainsKey(key2))
			{
				this.allSkillData[key2] = new List<object>();
			}
			this.allSkillData[key2].Add(keyValuePair2.Value);
		}
	}

	// Token: 0x060012C4 RID: 4804 RVA: 0x000700CC File Offset: 0x0006E2CC
	protected override void ButtonAddClick()
	{
		this.selfView.btn_back.AddButtonEvent(new UnityAction(this.OnQuitBtnClick));
	}

	// Token: 0x060012C5 RID: 4805 RVA: 0x000700EC File Offset: 0x0006E2EC
	private void InitSortBtn()
	{
		for (int i = 0; i < this.selfView.trans_QuaSort.childCount; i++)
		{
			Toggle component = this.selfView.trans_QuaSort.GetChild(i).GetComponent<Toggle>();
			component.onValueChanged.AddListener(new UnityAction<bool>(this.OnQuaSortToggleChanged));
			this.sortToggles[i] = component.isOn;
		}
		Toggle component2 = this.selfView.trans_toActive.GetComponent<Toggle>();
		this.isActiveOn = component2.isOn;
		component2.onValueChanged.AddListener(new UnityAction<bool>(this.OnActiveToggleChanged));
		Toggle component3 = this.selfView.trans_toPass.GetComponent<Toggle>();
		this.isPassiveOn = component3.isOn;
		component3.onValueChanged.AddListener(new UnityAction<bool>(this.OnPassiveToggleChanged));
	}

	// Token: 0x060012C6 RID: 4806 RVA: 0x000701B8 File Offset: 0x0006E3B8
	private void OnQuaSortToggleChanged(bool isOn)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		for (int i = 0; i < this.selfView.trans_QuaSort.childCount; i++)
		{
			Toggle component = this.selfView.trans_QuaSort.GetChild(i).GetComponent<Toggle>();
			this.sortToggles[i] = component.isOn;
		}
		this.UpdateSkillView();
	}

	// Token: 0x060012C7 RID: 4807 RVA: 0x0007022C File Offset: 0x0006E42C
	private void OnActiveToggleChanged(bool isOn)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		this.isActiveOn = isOn;
		this.UpdateSkillView();
	}

	// Token: 0x060012C8 RID: 4808 RVA: 0x0007025C File Offset: 0x0006E45C
	private void OnPassiveToggleChanged(bool isOn)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		this.isPassiveOn = isOn;
		this.UpdateSkillView();
	}

	// Token: 0x060012C9 RID: 4809 RVA: 0x0007028C File Offset: 0x0006E48C
	private void OnQuitBtnClick()
	{
		Game.UI.CloseUI<UI_GuideSkill>();
		Game.UI.OpenUI<UI_IllustratedGuide>(null);
	}

	// Token: 0x060012CA RID: 4810 RVA: 0x000702A4 File Offset: 0x0006E4A4
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.UpdateSkillView();
	}

	// Token: 0x060012CB RID: 4811 RVA: 0x000702B4 File Offset: 0x0006E4B4
	private void UpdateSkillView()
	{
		int count = this.ShowListUIAry.Count;
		for (int i = 0; i < count; i++)
		{
			UI_GuideSkill.ShowListUI showListUI = this.ShowListUIAry[i];
			showListUI.listNameGo.SetActive(false);
			showListUI.poolView.RemoveAllView();
			showListUI.poolView.gameObject.SetActive(false);
		}
		int num = 0;
		foreach (KeyValuePair<string, List<object>> keyValuePair in this.allSkillData)
		{
			string[] array = keyValuePair.Key.Split("_", StringSplitOptions.None);
			bool flag = array[1].Equals("p");
			if (flag)
			{
				if (!this.isPassiveOn)
				{
					continue;
				}
			}
			else if (!this.isActiveOn)
			{
				continue;
			}
			int num2 = this.sortKeys.IndexOf(array[0]);
			if (this.sortToggles[num2])
			{
				UI_GuideSkill.ShowListUI showListUI2 = this.GetShowListUI(num);
				showListUI2.listNameTex.text = SkillBase.GetQuaTex(flag, DropDefine.QualityAry.IndexOf(array[0]));
				this.ShowSkillList(keyValuePair.Value, showListUI2.poolView, flag);
				num++;
			}
		}
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x000703F0 File Offset: 0x0006E5F0
	private UI_GuideSkill.ShowListUI GetShowListUI(int listIndex)
	{
		if (this.ShowListUIAry.Count > listIndex)
		{
			UI_GuideSkill.ShowListUI showListUI = this.ShowListUIAry[listIndex];
			showListUI.listNameGo.SetActive(true);
			showListUI.poolView.gameObject.SetActive(true);
			return showListUI;
		}
		UI_GuideSkill.ShowListUI showListUI2 = default(UI_GuideSkill.ShowListUI);
		GameObject listNameGo = Object.Instantiate<GameObject>(this.selfView.trans_skillName.gameObject, this.selfView.trans_skillName.parent);
		showListUI2.listNameGo = listNameGo;
		showListUI2.listNameTex = showListUI2.listNameGo.transform.GetChild(0).GetComponent<Text>();
		PoolView poolView = Object.Instantiate<PoolView>(this.selfView.pool_skillList, this.selfView.trans_skillName.parent);
		showListUI2.poolView = poolView;
		showListUI2.poolView.RemoveAllView();
		this.ShowListUIAry.Add(showListUI2);
		return showListUI2;
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x000704CC File Offset: 0x0006E6CC
	private void ShowSkillList(List<object> list, PoolView poolView, bool isPassive)
	{
		for (int i = 0; i < list.Count; i++)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)list[i];
			if (!dictionary.DIC("lock") && (!GameHelperClient.isSaveHero || !dictionary.DIC("saveMode")))
			{
				GameObject go = poolView.AddView();
				this.SetData(go, dictionary, isPassive);
			}
		}
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x00070528 File Offset: 0x0006E728
	private void SetData(GameObject go, Dictionary<string, object> listItem, bool isPassive)
	{
		UI_GuideSkillItem component = go.transform.GetComponent<UI_GuideSkillItem>();
		string text = listItem.DIC("icon");
		if (GameHelperClient.isSaveHero && listItem.DIC("saveMode"))
		{
			text = PathDefine.Concat(text, StringDefine.SaveMode);
		}
		string text2 = listItem.DIC("id");
		string skillName = Game.Language.Get(isPassive ? ("p_" + text2) : ("a_" + text2), "");
		component.SetSkill(listItem, text2, skillName, PathDefine.Concat("Bundles/UI/Icon/Skill/", text), isPassive);
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x000705B7 File Offset: 0x0006E7B7
	public void ShowSkillDec(string skillName, string skillInfo, Sprite sprite)
	{
		if (!this.guideDecItem.gameObject.activeSelf)
		{
			this.guideDecItem.gameObject.SetActive(true);
		}
		this.guideDecItem.SetSkill(skillName, skillInfo, sprite);
	}

	// Token: 0x0400110E RID: 4366
	private List<UI_GuideSkill.ShowListUI> ShowListUIAry = new List<UI_GuideSkill.ShowListUI>();

	// Token: 0x0400110F RID: 4367
	private Dictionary<string, List<object>> allSkillData = new Dictionary<string, List<object>>();

	// Token: 0x04001110 RID: 4368
	private readonly bool[] sortToggles;

	// Token: 0x04001111 RID: 4369
	private readonly List<string> sortKeys;

	// Token: 0x04001112 RID: 4370
	private bool isActiveOn;

	// Token: 0x04001113 RID: 4371
	private bool isPassiveOn;

	// Token: 0x04001114 RID: 4372
	public UI_GuideSkill_View selfView;

	// Token: 0x04001115 RID: 4373
	private UI_GuideDecItem guideDecItem;

	// Token: 0x02000334 RID: 820
	private struct ShowListUI
	{
		// Token: 0x04001116 RID: 4374
		public GameObject listNameGo;

		// Token: 0x04001117 RID: 4375
		public Text listNameTex;

		// Token: 0x04001118 RID: 4376
		public PoolView poolView;
	}
}
