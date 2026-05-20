using System;
using System.Collections.Generic;

// Token: 0x02000133 RID: 307
public class GameDataManager
{
	// Token: 0x0600061B RID: 1563 RVA: 0x00023878 File Offset: 0x00021A78
	public GameDataManager()
	{
		foreach (KeyValuePair<string, object> keyValuePair in ((Dictionary<string, object>)ExcelManager.allExcelData["equipment"]))
		{
			string key = keyValuePair.Value.DIC("id");
			this.EquipAttributeDataDic.Add(key, EquipBase.CreateEquipAttributeDataList(keyValuePair.Value));
		}
		foreach (KeyValuePair<string, object> keyValuePair2 in ((Dictionary<string, object>)ExcelManager.allExcelData["hero"]))
		{
			string key2 = keyValuePair2.Value.DIC("id");
			RoleAttribute value = default(RoleAttribute);
			value.hp = keyValuePair2.Value.DIC("hp");
			value.mp = keyValuePair2.Value.DIC("mp");
			value.attackPower = keyValuePair2.Value.DIC("attack");
			value.STA = keyValuePair2.Value.DIC("STA");
			value.STAadd = keyValuePair2.Value.DIC("STAadd");
			value.AGI = keyValuePair2.Value.DIC("AGI");
			value.AGIadd = keyValuePair2.Value.DIC("AGIadd");
			value.STR = keyValuePair2.Value.DIC("STR");
			value.STRadd = keyValuePair2.Value.DIC("STRadd");
			value.armor = keyValuePair2.Value.DIC("Armor");
			value.moveSpeed = keyValuePair2.Value.DIC("moveSpeed");
			value.attackSpeed = keyValuePair2.Value.DIC("attackSpeed");
			value.hpRecover = keyValuePair2.Value.DIC("hpRecover");
			value.mpRecover = keyValuePair2.Value.DIC("mpRecover");
			value.roleName = keyValuePair2.Value.DIC("name");
			value.id = keyValuePair2.Value.DIC("id");
			value.isSave = keyValuePair2.Value.DIC("save");
			value.isSaveMode = keyValuePair2.Value.DIC("saveMode");
			this.HeroAttributeDic.Add(key2, value);
		}
		foreach (KeyValuePair<string, object> keyValuePair3 in ((Dictionary<string, object>)ExcelManager.allExcelData["activeSkill"]))
		{
			int key3 = keyValuePair3.Value.DIC("id");
			ActiveSkillData activeSkillData = default(ActiveSkillData);
			activeSkillData.name = keyValuePair3.Value.DIC("name");
			activeSkillData.id = keyValuePair3.Value.DIC("id");
			activeSkillData.info = keyValuePair3.Value.DIC("info");
			activeSkillData.quality = keyValuePair3.Value.DIC("quality");
			activeSkillData.info = keyValuePair3.Value.DIC("info");
			activeSkillData.cost = keyValuePair3.Value.DIC("cost");
			activeSkillData.cd = keyValuePair3.Value.DIC("cd");
			activeSkillData.indicator = keyValuePair3.Value.DIC("indicator");
			activeSkillData.range = keyValuePair3.Value.DIC("range");
			activeSkillData.castingRange = keyValuePair3.Value.DIC("castingRange");
			activeSkillData.icon = keyValuePair3.Value.DIC("icon");
			activeSkillData.damageBase = keyValuePair3.Value.DIC("damageBase");
			activeSkillData.damageType = keyValuePair3.Value.DIC("damageType");
			float num;
			activeSkillData.damageValue = (float.TryParse(keyValuePair3.Value.DIC("damageValue"), out num) ? num : 0f);
			float num2;
			activeSkillData.interval = (float.TryParse(keyValuePair3.Value.DIC("interval"), out num2) ? num2 : 0f);
			float num3;
			activeSkillData.duration = (float.TryParse(keyValuePair3.Value.DIC("duration"), out num3) ? num3 : 0f);
			activeSkillData.attribute = GameDataManager.GetSkillAttribute(keyValuePair3.Value.DIC("attribute"));
			activeSkillData.isSaveMode = keyValuePair3.Value.DIC("saveMode");
			activeSkillData.canAuto = keyValuePair3.Value.DIC("auto");
			activeSkillData.total = keyValuePair3.Value.DIC("total");
			activeSkillData.chargingNum = keyValuePair3.Value.DIC("chargingNum");
			activeSkillData.chargingCd = (float)keyValuePair3.Value.DIC("chargingCd");
			string text = keyValuePair3.Value.DIC("damageExValue");
			if (!string.IsNullOrEmpty(text))
			{
				string[] array = text.Split('|', StringSplitOptions.None);
				activeSkillData.damageExStr = array;
				activeSkillData.damageExValue = new float[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					activeSkillData.damageExValue[i] = float.Parse(array[i]);
				}
			}
			this.ActiveSkillDataDic.Add((ActiveSkillEnum)key3, activeSkillData);
		}
		foreach (KeyValuePair<string, object> keyValuePair4 in ((Dictionary<string, object>)ExcelManager.allExcelData["item"]))
		{
			int key4 = keyValuePair4.Value.DIC("id");
			ItemData value2 = default(ItemData);
			value2.name = "pickitem_" + keyValuePair4.Value.DIC("id");
			value2.quality = DropDefine.QualityAry.IndexOf(keyValuePair4.Value.DIC("quality"));
			value2.model = keyValuePair4.Value.DIC("model");
			this.ItemDataDic.Add((ItemType)key4, value2);
		}
		foreach (KeyValuePair<string, object> keyValuePair5 in ((Dictionary<string, object>)ExcelManager.allExcelData["remains"]))
		{
			int key5 = keyValuePair5.Value.DIC("id");
			RemainsData remainsData = default(RemainsData);
			remainsData.grade = DropDefine.QualityAry.IndexOf(keyValuePair5.Value.DIC("grade"));
			string value3 = keyValuePair5.Value.DIC("condition");
			if (string.IsNullOrEmpty(value3))
			{
				remainsData.conditions = EntryConditions.None;
			}
			else
			{
				remainsData.conditions = (EntryConditions)Enum.Parse(typeof(EntryConditions), value3);
			}
			this.RemainsDataDic.Add((ItemType)key5, remainsData);
			ItemData value4 = default(ItemData);
			value4.name = "pickitem_" + keyValuePair5.Value.DIC("id");
			value4.quality = remainsData.grade;
			value4.model = "Remains";
			this.ItemDataDic.Add((ItemType)key5, value4);
		}
		foreach (KeyValuePair<string, object> keyValuePair6 in ((Dictionary<string, object>)ExcelManager.allExcelData["enemyEntries"]))
		{
			EnemyEntriesData enemyEntriesData = default(EnemyEntriesData);
			enemyEntriesData.enemyEntriesType = (EnemyEntriesType)keyValuePair6.Value.DIC("id");
			enemyEntriesData.level = keyValuePair6.Value.DIC("level");
			enemyEntriesData.bossLevel = keyValuePair6.Value.DIC("bossLevel");
			enemyEntriesData.name = keyValuePair6.Value.DIC("name");
			enemyEntriesData.skin = keyValuePair6.Value.DIC("skin");
			this.EnemyEntriesDic.Add(enemyEntriesData.enemyEntriesType, enemyEntriesData);
		}
		foreach (KeyValuePair<string, object> keyValuePair7 in ((Dictionary<string, object>)ExcelManager.allExcelData["forging"]))
		{
			ForgingData forgingData = default(ForgingData);
			string type = keyValuePair7.Value.DIC("type");
			forgingData.id = keyValuePair7.Value.DIC("id");
			forgingData.quality = DropDefine.QualityAry.IndexOf(keyValuePair7.Value.DIC("grade"));
			forgingData.icon = keyValuePair7.Value.DIC("icon");
			forgingData.minValue = keyValuePair7.Value.DIC("minValue");
			forgingData.maxValue = keyValuePair7.Value.DIC("maxValue");
			forgingData.isPercent = (keyValuePair7.Value.DIC("isPercent") == 1);
			forgingData.isFloat = (keyValuePair7.Value.DIC().ContainsKey("isFloat") && keyValuePair7.Value.DIC("isFloat") == 1);
			forgingData.type = type;
			forgingData.goldLevel = keyValuePair7.Value.DIC("goldLevel");
			this.ForgingDataDic.Add(forgingData.id, forgingData);
		}
		foreach (KeyValuePair<string, object> keyValuePair8 in ((Dictionary<string, object>)ExcelManager.allExcelData["contract"]))
		{
			ContractData contractData = default(ContractData);
			string type2 = keyValuePair8.Value.DIC("type");
			contractData.id = keyValuePair8.Value.DIC("id");
			contractData.quality = DropDefine.QualityAry.IndexOf(keyValuePair8.Value.DIC("grade"));
			contractData.icon = keyValuePair8.Value.DIC("icon");
			contractData.minValue = keyValuePair8.Value.DIC("minValue");
			contractData.maxValue = keyValuePair8.Value.DIC("maxValue");
			contractData.isPercent = (keyValuePair8.Value.DIC("isPercent") == 1);
			contractData.type = type2;
			contractData.waveUpLevel = keyValuePair8.Value.DIC("waveUpLevel");
			Dictionary<string, object> dictionary = keyValuePair8.Value.DIC();
			if (dictionary.ContainsKey("limit"))
			{
				object obj = dictionary["limit"];
				if (!string.IsNullOrEmpty((obj != null) ? obj.ToString() : null))
				{
					contractData.limit = dictionary["limit"].ToInt32();
					contractData.hasLimit = true;
				}
			}
			this.ContractDataDic.Add(contractData.id, contractData);
		}
		this.cardTotalManager = new CardTotalManager();
		this.cardTotalManager.Init();
		foreach (KeyValuePair<string, object> keyValuePair9 in ((Dictionary<string, object>)ExcelManager.allExcelData["cards"]))
		{
			if (!keyValuePair9.Value.DIC("lock"))
			{
				CardData cardData = default(CardData);
				cardData.id = keyValuePair9.Value.DIC("id");
				cardData.quality = DropDefine.QualityAry.IndexOf(keyValuePair9.Value.DIC("quality"));
				cardData.icon = keyValuePair9.Value.DIC("icon");
				cardData.capacity = keyValuePair9.Value.DIC("capacity");
				cardData.limit = keyValuePair9.Value.DIC("limit");
				string value5 = keyValuePair9.Value.DIC("passsiveSkill");
				if (string.IsNullOrEmpty(value5))
				{
					cardData.cardSkill = CardSkillType.None;
				}
				else
				{
					cardData.cardSkill = (CardSkillType)Enum.Parse(typeof(CardSkillType), value5);
				}
				cardData.isTeam = keyValuePair9.Value.DIC("isTeam");
				cardData.progress = keyValuePair9.Value.DIC("progress");
				cardData.kingLock = keyValuePair9.Value.DIC("kingLock");
				cardData.unlockType = (UnlockType)Enum.Parse(typeof(UnlockType), keyValuePair9.Value.DIC("unlockType"));
				cardData.unlockData = keyValuePair9.Value.DIC("unlockData");
				cardData.unlockValue = keyValuePair9.Value.DIC("unlockValue");
				cardData.dustBreakLevel = keyValuePair9.Value.DIC("dustBreakLevel");
				if (cardData.unlockType == UnlockType.Drop)
				{
					string key6 = ((Dictionary<string, object>)ExcelManager.allExcelData["enemy"].DIC(cardData.unlockData)).DIC("model");
					if (!this.ExDropDataDic.ContainsKey(key6))
					{
						ExDropData value6 = default(ExDropData);
						value6.exDropChance = new List<ExDropChance>();
						this.ExDropDataDic.Add(key6, value6);
					}
					ExDropData exDropData = this.ExDropDataDic[key6];
					exDropData.allChance += cardData.unlockValue;
					ExDropChance item = default(ExDropChance);
					item.dropChance = cardData.unlockValue;
					item.dropCardId = cardData.id;
					this.ExDropDataDic[key6] = exDropData;
					exDropData.exDropChance.Add(item);
				}
				else if (cardData.unlockType == UnlockType.Total)
				{
					this.cardTotalManager.AddTotalEvent(cardData.id, cardData.unlockData);
				}
				cardData.entries = new CardEntries
				{
					critical = keyValuePair9.Value.DIC("critical"),
					criticalDamage = keyValuePair9.Value.DIC("criticalDamage"),
					attack = keyValuePair9.Value.DIC("attack"),
					attackSpeed = keyValuePair9.Value.DIC("attackSpeed"),
					attackAddHp = keyValuePair9.Value.DIC("attackAddHp"),
					moveSpeed = keyValuePair9.Value.DIC("moveSpeed"),
					sta = keyValuePair9.Value.DIC("sta"),
					agi = keyValuePair9.Value.DIC("agi"),
					str = keyValuePair9.Value.DIC("str"),
					armor = keyValuePair9.Value.DIC("armor"),
					hpAdd = keyValuePair9.Value.DIC("hpAdd"),
					mpAdd = keyValuePair9.Value.DIC("mpAdd"),
					startMoney = keyValuePair9.Value.DIC("startMoney"),
					startGem = keyValuePair9.Value.DIC("startGem"),
					lucky = keyValuePair9.Value.DIC("lucky"),
					skillDamage = keyValuePair9.Value.DIC("skillDamage"),
					skillRange = keyValuePair9.Value.DIC("skillRange"),
					skillTime = keyValuePair9.Value.DIC("skillTime"),
					skillExpend = keyValuePair9.Value.DIC("skillExpend"),
					skillCd = keyValuePair9.Value.DIC("skillCd"),
					expAdd = keyValuePair9.Value.DIC("expAdd"),
					normalDamage = keyValuePair9.Value.DIC("normalDamage"),
					maxHp = keyValuePair9.Value.DIC("maxHp"),
					maxMp = keyValuePair9.Value.DIC("maxMp"),
					normalBreak = keyValuePair9.Value.DIC("normalBreak"),
					skillBreak = keyValuePair9.Value.DIC("skillBreak"),
					allDamage = keyValuePair9.Value.DIC("allDamage"),
					addMoney = keyValuePair9.Value.DIC("addMoney"),
					addEnemyLimit = keyValuePair9.Value.DIC("addEnemyLimit"),
					refreshNum = keyValuePair9.Value.DIC("refreshNum"),
					lifeStealing = keyValuePair9.Value.DIC("lifeStealing"),
					reduceInjury = keyValuePair9.Value.DIC("reduceInjury"),
					extraDamage = keyValuePair9.Value.DIC("extraDamage"),
					dodge = keyValuePair9.Value.DIC("dodge"),
					hpPercent = keyValuePair9.Value.DIC("hpPercent"),
					hpSecRate = keyValuePair9.Value.DIC("hpSecRate"),
					skillReduction = keyValuePair9.Value.DIC("skillReduction"),
					strPercent = keyValuePair9.Value.DIC("strPercent"),
					agiPercent = keyValuePair9.Value.DIC("agiPercent"),
					staPercent = keyValuePair9.Value.DIC("staPercent"),
					attackDistance = keyValuePair9.Value.DIC("attackDistance"),
					fireDamage = keyValuePair9.Value.DIC("fireDamage"),
					iceDamage = keyValuePair9.Value.DIC("iceDamage"),
					lightDamage = keyValuePair9.Value.DIC("lightDamage"),
					relicAdd = keyValuePair9.Value.DIC("relicAdd"),
					bookAdd = keyValuePair9.Value.DIC("bookAdd"),
					forgingAdd = keyValuePair9.Value.DIC("forgingAdd"),
					effectDamage = keyValuePair9.Value.DIC("effectDamage"),
					buffDamage = keyValuePair9.Value.DIC("buffDamage"),
					relifeTime = keyValuePair9.Value.DIC("relifeTime"),
					addCall = keyValuePair9.Value.DIC("addCall"),
					addHenshin = keyValuePair9.Value.DIC("addHenshin"),
					addNormalEnemy = keyValuePair9.Value.DIC("addNormalEnemy"),
					addBossEnemy = keyValuePair9.Value.DIC("addBossEnemy"),
					attackPercent = keyValuePair9.Value.DIC("attackPercent"),
					forgingAddValue = keyValuePair9.Value.DIC("forgingAddValue"),
					equipAddValue = keyValuePair9.Value.DIC("equipAddValue"),
					hpAddUpgrade = keyValuePair9.Value.DIC("hpAddUpgrade"),
					armedAdd = keyValuePair9.Value.DIC("armedAdd"),
					castSpeed = keyValuePair9.Value.DIC("castSpeed")
				};
				this.CardDataDic.Add(cardData.id, cardData);
				ItemType key7 = cardData.id + ItemType.Card_0;
				ItemData value7 = default(ItemData);
				value7.name = "card_" + cardData.id.ToString();
				value7.quality = cardData.quality;
				value7.model = "Card";
				this.ItemDataDic.Add(key7, value7);
			}
		}
		foreach (KeyValuePair<string, object> keyValuePair10 in ((Dictionary<string, object>)ExcelManager.allExcelData["drop"]))
		{
			DropData dropData = default(DropData);
			string id = keyValuePair10.Value.DIC("id");
			dropData.id = id;
			string[] array2 = keyValuePair10.Value.DIC("itemList").Split('|', StringSplitOptions.None);
			int num4 = array2.Length / 2;
			dropData.dropItems = new DropItemData[num4];
			for (int j = 0; j < num4; j++)
			{
				DropItemData dropItemData = default(DropItemData);
				dropItemData.startItem = int.Parse(array2[j * 2]);
				dropItemData.endItem = int.Parse(array2[j * 2 + 1]);
				dropData.dropItems[j] = dropItemData;
			}
			string[] array3 = keyValuePair10.Value.DIC("dropWeight").Split('|', StringSplitOptions.None);
			int num5 = array3.Length;
			dropData.dropWeight = new float[num5];
			for (int k = 0; k < num5; k++)
			{
				dropData.dropWeight[k] = float.Parse(array3[k]);
			}
			MaxMinData dropNum = default(MaxMinData);
			string[] array4 = keyValuePair10.Value.DIC("dropNum").Split('|', StringSplitOptions.None);
			dropNum.min = int.Parse(array4[0]);
			dropNum.max = int.Parse(array4[1]);
			dropData.dropNum = dropNum;
			this.DropDataDic.Add(dropData.id, dropData);
		}
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x00025004 File Offset: 0x00023204
	public static SkillAttribute GetSkillAttribute(string skillType)
	{
		int num = ConstDefine.SkillAttributeStr.Length;
		for (int i = 0; i < num; i++)
		{
			if (skillType.Equals(ConstDefine.SkillAttributeStr[i]))
			{
				return (SkillAttribute)i;
			}
		}
		return SkillAttribute.None;
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x00025038 File Offset: 0x00023238
	public void InitEnemySpawn()
	{
		Dictionary<string, object> dictionary = (Dictionary<string, object>)ExcelManager.allExcelData["spawnEnemy" + GameHelperClient.MapLevel.ToString()];
		GameHelperClient.spawnConfig.enemySpawnData = new SOSpawnConfig.EnemySpawnTime[dictionary.Count];
		int num = 0;
		foreach (KeyValuePair<string, object> keyValuePair in dictionary)
		{
			SOSpawnConfig.EnemySpawnTime enemySpawnTime = new SOSpawnConfig.EnemySpawnTime();
			enemySpawnTime.spawnTime = keyValuePair.Value.DIC("spawnTime");
			string[] array = keyValuePair.Value.DIC("maxEnemyNum").Split('|', StringSplitOptions.None);
			enemySpawnTime.newMaxEnemyNum = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				int num2;
				if (int.TryParse(array[i], out num2))
				{
					enemySpawnTime.newMaxEnemyNum[i] = num2;
				}
			}
			enemySpawnTime.newCreateNum = keyValuePair.Value.DIC("createNum");
			string[] array2 = keyValuePair.Value.DIC("enemyType").Split(',', StringSplitOptions.None);
			enemySpawnTime.enemyType = new EnemyType[array2.Length];
			for (int j = 0; j < array2.Length; j++)
			{
				EnemyType enemyType;
				if (Enum.TryParse<EnemyType>(array2[j], out enemyType))
				{
					enemySpawnTime.enemyType[j] = enemyType;
				}
			}
			enemySpawnTime.attackLevel = keyValuePair.Value.DIC("attackLevel");
			enemySpawnTime.hpLevel = keyValuePair.Value.DIC("hpLevel");
			enemySpawnTime.bossNum = keyValuePair.Value.DIC("bossNum");
			string text = keyValuePair.Value.DIC("eventList");
			if (!string.IsNullOrEmpty(text))
			{
				string[] array3 = text.Split(',', StringSplitOptions.None);
				enemySpawnTime.eventList = new RoguelikeEventType[array3.Length];
				for (int k = 0; k < array3.Length; k++)
				{
					RoguelikeEventType roguelikeEventType;
					if (Enum.TryParse<RoguelikeEventType>(array3[k], out roguelikeEventType))
					{
						enemySpawnTime.eventList[k] = roguelikeEventType;
					}
				}
			}
			enemySpawnTime.eliteShield = keyValuePair.Value.DIC("eliteShield");
			enemySpawnTime.bossAttackLevel = keyValuePair.Value.DIC("bossAttackLevel");
			enemySpawnTime.bossHpLevel = keyValuePair.Value.DIC("bossHpLevel");
			GameHelperClient.spawnConfig.enemySpawnData[num] = enemySpawnTime;
			num++;
		}
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x000252BC File Offset: 0x000234BC
	public void InitEnemy()
	{
		foreach (KeyValuePair<string, object> keyValuePair in ((Dictionary<string, object>)ExcelManager.allExcelData["enemy"]))
		{
			string key = keyValuePair.Value.DIC("id");
			RoleAttribute value = default(RoleAttribute);
			value.hp = keyValuePair.Value.DIC("hp");
			value.attackPower = keyValuePair.Value.DIC("attack");
			value.attackSpeed = keyValuePair.Value.DIC("attackSpeed");
			value.moveSpeed = keyValuePair.Value.DIC("moveSpeed");
			value.type = keyValuePair.Value.DIC("enemyType");
			value.model = keyValuePair.Value.DIC("model");
			value.materialIndex = keyValuePair.Value.DIC("skin");
			value.roleName = keyValuePair.Value.DIC("name");
			value.id = keyValuePair.Value.DIC("id");
			value.shiled = keyValuePair.Value.DIC("shield");
			value.dropCard = (keyValuePair.Value.DIC("dropCard") == 1);
			value.armor = keyValuePair.Value.DIC("armor");
			this.HeroAttributeDic.Add(key, value);
		}
	}

	// Token: 0x04000883 RID: 2179
	public Dictionary<string, RoleAttribute> HeroAttributeDic = new Dictionary<string, RoleAttribute>();

	// Token: 0x04000884 RID: 2180
	public Dictionary<ActiveSkillEnum, ActiveSkillData> ActiveSkillDataDic = new Dictionary<ActiveSkillEnum, ActiveSkillData>();

	// Token: 0x04000885 RID: 2181
	public Dictionary<ItemType, ItemData> ItemDataDic = new Dictionary<ItemType, ItemData>();

	// Token: 0x04000886 RID: 2182
	public Dictionary<ItemType, RemainsData> RemainsDataDic = new Dictionary<ItemType, RemainsData>();

	// Token: 0x04000887 RID: 2183
	public Dictionary<EnemyEntriesType, EnemyEntriesData> EnemyEntriesDic = new Dictionary<EnemyEntriesType, EnemyEntriesData>();

	// Token: 0x04000888 RID: 2184
	public Dictionary<int, ForgingData> ForgingDataDic = new Dictionary<int, ForgingData>();

	// Token: 0x04000889 RID: 2185
	public Dictionary<int, ContractData> ContractDataDic = new Dictionary<int, ContractData>();

	// Token: 0x0400088A RID: 2186
	public Dictionary<int, CardData> CardDataDic = new Dictionary<int, CardData>();

	// Token: 0x0400088B RID: 2187
	public Dictionary<string, ExDropData> ExDropDataDic = new Dictionary<string, ExDropData>();

	// Token: 0x0400088C RID: 2188
	public CardTotalManager cardTotalManager;

	// Token: 0x0400088D RID: 2189
	public Dictionary<string, DropData> DropDataDic = new Dictionary<string, DropData>();

	// Token: 0x0400088E RID: 2190
	public Dictionary<string, List<EquipBase.EquipAttributeData>> EquipAttributeDataDic = new Dictionary<string, List<EquipBase.EquipAttributeData>>();
}
