using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D0 RID: 208
public class EquipBase
{
	// Token: 0x0600041E RID: 1054 RVA: 0x00018D99 File Offset: 0x00016F99
	public void AddEvolutionEntry(EquipEvolutionEntryData evolutionEntry)
	{
		if (evolutionEntry == null)
		{
			return;
		}
		this.evolutionEntryList.Add(evolutionEntry.Clone());
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x00018DB0 File Offset: 0x00016FB0
	public static List<EquipBase.EquipAttributeData> CreateEquipAttributeDataList(object data)
	{
		Dictionary<string, object> dictionary = data.DIC();
		if (dictionary.ContainsKey("attributeType") && dictionary.ContainsKey("attributeValue"))
		{
			return EquipBase.CreateEquipAttributeDataList(data.DIC("attributeType"), data.DIC("attributeValue"));
		}
		List<EquipBase.EquipAttributeData> list = new List<EquipBase.EquipAttributeData>(EquipBase.AttributeDefineAry.Length);
		foreach (string text in EquipBase.AttributeDefineAry)
		{
			if (dictionary.ContainsKey(text))
			{
				list.Add(EquipBase.CreateEquipAttributeData(data, text));
			}
		}
		return list;
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x00018E3C File Offset: 0x0001703C
	private static List<EquipBase.EquipAttributeData> CreateEquipAttributeDataList(string attributeTypeStr, string attributeValueStr)
	{
		List<EquipBase.EquipAttributeData> list = new List<EquipBase.EquipAttributeData>();
		if (string.IsNullOrEmpty(attributeTypeStr) || attributeTypeStr.Equals("0") || string.IsNullOrEmpty(attributeValueStr) || attributeValueStr.Equals("0"))
		{
			return list;
		}
		string[] array = attributeTypeStr.Split('|', StringSplitOptions.None);
		string[] array2 = attributeValueStr.Split('|', StringSplitOptions.None);
		int num = Mathf.Min(array.Length, array2.Length);
		for (int i = 0; i < num; i++)
		{
			string text = array[i].Trim();
			if (!string.IsNullOrEmpty(text))
			{
				float num2;
				if (!float.TryParse(array2[i], out num2))
				{
					num2 = 0f;
				}
				EquipBase.EquipValueType equipValueType = EquipBase.GetEquipValueType(text);
				if (EquipBase.IsCriticalAttribute(text))
				{
					num2 = (float)Mathf.RoundToInt(num2 * 100f);
				}
				else if (equipValueType == EquipBase.EquipValueType.IntValue)
				{
					num2 = (float)Mathf.RoundToInt(num2);
				}
				list.Add(new EquipBase.EquipAttributeData
				{
					attributeType = text,
					value = num2,
					equipValueType = equipValueType
				});
			}
		}
		return list;
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x00018F37 File Offset: 0x00017137
	public static List<EquipBase.EquipAttributeData> CloneEquipAttributeDataList(List<EquipBase.EquipAttributeData> attributeDataAry)
	{
		if (attributeDataAry != null)
		{
			return new List<EquipBase.EquipAttributeData>(attributeDataAry);
		}
		return new List<EquipBase.EquipAttributeData>();
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x00018F48 File Offset: 0x00017148
	private static EquipBase.EquipAttributeData CreateEquipAttributeData(object data, string attributeType)
	{
		EquipBase.EquipAttributeData equipAttributeData = new EquipBase.EquipAttributeData
		{
			attributeType = attributeType,
			equipValueType = EquipBase.GetEquipValueType(attributeType)
		};
		if (EquipBase.IsCriticalAttribute(attributeType))
		{
			equipAttributeData.value = (float)Mathf.RoundToInt(data.DIC(attributeType) * 100f);
		}
		else if (equipAttributeData.equipValueType == EquipBase.EquipValueType.FloatValue)
		{
			equipAttributeData.value = data.DIC(attributeType);
		}
		else
		{
			equipAttributeData.value = (float)data.DIC(attributeType);
		}
		return equipAttributeData;
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x00018FC4 File Offset: 0x000171C4
	public static EquipBase.EquipAttributeData CreateEquipAttributeData(string attributeType, float value)
	{
		EquipBase.EquipValueType equipValueType = EquipBase.GetEquipValueType(attributeType);
		if (EquipBase.IsCriticalAttribute(attributeType))
		{
			value = (float)Mathf.RoundToInt(value * 100f);
		}
		else if (equipValueType == EquipBase.EquipValueType.IntValue)
		{
			value = (float)Mathf.RoundToInt(value);
		}
		return new EquipBase.EquipAttributeData
		{
			attributeType = attributeType,
			value = value,
			equipValueType = equipValueType
		};
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x0001901E File Offset: 0x0001721E
	private static EquipBase.EquipValueType GetEquipValueType(string attributeType)
	{
		if (!EquipBase.FloatAttributeDefineSet.Contains(attributeType))
		{
			return EquipBase.EquipValueType.IntValue;
		}
		return EquipBase.EquipValueType.FloatValue;
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x00019030 File Offset: 0x00017230
	private static bool IsCriticalAttribute(string attributeType)
	{
		return attributeType == "baojilv" || attributeType == "baojiDamage";
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x0001904C File Offset: 0x0001724C
	public static string GetEquipSkillInfoKey(EquipSkillType skillType)
	{
		return PathDefine.Concat("equipSkill_", skillType, "_m");
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x00019063 File Offset: 0x00017263
	public static string GetEquipSkillTotalKey(EquipSkillType skillType)
	{
		return PathDefine.Concat("equipSkill_", skillType, StringDefine.Total);
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x0001907A File Offset: 0x0001727A
	private static bool HasLanguageKey(string key)
	{
		return Game.Language != null && Game.Language.languageDic != null && Game.Language.languageDic.ContainsKey(key);
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x000190A1 File Offset: 0x000172A1
	private static string GetLanguageOrEmpty(string key)
	{
		if (!EquipBase.HasLanguageKey(key))
		{
			return "";
		}
		return Game.Language.Get(key, " ");
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x000190C4 File Offset: 0x000172C4
	public float GetAttributeValue(string attributeType)
	{
		for (int i = 0; i < this.equipAttributeDataAry.Count; i++)
		{
			if (this.equipAttributeDataAry[i].attributeType == attributeType)
			{
				return this.equipAttributeDataAry[i].value;
			}
		}
		return 0f;
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x00019118 File Offset: 0x00017318
	public bool HasAttributeType(string attributeType)
	{
		for (int i = 0; i < this.equipAttributeDataAry.Count; i++)
		{
			if (this.equipAttributeDataAry[i].attributeType == attributeType)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x00019157 File Offset: 0x00017357
	public int GetIntAttributeValue(string attributeType)
	{
		return Mathf.RoundToInt(this.GetAttributeValue(attributeType));
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x00019168 File Offset: 0x00017368
	private void AddAttributeValue(string attributeType, float addValue)
	{
		EquipBase.EquipValueType equipValueType = EquipBase.GetEquipValueType(attributeType);
		for (int i = 0; i < this.equipAttributeDataAry.Count; i++)
		{
			EquipBase.EquipAttributeData equipAttributeData = this.equipAttributeDataAry[i];
			if (!(equipAttributeData.attributeType != attributeType))
			{
				equipAttributeData.value += addValue;
				if (equipValueType == EquipBase.EquipValueType.IntValue)
				{
					equipAttributeData.value = (float)Mathf.RoundToInt(equipAttributeData.value);
				}
				equipAttributeData.equipValueType = equipValueType;
				this.equipAttributeDataAry[i] = equipAttributeData;
				return;
			}
		}
		this.equipAttributeDataAry.Add(new EquipBase.EquipAttributeData
		{
			attributeType = attributeType,
			value = ((equipValueType == EquipBase.EquipValueType.IntValue) ? ((float)Mathf.RoundToInt(addValue)) : addValue),
			equipValueType = equipValueType
		});
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x0001921D File Offset: 0x0001741D
	public void AddExtraAttributeData(EquipBase.EquipAttributeData attributeData)
	{
		this.AddExtraAttributeData(attributeData, 0f, 0f);
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x00019230 File Offset: 0x00017430
	public void AddExtraAttributeData(EquipBase.EquipAttributeData attributeData, float levelUpValue, float levelUpRatio)
	{
		this.AddAttributeValue(attributeData.attributeType, attributeData.value);
		this.AddExtraLevelUpData(attributeData.attributeType, levelUpValue, levelUpRatio);
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x00019254 File Offset: 0x00017454
	private void AddExtraLevelUpData(string attributeType, float levelUpValue, float levelUpRatio)
	{
		if (string.IsNullOrEmpty(attributeType) || Mathf.Approximately(levelUpValue, 0f))
		{
			return;
		}
		if (this.levelKeys == null)
		{
			this.levelKeys = new List<string>();
		}
		if (this.levelValues == null)
		{
			this.levelValues = new List<string>();
		}
		if (this.levelRatios == null)
		{
			this.levelRatios = new List<float>();
		}
		this.levelKeys.Add(attributeType);
		this.levelValues.Add(levelUpValue.ToString());
		this.levelRatios.Add((levelUpRatio <= 0f) ? 1f : levelUpRatio);
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x000192E9 File Offset: 0x000174E9
	public void AddExtraEquipSkill(EquipSkillType skillType, float[] skillValues = null, float[] skillValueUps = null)
	{
		if (skillType == EquipSkillType.None || this.HasEquipSkill(skillType))
		{
			return;
		}
		this.extraEquipSkillAry.Add(skillType);
		if (skillValues != null)
		{
			this.extraSkillValueAryDic[skillType] = skillValues;
		}
		if (skillValueUps != null)
		{
			this.extraSkillValueUpAryDic[skillType] = skillValueUps;
		}
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x00019325 File Offset: 0x00017525
	public IEnumerable<EquipSkillType> GetEquipSkills()
	{
		if (this.equipSkill != EquipSkillType.None)
		{
			yield return this.equipSkill;
		}
		int num;
		for (int i = 0; i < this.extraEquipSkillAry.Count; i = num + 1)
		{
			EquipSkillType equipSkillType = this.extraEquipSkillAry[i];
			if (equipSkillType != EquipSkillType.None)
			{
				yield return equipSkillType;
			}
			num = i;
		}
		yield break;
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x00019335 File Offset: 0x00017535
	public bool HasEquipSkill(EquipSkillType skillType)
	{
		return this.equipSkill == skillType || this.extraEquipSkillAry.Contains(skillType);
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x00019350 File Offset: 0x00017550
	public float[] GetSkillValueAry(EquipSkillType skillType)
	{
		float[] result;
		if (!this.extraSkillValueAryDic.TryGetValue(skillType, out result))
		{
			return this.skillValueAry;
		}
		return result;
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x00019378 File Offset: 0x00017578
	public float[] GetSkillValueUpAry(EquipSkillType skillType)
	{
		float[] result;
		if (!this.extraSkillValueUpAryDic.TryGetValue(skillType, out result))
		{
			return this.skillValueUpAry;
		}
		return result;
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x0001939D File Offset: 0x0001759D
	public static bool IsEquipSkillKingLocked(EquipSkillType skillType)
	{
		return skillType != EquipSkillType.None && EquipBase.SafeExcelBool(EquipBase.GetEquipmentSkillConfig(skillType.ToString()), "kingLock");
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x000193C4 File Offset: 0x000175C4
	private static Dictionary<string, object> GetEquipmentSkillConfig(string skillName)
	{
		if (string.IsNullOrEmpty(skillName))
		{
			return null;
		}
		object obj;
		if (!ExcelManager.allExcelData.TryGetValue("equipmentSkill", out obj))
		{
			return null;
		}
		Dictionary<string, object> dictionary = obj as Dictionary<string, object>;
		if (dictionary == null)
		{
			return null;
		}
		object obj2;
		if (!dictionary.TryGetValue(skillName, out obj2))
		{
			return null;
		}
		return obj2 as Dictionary<string, object>;
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x00019410 File Offset: 0x00017610
	private static string SafeExcelString(Dictionary<string, object> data, string key)
	{
		object obj;
		if (data == null || !data.TryGetValue(key, out obj) || obj == null)
		{
			return "";
		}
		return obj.ToString();
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x0001943C File Offset: 0x0001763C
	private static bool SafeExcelBool(Dictionary<string, object> data, string key)
	{
		string text = EquipBase.SafeExcelString(data, key);
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		bool result;
		if (bool.TryParse(text, out result))
		{
			return result;
		}
		int num;
		return int.TryParse(text, out num) && num != 0;
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x00019478 File Offset: 0x00017678
	private static float[] ParseSkillValues(string valueStr)
	{
		if (string.IsNullOrEmpty(valueStr))
		{
			return null;
		}
		string[] array = valueStr.Split('|', StringSplitOptions.None);
		float[] array2 = new float[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = float.Parse(array[i]);
		}
		return array2;
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x000194BC File Offset: 0x000176BC
	public string GetEquipSkillTotalKey()
	{
		foreach (EquipSkillType skillType in this.GetEquipSkills())
		{
			string equipSkillTotalKey = EquipBase.GetEquipSkillTotalKey(skillType);
			if (EquipBase.HasLanguageKey(equipSkillTotalKey))
			{
				return equipSkillTotalKey;
			}
		}
		return PathDefine.Concat("equip_", this.equipIndex, StringDefine.Total);
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x0001952C File Offset: 0x0001772C
	private bool HasAnySkillValue()
	{
		foreach (EquipSkillType skillType in this.GetEquipSkills())
		{
			float[] array = this.GetSkillValueAry(skillType);
			if (array != null && array.Length != 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x00019588 File Offset: 0x00017788
	public void AddEquipEvent(EquipEventBase equipEvent)
	{
		if (equipEvent == null)
		{
			return;
		}
		if (this.equipEventBase == null)
		{
			this.equipEventBase = equipEvent;
			return;
		}
		this.extraEquipEventAry.Add(equipEvent);
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x000195AA File Offset: 0x000177AA
	public IEnumerable<EquipEventBase> GetEquipEvents()
	{
		if (this.equipEventBase != null)
		{
			yield return this.equipEventBase;
		}
		int num;
		for (int i = 0; i < this.extraEquipEventAry.Count; i = num + 1)
		{
			EquipEventBase equipEventBase = this.extraEquipEventAry[i];
			if (equipEventBase != null)
			{
				yield return equipEventBase;
			}
			num = i;
		}
		yield break;
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x000195BC File Offset: 0x000177BC
	public void Init(RoleType roleType)
	{
		object dic = ExcelManager.allExcelData["equipment"].DIC(this.equipIndex);
		this.name = Game.Language.Get(PathDefine.Concat("equip_", this.equipIndex), "");
		this.level = 0;
		this.equipSkill = EquipSkillType.None;
		this.onlySkill = false;
		this.shopStreng = false;
		this.skillValueAry = null;
		this.skillValueUpAry = null;
		this.skillStrAry = null;
		this.extraEquipSkillAry.Clear();
		this.extraSkillValueAryDic.Clear();
		this.extraSkillValueUpAryDic.Clear();
		string text = dic.DIC("skill");
		Dictionary<string, object> equipmentSkillConfig = EquipBase.GetEquipmentSkillConfig(text);
		if (!string.IsNullOrEmpty(text))
		{
			this.equipSkill = (EquipSkillType)Enum.Parse(typeof(EquipSkillType), text);
			if (equipmentSkillConfig != null)
			{
				this.onlySkill = EquipBase.SafeExcelBool(equipmentSkillConfig, "onlySkill");
			}
		}
		if (equipmentSkillConfig == null)
		{
			Dictionary<string, object> dictionary = dic.DIC();
			this.onlySkill = (dictionary.ContainsKey("onlySkill") && dic.DIC("onlySkill") == 1);
		}
		if (roleType != RoleType.King)
		{
			this.SetStar();
		}
		string text2 = dic.DIC("skillValue");
		if (!string.IsNullOrEmpty(text2))
		{
			this.skillStrAry = text2.Split('|', StringSplitOptions.None);
			this.skillValueAry = EquipBase.ParseSkillValues(text2);
			this.skillValueUpAry = EquipBase.ParseSkillValues(dic.DIC("skillValueUp"));
		}
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x00019724 File Offset: 0x00017924
	private void SetStar()
	{
		object obj = ExcelManager.allExcelData["equipment"].DIC(this.equipIndex);
		this.iconName = obj.DIC("equipmentIcon");
		this.SetIcon();
		List<EquipBase.EquipAttributeData> attributeDataAry;
		if (Game.GameData != null && Game.GameData.EquipAttributeDataDic.TryGetValue(this.equipIndex, out attributeDataAry))
		{
			this.equipAttributeDataAry = EquipBase.CloneEquipAttributeDataList(attributeDataAry);
		}
		else
		{
			this.equipAttributeDataAry = EquipBase.CreateEquipAttributeDataList(obj);
		}
		string text = Game.Language.Get(PathDefine.Concat("equip_", this.equipIndex, "_m"), " ");
		this.info = text;
		this.maxLevel = obj.DIC("maxLevel");
		this.shopStreng = (obj.DIC().ContainsKey("shopStreng") && obj.DIC("shopStreng"));
		this.levelUpGem = obj.DIC("levelUpGem");
		string[] array = obj.DIC("levelUpGold").Split("|", StringSplitOptions.None);
		this.baseLevelUpGold = int.Parse(array[0]);
		this.levelUpGoldAdd = int.Parse(array[1]);
		string[] array2 = obj.DIC("levelUpChance").Split("|", StringSplitOptions.None);
		this.baseLevelUpChance = float.Parse(array2[0]);
		this.levelUpChanceAdd = float.Parse(array2[1]);
		this.minLevelUpSpeed = float.Parse(array2[2]);
		string text2 = obj.DIC("levelUpData");
		if (!string.IsNullOrEmpty(text2))
		{
			this.levelKeys = new List<string>(text2.Split('|', StringSplitOptions.None));
			this.levelValues = new List<string>(obj.DIC("levelUpValue").Split('|', StringSplitOptions.None));
			string[] array3 = obj.DIC("levelUpRatio").Split('|', StringSplitOptions.None);
			this.levelRatios = new List<float>();
			foreach (string s in array3)
			{
				this.levelRatios.Add(float.Parse(s));
			}
		}
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x0001991E File Offset: 0x00017B1E
	private void SetIcon()
	{
		if (this.iconImg == null)
		{
			return;
		}
		this.iconImg.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Equipment/" + this.iconName);
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x00019950 File Offset: 0x00017B50
	public void OnLevelUpSuccess(bool isShopTip, int addLevel)
	{
		for (int i = 0; i < addLevel; i++)
		{
			this.SetLevelUpData();
			this.level++;
			foreach (EquipEventBase equipEventBase in this.GetEquipEvents())
			{
				equipEventBase.OnLevelUpSuccess();
			}
			GameHelperClient.localPlayer.playerAttribute.OnEquipLevelUpSuccess(this);
		}
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui != null)
		{
			ui.RefreshPlayerEquip();
		}
		if (isShopTip)
		{
			Util.ShowTipsNoLanguage(this.name + Game.Language.Get("tip_levelUpSuccess", ""));
		}
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/强化成功", 1f, 3f);
		this.upgradeFailed = 0;
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x00019A28 File Offset: 0x00017C28
	private void SetLevelUpData()
	{
		if (this.levelKeys == null || this.levelKeys.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this.levelKeys.Count; i++)
		{
			string attributeType = this.levelKeys[i];
			this.AddAttributeValue(attributeType, this.GetLevelUpAttributeValue(i, this.level, this.level + 1));
		}
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x00019A8C File Offset: 0x00017C8C
	public static float GetTotalStatsIncrease(float baseIncrease, float multiplier, int startLevel, int targetLevel)
	{
		float num = 0f;
		for (int i = startLevel; i < targetLevel; i++)
		{
			int num2 = i;
			num += baseIncrease * Mathf.Pow(multiplier, (float)num2);
		}
		return num;
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x00019ABC File Offset: 0x00017CBC
	private static int GetIntTotalStatsIncrease(float baseIncrease, float multiplier, int startLevel, int targetLevel)
	{
		int num = 0;
		for (int i = startLevel; i < targetLevel; i++)
		{
			int num2 = i;
			num += Mathf.RoundToInt(baseIncrease * Mathf.Pow(multiplier, (float)num2));
		}
		return num;
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x00019AEC File Offset: 0x00017CEC
	private float GetLevelUpAttributeValue(int levelIndex, int startLevel, int targetLevel)
	{
		string attributeType = this.levelKeys[levelIndex];
		float baseIncrease = float.Parse(this.levelValues[levelIndex]);
		float multiplier = this.levelRatios[levelIndex];
		if (EquipBase.IsCriticalAttribute(attributeType))
		{
			return (float)EquipBase.GetCriticalTotalStatsIncrease(baseIncrease, multiplier, startLevel, targetLevel);
		}
		if (EquipBase.GetEquipValueType(attributeType) == EquipBase.EquipValueType.IntValue)
		{
			return (float)EquipBase.GetIntTotalStatsIncrease(baseIncrease, multiplier, startLevel, targetLevel);
		}
		return EquipBase.GetTotalStatsIncrease(baseIncrease, multiplier, startLevel, targetLevel);
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x00019B54 File Offset: 0x00017D54
	private float GetLevelUpAttributeValue(string attributeType, int startLevel, int targetLevel)
	{
		if (this.levelKeys == null || this.levelKeys.Count == 0)
		{
			return 0f;
		}
		float num = 0f;
		for (int i = 0; i < this.levelKeys.Count; i++)
		{
			if (this.levelKeys[i] == attributeType)
			{
				num += this.GetLevelUpAttributeValue(i, startLevel, targetLevel);
			}
		}
		return num;
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x00019BB9 File Offset: 0x00017DB9
	public static string GetAttributeDisplayName(string attributeType)
	{
		return Game.Language.Get(EquipBase.GetAttributeLanguageKey(attributeType), "");
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x00019BD0 File Offset: 0x00017DD0
	public static string GetAttributeDisplayText(EquipBase.EquipAttributeData attributeData)
	{
		if (!EquipBase.ShouldShowAttribute(attributeData.attributeType, attributeData.value))
		{
			return "";
		}
		return EquipBase.GetAttributeDisplayName(attributeData.attributeType) + EquipBase.FormatAttributeValue(attributeData);
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x00019C04 File Offset: 0x00017E04
	public static string GetEquipSkillDescription(EquipSkillType skillType, float[] skillValues = null)
	{
		string languageOrEmpty = EquipBase.GetLanguageOrEmpty(EquipBase.GetEquipSkillInfoKey(skillType));
		if (string.IsNullOrEmpty(languageOrEmpty))
		{
			return "";
		}
		if (skillValues == null || skillValues.Length == 0)
		{
			return string.Format(ColorDefine.NormalColor, languageOrEmpty);
		}
		object[] array = new object[skillValues.Length];
		for (int i = 0; i < skillValues.Length; i++)
		{
			array[i] = EquipBase.FormatSkillValue(skillValues[i], 0f);
		}
		return string.Format(ColorDefine.NormalColor, string.Format(languageOrEmpty, array));
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x00019C78 File Offset: 0x00017E78
	private static string GetAttributeLanguageKey(string attributeType)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(attributeType);
		if (num <= 1664380433U)
		{
			if (num <= 1046375379U)
			{
				if (num <= 463965674U)
				{
					if (num <= 98677012U)
					{
						if (num != 85482283U)
						{
							if (num != 86370480U)
							{
								if (num == 98677012U)
								{
									if (attributeType == "HPadd")
									{
										return "hpAddSec";
									}
								}
							}
							else if (attributeType == "STR")
							{
								return "str";
							}
						}
						else if (attributeType == "skillReduction")
						{
							return "技能抵抗";
						}
					}
					else if (num != 195454305U)
					{
						if (num != 414037821U)
						{
							if (num == 463965674U)
							{
								if (attributeType == "addCallMonster")
								{
									return "召唤物强度";
								}
							}
						}
						else if (attributeType == "skillNoneDamage")
						{
							return "无属性技能伤害";
						}
					}
					else if (attributeType == "breakShield")
					{
						return "物理破盾伤害";
					}
				}
				else if (num <= 854945700U)
				{
					if (num != 585249028U)
					{
						if (num != 815078760U)
						{
							if (num == 854945700U)
							{
								if (attributeType == "reduceInjury")
								{
									return "gdj";
								}
							}
						}
						else if (attributeType == "skillBreakShield")
						{
							return "法术破盾伤害";
						}
					}
					else if (attributeType == "doge")
					{
						return "闪避值";
					}
				}
				else if (num != 930360186U)
				{
					if (num != 1001345402U)
					{
						if (num == 1046375379U)
						{
							if (attributeType == "forgeAdd")
							{
								return "属性锻造器增幅";
							}
						}
					}
					else if (attributeType == "addHenshinTime")
					{
						return "变身持续时间";
					}
				}
				else if (attributeType == "attackDistance")
				{
					return "攻击距离";
				}
			}
			else if (num <= 1395526040U)
			{
				if (num <= 1199813465U)
				{
					if (num != 1148098932U)
					{
						if (num != 1175216289U)
						{
							if (num == 1199813465U)
							{
								if (attributeType == "hptouqu")
								{
									return "攻击生命偷取";
								}
							}
						}
						else if (attributeType == "maxHpAddPercent")
						{
							return "最大生命值提升";
						}
					}
					else if (attributeType == "xixue")
					{
						return "xixue";
					}
				}
				else if (num != 1203207781U)
				{
					if (num != 1362809445U)
					{
						if (num == 1395526040U)
						{
							if (attributeType == "mp")
							{
								return "法力值";
							}
						}
					}
					else if (attributeType == "hp")
					{
						return "生命值";
					}
				}
				else if (attributeType == "effectDamage")
				{
					return "攻击特效加成";
				}
			}
			else if (num <= 1525324619U)
			{
				if (num != 1397391117U)
				{
					if (num != 1500146022U)
					{
						if (num == 1525324619U)
						{
							if (attributeType == "MPadd")
							{
								return "mpAddSec";
							}
						}
					}
					else if (attributeType == "strAllAdd")
					{
						return "力量总加成";
					}
				}
				else if (attributeType == "buffDamage")
				{
					return "BUFF伤害加成";
				}
			}
			else if (num != 1562092049U)
			{
				if (num != 1591881960U)
				{
					if (num == 1664380433U)
					{
						if (attributeType == "skillCd")
						{
							return "技能急速";
						}
					}
				}
				else if (attributeType == "normalAttackAddDamage")
				{
					return "物理伤害加成";
				}
			}
			else if (attributeType == "iceDamage")
			{
				return "冰冻伤害";
			}
		}
		else if (num <= 3056705585U)
		{
			if (num <= 2226667892U)
			{
				if (num <= 2032614238U)
				{
					if (num != 1849085123U)
					{
						if (num != 1920729928U)
						{
							if (num == 2032614238U)
							{
								if (attributeType == "agiAllAdd")
								{
									return "敏捷总加成";
								}
							}
						}
						else if (attributeType == "AGI")
						{
							return "dex";
						}
					}
					else if (attributeType == "skillRange")
					{
						return "技能范围";
					}
				}
				else if (num != 2043142110U)
				{
					if (num != 2100203542U)
					{
						if (num == 2226667892U)
						{
							if (attributeType == "Armor")
							{
								return "armor";
							}
						}
					}
					else if (attributeType == "baojilv")
					{
						return "baojiLv";
					}
				}
				else if (attributeType == "lightDamage")
				{
					return "雷电伤害";
				}
			}
			else if (num <= 2432247726U)
			{
				if (num != 2280602383U)
				{
					if (num != 2311779407U)
					{
						if (num == 2432247726U)
						{
							if (attributeType == "skillExpend")
							{
								return "法力值消耗";
							}
						}
					}
					else if (attributeType == "skillTime")
					{
						return "技能持续时间";
					}
				}
				else if (attributeType == "haloRangeAdd")
				{
					return "光环范围提升";
				}
			}
			else if (num != 2469979973U)
			{
				if (num != 2790585571U)
				{
					if (num == 3056705585U)
					{
						if (attributeType == "castSpeed")
						{
							return "施法速度提升";
						}
					}
				}
				else if (attributeType == "staAllAdd")
				{
					return "耐力总加成";
				}
			}
			else if (attributeType == "addDamage")
			{
				return "总伤害加成";
			}
		}
		else if (num <= 3682159857U)
		{
			if (num <= 3435096107U)
			{
				if (num != 3374588740U)
				{
					if (num != 3420676352U)
					{
						if (num == 3435096107U)
						{
							if (attributeType == "addCallMonsterTime")
							{
								return "召唤物持续时间";
							}
						}
					}
					else if (attributeType == "hpSecRate")
					{
						return "每秒最大生命值百分比回复";
					}
				}
				else if (attributeType == "extraDamage")
				{
					return "exs";
				}
			}
			else if (num != 3467581074U)
			{
				if (num != 3571295423U)
				{
					if (num == 3682159857U)
					{
						if (attributeType == "baojiDamage")
						{
							return "baojiDamage";
						}
					}
				}
				else if (attributeType == "magicXiXue")
				{
					return "法术吸血";
				}
			}
			else if (attributeType == "attackPercent")
			{
				return "百分比攻击力加成";
			}
		}
		else if (num <= 3773565044U)
		{
			if (num != 3745882227U)
			{
				if (num != 3749413059U)
				{
					if (num == 3773565044U)
					{
						if (attributeType == "fireDamage")
						{
							return "火焰伤害";
						}
					}
				}
				else if (attributeType == "addHenshin")
				{
					return "变身强度";
				}
			}
			else if (attributeType == "skillDamage")
			{
				return "法术伤害加成";
			}
		}
		else if (num <= 3952937863U)
		{
			if (num != 3950067448U)
			{
				if (num == 3952937863U)
				{
					if (attributeType == "armedAdd")
					{
						return "武装伤害";
					}
				}
			}
			else if (attributeType == "hpAddUpgrade")
			{
				return "生命回复加成";
			}
		}
		else if (num != 4145017712U)
		{
			if (num == 4163228729U)
			{
				if (attributeType == "STA")
				{
					return "sta";
				}
			}
		}
		else if (attributeType == "luck")
		{
			return "幸运值";
		}
		return attributeType;
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x0001A4B0 File Offset: 0x000186B0
	private static bool ShouldShowAttribute(string attributeType, float value)
	{
		if (attributeType == "attackSpeed" || attributeType == "moveSpeed" || attributeType == "doge" || EquipBase.PercentDisplayAttributeDefineSet.Contains(attributeType) || attributeType == "attackDistance")
		{
			return !Mathf.Approximately(value, 0f);
		}
		return value > 0f;
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x0001A515 File Offset: 0x00018715
	private static string FormatAttributeValue(EquipBase.EquipAttributeData attributeData)
	{
		return EquipBase.FormatAttributeValue(attributeData.attributeType, attributeData.value, attributeData.equipValueType);
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x0001A530 File Offset: 0x00018730
	private static string FormatAttributeValue(string attributeType, float value, EquipBase.EquipValueType equipValueType)
	{
		string arg = (value >= 0f) ? "+" : "-";
		float num = Mathf.Abs(value);
		if (EquipBase.PercentDisplayAttributeDefineSet.Contains(attributeType))
		{
			float num2 = EquipBase.IsCriticalAttribute(attributeType) ? num : (num * 100f);
			return string.Format("{0}{1:F0}%", arg, num2);
		}
		if (attributeType == "moveSpeed")
		{
			return string.Format("{0}{1:F1}", arg, num);
		}
		if (equipValueType == EquipBase.EquipValueType.IntValue)
		{
			return string.Format("{0}{1}", arg, Mathf.RoundToInt(num));
		}
		return string.Format("{0}{1:0.##}", arg, num);
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x0001A5D8 File Offset: 0x000187D8
	private string GetAttributeInfo(EquipBase.EquipAttributeData attributeData, bool isEquipStreng, int nextLevel)
	{
		if (!EquipBase.ShouldShowAttribute(attributeData.attributeType, attributeData.value))
		{
			return "";
		}
		string text = EquipBase.GetAttributeDisplayText(attributeData);
		if (isEquipStreng && this.levelKeys != null)
		{
			float levelUpAttributeValue = this.GetLevelUpAttributeValue(attributeData.attributeType, this.level, nextLevel);
			if (!Mathf.Approximately(levelUpAttributeValue, 0f))
			{
				EquipBase.EquipAttributeData attributeData2 = attributeData;
				attributeData2.value += levelUpAttributeValue;
				text = string.Concat(new string[]
				{
					text,
					" -> ",
					EquipBase.FormatAttributeValue(attributeData2),
					"<color=#FF9700> (",
					EquipBase.FormatAttributeValue(attributeData.attributeType, levelUpAttributeValue, attributeData.equipValueType),
					")</color>"
				});
			}
		}
		return text + "\n";
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x0001A690 File Offset: 0x00018890
	private static int GetCriticalTotalStatsIncrease(float baseIncrease, float multiplier, int startLevel, int targetLevel)
	{
		int num = 0;
		for (int i = startLevel; i < targetLevel; i++)
		{
			int num2 = i;
			num += Mathf.RoundToInt(baseIncrease * Mathf.Pow(multiplier, (float)num2) * 100f);
		}
		return num;
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x0001A6C8 File Offset: 0x000188C8
	public string GetEquipInfo(int addLevel)
	{
		bool flag = addLevel > 0;
		if (this.level + addLevel > this.maxLevel)
		{
			addLevel = this.maxLevel - this.level;
		}
		int num = this.level + addLevel;
		string text = flag ? ((this.level >= this.maxLevel) ? PathDefine.Concat(string.Format(" Lv.{0}", this.level), "\n") : PathDefine.Concat(string.Format(" Lv.{0} -> Lv.{1}", this.level, num), "\n")) : "";
		if (flag)
		{
			if ((this.levelKeys == null || this.levelKeys.Count == 0) && !this.HasAnySkillValue())
			{
				flag = false;
			}
			if (this.level >= this.maxLevel)
			{
				flag = false;
			}
		}
		foreach (EquipBase.EquipAttributeData attributeData in this.equipAttributeDataAry)
		{
			text += this.GetAttributeInfo(attributeData, flag, num);
		}
		string infoResult = this.GetInfoResult(flag, addLevel);
		if (!string.IsNullOrEmpty(infoResult))
		{
			text += infoResult;
			text += "\n";
		}
		return text;
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x0001A80C File Offset: 0x00018A0C
	public string GetInfoResult(bool isEquipStreng, int addLevel)
	{
		string text = "";
		foreach (EquipSkillType skillType in this.GetEquipSkills())
		{
			string infoResult = this.GetInfoResult(skillType, isEquipStreng, addLevel);
			if (!string.IsNullOrEmpty(infoResult))
			{
				if (!string.IsNullOrEmpty(text))
				{
					text += StringDefine.WrapDouble;
				}
				text += infoResult;
			}
		}
		if (!string.IsNullOrEmpty(this.info) && !this.info.Equals(StringDefine.MyEmpty))
		{
			if (!string.IsNullOrEmpty(text) && !text.Equals(StringDefine.MyEmpty))
			{
				text += StringDefine.WrapDouble;
			}
			text += this.info;
		}
		return text;
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x0001A8D4 File Offset: 0x00018AD4
	public string GetInfoResult(EquipSkillType skillType, bool isEquipStreng, int addLevel)
	{
		string languageOrEmpty = EquipBase.GetLanguageOrEmpty(EquipBase.GetEquipSkillInfoKey(skillType));
		if ((string.IsNullOrEmpty(languageOrEmpty) || languageOrEmpty.Equals(StringDefine.MyEmpty)) && skillType == this.equipSkill)
		{
			return null;
		}
		return this.FormatSkillInfo(languageOrEmpty, skillType, isEquipStreng, addLevel);
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x0001A918 File Offset: 0x00018B18
	private string FormatSkillInfo(string infoResult, EquipSkillType skillType, bool isEquipStreng, int addLevel)
	{
		if (string.IsNullOrEmpty(infoResult))
		{
			return "";
		}
		float[] array = this.GetSkillValueAry(skillType);
		if (array == null || array.Length == 0)
		{
			return string.Format(ColorDefine.NormalColor, infoResult);
		}
		float[] array2 = this.GetSkillValueUpAry(skillType);
		object[] array3 = new object[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			float num = array[i];
			float num2 = (array2 != null && i < array2.Length) ? array2[i] : 0f;
			if (Mathf.Approximately(num2, 0f))
			{
				if (skillType == this.equipSkill && this.skillStrAry != null && i < this.skillStrAry.Length)
				{
					array3[i] = this.skillStrAry[i];
				}
				else
				{
					array3[i] = EquipBase.FormatSkillValue(num, 0f);
				}
			}
			else if (isEquipStreng)
			{
				float value = num + num2 * (float)this.level;
				float value2 = num + num2 * (float)(this.level + addLevel);
				array3[i] = string.Concat(new string[]
				{
					"<color=#40FF00>(",
					EquipBase.FormatSkillValue(value, num2),
					" -> ",
					EquipBase.FormatSkillValue(value2, num2),
					")</color>"
				});
			}
			else
			{
				array3[i] = EquipBase.FormatSkillValue(num + num2 * (float)this.level, num2);
			}
		}
		return string.Format(ColorDefine.NormalColor, string.Format(infoResult, array3));
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x0001AA64 File Offset: 0x00018C64
	private static string FormatSkillValue(float value, float skillValueUp = 0f)
	{
		if (!Mathf.Approximately(value, Mathf.Round(value)) || (!Mathf.Approximately(skillValueUp, 0f) && Mathf.Abs(skillValueUp) < 1f))
		{
			return string.Format("{0:F1}", value);
		}
		return string.Format("{0:F0}", value);
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x0001AABC File Offset: 0x00018CBC
	public static string GetEquipInfo(string shopId)
	{
		if (shopId.Equals("empty"))
		{
			return "";
		}
		if (shopId.Equals("forging") || shopId.Equals("forging_gold") || shopId.Equals("equipStreng") || shopId.Equals("dropGold"))
		{
			return Game.Language.Get(PathDefine.Concat(shopId, "_m"), "");
		}
		string text = "";
		string text2 = shopId.Split("_", StringSplitOptions.None)[1];
		if (text2.Equals("0"))
		{
			return Game.Language.Get(PathDefine.Concat(shopId, "_m"), "");
		}
		object obj = ExcelManager.allExcelData["equipment"].DIC(text2);
		List<EquipBase.EquipAttributeData> list;
		foreach (EquipBase.EquipAttributeData equipAttributeData in ((Game.GameData != null && Game.GameData.EquipAttributeDataDic.TryGetValue(text2, out list)) ? list : EquipBase.CreateEquipAttributeDataList(obj)))
		{
			if (EquipBase.ShouldShowAttribute(equipAttributeData.attributeType, equipAttributeData.value))
			{
				text = text + Game.Language.Get(EquipBase.GetAttributeLanguageKey(equipAttributeData.attributeType), "") + EquipBase.FormatAttributeValue(equipAttributeData) + "\n";
			}
		}
		string text3 = "";
		string value = obj.DIC("skill");
		EquipSkillType skillType;
		if (!string.IsNullOrEmpty(value) && Enum.TryParse<EquipSkillType>(value, out skillType))
		{
			text3 = EquipBase.GetLanguageOrEmpty(EquipBase.GetEquipSkillInfoKey(skillType));
		}
		if (!string.IsNullOrEmpty(text3) && !text3.Equals(StringDefine.MyEmpty))
		{
			string text4 = obj.DIC("skillValue");
			if (!string.IsNullOrEmpty(text4))
			{
				string[] array = text4.Split('|', StringSplitOptions.None);
				string format = text3;
				object[] args = array;
				text3 = string.Format(format, args);
			}
			text3 = string.Format(ColorDefine.NormalColor, text3);
			text += text3;
			text += "\n";
		}
		string languageOrEmpty = EquipBase.GetLanguageOrEmpty(PathDefine.Concat(shopId, "_m"));
		if (!string.IsNullOrEmpty(languageOrEmpty) && !languageOrEmpty.Equals(StringDefine.MyEmpty))
		{
			text += languageOrEmpty;
			text += "\n";
		}
		return text;
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x0001AD00 File Offset: 0x00018F00
	public void UpdateEvent()
	{
		foreach (EquipEventBase equipEventBase in this.GetEquipEvents())
		{
			equipEventBase.OnUpdate();
		}
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x0001AD4C File Offset: 0x00018F4C
	public bool IsMyth()
	{
		return int.Parse(this.equipIndex) > 100;
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x0001AD60 File Offset: 0x00018F60
	public EquipStrengUI.EquipStrengData GetLevelUpData()
	{
		return new EquipStrengUI.EquipStrengData
		{
			chance = Mathf.Max(this.minLevelUpSpeed, this.baseLevelUpChance + this.levelUpChanceAdd * (float)this.level),
			gold = this.baseLevelUpGold + this.level * this.levelUpGoldAdd,
			gem = this.levelUpGem
		};
	}

	// Token: 0x04000390 RID: 912
	private const string EquipmentSkillTableName = "equipmentSkill";

	// Token: 0x04000391 RID: 913
	public string equipIndex = "100";

	// Token: 0x04000392 RID: 914
	public int level;

	// Token: 0x04000393 RID: 915
	public string iconName;

	// Token: 0x04000394 RID: 916
	public Image iconImg;

	// Token: 0x04000395 RID: 917
	public string name;

	// Token: 0x04000396 RID: 918
	public List<EquipBase.EquipAttributeData> equipAttributeDataAry = new List<EquipBase.EquipAttributeData>();

	// Token: 0x04000397 RID: 919
	public static readonly string[] AttributeDefineAry = new string[]
	{
		"hp",
		"mp",
		"attack",
		"attackSpeed",
		"xixue",
		"hptouqu",
		"Armor",
		"moveSpeed",
		"STR",
		"STA",
		"AGI",
		"baojilv",
		"baojiDamage",
		"HPadd",
		"MPadd",
		"skillCd",
		"skillDamage",
		"breakShield",
		"skillBreakShield",
		"doge",
		"skillReduction",
		"luck",
		"normalAttackAddDamage",
		"staAllAdd",
		"strAllAdd",
		"agiAllAdd",
		"attackPercent",
		"addDamage",
		"skillNoneDamage",
		"fireDamage",
		"iceDamage",
		"lightDamage",
		"skillRange",
		"skillTime",
		"attackDistance",
		"buffDamage",
		"haloRangeAdd",
		"forgeAdd",
		"maxHpAddPercent",
		"skillExpend",
		"reduceInjury",
		"extraDamage",
		"castSpeed",
		"hpAddUpgrade",
		"addCallMonster",
		"addCallMonsterTime",
		"addHenshin",
		"addHenshinTime",
		"armedAdd",
		"hpSecRate",
		"magicXiXue",
		"effectDamage"
	};

	// Token: 0x04000398 RID: 920
	private static readonly HashSet<string> FloatAttributeDefineSet = new HashSet<string>
	{
		"attackSpeed",
		"hptouqu",
		"moveSpeed",
		"skillDamage",
		"breakShield",
		"skillBreakShield",
		"normalAttackAddDamage",
		"staAllAdd",
		"strAllAdd",
		"agiAllAdd",
		"attackPercent",
		"addDamage",
		"skillNoneDamage",
		"fireDamage",
		"iceDamage",
		"lightDamage",
		"skillRange",
		"skillTime",
		"attackDistance",
		"buffDamage",
		"haloRangeAdd",
		"forgeAdd",
		"maxHpAddPercent",
		"skillExpend",
		"castSpeed",
		"hpAddUpgrade",
		"addCallMonster",
		"addCallMonsterTime",
		"addHenshin",
		"addHenshinTime",
		"armedAdd",
		"hpSecRate",
		"magicXiXue",
		"effectDamage"
	};

	// Token: 0x04000399 RID: 921
	private static readonly HashSet<string> PercentDisplayAttributeDefineSet = new HashSet<string>
	{
		"attackSpeed",
		"baojilv",
		"baojiDamage",
		"hptouqu",
		"skillDamage",
		"breakShield",
		"skillBreakShield",
		"normalAttackAddDamage",
		"staAllAdd",
		"strAllAdd",
		"agiAllAdd",
		"attackPercent",
		"addDamage",
		"skillNoneDamage",
		"fireDamage",
		"iceDamage",
		"lightDamage",
		"skillRange",
		"skillTime",
		"buffDamage",
		"forgeAdd",
		"maxHpAddPercent",
		"skillExpend",
		"castSpeed",
		"hpAddUpgrade",
		"addCallMonster",
		"addCallMonsterTime",
		"addHenshin",
		"addHenshinTime",
		"armedAdd",
		"hpSecRate",
		"magicXiXue",
		"effectDamage"
	};

	// Token: 0x0400039A RID: 922
	public int upgradeFailed;

	// Token: 0x0400039B RID: 923
	public string info;

	// Token: 0x0400039C RID: 924
	public bool onlySkill;

	// Token: 0x0400039D RID: 925
	public EquipSkillType equipSkill;

	// Token: 0x0400039E RID: 926
	public List<EquipSkillType> extraEquipSkillAry = new List<EquipSkillType>();

	// Token: 0x0400039F RID: 927
	public Dictionary<EquipSkillType, float[]> extraSkillValueAryDic = new Dictionary<EquipSkillType, float[]>();

	// Token: 0x040003A0 RID: 928
	public Dictionary<EquipSkillType, float[]> extraSkillValueUpAryDic = new Dictionary<EquipSkillType, float[]>();

	// Token: 0x040003A1 RID: 929
	public List<EquipEvolutionEntryData> evolutionEntryList = new List<EquipEvolutionEntryData>();

	// Token: 0x040003A2 RID: 930
	public EquipEventBase equipEventBase;

	// Token: 0x040003A3 RID: 931
	public List<EquipEventBase> extraEquipEventAry = new List<EquipEventBase>();

	// Token: 0x040003A4 RID: 932
	public bool shopStreng;

	// Token: 0x040003A5 RID: 933
	private int levelUpGem;

	// Token: 0x040003A6 RID: 934
	public int maxLevel;

	// Token: 0x040003A7 RID: 935
	private List<string> levelKeys;

	// Token: 0x040003A8 RID: 936
	private List<string> levelValues;

	// Token: 0x040003A9 RID: 937
	private List<float> levelRatios;

	// Token: 0x040003AA RID: 938
	public float[] skillValueAry;

	// Token: 0x040003AB RID: 939
	public float[] skillValueUpAry;

	// Token: 0x040003AC RID: 940
	private string[] skillStrAry;

	// Token: 0x040003AD RID: 941
	private int baseLevelUpGold;

	// Token: 0x040003AE RID: 942
	private int levelUpGoldAdd;

	// Token: 0x040003AF RID: 943
	private float baseLevelUpChance;

	// Token: 0x040003B0 RID: 944
	private float levelUpChanceAdd;

	// Token: 0x040003B1 RID: 945
	private float minLevelUpSpeed;

	// Token: 0x040003B2 RID: 946
	public int[] totals;

	// Token: 0x040003B3 RID: 947
	public TotalNumType[] isTotalsPercent;

	// Token: 0x020000D1 RID: 209
	public struct EquipAttributeData
	{
		// Token: 0x040003B4 RID: 948
		public string attributeType;

		// Token: 0x040003B5 RID: 949
		public float value;

		// Token: 0x040003B6 RID: 950
		public EquipBase.EquipValueType equipValueType;
	}

	// Token: 0x020000D2 RID: 210
	public enum EquipValueType
	{
		// Token: 0x040003B8 RID: 952
		IntValue,
		// Token: 0x040003B9 RID: 953
		FloatValue
	}
}
