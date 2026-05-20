using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DC RID: 220
public class EquipEvolutionEntryData
{
	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06000480 RID: 1152 RVA: 0x0001B927 File Offset: 0x00019B27
	public bool IsAttribute
	{
		get
		{
			return string.Equals(this.entryType, "Attribute", StringComparison.OrdinalIgnoreCase);
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06000481 RID: 1153 RVA: 0x0001B93A File Offset: 0x00019B3A
	public bool IsSkill
	{
		get
		{
			return string.Equals(this.entryType, "Skill", StringComparison.OrdinalIgnoreCase);
		}
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x0001B950 File Offset: 0x00019B50
	public EquipEvolutionEntryData Clone()
	{
		return new EquipEvolutionEntryData
		{
			sourceEquip = this.sourceEquip,
			entryType = this.entryType,
			attributeType = this.attributeType,
			attributeValue = this.attributeValue,
			levelUpValue = this.levelUpValue,
			levelUpRatio = this.levelUpRatio,
			hasLevelUpValue = this.hasLevelUpValue,
			hasLevelUpRatio = this.hasLevelUpRatio,
			equipSkill = this.equipSkill,
			skillValueAry = ((this.skillValueAry == null) ? null : ((float[])this.skillValueAry.Clone())),
			skillValueUpAry = ((this.skillValueUpAry == null) ? null : ((float[])this.skillValueUpAry.Clone())),
			weight = this.weight,
			isBlock = this.isBlock
		};
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x0001BA28 File Offset: 0x00019C28
	public RoguelikeUIData CreateRoguelikeUIData(string icon, string data)
	{
		return new RoguelikeUIData
		{
			name = this.GetDisplayName(),
			dec = this.GetDisplayDescription(),
			icon = icon,
			data = data,
			quality = -1
		};
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x0001BA70 File Offset: 0x00019C70
	private string GetDisplayName()
	{
		if (this.IsAttribute)
		{
			return EquipBase.GetAttributeDisplayName(this.attributeType);
		}
		if (this.IsSkill)
		{
			return Game.Language.Get(PathDefine.Concat("equipSkill_", this.equipSkill.ToString()), "");
		}
		return "";
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x0001BACC File Offset: 0x00019CCC
	private string GetDisplayDescription()
	{
		if (this.IsAttribute)
		{
			return EquipBase.GetAttributeDisplayText(EquipBase.CreateEquipAttributeData(this.attributeType, this.attributeValue));
		}
		if (!this.IsSkill)
		{
			return "";
		}
		string equipSkillDescription = EquipBase.GetEquipSkillDescription(this.equipSkill, this.skillValueAry);
		if (!string.IsNullOrEmpty(equipSkillDescription))
		{
			return equipSkillDescription;
		}
		return this.equipSkill.ToString();
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x0001BB34 File Offset: 0x00019D34
	public void ApplyTo(EquipBase equipBase)
	{
		if (equipBase == null)
		{
			return;
		}
		if (this.IsAttribute && !string.IsNullOrEmpty(this.attributeType))
		{
			EquipBase.EquipAttributeData attributeData = EquipBase.CreateEquipAttributeData(this.attributeType, this.attributeValue);
			equipBase.AddExtraAttributeData(attributeData, this.GetLevelUpValue(), this.GetLevelUpRatio());
			equipBase.AddEvolutionEntry(this);
			return;
		}
		if (this.IsSkill && !equipBase.HasEquipSkill(this.equipSkill))
		{
			equipBase.AddExtraEquipSkill(this.equipSkill, this.skillValueAry, this.skillValueUpAry);
			equipBase.AddEvolutionEntry(this);
		}
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x0001BBBC File Offset: 0x00019DBC
	public static EquipEvolutionEntryData Create(object data)
	{
		Dictionary<string, object> dictionary = data.DIC();
		EquipEvolutionEntryData equipEvolutionEntryData = new EquipEvolutionEntryData
		{
			sourceEquip = (dictionary.ContainsKey("sourceEquip") ? data.DIC("sourceEquip") : ""),
			entryType = (dictionary.ContainsKey("entryType") ? data.DIC("entryType") : ""),
			attributeType = (dictionary.ContainsKey("attributeType") ? data.DIC("attributeType") : ""),
			weight = (dictionary.ContainsKey("weight") ? Mathf.Max(0, data.DIC("weight")) : 1),
			isBlock = (dictionary.ContainsKey("isBlock") && data.DIC("isBlock"))
		};
		if (dictionary.ContainsKey("attributeValue"))
		{
			equipEvolutionEntryData.attributeValue = data.DIC("attributeValue");
		}
		float num;
		if (EquipEvolutionEntryData.TryParseFloat(data, "levelUpValue", out num))
		{
			equipEvolutionEntryData.levelUpValue = num;
			equipEvolutionEntryData.hasLevelUpValue = true;
		}
		float num2;
		if (EquipEvolutionEntryData.TryParseFloat(data, "levelUpRatio", out num2))
		{
			equipEvolutionEntryData.levelUpRatio = num2;
			equipEvolutionEntryData.hasLevelUpRatio = true;
		}
		if (dictionary.ContainsKey("equipSkill"))
		{
			string value = data.DIC("equipSkill");
			EquipSkillType equipSkillType;
			if (!string.IsNullOrEmpty(value) && Enum.TryParse<EquipSkillType>(value, out equipSkillType))
			{
				equipEvolutionEntryData.equipSkill = equipSkillType;
			}
		}
		if (dictionary.ContainsKey("skillValue"))
		{
			equipEvolutionEntryData.skillValueAry = EquipEvolutionEntryData.ParseFloatAry(data.DIC("skillValue"));
		}
		if (dictionary.ContainsKey("skillValueUp"))
		{
			equipEvolutionEntryData.skillValueUpAry = EquipEvolutionEntryData.ParseFloatAry(data.DIC("skillValueUp"));
		}
		return equipEvolutionEntryData;
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x0001BD62 File Offset: 0x00019F62
	private float GetLevelUpValue()
	{
		if (!this.hasLevelUpValue)
		{
			return this.attributeValue * 0.1f;
		}
		return this.levelUpValue;
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x0001BD7F File Offset: 0x00019F7F
	private float GetLevelUpRatio()
	{
		if (!this.hasLevelUpRatio)
		{
			return 1.1f;
		}
		return this.levelUpRatio;
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x0001BD98 File Offset: 0x00019F98
	private static bool TryParseFloat(object data, string key, out float value)
	{
		value = 0f;
		if (!data.DIC().ContainsKey(key))
		{
			return false;
		}
		string text = data.DIC(key);
		return !string.IsNullOrEmpty(text) && float.TryParse(text, out value);
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x0001BDD8 File Offset: 0x00019FD8
	private static float[] ParseFloatAry(string valueStr)
	{
		if (string.IsNullOrEmpty(valueStr))
		{
			return null;
		}
		string[] array = valueStr.Split('|', StringSplitOptions.None);
		float[] array2 = new float[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			float num;
			array2[i] = (float.TryParse(array[i], out num) ? num : 0f);
		}
		return array2;
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x0001BE28 File Offset: 0x0001A028
	public static EquipEvolutionEntryData GetRandom(EquipBase equipBase)
	{
		List<EquipEvolutionEntryData> randomOptions = EquipEvolutionEntryData.GetRandomOptions(equipBase, 1);
		if (randomOptions.Count <= 0)
		{
			return null;
		}
		return randomOptions[0];
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x0001BE50 File Offset: 0x0001A050
	public static EquipEvolutionEntryData GetRandom(EquipBase equipBase, string sourceEquip)
	{
		List<EquipEvolutionEntryData> randomOptions = EquipEvolutionEntryData.GetRandomOptions(equipBase, 1, sourceEquip);
		if (randomOptions.Count <= 0)
		{
			return null;
		}
		return randomOptions[0];
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x0001BE78 File Offset: 0x0001A078
	public static List<EquipEvolutionEntryData> GetRandomOptions(EquipBase equipBase, int count)
	{
		return EquipEvolutionEntryData.GetRandomOptions(equipBase, count, (equipBase == null) ? "" : equipBase.equipIndex);
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x0001BE94 File Offset: 0x0001A094
	public static List<EquipEvolutionEntryData> GetRandomOptions(EquipBase equipBase, int count, string sourceEquip)
	{
		List<EquipEvolutionEntryData> availableEntries = EquipEvolutionEntryData.GetAvailableEntries(equipBase, sourceEquip);
		List<EquipEvolutionEntryData> list = new List<EquipEvolutionEntryData>();
		int num = 0;
		while (num < count && availableEntries.Count > 0)
		{
			int num2 = 0;
			for (int i = 0; i < availableEntries.Count; i++)
			{
				num2 += availableEntries[i].weight;
			}
			if (num2 <= 0)
			{
				break;
			}
			int num3 = Random.Range(0, num2);
			for (int j = 0; j < availableEntries.Count; j++)
			{
				num3 -= availableEntries[j].weight;
				if (num3 < 0)
				{
					list.Add(availableEntries[j]);
					availableEntries.RemoveAt(j);
					break;
				}
			}
			num++;
		}
		return list;
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0001BF40 File Offset: 0x0001A140
	public static List<EquipEvolutionEntryData> GetUpdatedEntries(IEnumerable<EquipEvolutionEntryData> entries, string sourceEquip)
	{
		List<EquipEvolutionEntryData> list = new List<EquipEvolutionEntryData>();
		if (entries == null)
		{
			return list;
		}
		foreach (EquipEvolutionEntryData equipEvolutionEntryData in entries)
		{
			if (equipEvolutionEntryData != null)
			{
				EquipEvolutionEntryData equipEvolutionEntryData2;
				list.Add(EquipEvolutionEntryData.TryGetEntry(sourceEquip, equipEvolutionEntryData, out equipEvolutionEntryData2) ? equipEvolutionEntryData2 : equipEvolutionEntryData.Clone());
			}
		}
		return list;
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x0001BFAC File Offset: 0x0001A1AC
	public static List<EquipEvolutionEntryData> GetSkillEntries(string sourceEquip, IEnumerable<string> equipSkillNames)
	{
		List<EquipEvolutionEntryData> list = new List<EquipEvolutionEntryData>();
		if (string.IsNullOrEmpty(sourceEquip) || equipSkillNames == null)
		{
			return list;
		}
		foreach (string value in equipSkillNames)
		{
			EquipSkillType equipSkillType;
			EquipEvolutionEntryData item;
			if (!string.IsNullOrEmpty(value) && Enum.TryParse<EquipSkillType>(value, out equipSkillType) && equipSkillType != EquipSkillType.None && EquipEvolutionEntryData.TryGetSkillEntry(sourceEquip, equipSkillType, out item))
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x0001C02C File Offset: 0x0001A22C
	private static List<EquipEvolutionEntryData> GetAvailableEntries(EquipBase equipBase, string sourceEquip)
	{
		List<EquipEvolutionEntryData> list = new List<EquipEvolutionEntryData>();
		if (equipBase == null || string.IsNullOrEmpty(sourceEquip))
		{
			return list;
		}
		Dictionary<string, object> entryTable = EquipEvolutionEntryData.GetEntryTable();
		if (entryTable == null)
		{
			return list;
		}
		foreach (object data in entryTable.Values)
		{
			EquipEvolutionEntryData equipEvolutionEntryData = EquipEvolutionEntryData.Create(data);
			if (EquipEvolutionEntryData.IsValidEntry(equipEvolutionEntryData, sourceEquip) && !equipEvolutionEntryData.isBlock && (!equipEvolutionEntryData.IsAttribute || !equipBase.HasAttributeType(equipEvolutionEntryData.attributeType)) && (!equipEvolutionEntryData.IsSkill || !equipBase.HasEquipSkill(equipEvolutionEntryData.equipSkill)))
			{
				list.Add(equipEvolutionEntryData);
			}
		}
		return list;
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x0001C0E0 File Offset: 0x0001A2E0
	private static bool TryGetEntry(string sourceEquip, EquipEvolutionEntryData targetEntry, out EquipEvolutionEntryData entry)
	{
		entry = null;
		if (targetEntry == null || string.IsNullOrEmpty(sourceEquip))
		{
			return false;
		}
		Dictionary<string, object> entryTable = EquipEvolutionEntryData.GetEntryTable();
		if (entryTable == null)
		{
			return false;
		}
		foreach (object data in entryTable.Values)
		{
			EquipEvolutionEntryData equipEvolutionEntryData = EquipEvolutionEntryData.Create(data);
			if (EquipEvolutionEntryData.IsValidEntry(equipEvolutionEntryData, sourceEquip) && EquipEvolutionEntryData.IsSameEntry(equipEvolutionEntryData, targetEntry))
			{
				entry = equipEvolutionEntryData;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x0001C168 File Offset: 0x0001A368
	private static bool TryGetSkillEntry(string sourceEquip, EquipSkillType equipSkill, out EquipEvolutionEntryData entry)
	{
		entry = null;
		if (string.IsNullOrEmpty(sourceEquip) || equipSkill == EquipSkillType.None)
		{
			return false;
		}
		Dictionary<string, object> entryTable = EquipEvolutionEntryData.GetEntryTable();
		if (entryTable == null)
		{
			return false;
		}
		foreach (object data in entryTable.Values)
		{
			EquipEvolutionEntryData equipEvolutionEntryData = EquipEvolutionEntryData.Create(data);
			if (string.Equals(equipEvolutionEntryData.sourceEquip, sourceEquip, StringComparison.OrdinalIgnoreCase) && equipEvolutionEntryData.IsSkill && equipEvolutionEntryData.equipSkill == equipSkill)
			{
				entry = equipEvolutionEntryData;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x0001C200 File Offset: 0x0001A400
	private static Dictionary<string, object> GetEntryTable()
	{
		object obj;
		if (!ExcelManager.allExcelData.TryGetValue("equipEvolutionEntry", out obj))
		{
			return null;
		}
		return obj as Dictionary<string, object>;
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x0001C228 File Offset: 0x0001A428
	private static bool IsValidEntry(EquipEvolutionEntryData entry, string sourceEquip)
	{
		if (entry.weight <= 0 || !string.Equals(entry.sourceEquip, sourceEquip, StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}
		if (entry.IsAttribute)
		{
			return !string.IsNullOrEmpty(entry.attributeType);
		}
		return entry.IsSkill && entry.equipSkill != EquipSkillType.None;
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x0001C27C File Offset: 0x0001A47C
	private static bool IsSameEntry(EquipEvolutionEntryData lhs, EquipEvolutionEntryData rhs)
	{
		if (lhs.IsAttribute && rhs.IsAttribute)
		{
			return lhs.attributeType == rhs.attributeType;
		}
		return lhs.IsSkill && rhs.IsSkill && lhs.equipSkill == rhs.equipSkill;
	}

	// Token: 0x0400040B RID: 1035
	private const string EntryTypeAttribute = "Attribute";

	// Token: 0x0400040C RID: 1036
	private const string EntryTypeSkill = "Skill";

	// Token: 0x0400040D RID: 1037
	public string sourceEquip;

	// Token: 0x0400040E RID: 1038
	public string entryType;

	// Token: 0x0400040F RID: 1039
	public string attributeType;

	// Token: 0x04000410 RID: 1040
	public float attributeValue;

	// Token: 0x04000411 RID: 1041
	public float levelUpValue;

	// Token: 0x04000412 RID: 1042
	public float levelUpRatio = 1.1f;

	// Token: 0x04000413 RID: 1043
	public bool hasLevelUpValue;

	// Token: 0x04000414 RID: 1044
	public bool hasLevelUpRatio;

	// Token: 0x04000415 RID: 1045
	public EquipSkillType equipSkill = EquipSkillType.None;

	// Token: 0x04000416 RID: 1046
	public float[] skillValueAry;

	// Token: 0x04000417 RID: 1047
	public float[] skillValueUpAry;

	// Token: 0x04000418 RID: 1048
	public int weight = 1;

	// Token: 0x04000419 RID: 1049
	public bool isBlock;
}
