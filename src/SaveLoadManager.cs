using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class SaveLoadManager
{
	// Token: 0x06000153 RID: 339 RVA: 0x00008B8C File Offset: 0x00006D8C
	public static void OnCompLevel()
	{
		/*
An exception occurred when decompiling this method (06000153)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void SaveLoadManager::OnCompLevel()

 ---> System.InvalidCastException: Unable to cast object of type 'ICSharpCode.NRefactory.CSharp.ParenthesizedExpression' to type 'ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression'.
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformByteCode(ILExpression byteCode) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 640
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformExpression(ILExpression expr) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 414
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformNode(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 270
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformBlock(ILBlock block) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 254
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformNode(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 295
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformBlock(ILBlock block) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 254
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 151
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00008BC8 File Offset: 0x00006DC8
	public static void SaveGameData()
	{
		if (!SaveLoadManager.isInit)
		{
			return;
		}
		SaveLoadManager.NormalizeGameSaveDataAmounts();
		List<CardManager.HaveCardData> list = new List<CardManager.HaveCardData>();
		foreach (KeyValuePair<int, CardManager.HaveCardData> keyValuePair in SaveLoadManager.haveCardDataDic)
		{
			list.Add(keyValuePair.Value);
		}
		SaveLoadManager.gameSaveData.haveCardDataDic = list;
		string contents = JsonUtility.ToJson(SaveLoadManager.gameSaveData, true);
		File.WriteAllText(SaveLoadManager.path, contents);
	}

	// Token: 0x06000155 RID: 341 RVA: 0x00008C58 File Offset: 0x00006E58
	public static void InitSaveLoadManager()
	{
		SaveLoadManager.isInit = true;
		if (Game.Save.Check("IsAutoUseCard"))
		{
			GameHelperClient.IsAutoUseCard = Game.Save.Load<bool>("IsAutoUseCard");
		}
		if (Game.Save.Check("IsAutoBattle"))
		{
			GameHelperClient.IsAutoBattle = Game.Save.Load<bool>("IsAutoBattle");
		}
		if (Game.Save.Check("IsShowDamage"))
		{
			GameHelperClient.IsShowDamage = Game.Save.Load<bool>("IsShowDamage");
		}
		if (Game.Save.Check("IsKeyPickTalisman"))
		{
			GameHelperClient.IsKeyPickTalisman = Game.Save.Load<bool>("IsKeyPickTalisman");
		}
		if (Game.Save.Check("IsSmartCasting"))
		{
			GameHelperClient.IsSmartCasting = Game.Save.Load<bool>("IsSmartCasting");
		}
		if (Game.Save.Check("IsPickShare"))
		{
			GameHelperClient.IsPickShare = Game.Save.Load<bool>("IsPickShare");
		}
		if (Game.Save.Check("PlayerNameDisplayMode"))
		{
			GameHelperClient.playerNameDisplayMode = (GameHelperClient.PlayerNameDisplayMode)Mathf.Clamp(Game.Save.Load<int>("PlayerNameDisplayMode"), 0, 2);
		}
		if (!File.Exists(SaveLoadManager.path))
		{
			SaveLoadManager.LoadDefaultSave();
		}
		else
		{
			string json = File.ReadAllText(SaveLoadManager.path);
			try
			{
				SaveLoadManager.gameSaveData = JsonUtility.FromJson<SaveLoadManager.GameSaveData>(json);
				if (SaveLoadManager.gameSaveData == null)
				{
					SaveLoadManager.LoadDefaultSave();
					return;
				}
				SaveLoadManager.gameSaveData = JsonUtility.FromJson<SaveLoadManager.GameSaveData>(json);
				SaveLoadManager.NormalizeGameSaveDataAmounts();
				SaveLoadManager.gameSaveData.maxCapacity = Mathf.Max(1, SaveLoadManager.gameSaveData.maxCapacity);
				if (SaveLoadManager.gameSaveData.equipCards == null)
				{
					SaveLoadManager.gameSaveData.equipCards = new List<int>();
				}
				if (SaveLoadManager.gameSaveData.guideSkillKeys == null)
				{
					SaveLoadManager.gameSaveData.guideSkillKeys = new List<string>();
				}
				SaveLoadManager.EnsureEquipCardPresets();
				if (SaveLoadManager.haveCardDataDic == null)
				{
					List<CardManager.HaveCardData> list = SaveLoadManager.gameSaveData.haveCardDataDic;
					SaveLoadManager.haveCardDataDic = new Dictionary<int, CardManager.HaveCardData>();
					if (list != null)
					{
						foreach (CardManager.HaveCardData haveCardData in list)
						{
							SaveLoadManager.haveCardDataDic.Add(haveCardData.cardId, haveCardData);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("存档读取失败: " + ex.Message);
				(Game.UI.OpenUI<UI_Confirm>(null) as UI_Confirm).SetConfirmText(Game.Language.Get("检测到存档损坏，是否删除存档？", ""), new Action(SaveLoadManager.OnDeleteSave), null, null, "");
			}
		}
		SaveLoadManager.InitPlayerKingData();
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00008EEC File Offset: 0x000070EC
	public static void OnDeleteSave()
	{
		SaveLoadManager.LoadDefaultSave();
		GameHelperClient.OnGameReset();
	}

	// Token: 0x06000157 RID: 343 RVA: 0x00008EF8 File Offset: 0x000070F8
	private static void LoadDefaultSave()
	{
		SaveLoadManager.gameSaveData = new SaveLoadManager.GameSaveData();
		SaveLoadManager.gameSaveData.equipCards = Game.Save.Load<List<int>>("cards");
		SaveLoadManager.haveCardDataDic = Game.Save.Load<Dictionary<int, CardManager.HaveCardData>>("HaveCardData");
		if (SaveLoadManager.gameSaveData.equipCards == null)
		{
			SaveLoadManager.gameSaveData.equipCards = new List<int>();
		}
		if (SaveLoadManager.gameSaveData.guideSkillKeys == null)
		{
			SaveLoadManager.gameSaveData.guideSkillKeys = new List<string>();
		}
		SaveLoadManager.EnsureEquipCardPresets();
		if (SaveLoadManager.haveCardDataDic == null)
		{
			SaveLoadManager.haveCardDataDic = new Dictionary<int, CardManager.HaveCardData>();
		}
		if (Game.Save.Check("jiyi"))
		{
			SaveLoadManager.gameSaveData.memory = (long)Game.Save.Load<int>("jiyi");
		}
		else
		{
			SaveLoadManager.gameSaveData.memory = 0L;
		}
		SaveLoadManager.gameSaveData.cardDust = 0L;
		SaveLoadManager.NormalizeGameSaveDataAmounts();
		if (Game.Save.Check("maxPower"))
		{
			SaveLoadManager.gameSaveData.maxCapacity = Game.Save.Load<int>("maxPower");
		}
		else
		{
			SaveLoadManager.gameSaveData.maxCapacity = 1;
		}
		SaveLoadManager.SaveGameData();
	}

	// Token: 0x06000158 RID: 344 RVA: 0x0000900E File Offset: 0x0000720E
	public static int GetEquipCardPresetCount()
	{
		return SaveLoadManager.equipCardPresetCount;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x00009018 File Offset: 0x00007218
	public static void EnsureEquipCardPresets()
	{
		if (SaveLoadManager.gameSaveData == null)
		{
			return;
		}
		if (SaveLoadManager.gameSaveData.equipCardPresets == null)
		{
			SaveLoadManager.gameSaveData.equipCardPresets = new List<SaveLoadManager.EquipCardPresetData>();
		}
		int num = SaveLoadManager.GetEquipCardPresetCount();
		for (int i = 0; i < num; i++)
		{
			if (i >= SaveLoadManager.gameSaveData.equipCardPresets.Count)
			{
				SaveLoadManager.gameSaveData.equipCardPresets.Add(SaveLoadManager.CreateDefaultEquipCardPreset(i));
			}
			else if (SaveLoadManager.gameSaveData.equipCardPresets[i] == null)
			{
				SaveLoadManager.gameSaveData.equipCardPresets[i] = SaveLoadManager.CreateDefaultEquipCardPreset(i);
			}
			else
			{
				if (string.IsNullOrEmpty(SaveLoadManager.gameSaveData.equipCardPresets[i].presetName))
				{
					SaveLoadManager.gameSaveData.equipCardPresets[i].presetName = SaveLoadManager.GetDefaultEquipCardPresetName(i);
				}
				if (SaveLoadManager.gameSaveData.equipCardPresets[i].equipCards == null)
				{
					SaveLoadManager.gameSaveData.equipCardPresets[i].equipCards = new List<int>();
				}
				if (!SaveLoadManager.gameSaveData.equipCardPresets[i].isSaved && SaveLoadManager.gameSaveData.equipCardPresets[i].equipCards.Count > 0)
				{
					SaveLoadManager.gameSaveData.equipCardPresets[i].isSaved = true;
				}
			}
		}
		if (SaveLoadManager.gameSaveData.currentEquipCardPresetIndex < 1 || SaveLoadManager.gameSaveData.currentEquipCardPresetIndex > num || SaveLoadManager.gameSaveData.equipCardPresets.Count < SaveLoadManager.gameSaveData.currentEquipCardPresetIndex || !SaveLoadManager.gameSaveData.equipCardPresets[SaveLoadManager.gameSaveData.currentEquipCardPresetIndex - 1].isSaved)
		{
			SaveLoadManager.gameSaveData.currentEquipCardPresetIndex = 0;
		}
	}

	// Token: 0x0600015A RID: 346 RVA: 0x000091D0 File Offset: 0x000073D0
	public static SaveLoadManager.EquipCardPresetData GetEquipCardPreset(int presetIndex)
	{
		SaveLoadManager.EquipCardPresetData result;
		if (!SaveLoadManager.TryGetEquipCardPreset(presetIndex, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x0600015B RID: 347 RVA: 0x000091EC File Offset: 0x000073EC
	public static bool SaveEquipCardPreset(int presetIndex, string presetName = null)
	{
		SaveLoadManager.EquipCardPresetData equipCardPresetData;
		if (!SaveLoadManager.TryGetEquipCardPreset(presetIndex, out equipCardPresetData))
		{
			return false;
		}
		equipCardPresetData.equipCards = new List<int>(SaveLoadManager.gameSaveData.equipCards);
		equipCardPresetData.isSaved = true;
		SaveLoadManager.gameSaveData.currentEquipCardPresetIndex = presetIndex;
		if (!string.IsNullOrEmpty(presetName))
		{
			equipCardPresetData.presetName = presetName;
		}
		else if (string.IsNullOrEmpty(equipCardPresetData.presetName))
		{
			equipCardPresetData.presetName = SaveLoadManager.GetDefaultEquipCardPresetName(presetIndex - 1);
		}
		return true;
	}

	// Token: 0x0600015C RID: 348 RVA: 0x0000925C File Offset: 0x0000745C
	public static bool SaveEquipCardPresetToNew(string presetName, out int presetIndex)
	{
		SaveLoadManager.EnsureEquipCardPresets();
		int num = SaveLoadManager.GetEquipCardPresetCount();
		for (int i = 1; i <= num; i++)
		{
			if (!SaveLoadManager.IsEquipCardPresetSaved(i))
			{
				presetIndex = i;
				return SaveLoadManager.SaveEquipCardPreset(i, presetName);
			}
		}
		presetIndex = 0;
		return false;
	}

	// Token: 0x0600015D RID: 349 RVA: 0x00009298 File Offset: 0x00007498
	public static bool RenameEquipCardPreset(int presetIndex, string presetName)
	{
		SaveLoadManager.EquipCardPresetData equipCardPresetData;
		if (string.IsNullOrEmpty(presetName) || !SaveLoadManager.TryGetEquipCardPreset(presetIndex, out equipCardPresetData) || !equipCardPresetData.isSaved)
		{
			return false;
		}
		equipCardPresetData.presetName = presetName;
		return true;
	}

	// Token: 0x0600015E RID: 350 RVA: 0x000092CC File Offset: 0x000074CC
	public static bool DeleteEquipCardPreset(int presetIndex)
	{
		SaveLoadManager.EquipCardPresetData equipCardPresetData;
		if (!SaveLoadManager.TryGetEquipCardPreset(presetIndex, out equipCardPresetData))
		{
			return false;
		}
		equipCardPresetData.presetName = SaveLoadManager.GetDefaultEquipCardPresetName(presetIndex - 1);
		equipCardPresetData.equipCards = new List<int>();
		equipCardPresetData.isSaved = false;
		if (SaveLoadManager.gameSaveData.currentEquipCardPresetIndex == presetIndex)
		{
			SaveLoadManager.gameSaveData.currentEquipCardPresetIndex = 0;
		}
		return true;
	}

	// Token: 0x0600015F RID: 351 RVA: 0x0000931E File Offset: 0x0000751E
	public static int GetCurrentEquipCardPresetIndex()
	{
		SaveLoadManager.EnsureEquipCardPresets();
		return SaveLoadManager.gameSaveData.currentEquipCardPresetIndex;
	}

	// Token: 0x06000160 RID: 352 RVA: 0x0000932F File Offset: 0x0000752F
	public static void SetCurrentEquipCardPresetIndex(int presetIndex)
	{
		if (presetIndex > 0 && SaveLoadManager.IsEquipCardPresetSaved(presetIndex))
		{
			SaveLoadManager.gameSaveData.currentEquipCardPresetIndex = presetIndex;
			return;
		}
		SaveLoadManager.gameSaveData.currentEquipCardPresetIndex = 0;
	}

	// Token: 0x06000161 RID: 353 RVA: 0x00009354 File Offset: 0x00007554
	public static void ClearEquipCards(bool refreshUI = true)
	{
		if (SaveLoadManager.haveCardDataDic == null)
		{
			SaveLoadManager.haveCardDataDic = new Dictionary<int, CardManager.HaveCardData>();
		}
		foreach (int cardId in SaveLoadManager.gameSaveData.equipCards)
		{
			SaveLoadManager.AddCardToWarehouse(cardId);
		}
		SaveLoadManager.gameSaveData.equipCards = new List<int>();
		SaveLoadManager.gameSaveData.currentEquipCardPresetIndex = 0;
		CardManager cardManager = EntityStatic.Get<CardManager>();
		if (cardManager == null)
		{
			return;
		}
		cardManager.RefreshEquipCardsFromSave(refreshUI);
	}

	// Token: 0x06000162 RID: 354 RVA: 0x000093E4 File Offset: 0x000075E4
	public static bool IsEquipCardPresetSaved(int presetIndex)
	{
		SaveLoadManager.EquipCardPresetData equipCardPresetData;
		return SaveLoadManager.TryGetEquipCardPreset(presetIndex, out equipCardPresetData) && equipCardPresetData.isSaved;
	}

	// Token: 0x06000163 RID: 355 RVA: 0x00009404 File Offset: 0x00007604
	public static bool LoadEquipCardPreset(int presetIndex, bool refreshUI = true)
	{
		List<int> list;
		return SaveLoadManager.LoadEquipCardPreset(presetIndex, refreshUI, out list);
	}

	// Token: 0x06000164 RID: 356 RVA: 0x0000941C File Offset: 0x0000761C
	public static bool LoadEquipCardPreset(int presetIndex, bool refreshUI, out List<int> skipCardIds)
	{
		skipCardIds = new List<int>();
		SaveLoadManager.EquipCardPresetData equipCardPresetData;
		if (!SaveLoadManager.TryGetEquipCardPreset(presetIndex, out equipCardPresetData))
		{
			return false;
		}
		if (SaveLoadManager.haveCardDataDic == null)
		{
			SaveLoadManager.haveCardDataDic = new Dictionary<int, CardManager.HaveCardData>();
		}
		foreach (int cardId in SaveLoadManager.gameSaveData.equipCards)
		{
			SaveLoadManager.AddCardToWarehouse(cardId);
		}
		List<int> list = new List<int>();
		if (equipCardPresetData.equipCards != null)
		{
			foreach (int num in equipCardPresetData.equipCards)
			{
				if (!Game.GameData.CardDataDic.ContainsKey(num) || !SaveLoadManager.TryTakeCardFromWarehouse(num))
				{
					skipCardIds.Add(num);
				}
				else
				{
					list.Add(num);
				}
			}
		}
		SaveLoadManager.gameSaveData.equipCards = list;
		SaveLoadManager.gameSaveData.currentEquipCardPresetIndex = presetIndex;
		CardManager cardManager = EntityStatic.Get<CardManager>();
		if (cardManager != null)
		{
			cardManager.RefreshEquipCardsFromSave(refreshUI);
		}
		return true;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00009530 File Offset: 0x00007730
	private static bool TryGetEquipCardPreset(int presetIndex, out SaveLoadManager.EquipCardPresetData presetData)
	{
		presetData = null;
		SaveLoadManager.EnsureEquipCardPresets();
		int num = SaveLoadManager.GetEquipCardPresetCount();
		if (presetIndex < 1 || presetIndex > num || SaveLoadManager.gameSaveData.equipCardPresets == null)
		{
			return false;
		}
		presetData = SaveLoadManager.gameSaveData.equipCardPresets[presetIndex - 1];
		return presetData != null;
	}

	// Token: 0x06000166 RID: 358 RVA: 0x0000957A File Offset: 0x0000777A
	private static SaveLoadManager.EquipCardPresetData CreateDefaultEquipCardPreset(int index)
	{
		return new SaveLoadManager.EquipCardPresetData
		{
			presetName = SaveLoadManager.GetDefaultEquipCardPresetName(index),
			equipCards = new List<int>(),
			isSaved = false
		};
	}

	// Token: 0x06000167 RID: 359 RVA: 0x000095A0 File Offset: 0x000077A0
	private static string GetDefaultEquipCardPresetName(int index)
	{
		return (index + 1).ToString();
	}

	// Token: 0x06000168 RID: 360 RVA: 0x000095B8 File Offset: 0x000077B8
	private static void AddCardToWarehouse(int cardId)
	{
		if (SaveLoadManager.haveCardDataDic == null)
		{
			SaveLoadManager.haveCardDataDic = new Dictionary<int, CardManager.HaveCardData>();
		}
		CardManager.HaveCardData haveCardData;
		if (SaveLoadManager.haveCardDataDic.TryGetValue(cardId, out haveCardData))
		{
			haveCardData.haveNum++;
			if (haveCardData.haveNum > 999)
			{
				haveCardData.haveNum = 999;
			}
			SaveLoadManager.haveCardDataDic[cardId] = haveCardData;
			return;
		}
		CardManager.HaveCardData value = new CardManager.HaveCardData
		{
			cardId = cardId,
			haveNum = 1
		};
		SaveLoadManager.haveCardDataDic.Add(cardId, value);
	}

	// Token: 0x06000169 RID: 361 RVA: 0x00009640 File Offset: 0x00007840
	private static bool TryTakeCardFromWarehouse(int cardId)
	{
		CardManager.HaveCardData haveCardData;
		if (SaveLoadManager.haveCardDataDic == null || !SaveLoadManager.haveCardDataDic.TryGetValue(cardId, out haveCardData) || haveCardData.haveNum <= 0)
		{
			return false;
		}
		haveCardData.haveNum--;
		SaveLoadManager.haveCardDataDic[cardId] = haveCardData;
		return true;
	}

	// Token: 0x0600016A RID: 362 RVA: 0x00009687 File Offset: 0x00007887
	public static void SetJiyi(long memoryValue)
	{
		SaveLoadManager.gameSaveData.memory = Math.Max(0L, memoryValue);
	}

	// Token: 0x0600016B RID: 363 RVA: 0x0000969B File Offset: 0x0000789B
	public static void SetMaxPower(int power)
	{
		SaveLoadManager.gameSaveData.maxCapacity = power;
	}

	// Token: 0x0600016C RID: 364 RVA: 0x000096A8 File Offset: 0x000078A8
	public static void SaveJiYi(long add)
	{
		SaveLoadManager.gameSaveData.memory = SaveLoadManager.AddNonNegativeLong(SaveLoadManager.gameSaveData.memory, add);
	}

	// Token: 0x0600016D RID: 365 RVA: 0x000096C4 File Offset: 0x000078C4
	private static long AddNonNegativeLong(long value, long add)
	{
		value = Math.Max(0L, value);
		if (add > 0L && value > 9223372036854775807L - add)
		{
			return long.MaxValue;
		}
		if (add < 0L && value < -add)
		{
			return 0L;
		}
		long val = value + add;
		return Math.Max(0L, val);
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00009711 File Offset: 0x00007911
	private static void NormalizeGameSaveDataAmounts()
	{
		if (SaveLoadManager.gameSaveData == null)
		{
			return;
		}
		SaveLoadManager.gameSaveData.memory = Math.Max(0L, SaveLoadManager.gameSaveData.memory);
		SaveLoadManager.gameSaveData.cardDust = Math.Max(0L, SaveLoadManager.gameSaveData.cardDust);
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00009754 File Offset: 0x00007954
	public void Save(string saveKey, object saveData)
	{
		if (saveData == null)
		{
			return;
		}
		string value = JsonConvert.SerializeObject(saveData);
		PlayerPrefs.SetString(saveKey, value);
		PlayerPrefs.Save();
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00009778 File Offset: 0x00007978
	public void Save(string saveKey, string saveData)
	{
		if (string.IsNullOrEmpty(saveData))
		{
			return;
		}
		PlayerPrefs.SetString(saveKey, saveData);
		PlayerPrefs.Save();
	}

	// Token: 0x06000171 RID: 369 RVA: 0x0000978F File Offset: 0x0000798F
	public void SaveInt(string saveKey, int saveData)
	{
		PlayerPrefs.SetInt(saveKey, saveData);
		PlayerPrefs.Save();
	}

	// Token: 0x06000172 RID: 370 RVA: 0x0000979D File Offset: 0x0000799D
	public void SaveFloat(string saveKey, float saveData)
	{
		PlayerPrefs.SetFloat(saveKey, saveData);
		PlayerPrefs.Save();
	}

	// Token: 0x06000173 RID: 371 RVA: 0x000097AC File Offset: 0x000079AC
	public T Load<T>(string saveKey)
	{
		if (!PlayerPrefs.HasKey(saveKey))
		{
			return default(T);
		}
		if (typeof(T) == typeof(string))
		{
			return (T)((object)PlayerPrefs.GetString(saveKey));
		}
		return JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(saveKey));
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00009800 File Offset: 0x00007A00
	public T Load<T>(string saveKey, string defaultValue)
	{
		if (!PlayerPrefs.HasKey(saveKey))
		{
			return default(T);
		}
		if (typeof(T) == typeof(string))
		{
			return (T)((object)PlayerPrefs.GetString(saveKey, defaultValue));
		}
		return JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(saveKey));
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00009852 File Offset: 0x00007A52
	public int LoadInt(string saveKey)
	{
		if (PlayerPrefs.HasKey(saveKey))
		{
			return PlayerPrefs.GetInt(saveKey);
		}
		Debug.LogError("没有找到存档数据");
		return 0;
	}

	// Token: 0x06000176 RID: 374 RVA: 0x0000986E File Offset: 0x00007A6E
	public float LoadFloat(string saveKey)
	{
		if (PlayerPrefs.HasKey(saveKey))
		{
			return PlayerPrefs.GetFloat(saveKey);
		}
		Debug.LogError("没有找到存档数据");
		return 0f;
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0000988E File Offset: 0x00007A8E
	public bool Check(string saveKey)
	{
		return PlayerPrefs.HasKey(saveKey);
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00009896 File Offset: 0x00007A96
	public void ClearAll()
	{
		PlayerPrefs.DeleteAll();
	}

	// Token: 0x06000179 RID: 377 RVA: 0x000098A0 File Offset: 0x00007AA0
	public void SavePlayerKingData(SaveLoadManager.PlayerKingData newPlayerKingData)
	{
		if (SaveLoadManager.playerKingSave.playerKingDataList == null || SaveLoadManager.playerKingSave.playerKingDataList.Count == 0)
		{
			SaveLoadManager.playerKingSave.playerKingDataList = new List<SaveLoadManager.PlayerKingData>();
		}
		SaveLoadManager.playerKingSave.playerKingDataList.Add(newPlayerKingData);
		string contents = JsonUtility.ToJson(SaveLoadManager.playerKingSave, true);
		File.WriteAllText(SaveLoadManager.PlayerKingPath, contents);
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00009908 File Offset: 0x00007B08
	public void ClearPlayerKingData()
	{
		SaveLoadManager.playerKingSave.playerKingDataList = new List<SaveLoadManager.PlayerKingData>();
		string contents = JsonUtility.ToJson(SaveLoadManager.playerKingSave, true);
		File.WriteAllText(SaveLoadManager.PlayerKingPath, contents);
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00009940 File Offset: 0x00007B40
	public static void InitPlayerKingData()
	{
		if (File.Exists(SaveLoadManager.PlayerKingPath))
		{
			SaveLoadManager.playerKingSave = JsonUtility.FromJson<SaveLoadManager.PlayerKingSave>(File.ReadAllText(SaveLoadManager.PlayerKingPath));
		}
	}

	// Token: 0x04000170 RID: 368
	private const int DefaultEquipCardPresetCount = 20;

	// Token: 0x04000171 RID: 369
	private static int equipCardPresetCount = 20;

	// Token: 0x04000172 RID: 370
	public const string IsAutoUseCard = "IsAutoUseCard";

	// Token: 0x04000173 RID: 371
	public const string IsAutoBattle = "IsAutoBattle";

	// Token: 0x04000174 RID: 372
	public const string IsShowDamage = "IsShowDamage";

	// Token: 0x04000175 RID: 373
	public const string IsKeyPickTalisman = "IsKeyPickTalisman";

	// Token: 0x04000176 RID: 374
	public const string IsSmartCasting = "IsSmartCasting";

	// Token: 0x04000177 RID: 375
	public const string IsPickShare = "IsPickShare";

	// Token: 0x04000178 RID: 376
	public const string PlayerNameDisplayMode = "PlayerNameDisplayMode";

	// Token: 0x04000179 RID: 377
	public static Dictionary<int, CardManager.HaveCardData> haveCardDataDic;

	// Token: 0x0400017A RID: 378
	public static SaveLoadManager.PlayerKingSave playerKingSave;

	// Token: 0x0400017B RID: 379
	public static List<SaveLoadManager.TeamBuildData> teamBuildDataList;

	// Token: 0x0400017C RID: 380
	public static SaveLoadManager.GameSaveData gameSaveData;

	// Token: 0x0400017D RID: 381
	private static string path = Application.persistentDataPath + "/save.json";

	// Token: 0x0400017E RID: 382
	private static string PlayerKingPath = Application.persistentDataPath + "/PlayerKing.json";

	// Token: 0x0400017F RID: 383
	private static bool isInit = false;

	// Token: 0x0200004D RID: 77
	public enum LevelMask
	{
		// Token: 0x04000181 RID: 385
		GoblinKingdom = 1,
		// Token: 0x04000182 RID: 386
		GoblinKingdom_Hard
	}

	// Token: 0x0200004E RID: 78
	[Serializable]
	public class GameSaveData
	{
		// Token: 0x04000183 RID: 387
		public long memory;

		// Token: 0x04000184 RID: 388
		public long cardDust;

		// Token: 0x04000185 RID: 389
		public int maxCapacity;

		// Token: 0x04000186 RID: 390
		public List<int> equipCards;

		// Token: 0x04000187 RID: 391
		public List<SaveLoadManager.EquipCardPresetData> equipCardPresets;

		// Token: 0x04000188 RID: 392
		public int currentEquipCardPresetIndex;

		// Token: 0x04000189 RID: 393
		public List<CardManager.HaveCardData> haveCardDataDic;

		// Token: 0x0400018A RID: 394
		public int levelMaskComplete;

		// Token: 0x0400018B RID: 395
		public List<string> guideSkillKeys;
	}

	// Token: 0x0200004F RID: 79
	[Serializable]
	public class EquipCardPresetData
	{
		// Token: 0x0400018C RID: 396
		public string presetName;

		// Token: 0x0400018D RID: 397
		public List<int> equipCards;

		// Token: 0x0400018E RID: 398
		public bool isSaved;
	}

	// Token: 0x02000050 RID: 80
	[Serializable]
	public struct PlayerKingSave
	{
		// Token: 0x0400018F RID: 399
		public List<SaveLoadManager.PlayerKingData> playerKingDataList;
	}

	// Token: 0x02000051 RID: 81
	[Serializable]
	public class TeamBuildData
	{
		// Token: 0x04000190 RID: 400
		public List<SaveLoadManager.PlayerKingData> members = new List<SaveLoadManager.PlayerKingData>();

		// Token: 0x04000191 RID: 401
		public string teamMessage = "我们是冠军！";

		// Token: 0x04000192 RID: 402
		public int rank;

		// Token: 0x04000193 RID: 403
		public int order;

		// Token: 0x04000194 RID: 404
		public int steamGlobalRank;

		// Token: 0x04000195 RID: 405
		public ulong leaderboardSteamID;

		// Token: 0x04000196 RID: 406
		public bool isBuildDataIncomplete;

		// Token: 0x04000197 RID: 407
		public long challengeTimestamp;

		// Token: 0x04000198 RID: 408
		[NonSerialized]
		public bool isLegacyOrder;
	}

	// Token: 0x02000052 RID: 82
	[Serializable]
	public struct PlayerKingData
	{
		// Token: 0x04000199 RID: 409
		public string kingName;

		// Token: 0x0400019A RID: 410
		public ulong steamID;

		// Token: 0x0400019B RID: 411
		public HeroType heroType;

		// Token: 0x0400019C RID: 412
		public SaveLoadManager.PlayerKingSkillData[] skill;

		// Token: 0x0400019D RID: 413
		public SaveLoadManager.PlayerKingEquipData[] equip;

		// Token: 0x0400019E RID: 414
		public SaveLoadManager.PlayerKingRelicData[] relic;

		// Token: 0x0400019F RID: 415
		public int[] card;

		// Token: 0x040001A0 RID: 416
		public int level;

		// Token: 0x040001A1 RID: 417
		public float allDamage;

		// Token: 0x040001A2 RID: 418
		public int allMoney;

		// Token: 0x040001A3 RID: 419
		public int allGem;

		// Token: 0x040001A4 RID: 420
		public long maxHp;

		// Token: 0x040001A5 RID: 421
		public int maxMp;

		// Token: 0x040001A6 RID: 422
		public int str;

		// Token: 0x040001A7 RID: 423
		public int agi;

		// Token: 0x040001A8 RID: 424
		public int sta;

		// Token: 0x040001A9 RID: 425
		public int armor;

		// Token: 0x040001AA RID: 426
		public int dodge;

		// Token: 0x040001AB RID: 427
		public int skillReduction;

		// Token: 0x040001AC RID: 428
		public float moveSpeed;

		// Token: 0x040001AD RID: 429
		public int lucky;

		// Token: 0x040001AE RID: 430
		public int hpAdd;

		// Token: 0x040001AF RID: 431
		public int mpAdd;

		// Token: 0x040001B0 RID: 432
		public float hpSecRate;

		// Token: 0x040001B1 RID: 433
		public float attackAddHp;

		// Token: 0x040001B2 RID: 434
		public float lifeStealing;

		// Token: 0x040001B3 RID: 435
		public float magicXiXue;

		// Token: 0x040001B4 RID: 436
		public int attack;

		// Token: 0x040001B5 RID: 437
		public float attackSpeed;

		// Token: 0x040001B6 RID: 438
		public float critical;

		// Token: 0x040001B7 RID: 439
		public float criticalDamage;

		// Token: 0x040001B8 RID: 440
		public float normalDamage;

		// Token: 0x040001B9 RID: 441
		public float normalBreak;

		// Token: 0x040001BA RID: 442
		public float skillDamage;

		// Token: 0x040001BB RID: 443
		public float skillBreak;

		// Token: 0x040001BC RID: 444
		public int skillCd;

		// Token: 0x040001BD RID: 445
		public float skillRange;

		// Token: 0x040001BE RID: 446
		public float skillTime;

		// Token: 0x040001BF RID: 447
		public float skillExpend;

		// Token: 0x040001C0 RID: 448
		public int reduceInjury;

		// Token: 0x040001C1 RID: 449
		public int extraDamage;

		// Token: 0x040001C2 RID: 450
		public float attackDistance;

		// Token: 0x040001C3 RID: 451
		public float castSpeed;

		// Token: 0x040001C4 RID: 452
		public float skillNoneDamage;

		// Token: 0x040001C5 RID: 453
		public float fireDamage;

		// Token: 0x040001C6 RID: 454
		public float iceDamage;

		// Token: 0x040001C7 RID: 455
		public float lightDamage;

		// Token: 0x040001C8 RID: 456
		public float effectDamage;

		// Token: 0x040001C9 RID: 457
		public float hpAddUpgrade;

		// Token: 0x040001CA RID: 458
		public float buffDamage;

		// Token: 0x040001CB RID: 459
		public float haloRangeAdd;

		// Token: 0x040001CC RID: 460
		public float addCallMonsterAttack;

		// Token: 0x040001CD RID: 461
		public float addCallMonsterHp;

		// Token: 0x040001CE RID: 462
		public float addCallMonsterSize;

		// Token: 0x040001CF RID: 463
		public float addCallMonsterTime;

		// Token: 0x040001D0 RID: 464
		public float addHenshin;

		// Token: 0x040001D1 RID: 465
		public float addHenshinTime;

		// Token: 0x040001D2 RID: 466
		public float armedAdd;

		// Token: 0x040001D3 RID: 467
		public float equipAdd;

		// Token: 0x040001D4 RID: 468
		public int relifeTime;

		// Token: 0x040001D5 RID: 469
		public float addHatred;

		// Token: 0x040001D6 RID: 470
		public float forgeAdd;
	}

	// Token: 0x02000053 RID: 83
	[Serializable]
	public struct PlayerKingSkillData
	{
		// Token: 0x040001D7 RID: 471
		public string skillName;

		// Token: 0x040001D8 RID: 472
		public int skillData;
	}

	// Token: 0x02000054 RID: 84
	[Serializable]
	public struct PlayerKingEquipData
	{
		// Token: 0x040001D9 RID: 473
		public string equip;

		// Token: 0x040001DA RID: 474
		public int equipData;

		// Token: 0x040001DB RID: 475
		public string[] equipEvolutionSkill;
	}

	// Token: 0x02000055 RID: 85
	[Serializable]
	public struct PlayerKingRelicData
	{
		// Token: 0x040001DC RID: 476
		public string relicName;

		// Token: 0x040001DD RID: 477
		public int relicLevel;
	}
}
