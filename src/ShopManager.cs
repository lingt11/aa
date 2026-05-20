using System;
using System.Collections.Generic;
using System.Globalization;
using Mirror;
using UnityEngine;

// Token: 0x02000150 RID: 336
public class ShopManager : IUpdate, IApplicationQuit
{
	// Token: 0x0600067A RID: 1658 RVA: 0x00027554 File Offset: 0x00025754
	public ShopManager()
	{
		object dic = ExcelManager.allExcelData["shop"];
		for (int i = 0; i < 8; i++)
		{
			object data = dic.DIC("book_" + (i + 1).ToString());
			this.AddShopItemData(data, this.buyBookList);
		}
		int num = 0;
		object dic2 = ExcelManager.allExcelData["equipment"];
		for (int j = 0; j < 999; j++)
		{
			string key = (j + 1).ToString();
			if (dic2.DIC(key) == null)
			{
				num = j + 1;
				break;
			}
		}
		for (int k = 0; k < num; k++)
		{
			if (k == 1)
			{
				object data2 = dic.DIC("forging");
				this.AddShopItemData(data2, this.buyItemList);
				object data3 = dic.DIC("forging_gold");
				this.AddShopItemData(data3, this.buyItemList);
				object data4 = dic.DIC("equipStreng");
				this.AddShopItemData(data4, this.buyItemList);
				object data5 = dic.DIC("dropGold");
				this.AddShopItemData(data5, this.buyItemList);
				object data6 = dic.DIC("empty");
				this.AddShopItemData(data6, this.buyItemList);
				data6 = dic.DIC("empty");
				this.AddShopItemData(data6, this.buyItemList);
			}
			object data7 = dic.DIC("equip_" + k.ToString());
			this.AddShopItemData(data7, this.buyItemList);
		}
		for (int l = 0; l < 999; l++)
		{
			string key2 = "medicine_" + l.ToString();
			if (dic.DIC(key2) == null)
			{
				break;
			}
			object data8 = dic.DIC(key2);
			this.AddShopItemData(data8, this.buyMedicineList);
		}
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x00027754 File Offset: 0x00025954
	private void AddShopItemData(object data, List<ShopItem> list)
	{
		ShopItem shopItem = new ShopItem();
		shopItem.id = data.DIC("id");
		shopItem.gold = data.DIC("gold");
		shopItem.goldAdd = data.DIC("goldAdd");
		shopItem.goldMax = this.GetOptionalShopInt(data, "goldMax");
		shopItem.gem = data.DIC("zhuan");
		shopItem.gemAdd = data.DIC("zhuanAdd");
		shopItem.gemMax = this.GetOptionalShopInt(data, "zhuanMax");
		shopItem.iconPath = data.DIC("icon");
		shopItem.type = data.DIC("type");
		if (data.DIC().ContainsKey("specialBuy"))
		{
			shopItem.specialBuy = data.DIC("specialBuy");
		}
		string text = data.DIC("value");
		if (!string.IsNullOrEmpty(text))
		{
			shopItem.strValues = text.Split('|', StringSplitOptions.None);
			shopItem.values = new float[shopItem.strValues.Length];
			for (int i = 0; i < shopItem.strValues.Length; i++)
			{
				shopItem.values[i] = float.Parse(shopItem.strValues[i]);
			}
		}
		shopItem.times = data.DIC("time");
		if (shopItem.cdSet == 0f)
		{
			shopItem.cdSet = 1f;
		}
		shopItem.cd = 0f;
		list.Add(shopItem);
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x000278C0 File Offset: 0x00025AC0
	private int GetOptionalShopInt(object data, string key)
	{
		if (!data.DIC().ContainsKey(key))
		{
			return 0;
		}
		int result;
		if (!int.TryParse(data.DIC(key), out result))
		{
			return 0;
		}
		return result;
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x000278F0 File Offset: 0x00025AF0
	public void Update()
	{
		foreach (ShopItem shopItem in this.buyBookList)
		{
			shopItem.Update();
		}
		foreach (ShopItem shopItem2 in this.buyItemList)
		{
			shopItem2.Update();
		}
		foreach (ShopItem shopItem3 in this.buyMedicineList)
		{
			shopItem3.Update();
		}
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x000279C0 File Offset: 0x00025BC0
	public void ClearAllShopCD()
	{
		foreach (ShopItem shopItem in this.buyBookList)
		{
			shopItem.cd = 0f;
		}
		foreach (ShopItem shopItem2 in this.buyItemList)
		{
			shopItem2.cd = 0f;
		}
		foreach (ShopItem shopItem3 in this.buyMedicineList)
		{
			shopItem3.cd = 0f;
		}
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x00002D1D File Offset: 0x00000F1D
	public void OnApplicationQuit()
	{
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x00027AA0 File Offset: 0x00025CA0
	private void MonsterGold(int level)
	{
		GameHelperClient.localPlayer.CmdCreateLocalTyrant(level);
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x00027AAD File Offset: 0x00025CAD
	private void MonsterJuMo(int level)
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.Goblin_HellFlame_0 + level, true);
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x00027AC1 File Offset: 0x00025CC1
	private void MonsterFeiTing(int level)
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.Goblin_Mine_0 + level, false);
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x00027AD5 File Offset: 0x00025CD5
	private void MonsterXinMo(int level)
	{
		GameHelperClient.localPlayer.CmdCreateHeartDemon(level);
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x00027AE2 File Offset: 0x00025CE2
	private void MonsterGambler(int level)
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.Goblin_Blacksmith_0 + level, true);
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x00027AF6 File Offset: 0x00025CF6
	private void MonsterGuaiWu1()
	{
		this.BuyMonsterWave("item_1", 1);
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x00027B04 File Offset: 0x00025D04
	private void MonsterGuaiWu2()
	{
		this.BuyMonsterWave("item_2", 2);
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x00027B12 File Offset: 0x00025D12
	private void MonsterGuaiWu3()
	{
		this.BuyMonsterWave("item_3", 3);
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x00027B20 File Offset: 0x00025D20
	private void MonsterGuaiWu4()
	{
		this.BuyMonsterWave("item_4", 4);
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x00027B2E File Offset: 0x00025D2E
	public void monster_1()
	{
		this.MonsterGuaiWu1();
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x00027B36 File Offset: 0x00025D36
	public void monster_2()
	{
		this.MonsterGuaiWu2();
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x00027B3E File Offset: 0x00025D3E
	public void monster_3()
	{
		this.MonsterGuaiWu3();
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x00027B46 File Offset: 0x00025D46
	public void monster_4()
	{
		this.MonsterGuaiWu4();
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x00027B4E File Offset: 0x00025D4E
	public void monster_5(int level)
	{
		this.MonsterGold(level);
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x00027B57 File Offset: 0x00025D57
	public void monster_6(int level)
	{
		this.MonsterFeiTing(level);
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x00027B60 File Offset: 0x00025D60
	public void monster_7(int level)
	{
		this.MonsterJuMo(level);
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x00027B69 File Offset: 0x00025D69
	public void monster_8(int level)
	{
		this.MonsterXinMo(level);
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x00027B72 File Offset: 0x00025D72
	public void monster_9(int level)
	{
		this.MonsterGambler(level);
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x00027B7C File Offset: 0x00025D7C
	private ShopItem GetShopItem(string id, List<ShopItem> buyBookList)
	{
		foreach (ShopItem shopItem in buyBookList)
		{
			if (shopItem.id.Equals(id))
			{
				return shopItem;
			}
		}
		return null;
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x00027BD8 File Offset: 0x00025DD8
	private void BuyMonsterWave(string id, int level)
	{
		NetworkClient.connection.Send<ServerNetMessage>(new ServerNetMessage
		{
			serverNetOperation = ServerNetOperation.OnBuyShop,
			datas = new int[1]
		}, 0);
		GameHelperClient.AddShowBuff(PathDefine.Concat(Game.Language.Get("怪物群", ""), level), Game.Language.Get("怪物群说明", ""), "Shop/guaiwuqun", Mathf.Min(100f, GameHelperClient.CountDownTime));
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x00027C5B File Offset: 0x00025E5B
	public void BuyMedicine(ShopItem shopItem)
	{
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/购买药水和书", 1f, 3f);
		GameHelperClient.localPlayer.playerAttribute.AddMedicine(shopItem);
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x00027C88 File Offset: 0x00025E88
	public void BuyBook(string id)
	{
		ItemType bookType = ItemType.None;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(id);
		if (num <= 2091892201U)
		{
			if (num <= 2058336963U)
			{
				if (num != 2041559344U)
				{
					if (num == 2058336963U)
					{
						if (id == "book_4")
						{
							bookType = ItemType.Passsive_Book_C;
						}
					}
				}
				else if (id == "book_5")
				{
					bookType = ItemType.Active_Book_B;
				}
			}
			else if (num != 2075114582U)
			{
				if (num == 2091892201U)
				{
					if (id == "book_6")
					{
						bookType = ItemType.Passsive_Book_B;
					}
				}
			}
			else if (id == "book_7")
			{
				bookType = ItemType.Active_Book_A;
			}
		}
		else if (num <= 2142225058U)
		{
			if (num != 2108669820U)
			{
				if (num == 2142225058U)
				{
					if (id == "book_3")
					{
						bookType = ItemType.Active_Book_C;
					}
				}
			}
			else if (id == "book_1")
			{
				bookType = ItemType.Active_Book_D;
			}
		}
		else if (num != 2159002677U)
		{
			if (num == 2259668391U)
			{
				if (id == "book_8")
				{
					bookType = ItemType.Passsive_Book_A;
				}
			}
		}
		else if (id == "book_2")
		{
			bookType = ItemType.Passsive_Book_D;
		}
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/购买药水和书", 1f, 3f);
		this.GetShopItem(id, this.buyBookList).ApplyPriceGrowth();
		GameHelperClient.localPlayer.playerAttribute.AddBook(bookType, BagItemType.Book, id);
		UI_Shop ui = Game.UI.GetUI<UI_Shop>();
		if (ui == null)
		{
			return;
		}
		ui.ShowShopPanel();
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x00027E28 File Offset: 0x00026028
	private bool CheckCanBuyEquip()
	{
		if (GameHelperClient.localPlayer.playerAttribute.equipList.Count == GameHelperClient.MaxEquipNum)
		{
			Util.ShowTips("装备已满，请先出售装备！");
			return false;
		}
		return true;
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x00027E54 File Offset: 0x00026054
	public static bool TryOpenEquipStreng(ItemType itemType = ItemType.None)
	{
		if (GameHelperClient.isGameOver)
		{
			Util.ShowTips("当前阶段无法强化");
			return false;
		}
		if (GameHelperClient.localPlayer.playerAttribute.equipList.Count == 0)
		{
			if (itemType == ItemType.None)
			{
				Util.ShowTips("没有可以强化的旧时代系列装备！");
				return false;
			}
			Util.ShowTips("没有可以强化的装备！");
			return false;
		}
		else
		{
			UI_EquipStreng ui = Game.UI.GetUI<UI_EquipStreng>();
			if (ui != null && ui.isOpen)
			{
				Util.ShowTips("请先完成当前强化！");
				return false;
			}
			bool flag = false;
			using (List<EquipBase>.Enumerator enumerator = GameHelperClient.localPlayer.playerAttribute.equipList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (ShopManager.CanEquipStreng(enumerator.Current, itemType))
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				(Game.UI.OpenUI<UI_EquipStreng>(null) as UI_EquipStreng).SetStrengItemType(itemType);
				return true;
			}
			if (itemType == ItemType.None)
			{
				Util.ShowTips("没有可以强化的旧时代系列装备！");
				return false;
			}
			Util.ShowTipsNoLanguage(string.Format(ColorDefine.RedForColor, Game.Language.Get("达到最高等级", "")));
			return false;
		}
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x00027F6C File Offset: 0x0002616C
	public static bool CanEquipStreng(EquipBase equipBase, ItemType itemType)
	{
		return equipBase != null && equipBase.level < equipBase.maxLevel && (itemType != ItemType.None || equipBase.shopStreng);
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x00027F90 File Offset: 0x00026190
	public ShopManager.ShopBuyResult OnBuyItem(ShopItem shopItem)
	{
		string id = shopItem.id;
		string specialBuy = shopItem.specialBuy;
		if (!(specialBuy == "forging") && !(specialBuy == "forging_gold"))
		{
			if (!(specialBuy == "artifact"))
			{
				if (!(specialBuy == "equipStreng"))
				{
					if (specialBuy == "dropGold")
					{
						Game.UI.OpenUI<UI_DropGold>(null);
						return ShopManager.ShopBuyResult.Success;
					}
					if (!this.CheckCanBuyEquip())
					{
						return ShopManager.ShopBuyResult.Fail;
					}
					ShopManager.OnBuyEquipSuccess(id, 0, null);
					return ShopManager.ShopBuyResult.Success;
				}
				else
				{
					if (!ShopManager.TryOpenEquipStreng(ItemType.None))
					{
						return ShopManager.ShopBuyResult.Fail;
					}
					return ShopManager.ShopBuyResult.SuccessNoShopCost;
				}
			}
			else
			{
				if (!this.CheckCanBuyEquip())
				{
					return ShopManager.ShopBuyResult.Fail;
				}
				if (!Util.CheckCanRoguelike())
				{
					return ShopManager.ShopBuyResult.Fail;
				}
				this.OnBuyShenQi();
				return ShopManager.ShopBuyResult.Success;
			}
		}
		else
		{
			if (!Util.CheckCanRoguelike())
			{
				return ShopManager.ShopBuyResult.Fail;
			}
			this.OnBuyForging(shopItem.specialBuy.Equals("forging_gold"));
			return ShopManager.ShopBuyResult.Success;
		}
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x00028058 File Offset: 0x00026258
	public static EquipBase OnBuyEquipSuccess(string id, int initLevel = 0, EquipEvolutionEntryData evolutionEntry = null)
	{
		List<EquipEvolutionEntryData> list;
		if (evolutionEntry != null)
		{
			(list = new List<EquipEvolutionEntryData>()).Add(evolutionEntry);
		}
		else
		{
			list = null;
		}
		List<EquipEvolutionEntryData> evolutionEntries = list;
		return ShopManager.OnBuyEquipSuccess(id, initLevel, evolutionEntries);
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x00028080 File Offset: 0x00026280
	public static EquipBase OnBuyEquipSuccess(string id, int initLevel, List<EquipEvolutionEntryData> evolutionEntries)
	{
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/购买药水和书", 1f, 3f);
		EquipBase equipBase = new EquipBase();
		equipBase.equipIndex = id.Split("_", StringSplitOptions.None)[1];
		equipBase.Init(GameHelperClient.localPlayer.roleType);
		equipBase.level = initLevel;
		if (evolutionEntries != null)
		{
			for (int i = 0; i < evolutionEntries.Count; i++)
			{
				EquipEvolutionEntryData equipEvolutionEntryData = evolutionEntries[i];
				if (equipEvolutionEntryData != null)
				{
					equipEvolutionEntryData.ApplyTo(equipBase);
				}
			}
		}
		GameHelperClient.localPlayer.playerAttribute.AddEquip(equipBase);
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui != null)
		{
			ui.RefreshPlayerEquip();
		}
		return equipBase;
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x00028128 File Offset: 0x00026328
	private void OnBuyShenQi()
	{
		int num = 0;
		object dic = ExcelManager.allExcelData["equipment"];
		for (int i = 0; i < 999; i++)
		{
			string key = (100 + i + 1).ToString();
			if (dic.DIC(key) == null)
			{
				num = i;
				break;
			}
		}
		List<EquipBase> equipList = GameHelperClient.localPlayer.playerAttribute.equipList;
		int count = equipList.Count;
		this.randomData = new List<int>();
		for (int j = 0; j < num; j++)
		{
			int num2 = 100 + j + 1;
			bool flag = true;
			if (count > 0)
			{
				for (int k = 0; k < count; k++)
				{
					EquipBase equipBase = equipList[k];
					if (int.Parse(equipBase.equipIndex) == num2)
					{
						flag = false;
						break;
					}
					if (this.IsShenQiConflict(equipBase.equipIndex, num2))
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				this.randomData.Add(100 + j + 1);
			}
		}
		for (int l = 0; l < this.randomData.Count; l++)
		{
			int num3 = Random.Range(0, this.randomData.Count);
			List<int> list = this.randomData;
			int index = num3;
			List<int> list2 = this.randomData;
			int index2 = l;
			int value = this.randomData[l];
			int value2 = this.randomData[num3];
			list[index] = value;
			list2[index2] = value2;
		}
		RoguelikeUIData[] array = new RoguelikeUIData[3];
		for (int m = 0; m < 3; m++)
		{
			RoguelikeUIData roguelikeUIData = default(RoguelikeUIData);
			int num4 = this.randomData[m];
			object dic2 = ExcelManager.allExcelData["equipment"].DIC(num4.ToString());
			string text = PathDefine.Concat("equip_", num4);
			string name = Game.Language.Get(text, "");
			string str = dic2.DIC("equipmentIcon");
			roguelikeUIData.name = name;
			roguelikeUIData.icon = "Bundles/UI/Icon/Shop/" + str;
			roguelikeUIData.dec = EquipBase.GetEquipInfo(text);
			roguelikeUIData.data = "equip_" + num4.ToString();
			roguelikeUIData.quality = -1;
			array[m] = roguelikeUIData;
		}
		this.roguelikeIndex = 3;
		UI_Roguelike ui_Roguelike = Game.UI.OpenUI<UI_Roguelike>(null) as UI_Roguelike;
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/学习主动技能", 1f, 3f);
		ui_Roguelike.ShowRoguelike(array, new Action<RoguelikeUIData>(this.OnBuyShenQiRoguelike), Game.Language.Get("装备选择", ""), new UI_Roguelike.RefreshActionEvent(this.OnRefreshRoguelikeUIData), new Action(this.OnSelectForgingEnd), 0f, null, "equip");
		MySystemEvent.Instance.DispatchMessage(42);
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x000283F4 File Offset: 0x000265F4
	private bool IsShenQiConflict(string roleEquipIndex, int selectEquipIndex)
	{
		object obj;
		if (string.IsNullOrEmpty(roleEquipIndex) || !ExcelManager.allExcelData.TryGetValue("equipment", out obj))
		{
			return false;
		}
		Dictionary<string, object> dictionary = obj as Dictionary<string, object>;
		object dic;
		if (dictionary == null || !dictionary.TryGetValue(roleEquipIndex, out dic))
		{
			return false;
		}
		Dictionary<string, object> dictionary2 = dic.DIC();
		if (!dictionary2.ContainsKey("conflict"))
		{
			return false;
		}
		object obj2 = dictionary2["conflict"];
		if (obj2 == null)
		{
			return false;
		}
		string text = obj2.ToString();
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		string[] array = text.Split(new char[]
		{
			'|',
			',',
			';',
			' '
		}, StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < array.Length; i++)
		{
			int num;
			if (int.TryParse(array[i], out num) && num == selectEquipIndex)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x000284B8 File Offset: 0x000266B8
	private RoguelikeUIData OnRefreshRoguelikeUIData()
	{
		RoguelikeUIData result = default(RoguelikeUIData);
		int num = this.randomData[this.roguelikeIndex];
		object dic = ExcelManager.allExcelData["equipment"].DIC(num.ToString());
		string text = PathDefine.Concat("equip_", num);
		string name = Game.Language.Get(text, "");
		string str = dic.DIC("equipmentIcon");
		result.name = name;
		result.icon = "Bundles/UI/Icon/Shop/" + str;
		result.dec = EquipBase.GetEquipInfo(text);
		result.data = "equip_" + num.ToString();
		result.quality = -1;
		this.roguelikeIndex++;
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/学习主动技能", 1f, 3f);
		return result;
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x0002859C File Offset: 0x0002679C
	private void OnBuyShenQiRoguelike(RoguelikeUIData roguelikeUIData)
	{
		EquipBase equipBase = this.CreateBuyShenQiEquipBase(roguelikeUIData.data);
		if (!this.ShouldSelectBuyShenQiEvolutionEntry(equipBase))
		{
			ShopManager.OnBuyEquipSuccess(roguelikeUIData.data, 0, null);
			return;
		}
		List<EquipEvolutionEntryData> evolutionEntries = EquipEvolutionEntryData.GetRandomOptions(equipBase, 3, equipBase.equipIndex);
		if (evolutionEntries.Count == 0)
		{
			ShopManager.OnBuyEquipSuccess(roguelikeUIData.data, 0, null);
			return;
		}
		string equipData = roguelikeUIData.data;
		Game.TimerManager.AddTimer(0.1f, delegate()
		{
			this.ShowBuyShenQiEvolutionEntry(equipData, equipBase.equipIndex, evolutionEntries);
		});
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x00028646 File Offset: 0x00026846
	private EquipBase CreateBuyShenQiEquipBase(string equipData)
	{
		EquipBase equipBase = new EquipBase();
		equipBase.equipIndex = equipData.Split("_", StringSplitOptions.None)[1];
		equipBase.Init(GameHelperClient.localPlayer.roleType);
		return equipBase;
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x00028674 File Offset: 0x00026874
	private bool ShouldSelectBuyShenQiEvolutionEntry(EquipBase equipBase)
	{
		return equipBase != null && (equipBase.equipIndex == "123" || equipBase.equipIndex == "124" || equipBase.equipIndex == "125" || ShopManager.CanEquipStreng(equipBase, ItemType.None));
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x000286C8 File Offset: 0x000268C8
	private void ShowBuyShenQiEvolutionEntry(string equipData, string equipIndex, List<EquipEvolutionEntryData> evolutionEntries)
	{
		RoguelikeUIData[] array = new RoguelikeUIData[evolutionEntries.Count];
		string equipEvolutionEntryIcon = this.GetEquipEvolutionEntryIcon(equipIndex);
		for (int i = 0; i < evolutionEntries.Count; i++)
		{
			array[i] = evolutionEntries[i].CreateRoguelikeUIData(equipEvolutionEntryIcon, i.ToString());
		}
		UI_Roguelike ui_Roguelike = Game.UI.OpenUI<UI_Roguelike>(null) as UI_Roguelike;
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/装备进化", 1f, 3f);
		ui_Roguelike.ShowRoguelike(array, delegate(RoguelikeUIData roguelikeData)
		{
			int index = int.Parse(roguelikeData.data);
			ShopManager.OnBuyEquipSuccess(equipData, 0, evolutionEntries[index]);
		}, Game.Language.Get("装备进化", ""), null, new Action(this.OnSelectForgingEnd), 0f, null, "equip_evolution");
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x000287A0 File Offset: 0x000269A0
	private string GetEquipEvolutionEntryIcon(string equipIndex)
	{
		string str = ExcelManager.allExcelData["equipment"].DIC(equipIndex).DIC("equipmentIcon");
		return "Bundles/UI/Icon/Shop/" + str;
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x000287D8 File Offset: 0x000269D8
	private List<int> GetForgingRandomList(Dictionary<int, ForgingData> forgingDataDic, int quality)
	{
		List<int> list = new List<int>();
		foreach (ForgingData forgingData in forgingDataDic.Values)
		{
			if (forgingData.quality == quality)
			{
				list.Add(forgingData.id);
			}
		}
		return list;
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x00028840 File Offset: 0x00026A40
	private RoguelikeUIData CreateForgingRoguelikeData(ForgingData forgingData, bool isGold, int qualityOverride = -1)
	{
		RoguelikeUIData result = default(RoguelikeUIData);
		result.name = Game.Language.Get("forging_" + forgingData.type, "");
		result.icon = "Bundles/UI/Icon/Remains/" + forgingData.icon;
		float minValue = isGold ? (forgingData.minValue * (float)forgingData.goldLevel) : forgingData.minValue;
		float maxValue = isGold ? (forgingData.maxValue * (float)forgingData.goldLevel) : forgingData.maxValue;
		float randomForgingValue = this.GetRandomForgingValue(forgingData, minValue, maxValue);
		result.dec = Util.GetForgingDec(forgingData, randomForgingValue, minValue, maxValue);
		result.data = PathDefine.Concat(forgingData.type, StringDefine.Underline, Util.FormatForgingValue(randomForgingValue));
		result.quality = ((qualityOverride >= 0) ? qualityOverride : forgingData.quality);
		return result;
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x00028912 File Offset: 0x00026B12
	private float GetRandomForgingValue(ForgingData forgingData, float minValue, float maxValue)
	{
		if (Mathf.Approximately(minValue, maxValue))
		{
			return minValue;
		}
		if (forgingData.isFloat)
		{
			return Random.Range(minValue, maxValue);
		}
		return (float)Random.Range(Mathf.RoundToInt(minValue), Mathf.RoundToInt(maxValue) + 1);
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x00028944 File Offset: 0x00026B44
	private string GetCompositeForgingPairKey(ForgingData firstForging, ForgingData secondForging)
	{
		if (string.CompareOrdinal(firstForging.type, secondForging.type) > 0)
		{
			return secondForging.type + "|" + firstForging.type;
		}
		return firstForging.type + "|" + secondForging.type;
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x00028994 File Offset: 0x00026B94
	private ForgingData GetCompositeSecondForging(Dictionary<int, ForgingData> forgingDataDic, List<int> randomList, ForgingData firstForging, HashSet<string> usedPairKeys)
	{
		int num = Random.Range(0, randomList.Count);
		for (int i = 0; i < randomList.Count; i++)
		{
			int index = (num + i) % randomList.Count;
			ForgingData forgingData = forgingDataDic[randomList[index]];
			string compositeForgingPairKey = this.GetCompositeForgingPairKey(firstForging, forgingData);
			if (!usedPairKeys.Contains(compositeForgingPairKey))
			{
				usedPairKeys.Add(compositeForgingPairKey);
				return forgingData;
			}
		}
		ForgingData forgingData2 = forgingDataDic[randomList[Random.Range(0, randomList.Count)]];
		usedPairKeys.Add(this.GetCompositeForgingPairKey(firstForging, forgingData2));
		return forgingData2;
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x00028A28 File Offset: 0x00026C28
	private RoguelikeUIData CreateCompositeForgingRoguelikeData(ForgingData firstForging, ForgingData secondForging, int compositeQuality, bool isGold)
	{
		RoguelikeUIData roguelikeUIData = this.CreateForgingRoguelikeData(firstForging, isGold, -1);
		RoguelikeUIData roguelikeUIData2 = this.CreateForgingRoguelikeData(secondForging, isGold, -1);
		return new RoguelikeUIData
		{
			name = roguelikeUIData.name + "&" + roguelikeUIData2.name,
			dec = roguelikeUIData.dec + StringDefine.Wrap + roguelikeUIData2.dec,
			icon = roguelikeUIData.icon,
			data = roguelikeUIData.data + "|" + roguelikeUIData2.data,
			quality = compositeQuality
		};
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x00028AC0 File Offset: 0x00026CC0
	private void ApplyForgingData(string dataType, float dataValue)
	{
		int value = Mathf.RoundToInt(dataValue);
		uint num = <PrivateImplementationDetails>.ComputeStringHash(dataType);
		if (num <= 2000437136U)
		{
			if (num <= 853522520U)
			{
				if (num <= 591723698U)
				{
					if (num <= 399922361U)
					{
						if (num != 326073063U)
						{
							if (num != 399922361U)
							{
								return;
							}
							if (!(dataType == "Sta"))
							{
								return;
							}
							this.forgingManager.UpdateSta(value);
							return;
						}
						else
						{
							if (!(dataType == "ArmedAdd"))
							{
								return;
							}
							this.forgingManager.UpdateArmedAdd(dataValue * 0.01f);
							return;
						}
					}
					else if (num != 521850149U)
					{
						if (num != 591723698U)
						{
							return;
						}
						if (!(dataType == "HPSec"))
						{
							return;
						}
						this.forgingManager.UpdateHPSec(value);
						return;
					}
					else
					{
						if (!(dataType == "SkillAdd"))
						{
							return;
						}
						this.forgingManager.UpdateSkillAdd(dataValue * 0.01f);
						return;
					}
				}
				else if (num <= 776144995U)
				{
					if (num != 618031408U)
					{
						if (num != 776144995U)
						{
							return;
						}
						if (!(dataType == "Three"))
						{
							return;
						}
						this.forgingManager.UpdateStr(value);
						this.forgingManager.UpdateSta(value);
						this.forgingManager.UpdateAgi(value);
						return;
					}
					else
					{
						if (!(dataType == "Str"))
						{
							return;
						}
						this.forgingManager.UpdateStr(value);
						return;
					}
				}
				else if (num != 785119009U)
				{
					if (num != 853522520U)
					{
						return;
					}
					if (!(dataType == "MP"))
					{
						return;
					}
					this.forgingManager.UpdateMP(value);
					return;
				}
				else
				{
					if (!(dataType == "SummonAdd"))
					{
						return;
					}
					this.forgingManager.UpdateSummonAdd(dataValue * 0.01f);
					return;
				}
			}
			else if (num <= 1287009716U)
			{
				if (num <= 1041612137U)
				{
					if (num != 895613287U)
					{
						if (num != 1041612137U)
						{
							return;
						}
						if (!(dataType == "CriticalDamage"))
						{
							return;
						}
						this.forgingManager.UpdateCriticalDamage(dataValue * 0.01f);
						return;
					}
					else
					{
						if (!(dataType == "SkillHit"))
						{
							return;
						}
						this.forgingManager.UpdateSkillHit(value);
						return;
					}
				}
				else if (num != 1182039611U)
				{
					if (num != 1287009716U)
					{
						return;
					}
					if (!(dataType == "XiXue"))
					{
						return;
					}
					this.forgingManager.UpdateXiXue(value);
					return;
				}
				else
				{
					if (!(dataType == "ExpAdd"))
					{
						return;
					}
					this.forgingManager.UpdateExpAdd(dataValue * 0.01f);
					return;
				}
			}
			else if (num <= 1680846624U)
			{
				if (num != 1483691181U)
				{
					if (num != 1680846624U)
					{
						return;
					}
					if (!(dataType == "HpSecRate"))
					{
						return;
					}
					this.forgingManager.UpdateHpSecRate(dataValue * 0.01f);
					return;
				}
				else
				{
					if (!(dataType == "SkillBreak"))
					{
						return;
					}
					this.forgingManager.UpdateSkillBreak(dataValue * 0.01f);
					return;
				}
			}
			else if (num != 1806474955U)
			{
				if (num != 1894470373U)
				{
					if (num != 2000437136U)
					{
						return;
					}
					if (!(dataType == "Luck"))
					{
						return;
					}
					this.forgingManager.UpdateLuck(value);
					return;
				}
				else
				{
					if (!(dataType == "HP"))
					{
						return;
					}
					this.forgingManager.UpdateHP(value);
					return;
				}
			}
			else
			{
				if (!(dataType == "HenshinAdd"))
				{
					return;
				}
				this.forgingManager.UpdateHenshinAdd(dataValue * 0.01f);
				return;
			}
		}
		else if (num <= 2752153801U)
		{
			if (num <= 2278596520U)
			{
				if (num <= 2107383588U)
				{
					if (num != 2076688730U)
					{
						if (num != 2107383588U)
						{
							return;
						}
						if (!(dataType == "ExtraDamage"))
						{
							return;
						}
						this.forgingManager.UpdateExtraDamage(value);
						return;
					}
					else
					{
						if (!(dataType == "HaloAdd"))
						{
							return;
						}
						this.forgingManager.UpdateHaloAdd(dataValue * 0.01f);
						return;
					}
				}
				else if (num != 2226667892U)
				{
					if (num != 2278596520U)
					{
						return;
					}
					if (!(dataType == "Critical"))
					{
						return;
					}
					this.forgingManager.UpdateCritical(dataValue * 0.01f);
					return;
				}
				else
				{
					if (!(dataType == "Armor"))
					{
						return;
					}
					this.forgingManager.UpdateArmor(value);
					return;
				}
			}
			else if (num <= 2343121693U)
			{
				if (num != 2311178276U)
				{
					if (num != 2343121693U)
					{
						return;
					}
					if (!(dataType == "Attack"))
					{
						return;
					}
					this.forgingManager.UpdateAttack(value);
					return;
				}
				else
				{
					if (!(dataType == "Doge"))
					{
						return;
					}
					this.forgingManager.UpdateDoge(value);
					return;
				}
			}
			else if (num != 2374343684U)
			{
				if (num != 2462836616U)
				{
					if (num != 2752153801U)
					{
						return;
					}
					if (!(dataType == "NormalAdd"))
					{
						return;
					}
					this.forgingManager.UpdateNormalAdd(dataValue * 0.01f);
					return;
				}
				else
				{
					if (!(dataType == "Agi"))
					{
						return;
					}
					this.forgingManager.UpdateAgi(value);
					return;
				}
			}
			else
			{
				if (!(dataType == "ReduceInjury"))
				{
					return;
				}
				this.forgingManager.UpdateReduceInjury(value);
				return;
			}
		}
		else if (num <= 3371345849U)
		{
			if (num <= 3354445110U)
			{
				if (num != 3135110330U)
				{
					if (num != 3354445110U)
					{
						return;
					}
					if (!(dataType == "GoldAdd"))
					{
						return;
					}
					this.forgingManager.UpdateGoldAdd(dataValue * 0.01f);
					return;
				}
				else
				{
					if (!(dataType == "HpPercent"))
					{
						return;
					}
					this.forgingManager.UpdateHpPercent(dataValue * 0.01f);
					return;
				}
			}
			else if (num != 3365943077U)
			{
				if (num != 3371345849U)
				{
					return;
				}
				if (!(dataType == "NormalBreak"))
				{
					return;
				}
				this.forgingManager.UpdateNormalBreak(dataValue * 0.01f);
				return;
			}
			else
			{
				if (!(dataType == "AddDamage"))
				{
					return;
				}
				this.forgingManager.UpdateAddDamage(dataValue * 0.01f);
				return;
			}
		}
		else if (num <= 3581048735U)
		{
			if (num != 3439798920U)
			{
				if (num != 3581048735U)
				{
					return;
				}
				if (!(dataType == "MoveSpeed"))
				{
					return;
				}
				this.forgingManager.UpdateMoveSpeed(dataValue);
				return;
			}
			else
			{
				if (!(dataType == "AttackSpeed"))
				{
					return;
				}
				this.forgingManager.UpdateAttackSpeed(dataValue * 0.01f);
				return;
			}
		}
		else if (num != 3990130861U)
		{
			if (num != 4235090694U)
			{
				if (num != 4292234280U)
				{
					return;
				}
				if (!(dataType == "CoolDown"))
				{
					return;
				}
				this.forgingManager.UpdateCoolDown(value);
				return;
			}
			else
			{
				if (!(dataType == "XiXueRate"))
				{
					return;
				}
				this.forgingManager.UpdateXiXueRate(dataValue * 0.01f);
				return;
			}
		}
		else
		{
			if (!(dataType == "MPSec"))
			{
				return;
			}
			this.forgingManager.UpdateMPSec(value);
			return;
		}
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x00029180 File Offset: 0x00027380
	private void ApplyForgingData(string forgingData)
	{
		string[] array = forgingData.Split(StringDefine.Underline, StringSplitOptions.None);
		if (array.Length < 2)
		{
			return;
		}
		float dataValue;
		if (float.TryParse(array[1], NumberStyles.Float, CultureInfo.InvariantCulture, out dataValue) || float.TryParse(array[1], out dataValue))
		{
			this.ApplyForgingData(array[0], dataValue);
		}
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x000291D0 File Offset: 0x000273D0
	private void OnBuyForging(bool isGold)
	{
		float[] forgingDrop = GameHelperClient.gameConfig.ForgingDrop;
		float[] forgingLucky = GameHelperClient.gameConfig.ForgingLucky;
		float num = Util.GetLuckAddValue(GameHelperClient.localPlayer.lucky) + GameHelperClient.localPlayer.ForgingAdd;
		int num2 = forgingDrop.Length;
		float num3 = 0f;
		int num4 = 0;
		float num5 = 0f;
		for (int i = 0; i < num2; i++)
		{
			num5 += forgingDrop[i] * (1f + num * forgingLucky[i]);
		}
		float num6 = Random.value * num5;
		for (int j = 0; j < num2; j++)
		{
			num3 += forgingDrop[j] * (1f + num * forgingLucky[j]);
			if (num6 < num3)
			{
				num4 = j;
				break;
			}
		}
		Dictionary<int, ForgingData> forgingDataDic = Game.GameData.ForgingDataDic;
		RoguelikeUIData[] array = new RoguelikeUIData[3];
		if (num4 == 3 || num4 == 4)
		{
			int quality = num4 - 2;
			List<int> forgingRandomList = this.GetForgingRandomList(forgingDataDic, quality);
			HashSet<string> usedPairKeys = new HashSet<string>();
			num2 = forgingRandomList.Count;
			for (int k = 0; k < num2; k++)
			{
				int num7 = Random.Range(0, num2);
				List<int> list = forgingRandomList;
				int index = k;
				List<int> list2 = forgingRandomList;
				int index2 = num7;
				int value = forgingRandomList[num7];
				int value2 = forgingRandomList[k];
				list[index] = value;
				list2[index2] = value2;
			}
			for (int l = 0; l < 3; l++)
			{
				ForgingData firstForging = forgingDataDic[forgingRandomList[l]];
				ForgingData compositeSecondForging = this.GetCompositeSecondForging(forgingDataDic, forgingRandomList, firstForging, usedPairKeys);
				array[l] = this.CreateCompositeForgingRoguelikeData(firstForging, compositeSecondForging, num4, isGold);
			}
		}
		else
		{
			List<int> forgingRandomList2 = this.GetForgingRandomList(forgingDataDic, num4);
			num2 = forgingRandomList2.Count;
			for (int m = 0; m < num2; m++)
			{
				int num8 = Random.Range(0, num2);
				List<int> list2 = forgingRandomList2;
				int index2 = m;
				List<int> list3 = forgingRandomList2;
				int index = num8;
				int value2 = forgingRandomList2[num8];
				int value = forgingRandomList2[m];
				list2[index2] = value2;
				list3[index] = value;
			}
			for (int n = 0; n < 3; n++)
			{
				int key = forgingRandomList2[n];
				ForgingData forgingData = forgingDataDic[key];
				array[n] = this.CreateForgingRoguelikeData(forgingData, isGold, -1);
			}
		}
		UI_Roguelike ui_Roguelike = Game.UI.OpenUI<UI_Roguelike>(null) as UI_Roguelike;
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/学习主动技能", 1f, 3f);
		ui_Roguelike.ShowRoguelike(array, new Action<RoguelikeUIData>(this.OnForgingSelect), Game.Language.Get("属性选择", ""), null, new Action(this.OnSelectForgingEnd), 0f, null, "forging");
		MySystemEvent.Instance.DispatchMessage<int>(29, isGold ? 10 : 1);
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x00029490 File Offset: 0x00027690
	private void OnForgingSelect(RoguelikeUIData roguelikeUIData)
	{
		if (!this.forgingManager.isInit)
		{
			this.forgingManager.isInit = true;
			GameHelperClient.AddShowBuff(StringDefine.ShowForgingData, "", "Shop/forgingBuff", -1f);
		}
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/购买药水和书", 1f, 3f);
		foreach (string forgingData in roguelikeUIData.data.Split('|', StringSplitOptions.None))
		{
			this.ApplyForgingData(forgingData);
		}
		UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
		if (ui == null)
		{
			return;
		}
		ui.RefreshBaoJi();
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x00029526 File Offset: 0x00027726
	private void OnSelectForgingEnd()
	{
		Game.UI.GetUI<UI_Shop>().OpenAnim(false);
	}

	// Token: 0x04000947 RID: 2375
	public const string ShopSpecialForging = "forging";

	// Token: 0x04000948 RID: 2376
	public const string ShopSpecialForgingGold = "forging_gold";

	// Token: 0x04000949 RID: 2377
	public const string ShopSpecialArtifact = "artifact";

	// Token: 0x0400094A RID: 2378
	public const string ShopSpecialEquipStreng = "equipStreng";

	// Token: 0x0400094B RID: 2379
	public const string ShopSpecialDropGold = "dropGold";

	// Token: 0x0400094C RID: 2380
	public const string ShopSpecialEmpty = "empty";

	// Token: 0x0400094D RID: 2381
	public List<ShopItem> buyBookList = new List<ShopItem>(8);

	// Token: 0x0400094E RID: 2382
	public List<ShopItem> buyItemList = new List<ShopItem>(8);

	// Token: 0x0400094F RID: 2383
	public List<ShopItem> buyMedicineList = new List<ShopItem>(8);

	// Token: 0x04000950 RID: 2384
	public ForgingManager forgingManager = new ForgingManager();

	// Token: 0x04000951 RID: 2385
	private int roguelikeIndex;

	// Token: 0x04000952 RID: 2386
	private List<int> randomData;

	// Token: 0x04000953 RID: 2387
	private const char ForgingComboDataSeparator = '|';

	// Token: 0x02000151 RID: 337
	public enum ShopBuyResult
	{
		// Token: 0x04000955 RID: 2389
		Fail,
		// Token: 0x04000956 RID: 2390
		Success,
		// Token: 0x04000957 RID: 2391
		SuccessNoShopCost
	}
}
