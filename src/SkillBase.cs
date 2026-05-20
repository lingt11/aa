using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020002F6 RID: 758
public class SkillBase
{
	// Token: 0x06001175 RID: 4469 RVA: 0x00065B60 File Offset: 0x00063D60
	public void SetData(Dictionary<string, object> dic)
	{
		this.data = new Dictionary<string, object>(dic);
		this.skillName = dic.DIC("name");
		this.skillId = dic.DIC("id");
		this.cost = dic.DIC("cost");
		this.distance = dic.DIC("range");
		this.valueStr = dic.DIC("value");
		this.quality = DropDefine.QualityAry.IndexOf(dic.DIC("quality"));
		this.baseSkillValues = SkillBase.ParseSkillValues(this.valueStr);
		this.levelUpValues = SkillBase.ParseSkillValues(dic.DIC("levelup"));
		this.RefreshSkillValues();
		this.skillAttribute = GameDataManager.GetSkillAttribute(dic.DIC("attribute"));
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x00065C2C File Offset: 0x00063E2C
	public void Use()
	{
		if (Game.GameData.ActiveSkillDataDic[this.activeSkillEnum].chargingNum > 0)
		{
			if (this.curCharging == 0)
			{
				Util.ShowTips("充能不足");
				return;
			}
			this.curCharging--;
			this.UpdateChargingBuffUI();
		}
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui != null)
		{
			ui.skillIndex = ui.skillList.IndexOf(this.skillUI);
		}
		if (this.skillUI.skillBase.isSwitch)
		{
			GameHelperClient.localPlayer.OnCloseSwitchSkill(this, this.skillUI.skillBase.useSkillId);
			return;
		}
		Util.OnLocalStartUseSkill(this.activeSkillEnum, this.roleBase, this.skillUI.skillBase.skillBookId);
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x00065CF1 File Offset: 0x00063EF1
	public bool CheckCD()
	{
		if (this.updateCd > 0f)
		{
			return true;
		}
		this.SetCDTime();
		return false;
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x00065D09 File Offset: 0x00063F09
	public void SetCDTime()
	{
		this.updateCd = this.cdTime * Util.GetCdReduce(this.roleBase.AllSkillCd);
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x00065D28 File Offset: 0x00063F28
	public string GetSkillInfo(out string exInfo, out float skillCd)
	{
		string text = Game.Language.Get(ConstDefine.SkillAttributeStr[(int)this.skillAttribute], "");
		text = string.Format(ColorDefine.SkillAttributeColor[(int)this.skillAttribute], text);
		string text2 = Game.Language.Get(this.isPasssiveSkill ? ("p_" + this.skillId + "_m") : ("a_" + this.skillId + "_m"), "");
		skillCd = 0f;
		string quaTex = SkillBase.GetQuaTex(this.isPasssiveSkill, this.quality);
		string text3 = "";
		if (this.isPasssiveSkill)
		{
			if (this.skillValues != null && this.skillValues.Length != 0)
			{
				text2 = string.Format(text2, SkillBase.GetSkillFormatValues(this.skillValues));
			}
		}
		else
		{
			ActiveSkillData activeSkillData = Game.GameData.ActiveSkillDataDic[this.activeSkillEnum];
			skillCd = this.cdTime * Util.GetCdReduce(this.roleBase.AllSkillCd);
			if (this.cost > 0)
			{
				text3 += string.Format(ColorDefine.QuaText[1], PathDefine.Concat(Game.Language.Get("消耗", ""), this.cost, "MP  "));
			}
			if (activeSkillData.duration > 0f && activeSkillData.duration < 900f)
			{
				text3 += string.Format(ColorDefine.QuaText[3], PathDefine.Concat(Game.Language.Get("持续时间", ""), activeSkillData.duration, Game.Language.Get("秒", "")));
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text3 += "\n";
			}
			if (activeSkillData.damageExStr != null && activeSkillData.damageExStr.Length != 0)
			{
				string format = text2;
				object[] damageExStr = activeSkillData.damageExStr;
				text2 = string.Format(format, damageExStr);
			}
			else if (activeSkillData.damageValue > 0f || activeSkillData.damageBase > 0)
			{
				string activeSkillDamageStr = SkillBase.GetActiveSkillDamageStr(activeSkillData);
				if (activeSkillData.interval > 0f)
				{
					text2 = string.Format(text2, activeSkillDamageStr, activeSkillData.interval);
				}
				else
				{
					text2 = string.Format(text2, activeSkillDamageStr);
				}
			}
		}
		exInfo = quaTex + "  " + text;
		return text3 + text2;
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x00065F7C File Offset: 0x0006417C
	private static string GetActiveSkillDamageStr(ActiveSkillData activeSkillData)
	{
		string text = (activeSkillData.damageBase > 0) ? activeSkillData.damageBase.ToString() : "";
		if (activeSkillData.damageValue > 0f)
		{
			if (activeSkillData.damageBase > 0)
			{
				text = PathDefine.Concat(text, StringDefine.AddSpace);
			}
			if (activeSkillData.damageType == 0)
			{
				text = PathDefine.Concat(text, Game.Language.Get("attack", ""), StringDefine.MulSpace, activeSkillData.damageValue);
			}
			else if (activeSkillData.damageType == 1)
			{
				text = PathDefine.Concat(text, Game.Language.Get("str", ""), StringDefine.MulSpace, activeSkillData.damageValue);
			}
			else if (activeSkillData.damageType == 2)
			{
				text = PathDefine.Concat(text, Game.Language.Get("dex", ""), StringDefine.MulSpace, activeSkillData.damageValue);
			}
			else if (activeSkillData.damageType == 3)
			{
				text = PathDefine.Concat(text, Game.Language.Get("sta", ""), StringDefine.MulSpace, activeSkillData.damageValue);
			}
			else if (activeSkillData.damageType == 4)
			{
				text = PathDefine.Concat(text, Game.Language.Get("最大生命值", ""), StringDefine.MulSpace, activeSkillData.damageValue);
			}
		}
		return text;
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x000660E0 File Offset: 0x000642E0
	public static string GetSkillInfo(string skillId, object skillData, bool isPasssive, bool isShowQua = false)
	{
		string text = Game.Language.Get(skillData.DIC("attribute"), "");
		text = string.Format(ColorDefine.SkillAttributeColor[(int)GameDataManager.GetSkillAttribute(text)], text);
		string text2 = Game.Language.Get(isPasssive ? ("p_" + skillId + "_m") : ("a_" + skillId + "_m"), "");
		string text3 = "";
		if (isPasssive)
		{
			string text4 = skillData.DIC("value");
			if (!string.IsNullOrEmpty(text4))
			{
				float[] values = Array.ConvertAll<string, float>(text4.Split('|', StringSplitOptions.None), (string s) => float.Parse(s));
				text2 = string.Format(text2, SkillBase.GetSkillFormatValues(values));
			}
		}
		else
		{
			ActiveSkillData activeSkillData = Game.GameData.ActiveSkillDataDic[(ActiveSkillEnum)int.Parse(skillId)];
			int num = activeSkillData.cost;
			float cd = activeSkillData.cd;
			if (num > 0)
			{
				text3 = string.Format(ColorDefine.QuaText[1], PathDefine.Concat(Game.Language.Get("消耗", ""), num, "MP"));
			}
			if (cd > 0f)
			{
				text3 = text3 + "\n" + string.Format(ColorDefine.QuaRelicText[0], PathDefine.Concat(Game.Language.Get("冷却时间", ""), cd, Game.Language.Get("秒", "")));
			}
			if (activeSkillData.duration > 0f && activeSkillData.duration < 900f)
			{
				text3 = text3 + "\n" + string.Format(ColorDefine.QuaText[3], PathDefine.Concat(Game.Language.Get("持续时间", ""), activeSkillData.duration, Game.Language.Get("秒", "")));
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text3 = "\n" + text3;
			}
			if (activeSkillData.damageExStr != null && activeSkillData.damageExStr.Length != 0)
			{
				string format = text2;
				object[] damageExStr = activeSkillData.damageExStr;
				text2 = string.Format(format, damageExStr);
			}
			else if (activeSkillData.damageValue > 0f || activeSkillData.damageBase > 0)
			{
				string activeSkillDamageStr = SkillBase.GetActiveSkillDamageStr(activeSkillData);
				if (activeSkillData.interval > 0f)
				{
					text2 = string.Format(text2, activeSkillDamageStr, activeSkillData.interval);
				}
				else
				{
					text2 = string.Format(text2, activeSkillDamageStr);
				}
			}
		}
		if (isShowQua)
		{
			string quaTex = SkillBase.GetQuaTex(isPasssive, DropDefine.QualityAry.IndexOf(skillData.DIC("quality")));
			return string.Concat(new string[]
			{
				quaTex,
				"\n\n",
				text,
				"\n\n",
				text2,
				text3
			});
		}
		return text + "\n\n" + text2 + text3;
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x000663B8 File Offset: 0x000645B8
	public virtual void OnLevelUp()
	{
		int oldLevel = this.level;
		this.level++;
		this.OnLevelChanged(oldLevel, this.level);
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x000663E8 File Offset: 0x000645E8
	public virtual void OnLevelRed()
	{
		if (this.level <= 0)
		{
			return;
		}
		int oldLevel = this.level;
		this.level--;
		this.OnLevelChanged(oldLevel, this.level);
	}

	// Token: 0x0600117E RID: 4478 RVA: 0x00066421 File Offset: 0x00064621
	protected virtual void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.RefreshSkillValues();
	}

	// Token: 0x0600117F RID: 4479 RVA: 0x00066429 File Offset: 0x00064629
	protected float GetSkillValue(int index, float defaultValue = 0f)
	{
		if (this.skillValues != null && index >= 0 && index < this.skillValues.Length)
		{
			return this.skillValues[index];
		}
		return defaultValue;
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x0006644C File Offset: 0x0006464C
	protected int GetSkillIntValue(int index, int defaultValue = 0)
	{
		return Mathf.RoundToInt(this.GetSkillValue(index, (float)defaultValue));
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x0006645C File Offset: 0x0006465C
	protected float GetLevelUpSkillValue(int index)
	{
		if (this.levelUpValues != null && index >= 0 && index < this.levelUpValues.Length)
		{
			return this.levelUpValues[index];
		}
		return 0f;
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x00066484 File Offset: 0x00064684
	private void RefreshSkillValues()
	{
		if (this.baseSkillValues == null || this.baseSkillValues.Length == 0)
		{
			this.skillValues = null;
			this.data["value"] = this.valueStr;
			return;
		}
		this.skillValues = new float[this.baseSkillValues.Length];
		for (int i = 0; i < this.baseSkillValues.Length; i++)
		{
			this.skillValues[i] = this.baseSkillValues[i] + this.GetLevelUpSkillValue(i) * (float)this.level;
		}
		this.data["value"] = string.Join("|", this.skillValues.Select(new Func<float, string>(SkillBase.GetSkillValueString)));
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x00032C44 File Offset: 0x00030E44
	private static float[] ParseSkillValues(string values)
	{
		if (string.IsNullOrEmpty(values))
		{
			return null;
		}
		return Array.ConvertAll<string, float>(values.Split('|', StringSplitOptions.None), new Converter<string, float>(float.Parse));
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x00066538 File Offset: 0x00064738
	private static string GetSkillValueString(float value)
	{
		float num = Mathf.Round(value);
		if (Mathf.Abs(value - num) < 0.001f)
		{
			return ((int)num).ToString();
		}
		return value.ToString("0.##");
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x00066574 File Offset: 0x00064774
	private static object[] GetSkillFormatValues(float[] values)
	{
		if (values == null || values.Length == 0)
		{
			return Array.Empty<object>();
		}
		object[] array = new object[values.Length];
		for (int i = 0; i < values.Length; i++)
		{
			array[i] = SkillBase.GetSkillValueString(values[i]);
		}
		return array;
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x000665B4 File Offset: 0x000647B4
	public static string GetQuaTex(bool isPassive, int qualityValue)
	{
		string text;
		if (qualityValue >= 0)
		{
			if (isPassive)
			{
				text = Game.Language.Get(PathDefine.Concat("p_quality_", qualityValue), "");
			}
			else
			{
				text = Game.Language.Get(PathDefine.Concat("a_quality_", qualityValue), "");
			}
			text = string.Format(ColorDefine.QuaText[qualityValue], text);
		}
		else
		{
			if (isPassive)
			{
				text = Game.Language.Get("p_quality_hero", "");
			}
			else
			{
				text = Game.Language.Get("a_quality_hero", "");
			}
			text = string.Format(ColorDefine.SkillHeroColor, text);
		}
		return text;
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x0006665C File Offset: 0x0006485C
	public bool IsHeroSkill()
	{
		return this.activeSkillEnum >= ActiveSkillEnum.Hero_Blink;
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x00066670 File Offset: 0x00064870
	public static string GetActiveSkillTip(ActiveSkillEnum activeSkillEnum)
	{
		ActiveSkillData activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillEnum];
		LanguageManager language = Game.Language;
		string str = "a_";
		int num = (int)activeSkillEnum;
		string text = language.Get(str + num.ToString() + "_m", "");
		if (activeSkillData.damageExStr != null && activeSkillData.damageExStr.Length != 0)
		{
			string format = text;
			object[] damageExStr = activeSkillData.damageExStr;
			text = string.Format(format, damageExStr);
		}
		else if (activeSkillData.damageValue > 0f || activeSkillData.damageBase > 0)
		{
			string activeSkillDamageStr = SkillBase.GetActiveSkillDamageStr(activeSkillData);
			if (activeSkillData.interval > 0f)
			{
				text = string.Format(text, activeSkillDamageStr, activeSkillData.interval);
			}
			else
			{
				text = string.Format(text, activeSkillDamageStr);
			}
		}
		return text;
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x00066723 File Offset: 0x00064923
	public void UpdateAuto()
	{
		this.isAuto = !this.isAuto;
		this.skillUI.switchGo.SetActive(this.isAuto);
	}

	// Token: 0x0600118A RID: 4490 RVA: 0x0006674C File Offset: 0x0006494C
	public virtual void Update()
	{
		if (this.updateCd > 0f)
		{
			this.updateCd -= Time.deltaTime;
		}
		if (this.chargingMax > 0 && this.curCharging < this.chargingMax)
		{
			this.curChargingTime -= Time.deltaTime;
			if (this.curChargingTime <= 0f)
			{
				this.curCharging++;
				this.SetChargingMaxAndCd();
				this.UpdateChargingBuffUI();
			}
		}
	}

	// Token: 0x0600118B RID: 4491 RVA: 0x000667C8 File Offset: 0x000649C8
	public void InitActiveSkill()
	{
		if (this.chargingMax > 0)
		{
			this.SetChargingMaxAndCd();
			this.curCharging = this.chargingMax;
			this.UpdateChargingBuffUI();
		}
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x000667EC File Offset: 0x000649EC
	private void UpdateChargingBuffUI()
	{
		if (this.chargingBuff == null)
		{
			if (this.isPasssiveSkill)
			{
				string text = Game.Language.Get(PathDefine.Concat("p_", this.skillId), "");
				this.chargingBuff = GameHelperClient.AddShowBuff(text, text, PathDefine.Concat("Skill/", this.activeSkillEnum, "_buff"), -1f);
			}
			else
			{
				string text2 = Game.Language.Get(PathDefine.Concat("a_", this.skillId), "");
				this.chargingBuff = GameHelperClient.AddShowBuff(text2, text2, PathDefine.Concat("Skill/", this.activeSkillEnum, "_buff"), -1f);
			}
		}
		this.chargingBuff.SetSpecialStr(this.curCharging.ToString());
	}

	// Token: 0x0600118D RID: 4493 RVA: 0x000668BC File Offset: 0x00064ABC
	private void SetChargingMaxAndCd()
	{
		this.chargingMax = GameHelperClient.localPlayer.GetSkillChargingMax(this.activeSkillEnum, this.chargingMax);
		this.curChargingTime = (this.chargingCd = GameHelperClient.localPlayer.GetSkillChargingCd(this.activeSkillEnum, this.chargingCd) * Util.GetCdReduce(this.roleBase.AllSkillCd));
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x0006691C File Offset: 0x00064B1C
	public virtual int GetSaveSkillData()
	{
		if (this is PasssiveSkill)
		{
			return 0;
		}
		if (this.activeSkillEnum == ActiveSkillEnum.SoulDevourer && GameHelperClient.localPlayer != null && GameHelperClient.localPlayer.RoleModeBase != null)
		{
			PlayerKoboldMode playerKoboldMode = GameHelperClient.localPlayer.RoleModeBase as PlayerKoboldMode;
			if (playerKoboldMode != null)
			{
				return playerKoboldMode.SkillLevel;
			}
		}
		return 0;
	}

	// Token: 0x04000F95 RID: 3989
	public PlayerBase roleBase;

	// Token: 0x04000F96 RID: 3990
	public float cdTime;

	// Token: 0x04000F97 RID: 3991
	public float updateCd;

	// Token: 0x04000F98 RID: 3992
	public ActiveSkillEnum activeSkillEnum;

	// Token: 0x04000F99 RID: 3993
	public string iconName;

	// Token: 0x04000F9A RID: 3994
	public SkillUI skillUI;

	// Token: 0x04000F9B RID: 3995
	public Dictionary<string, object> data;

	// Token: 0x04000F9C RID: 3996
	public string skillName;

	// Token: 0x04000F9D RID: 3997
	public string skillId;

	// Token: 0x04000F9E RID: 3998
	public bool isPasssiveSkill;

	// Token: 0x04000F9F RID: 3999
	public int cost;

	// Token: 0x04000FA0 RID: 4000
	public SkillAttribute skillAttribute;

	// Token: 0x04000FA1 RID: 4001
	public float distance;

	// Token: 0x04000FA2 RID: 4002
	public float[] skillValues;

	// Token: 0x04000FA3 RID: 4003
	private float[] baseSkillValues;

	// Token: 0x04000FA4 RID: 4004
	public float[] levelUpValues;

	// Token: 0x04000FA5 RID: 4005
	private string valueStr;

	// Token: 0x04000FA6 RID: 4006
	public int quality;

	// Token: 0x04000FA7 RID: 4007
	public string languageName;

	// Token: 0x04000FA8 RID: 4008
	public bool isSwitch;

	// Token: 0x04000FA9 RID: 4009
	public int skillBookId;

	// Token: 0x04000FAA RID: 4010
	public uint useSkillId;

	// Token: 0x04000FAB RID: 4011
	public bool isAuto;

	// Token: 0x04000FAC RID: 4012
	private RoleBuff chargingBuff;

	// Token: 0x04000FAD RID: 4013
	public int chargingMax;

	// Token: 0x04000FAE RID: 4014
	public float chargingCd;

	// Token: 0x04000FAF RID: 4015
	public int curCharging;

	// Token: 0x04000FB0 RID: 4016
	public float curChargingTime;

	// Token: 0x04000FB1 RID: 4017
	public int[] totals;

	// Token: 0x04000FB2 RID: 4018
	public bool[] isTotalsPercent;

	// Token: 0x04000FB3 RID: 4019
	public string totalName;

	// Token: 0x04000FB4 RID: 4020
	public string exDec;

	// Token: 0x04000FB5 RID: 4021
	public int level;
}
