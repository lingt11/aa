using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000202 RID: 514
public class RelicBase
{
	// Token: 0x06000935 RID: 2357 RVA: 0x000327F5 File Offset: 0x000309F5
	protected float GetValue(int index, float defaultValue = 0f)
	{
		return this.GetLevelValue(index, defaultValue, this.level);
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x00032805 File Offset: 0x00030A05
	protected int GetIntValue(int index, int defaultValue = 0)
	{
		return Mathf.RoundToInt(this.GetValue(index, (float)defaultValue));
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x00032815 File Offset: 0x00030A15
	protected float GetBaseValue(int index, float defaultValue = 0f)
	{
		if (this.values != null && index >= 0 && index < this.values.Length)
		{
			return this.values[index];
		}
		return defaultValue;
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x00032838 File Offset: 0x00030A38
	protected int GetBaseIntValue(int index, int defaultValue = 0)
	{
		return Mathf.RoundToInt(this.GetBaseValue(index, (float)defaultValue));
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x00032848 File Offset: 0x00030A48
	protected float GetLevelUpValue(int index)
	{
		if (this.levelUpValues != null && index >= 0 && index < this.levelUpValues.Length)
		{
			return this.levelUpValues[index];
		}
		return 0f;
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x0003286F File Offset: 0x00030A6F
	protected int GetLevelUpIntValue(int index)
	{
		return Mathf.RoundToInt(this.GetLevelUpValue(index));
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x0003287D File Offset: 0x00030A7D
	protected float GetLevelValue(int index, float defaultValue, int targetLevel)
	{
		return this.GetBaseValue(index, defaultValue) + this.GetLevelUpValue(index) * (float)targetLevel;
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x00032892 File Offset: 0x00030A92
	protected int GetLevelIntValue(int index, int defaultValue, int targetLevel)
	{
		return Mathf.RoundToInt(this.GetLevelValue(index, (float)defaultValue, targetLevel));
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x000328A3 File Offset: 0x00030AA3
	protected float GetLevelValueDelta(int index, float defaultValue, int oldLevel, int newLevel)
	{
		return this.GetLevelValue(index, defaultValue, newLevel) - this.GetLevelValue(index, defaultValue, oldLevel);
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x000328B9 File Offset: 0x00030AB9
	protected int GetLevelIntValueDelta(int index, int defaultValue, int oldLevel, int newLevel)
	{
		return this.GetLevelIntValue(index, defaultValue, newLevel) - this.GetLevelIntValue(index, defaultValue, oldLevel);
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x000328CF File Offset: 0x00030ACF
	public RelicValueType GetValueType(int index)
	{
		if (this.valueTypes != null && index >= 0 && index < this.valueTypes.Length)
		{
			return this.valueTypes[index];
		}
		return RelicValueType.Normal;
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x000328F2 File Offset: 0x00030AF2
	public string[] GetValueFormatStrings()
	{
		return RelicBase.GetValueFormatStrings(this.values, this.valueTypes, this.levelUpValues, this.level);
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x00032911 File Offset: 0x00030B11
	public string GetValueFormatString(int index)
	{
		return RelicBase.GetValueFormatString(this.GetValue(index, 0f), this.GetValueType(index));
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x0003292B File Offset: 0x00030B2B
	public string GetFormatDec(string dec)
	{
		return RelicBase.GetFormatDec(dec, this.values, this.valueTypes, this.levelUpValues, this.level);
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x0003294B File Offset: 0x00030B4B
	public string GetLevelCompareFormatDec(string dec, int nextLevel)
	{
		return RelicBase.GetLevelCompareFormatDec(dec, this.values, this.valueTypes, this.levelUpValues, this.level, nextLevel);
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x0003296C File Offset: 0x00030B6C
	public static string[] GetValueFormatStrings(Dictionary<string, object> relicData)
	{
		return RelicBase.GetValueFormatStrings(relicData, 0);
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x00032978 File Offset: 0x00030B78
	public static string[] GetValueFormatStrings(Dictionary<string, object> relicData, int level)
	{
		float[] inputValues = RelicBase.ParseValues((relicData != null) ? relicData.DIC("values") : null);
		RelicValueType[] inputTypes = RelicBase.ParseValueTypes((relicData != null) ? relicData.DIC("valueTypes") : null);
		float[] inputLevelUps = RelicBase.ParseValues((relicData != null) ? relicData.DIC("levelup") : null);
		return RelicBase.GetValueFormatStrings(inputValues, inputTypes, inputLevelUps, level);
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x000329D4 File Offset: 0x00030BD4
	public static string[] GetValueFormatStrings(float[] inputValues, RelicValueType[] inputTypes = null, float[] inputLevelUps = null, int level = 0)
	{
		if (inputValues == null || inputValues.Length == 0)
		{
			return Array.Empty<string>();
		}
		string[] array = new string[inputValues.Length];
		for (int i = 0; i < inputValues.Length; i++)
		{
			RelicValueType valueType = RelicValueType.Normal;
			if (inputTypes != null && i < inputTypes.Length)
			{
				valueType = inputTypes[i];
			}
			float num = 0f;
			if (inputLevelUps != null && i < inputLevelUps.Length)
			{
				num = inputLevelUps[i];
			}
			float value = inputValues[i] + num * (float)level;
			array[i] = RelicBase.GetColoredValueFormatString(value, valueType);
		}
		return array;
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x00032A3D File Offset: 0x00030C3D
	public static string GetFormatDec(string dec, Dictionary<string, object> relicData)
	{
		return RelicBase.GetFormatDec(dec, relicData, 0);
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x00032A48 File Offset: 0x00030C48
	public static string GetFormatDec(string dec, Dictionary<string, object> relicData, int level)
	{
		string[] valueFormatStrings = RelicBase.GetValueFormatStrings(relicData, level);
		return RelicBase.GetFormatDec(dec, valueFormatStrings, true);
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x00032A68 File Offset: 0x00030C68
	public static string GetFormatDec(string dec, float[] inputValues, RelicValueType[] inputTypes = null, float[] inputLevelUps = null, int level = 0)
	{
		string[] valueFormatStrings = RelicBase.GetValueFormatStrings(inputValues, inputTypes, inputLevelUps, level);
		return RelicBase.GetFormatDec(dec, valueFormatStrings, true);
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x00032A88 File Offset: 0x00030C88
	public static string GetLevelCompareFormatDec(string dec, Dictionary<string, object> relicData, int currentLevel, int nextLevel)
	{
		float[] inputValues = RelicBase.ParseValues((relicData != null) ? relicData.DIC("values") : null);
		RelicValueType[] inputTypes = RelicBase.ParseValueTypes((relicData != null) ? relicData.DIC("valueTypes") : null);
		float[] inputLevelUps = RelicBase.ParseValues((relicData != null) ? relicData.DIC("levelup") : null);
		return RelicBase.GetLevelCompareFormatDec(dec, inputValues, inputTypes, inputLevelUps, currentLevel, nextLevel);
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x00032AE8 File Offset: 0x00030CE8
	public static string GetLevelCompareFormatDec(string dec, float[] inputValues, RelicValueType[] inputTypes = null, float[] inputLevelUps = null, int currentLevel = 0, int nextLevel = 0)
	{
		string[] levelCompareFormatStrings = RelicBase.GetLevelCompareFormatStrings(inputValues, inputTypes, inputLevelUps, currentLevel, nextLevel);
		return RelicBase.GetFormatDec(dec, levelCompareFormatStrings, false);
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x00032B0C File Offset: 0x00030D0C
	public static string[] GetLevelCompareFormatStrings(Dictionary<string, object> relicData, int currentLevel, int nextLevel)
	{
		float[] inputValues = RelicBase.ParseValues((relicData != null) ? relicData.DIC("values") : null);
		RelicValueType[] inputTypes = RelicBase.ParseValueTypes((relicData != null) ? relicData.DIC("valueTypes") : null);
		float[] inputLevelUps = RelicBase.ParseValues((relicData != null) ? relicData.DIC("levelup") : null);
		return RelicBase.GetLevelCompareFormatStrings(inputValues, inputTypes, inputLevelUps, currentLevel, nextLevel);
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x00032B68 File Offset: 0x00030D68
	public static string[] GetLevelCompareFormatStrings(float[] inputValues, RelicValueType[] inputTypes = null, float[] inputLevelUps = null, int currentLevel = 0, int nextLevel = 0)
	{
		if (inputValues == null || inputValues.Length == 0)
		{
			return Array.Empty<string>();
		}
		string[] array = new string[inputValues.Length];
		for (int i = 0; i < inputValues.Length; i++)
		{
			RelicValueType valueType = RelicValueType.Normal;
			if (inputTypes != null && i < inputTypes.Length)
			{
				valueType = inputTypes[i];
			}
			float num = 0f;
			if (inputLevelUps != null && i < inputLevelUps.Length)
			{
				num = inputLevelUps[i];
			}
			string coloredValueFormatString = RelicBase.GetColoredValueFormatString(inputValues[i] + num * (float)currentLevel, valueType);
			if (Mathf.Approximately(num, 0f))
			{
				array[i] = RelicBase.AddFormatValueSpacing(coloredValueFormatString);
			}
			else
			{
				string coloredValueFormatString2 = RelicBase.GetColoredValueFormatString(inputValues[i] + num * (float)nextLevel, valueType);
				array[i] = RelicBase.AddFormatValueSpacing(PathDefine.Concat(coloredValueFormatString, " -> ", coloredValueFormatString2));
			}
		}
		return array;
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x00032C0C File Offset: 0x00030E0C
	private static string GetFormatDec(string dec, string[] formatValues, bool addSpacing = true)
	{
		if (formatValues.Length == 0)
		{
			return dec;
		}
		if (addSpacing)
		{
			for (int i = 0; i < formatValues.Length; i++)
			{
				formatValues[i] = RelicBase.AddFormatValueSpacing(formatValues[i]);
			}
		}
		return string.Format(dec, formatValues);
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x00032C44 File Offset: 0x00030E44
	public static float[] ParseValues(string valuesStr)
	{
		if (string.IsNullOrEmpty(valuesStr))
		{
			return null;
		}
		return Array.ConvertAll<string, float>(valuesStr.Split('|', StringSplitOptions.None), new Converter<string, float>(float.Parse));
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x00032C6A File Offset: 0x00030E6A
	public static RelicValueType[] ParseValueTypes(string valueTypesStr)
	{
		if (string.IsNullOrEmpty(valueTypesStr))
		{
			return null;
		}
		return Array.ConvertAll<string, RelicValueType>(valueTypesStr.Split('|', StringSplitOptions.None), (string s) => (RelicValueType)Enum.Parse(typeof(RelicValueType), s));
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x00032CA4 File Offset: 0x00030EA4
	private static string GetValueFormatString(float value, RelicValueType valueType)
	{
		switch (valueType)
		{
		case RelicValueType.Percent:
			return RelicBase.GetPercentString(value * 100f);
		case RelicValueType.PercentConst:
			return RelicBase.GetPercentString(value);
		case RelicValueType.InversePercent:
			return RelicBase.GetPercentString((1f - value) * 100f);
		default:
			return RelicBase.GetNumberString(value);
		}
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x00032CF4 File Offset: 0x00030EF4
	private static string GetColoredValueFormatString(float value, RelicValueType valueType)
	{
		return string.Format(ColorDefine.NormalColor, RelicBase.GetValueFormatString(value, valueType));
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x00032D07 File Offset: 0x00030F07
	private static string AddFormatValueSpacing(string value)
	{
		return PathDefine.Concat(" ", value, " ");
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x00032D19 File Offset: 0x00030F19
	private static string GetPercentString(float value)
	{
		return PathDefine.Concat(RelicBase.GetNumberString(value), StringDefine.Percent);
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x00032D2C File Offset: 0x00030F2C
	private static string GetNumberString(float value)
	{
		float num = Mathf.Round(value);
		if (Mathf.Abs(value - num) < 0.001f)
		{
			return ((int)num).ToString();
		}
		return value.ToString("0.##");
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void KillEnemy(RoleBase enemy)
	{
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void BaoJi(RoleBase enemy)
	{
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void AttackEnemy(RoleBase enemy)
	{
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void Exit()
	{
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void Enter()
	{
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void Update()
	{
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x00002D1D File Offset: 0x00000F1D
	public void CallMoDaiUI(int num)
	{
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x00032D68 File Offset: 0x00030F68
	public virtual void OnLevelUp()
	{
		int oldLevel = this.level;
		this.level++;
		this.OnLevelChanged(oldLevel, this.level);
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x00032D98 File Offset: 0x00030F98
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

	// Token: 0x0600095F RID: 2399 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected virtual void OnLevelChanged(int oldLevel, int newLevel)
	{
	}

	// Token: 0x06000960 RID: 2400 RVA: 0x00032DD4 File Offset: 0x00030FD4
	protected RoleBuff AddShowBuff(float time)
	{
		return GameHelperClient.AddShowBuff(PathDefine.Concat(Game.Language.Get(PathDefine.Concat("pickitem_", this.keyIndex), ""), Game.Language.Get("生效提示", "")), this.GetFormatDec(Game.Language.Get("pickitem_" + this.keyIndex + "_m", "")), PathDefine.Concat("Remains/", this.relicData.DIC("icon")), time);
	}

	// Token: 0x04000BAD RID: 2989
	public string icon;

	// Token: 0x04000BAE RID: 2990
	public Dictionary<string, object> relicData = new Dictionary<string, object>();

	// Token: 0x04000BAF RID: 2991
	public Text myTextNum;

	// Token: 0x04000BB0 RID: 2992
	public string keyIndex;

	// Token: 0x04000BB1 RID: 2993
	public int quality;

	// Token: 0x04000BB2 RID: 2994
	public PlayerBase playerBase;

	// Token: 0x04000BB3 RID: 2995
	public int[] totals;

	// Token: 0x04000BB4 RID: 2996
	public bool isTotalPercent;

	// Token: 0x04000BB5 RID: 2997
	public string exDec;

	// Token: 0x04000BB6 RID: 2998
	public int level;

	// Token: 0x04000BB7 RID: 2999
	public float[] values;

	// Token: 0x04000BB8 RID: 3000
	public RelicValueType[] valueTypes;

	// Token: 0x04000BB9 RID: 3001
	public float[] levelUpValues;
}
