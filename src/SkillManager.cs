using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000154 RID: 340
public class SkillManager : IUpdate, IApplicationQuit
{
	// Token: 0x060006B3 RID: 1715 RVA: 0x0002958E File Offset: 0x0002778E
	public static uint GetSkillId()
	{
		SkillManager.skillCreateId += 1U;
		return SkillManager.skillCreateId;
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x000295A1 File Offset: 0x000277A1
	public static uint GetSyncPassSkillIndex()
	{
		SkillManager.syncPassSkillIndex += 1U;
		return SkillManager.syncPassSkillIndex;
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x000295B4 File Offset: 0x000277B4
	public SkillManager()
	{
		this.GetData();
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0002962C File Offset: 0x0002782C
	private void GetData()
	{
		foreach (KeyValuePair<string, object> keyValuePair in ((Dictionary<string, object>)ExcelManager.allExcelData["passsiveSkill"]))
		{
			string key = keyValuePair.Value.DIC("quality");
			if (!keyValuePair.Value.DIC("lock"))
			{
				if (!this.passsiveSkillDic.ContainsKey(key))
				{
					this.passsiveSkillDic[key] = new List<object>();
				}
				this.passsiveSkillDic[key].Add(keyValuePair.Value);
			}
		}
		foreach (KeyValuePair<string, object> keyValuePair2 in ((Dictionary<string, object>)ExcelManager.allExcelData["activeSkill"]))
		{
			string key2 = keyValuePair2.Value.DIC("quality");
			if (!keyValuePair2.Value.DIC("lock"))
			{
				if (!this.activeSkillDic.ContainsKey(key2))
				{
					this.activeSkillDic[key2] = new List<object>();
				}
				this.activeSkillDic[key2].Add(keyValuePair2.Value);
			}
		}
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x0002978C File Offset: 0x0002798C
	public void GetRandomPassiveSkill(string quality)
	{
		RoguelikeUIData[] passiveSkillRoguelikeData = this.GetPassiveSkillRoguelikeData(quality);
		if (passiveSkillRoguelikeData == null || passiveSkillRoguelikeData.Length == 0)
		{
			return;
		}
		UI_Roguelike ui_Roguelike = Game.UI.OpenUI<UI_Roguelike>(null) as UI_Roguelike;
		if (GameHelperClient.CanSkillRefresh)
		{
			ui_Roguelike.ShowRoguelikeWithSlotRefresh(passiveSkillRoguelikeData, new Action<RoguelikeUIData>(this.OnPassiveSkillRoguelike), Game.Language.Get("被动选择", ""), new UI_Roguelike.IndexRefreshActionEvent(this.OnPassiveSkillRefreshRoguelike), this.passiveSkillRefreshNums, null, 0f, null, "passive_skill");
			return;
		}
		ui_Roguelike.ShowRoguelike(passiveSkillRoguelikeData, new Action<RoguelikeUIData>(this.OnPassiveSkillRoguelike), Game.Language.Get("被动选择", ""), null, null, 0f, null, "passive_skill");
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x0002983C File Offset: 0x00027A3C
	private void OnPassiveSkillRoguelike(RoguelikeUIData roguelikeUIData)
	{
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/学习被动技能", 1f, 3f);
		if (GameHelperClient.localPlayer.roleSkillList.Count > GameHelperClient.MaxSkillNum - 1)
		{
			Game.UI.GetUI<UI_PlayerState>().OnSwitchSkill(roguelikeUIData, new Action<RoguelikeUIData, SkillBase>(this.OnPassiveSkillSwitchSkill));
			return;
		}
		this.OnPassiveSkillSwitchSkill(roguelikeUIData, null);
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x000298A0 File Offset: 0x00027AA0
	private void OnPassiveSkillSwitchSkill(RoguelikeUIData roguelikeUIData, SkillBase removeSkill)
	{
		UI_Msg ui = Game.UI.GetUI<UI_Msg>();
		if (ui != null)
		{
			ui.ShowMsg(Game.Language.Get("studypass", "") + roguelikeUIData.name, false);
		}
		GameHelperClient.localPlayer.AddPasssiveSkillBook((PasssiveSkillEnum)int.Parse(roguelikeUIData.data), removeSkill);
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x000298F8 File Offset: 0x00027AF8
	public void GetRandomActiveSkill(string quality)
	{
		RoguelikeUIData[] activeSkillRoguelikeData = this.GetActiveSkillRoguelikeData(quality);
		if (activeSkillRoguelikeData == null || activeSkillRoguelikeData.Length == 0)
		{
			return;
		}
		UI_Roguelike ui_Roguelike = Game.UI.OpenUI<UI_Roguelike>(null) as UI_Roguelike;
		if (GameHelperClient.CanSkillRefresh)
		{
			ui_Roguelike.ShowRoguelikeWithSlotRefresh(activeSkillRoguelikeData, new Action<RoguelikeUIData>(this.OnActiveSkillRoguelike), Game.Language.Get("主动选择", ""), new UI_Roguelike.IndexRefreshActionEvent(this.OnActiveSkillRefreshRoguelike), this.activeSkillRefreshNums, null, 0f, null, "active_skill");
			return;
		}
		ui_Roguelike.ShowRoguelike(activeSkillRoguelikeData, new Action<RoguelikeUIData>(this.OnActiveSkillRoguelike), Game.Language.Get("主动选择", ""), null, null, 0f, null, "active_skill");
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x000299A8 File Offset: 0x00027BA8
	private void OnActiveSkillRoguelike(RoguelikeUIData roguelikeUIData)
	{
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/学习主动技能", 1f, 3f);
		if (GameHelperClient.localPlayer.isMageHat)
		{
			int num = int.Parse(roguelikeUIData.data);
			if (num < 200 && Game.GameData.ActiveSkillDataDic.ContainsKey(num + ActiveSkillEnum.C_SpellThunder))
			{
				Util.ShowTips("平凡法师帽！");
				roguelikeUIData.data = (num + 100).ToString();
			}
		}
		if (GameHelperClient.localPlayer.roleSkillList.Count > GameHelperClient.MaxSkillNum - 1)
		{
			Game.UI.GetUI<UI_PlayerState>().OnSwitchSkill(roguelikeUIData, new Action<RoguelikeUIData, SkillBase>(this.OnActiveSkillSwitchSkill));
			return;
		}
		this.OnActiveSkillSwitchSkill(roguelikeUIData, null);
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x00029A60 File Offset: 0x00027C60
	public void OnActiveSkillSwitchSkill(RoguelikeUIData roguelikeUIData, SkillBase removeSkill)
	{
		ActiveSkillEnum activeSkill = (ActiveSkillEnum)int.Parse(roguelikeUIData.data);
		UI_Msg ui = Game.UI.GetUI<UI_Msg>();
		if (ui != null)
		{
			ui.ShowMsg(Game.Language.Get("studyactive", "") + roguelikeUIData.name, false);
		}
		GameHelperClient.localPlayer.AddActiveSkillBook(activeSkill, removeSkill);
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x00029ABC File Offset: 0x00027CBC
	private RoguelikeUIData[] GetPassiveSkillRoguelikeData(string quality)
	{
		List<object> list = this.passsiveSkillDic[quality];
		this.myData.Clear();
		List<SkillBase> roleSkillList = GameHelperClient.localPlayer.roleSkillList;
		int count = roleSkillList.Count;
		foreach (object obj in list)
		{
			bool flag = true;
			for (int i = 0; i < count; i++)
			{
				if (!obj.DIC("repeat") && obj.DIC("name").Equals(roleSkillList[i].skillName))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				this.myData.Add(obj);
			}
		}
		return this.BuildSkillRoguelikeData(this.passiveSkillRefreshData, this.passiveSkillRefreshNums, true);
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x00029B94 File Offset: 0x00027D94
	private RoguelikeUIData[] GetActiveSkillRoguelikeData(string quality)
	{
		List<object> list = this.activeSkillDic[quality];
		this.myData.Clear();
		List<SkillBase> roleSkillList = GameHelperClient.localPlayer.roleSkillList;
		int count = roleSkillList.Count;
		foreach (object obj in list)
		{
			bool flag = true;
			for (int i = 0; i < count; i++)
			{
				if (obj.DIC("name").Equals(roleSkillList[i].skillName))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				this.myData.Add(obj);
			}
		}
		return this.BuildSkillRoguelikeData(this.activeSkillRefreshData, this.activeSkillRefreshNums, false);
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x00029C60 File Offset: 0x00027E60
	private RoguelikeUIData[] BuildSkillRoguelikeData(object[] skillRefreshData, int[] skillRefreshNums, bool isPassive)
	{
		int num = this.PrepareSkillRefreshData(skillRefreshData);
		if (num == 0)
		{
			this.ClearSkillRefreshNums(skillRefreshNums);
			return null;
		}
		int num2 = Mathf.Min(3, num);
		RoguelikeUIData[] array = new RoguelikeUIData[num2];
		for (int i = 0; i < num2; i++)
		{
			array[i] = this.CreateSkillRoguelikeData(skillRefreshData[i], isPassive);
			skillRefreshNums[i] = ((GameHelperClient.CanSkillRefresh && num > i + 3) ? 1 : 0);
		}
		for (int j = num2; j < skillRefreshNums.Length; j++)
		{
			skillRefreshNums[j] = 0;
		}
		return array;
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x00029CD9 File Offset: 0x00027ED9
	private RoguelikeUIData OnPassiveSkillRefreshRoguelike(int indexValue)
	{
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/学习主动技能", 1f, 3f);
		return this.CreateSkillRoguelikeData(this.passiveSkillRefreshData[indexValue + 3], true);
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x00029D06 File Offset: 0x00027F06
	private RoguelikeUIData OnActiveSkillRefreshRoguelike(int indexValue)
	{
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/学习主动技能", 1f, 3f);
		return this.CreateSkillRoguelikeData(this.activeSkillRefreshData[indexValue + 3], false);
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x00029D34 File Offset: 0x00027F34
	private int PrepareSkillRefreshData(object[] skillRefreshData)
	{
		this.ClearSkillRefreshData(skillRefreshData);
		int count = this.myData.Count;
		for (int i = 0; i < count; i++)
		{
			int num = Random.Range(0, count);
			List<object> list = this.myData;
			int index = num;
			List<object> list2 = this.myData;
			int index2 = i;
			object value = this.myData[i];
			object value2 = this.myData[num];
			list[index] = value;
			list2[index2] = value2;
		}
		int num2 = Mathf.Min(skillRefreshData.Length, count);
		for (int j = 0; j < num2; j++)
		{
			skillRefreshData[j] = this.myData[j];
		}
		return num2;
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x00029DE0 File Offset: 0x00027FE0
	private void ClearSkillRefreshData(object[] skillRefreshData)
	{
		for (int i = 0; i < skillRefreshData.Length; i++)
		{
			skillRefreshData[i] = null;
		}
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x00029E00 File Offset: 0x00028000
	private void ClearSkillRefreshNums(int[] skillRefreshNums)
	{
		for (int i = 0; i < skillRefreshNums.Length; i++)
		{
			skillRefreshNums[i] = 0;
		}
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x00029E20 File Offset: 0x00028020
	private RoguelikeUIData CreateSkillRoguelikeData(object skillData, bool isPassive)
	{
		string text = skillData.DIC("id");
		return new RoguelikeUIData
		{
			name = Game.Language.Get((isPassive ? "p_" : "a_") + text, ""),
			icon = "Bundles/UI/Icon/Skill/" + skillData.DIC("icon"),
			dec = SkillBase.GetSkillInfo(text, skillData, isPassive, false),
			data = text,
			quality = -1
		};
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x00002D1D File Offset: 0x00000F1D
	public void OnApplicationQuit()
	{
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x00029EAC File Offset: 0x000280AC
	public void Update()
	{
		float deltaTime = Time.deltaTime;
		for (int i = this.skills.Count - 1; i > -1; i--)
		{
			KeyValuePair<uint, ActiveSkillBase> keyValuePair = this.skills.ElementAt(i);
			ActiveSkillBase value = keyValuePair.Value;
			if (!value.isPassSkill)
			{
				value.skillTime -= deltaTime;
			}
			if (value.skillTime < 0f)
			{
				this.skills.Remove(keyValuePair.Key);
				value.Clear(-1);
			}
			else
			{
				value.UpdateEvent(deltaTime);
			}
		}
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x00029F34 File Offset: 0x00028134
	public void ClearSkill(uint skillId)
	{
		ActiveSkillBase activeSkillBase;
		if (this.skills.Remove(skillId, out activeSkillBase))
		{
			activeSkillBase.Clear(-1);
		}
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x00029F58 File Offset: 0x00028158
	public void ClearSkilByData(uint skillId, int clearData)
	{
		ActiveSkillBase activeSkillBase;
		if (this.skills.Remove(skillId, out activeSkillBase))
		{
			activeSkillBase.Clear(clearData);
		}
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x00029F7C File Offset: 0x0002817C
	public void StartSkillAciton(uint skillId)
	{
		ActiveSkillBase activeSkillBase;
		if (this.skills.TryGetValue(skillId, out activeSkillBase))
		{
			activeSkillBase.StartSkillAciton();
		}
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x00029FA0 File Offset: 0x000281A0
	public void EndSkillAciton(uint skillId)
	{
		ActiveSkillBase activeSkillBase;
		if (this.skills.TryGetValue(skillId, out activeSkillBase))
		{
			activeSkillBase.EndSkillAciton();
		}
	}

	// Token: 0x0400095E RID: 2398
	private static uint skillCreateId;

	// Token: 0x0400095F RID: 2399
	private static uint syncPassSkillIndex;

	// Token: 0x04000960 RID: 2400
	public Dictionary<uint, ActiveSkillBase> skills = new Dictionary<uint, ActiveSkillBase>();

	// Token: 0x04000961 RID: 2401
	private Dictionary<string, List<object>> passsiveSkillDic = new Dictionary<string, List<object>>();

	// Token: 0x04000962 RID: 2402
	private Dictionary<string, List<object>> activeSkillDic = new Dictionary<string, List<object>>();

	// Token: 0x04000963 RID: 2403
	private readonly object[] passiveSkillRefreshData = new object[6];

	// Token: 0x04000964 RID: 2404
	private readonly object[] activeSkillRefreshData = new object[6];

	// Token: 0x04000965 RID: 2405
	private readonly int[] passiveSkillRefreshNums = new int[3];

	// Token: 0x04000966 RID: 2406
	private readonly int[] activeSkillRefreshNums = new int[3];

	// Token: 0x04000967 RID: 2407
	private List<object> myData = new List<object>(64);
}
