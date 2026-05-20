using System;
using System.Collections.Generic;
using System.Reflection;
using Mirror;
using UnityEngine;

// Token: 0x020000CD RID: 205
[DevPriority(0)]
public class DevToolTest
{
	// Token: 0x060003A3 RID: 931 RVA: 0x000178D5 File Offset: 0x00015AD5
	[DevConsole("角色/属性/增加MP")]
	public static void AddRoleMP()
	{
		GameHelperClient.localPlayer.AddMp(GameHelperClient.localPlayer.maxMp);
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x000178EB File Offset: 0x00015AEB
	[DevConsole("角色/属性/测试面板")]
	public static void TestDebugInfo()
	{
		Game.UI.OpenUI<UI_RoleInfoDebug>(null);
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x000178F9 File Offset: 0x00015AF9
	[DevConsole("角色/属性/增加骷髅币")]
	public static void AddRoleZhuan()
	{
		GameHelperClient.localPlayer.AddGem(GameHelperClient.localPlayer.GetHeadUIPos(), 100000, false);
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00017915 File Offset: 0x00015B15
	[DevConsole("角色/属性/1000金币")]
	public static void AddGold()
	{
		GameHelperClient.localPlayer.AddGold(GameHelperClient.localPlayer.GetHeadUIPos(), 1000, true);
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x00017932 File Offset: 0x00015B32
	[DevConsole("角色/属性/10000金币")]
	public static void AddGold10000()
	{
		GameHelperClient.localPlayer.AddGold(GameHelperClient.localPlayer.GetHeadUIPos(), 10000, true);
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x0001794F File Offset: 0x00015B4F
	[DevConsole("角色/属性/999999999金币")]
	public static void AddGold999999999()
	{
		GameHelperClient.localPlayer.AddGold(GameHelperClient.localPlayer.GetHeadUIPos(), 999999999, true);
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x0001796C File Offset: 0x00015B6C
	[DevConsole("角色/属性/一刀斩")]
	public static void OneKill()
	{
		GameHelperClient.localPlayer.mAttackPower = 100000;
	}

	// Token: 0x060003AA RID: 938 RVA: 0x0001797D File Offset: 0x00015B7D
	[DevConsole("角色/属性/无敌")]
	public static void WuDi()
	{
		GameHelperClient.localPlayer.wudi = true;
	}

	// Token: 0x060003AB RID: 939 RVA: 0x0001798A File Offset: 0x00015B8A
	[DevConsole("角色/属性/加10000经验")]
	public static void AddExp()
	{
		GameHelperClient.localPlayer.GainExp(10000);
	}

	// Token: 0x060003AC RID: 940 RVA: 0x0001799C File Offset: 0x00015B9C
	[DevConsole("角色/属性/加100力量")]
	public static void AddStr()
	{
		GameHelperClient.localPlayer.AddSTR(100);
	}

	// Token: 0x060003AD RID: 941 RVA: 0x000179AA File Offset: 0x00015BAA
	[DevConsole("角色/属性/加100敏捷")]
	public static void AddAgi()
	{
		GameHelperClient.localPlayer.AddAGI(100);
	}

	// Token: 0x060003AE RID: 942 RVA: 0x000179B8 File Offset: 0x00015BB8
	[DevConsole("角色/属性/加100耐力")]
	public static void AddSta()
	{
		GameHelperClient.localPlayer.AddSTA(100);
	}

	// Token: 0x060003AF RID: 943 RVA: 0x000179C6 File Offset: 0x00015BC6
	[DevConsole("角色/属性/加1000三围")]
	public static void Add1000San()
	{
		GameHelperClient.localPlayer.AddSTA(1000);
		GameHelperClient.localPlayer.AddSTR(1000);
		GameHelperClient.localPlayer.AddAGI(1000);
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x000179F5 File Offset: 0x00015BF5
	[DevConsole("角色/属性/加100三围")]
	public static void Add100San()
	{
		GameHelperClient.localPlayer.AddSTA(100);
		GameHelperClient.localPlayer.AddSTR(100);
		GameHelperClient.localPlayer.AddAGI(100);
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x00017A1C File Offset: 0x00015C1C
	[DevConsole("角色/属性/一键开测")]
	public static void AddFuckingTest()
	{
		GameHelperClient.localPlayer.AddSTA(1300);
		GameHelperClient.localPlayer.AddSTR(1300);
		GameHelperClient.localPlayer.AddAGI(1300);
		ShopManager.OnBuyEquipSuccess("equip_6", 0, null);
		ShopManager.OnBuyEquipSuccess("equip_6", 0, null);
		ShopManager.OnBuyEquipSuccess("equip_14", 0, null);
		ShopManager.OnBuyEquipSuccess("equip_14", 0, null);
		ShopManager.OnBuyEquipSuccess("equip_14", 0, null);
		ShopManager.OnBuyEquipSuccess("equip_12", 0, null);
		GameHelperClient.localPlayer.AddGold(Vector3.zero, 99999999, true);
		GameHelperClient.localPlayer.AddGem(Vector3.zero, 99999999, false);
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00017AD0 File Offset: 0x00015CD0
	[DevConsole("角色/属性/全伤害增加10%")]
	public static void AddAllDamage()
	{
		GameHelperClient.localPlayer.addDamagePercent += 0.1f;
		GameHelperClient.localPlayer.normalBreakShieldBase += 0.2f;
		GameHelperClient.localPlayer.skillBreakShieldBase += 0.2f;
		GameHelperClient.localPlayer.skillExDamage += 0.15f;
		GameHelperClient.localPlayer.normalAttackAddDamage += 0.15f;
		GameHelperClient.localPlayer.UpdateBreakShield();
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00017B58 File Offset: 0x00015D58
	[DevConsole("角色/装备/词条测试")]
	public static void EquipEvolutionA()
	{
		DevToolTest.AddEvolutionAttributeToFirstEquip("attackDistance", GameHelperClient.localPlayer.playerAttribute.equipList[0].equipIndex);
		for (int i = 0; i < 10; i++)
		{
			GameHelperClient.localPlayer.CmdCreateItemByPos(ItemType.EquipAdd_10, GameHelperClient.localPlayer.MyTransform.position);
		}
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00017BB4 File Offset: 0x00015DB4
	public static void AddEvolutionAttributeToFirstEquip(string equipDefine, string sourceEquip = "")
	{
		EquipBase firstEquipForEvolutionTest = DevToolTest.GetFirstEquipForEvolutionTest();
		if (firstEquipForEvolutionTest == null || string.IsNullOrEmpty(equipDefine))
		{
			return;
		}
		EquipEvolutionEntryData equipEvolutionEntryData;
		if (!DevToolTest.TryGetEvolutionAttributeEntry(equipDefine, sourceEquip, out equipEvolutionEntryData))
		{
			UI_Msg ui = Game.UI.GetUI<UI_Msg>();
			if (ui == null)
			{
				return;
			}
			ui.ShowMsg("未找到测试进化词条:" + equipDefine, false);
			return;
		}
		else
		{
			equipEvolutionEntryData.ApplyTo(firstEquipForEvolutionTest);
			GameHelperClient.localPlayer.playerAttribute.RefreshEquipPower();
			UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
			if (ui2 != null)
			{
				ui2.RefreshPlayerEquip();
			}
			UI_Msg ui3 = Game.UI.GetUI<UI_Msg>();
			if (ui3 == null)
			{
				return;
			}
			ui3.ShowMsg(string.Format("测试进化词条:{0} {1}+{2}", equipEvolutionEntryData.sourceEquip, equipDefine, equipEvolutionEntryData.attributeValue), false);
			return;
		}
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00017C5C File Offset: 0x00015E5C
	public static void AddEvolutionSkillToFirstEquip(EquipSkillType equipSkillType, string sourceEquip = "")
	{
		EquipBase firstEquipForEvolutionTest = DevToolTest.GetFirstEquipForEvolutionTest();
		if (firstEquipForEvolutionTest == null || equipSkillType == EquipSkillType.None)
		{
			return;
		}
		EquipEvolutionEntryData equipEvolutionEntryData;
		if (!DevToolTest.TryGetEvolutionSkillEntry(equipSkillType, sourceEquip, out equipEvolutionEntryData))
		{
			UI_Msg ui = Game.UI.GetUI<UI_Msg>();
			if (ui == null)
			{
				return;
			}
			ui.ShowMsg(string.Format("未找到测试进化技能:{0}", equipSkillType), false);
			return;
		}
		else
		{
			bool flag = firstEquipForEvolutionTest.HasEquipSkill(equipSkillType);
			equipEvolutionEntryData.ApplyTo(firstEquipForEvolutionTest);
			if (!flag && firstEquipForEvolutionTest.HasEquipSkill(equipSkillType))
			{
				DevToolTest.InvokeAddEquipSkill(firstEquipForEvolutionTest, equipSkillType);
			}
			UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
			if (ui2 != null)
			{
				ui2.RefreshPlayerEquip();
			}
			UI_Msg ui3 = Game.UI.GetUI<UI_Msg>();
			if (ui3 == null)
			{
				return;
			}
			ui3.ShowMsg(string.Format("测试进化技能:{0} {1}", equipEvolutionEntryData.sourceEquip, equipSkillType), false);
			return;
		}
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00017D08 File Offset: 0x00015F08
	private static EquipBase GetFirstEquipForEvolutionTest()
	{
		PlayerBase localPlayer = GameHelperClient.localPlayer;
		if (localPlayer == null || localPlayer.playerAttribute == null || localPlayer.playerAttribute.equipList.Count == 0)
		{
			UI_Msg ui = Game.UI.GetUI<UI_Msg>();
			if (ui != null)
			{
				ui.ShowMsg("当前没有装备", false);
			}
			return null;
		}
		return localPlayer.playerAttribute.equipList[0];
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x00017D6C File Offset: 0x00015F6C
	private static void InvokeAddEquipSkill(EquipBase equip, EquipSkillType equipSkillType)
	{
		PlayerAttribute playerAttribute = GameHelperClient.localPlayer.playerAttribute;
		MethodInfo method = typeof(PlayerAttribute).GetMethod("AddEquipSkill", BindingFlags.Instance | BindingFlags.NonPublic);
		if (method == null)
		{
			Debug.LogError("DevToolTest AddEquipSkill method not found");
			return;
		}
		method.Invoke(playerAttribute, new object[]
		{
			equip,
			equipSkillType
		});
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x00017DCC File Offset: 0x00015FCC
	private static bool TryGetEvolutionAttributeEntry(string equipDefine, string sourceEquip, out EquipEvolutionEntryData entry)
	{
		return DevToolTest.TryGetEvolutionEntry(sourceEquip, (EquipEvolutionEntryData data) => data.IsAttribute && data.attributeType == equipDefine, out entry);
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x00017DFC File Offset: 0x00015FFC
	private static bool TryGetEvolutionSkillEntry(EquipSkillType equipSkillType, string sourceEquip, out EquipEvolutionEntryData entry)
	{
		return DevToolTest.TryGetEvolutionEntry(sourceEquip, (EquipEvolutionEntryData data) => data.IsSkill && data.equipSkill == equipSkillType, out entry);
	}

	// Token: 0x060003BA RID: 954 RVA: 0x00017E2C File Offset: 0x0001602C
	private static bool TryGetEvolutionEntry(string sourceEquip, Func<EquipEvolutionEntryData, bool> match, out EquipEvolutionEntryData entry)
	{
		entry = null;
		object obj;
		if (!ExcelManager.allExcelData.TryGetValue("equipEvolutionEntry", out obj))
		{
			return false;
		}
		Dictionary<string, object> dictionary = obj as Dictionary<string, object>;
		if (dictionary == null)
		{
			return false;
		}
		string[] array;
		if (!string.IsNullOrEmpty(sourceEquip))
		{
			(array = new string[1])[0] = sourceEquip;
		}
		else
		{
			string[] array2 = new string[3];
			array2[0] = "123";
			array2[1] = "124";
			array = array2;
			array2[2] = "125";
		}
		string[] array3 = array;
		for (int i = 0; i < array3.Length; i++)
		{
			foreach (object data in dictionary.Values)
			{
				EquipEvolutionEntryData equipEvolutionEntryData = EquipEvolutionEntryData.Create(data);
				if (!(equipEvolutionEntryData.sourceEquip != array3[i]) && match(equipEvolutionEntryData))
				{
					entry = equipEvolutionEntryData;
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060003BB RID: 955 RVA: 0x00017F0C File Offset: 0x0001610C
	[DevConsole("角色/属性/一键开测（物理）")]
	public static void AddFuckingTestNormal()
	{
		GameHelperClient.localPlayer.AddSTA(3000);
		GameHelperClient.localPlayer.AddSTR(3000);
		GameHelperClient.localPlayer.AddAGI(3000);
		ShopManager.OnBuyEquipSuccess("equip_113", 0, null);
		ShopManager.OnBuyEquipSuccess("equip_114", 0, null);
		ShopManager.OnBuyEquipSuccess("equip_1", 0, null);
		ShopManager.OnBuyEquipSuccess("equip_2", 0, null);
		ShopManager.OnBuyEquipSuccess("equip_25", 0, null);
		GameHelperClient.localPlayer.AddGold(Vector3.zero, 99999999, true);
		GameHelperClient.localPlayer.AddGem(Vector3.zero, 99999999, false);
		GameHelperClient.localPlayer.AddPasssiveSkillBook(PasssiveSkillEnum.A多重攻击, null);
		GameHelperClient.localPlayer.AddPasssiveSkillBook(PasssiveSkillEnum.A究超长臂猿, null);
		DevToolTest.OneKill();
		DevToolTest.WuDi();
	}

	// Token: 0x060003BC RID: 956 RVA: 0x00017FDC File Offset: 0x000161DC
	[DevConsole("召唤/赌徒")]
	public static void AddGoblin_BlacksmithA()
	{
		Util.ShowTips("tip_tie");
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.Goblin_Blacksmith_0, true);
	}

	// Token: 0x060003BD RID: 957 RVA: 0x00017FF8 File Offset: 0x000161F8
	[DevConsole("召唤/巨魔")]
	public static void AddGoblin_HellFlameA()
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.Goblin_HellFlame_0, true);
	}

	// Token: 0x060003BE RID: 958 RVA: 0x0001800A File Offset: 0x0001620A
	[DevConsole("召唤/金币怪")]
	public static void AddGoblin_LocalTyrant()
	{
		GameHelperClient.localPlayer.CmdCreateLocalTyrant(1);
	}

	// Token: 0x060003BF RID: 959 RVA: 0x00018017 File Offset: 0x00016217
	[DevConsole("召唤/飞艇")]
	public static void AddGoblin_Mine()
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.Goblin_Mine_0, false);
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x00018029 File Offset: 0x00016229
	[DevConsole("召唤/心魔")]
	public static void AddCreateHeartDemon()
	{
		GameHelperClient.localPlayer.CmdCreateHeartDemon(1);
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00018036 File Offset: 0x00016236
	[DevConsole("召唤/哥布林大王1")]
	public static void AddGoblin_BOSS1()
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.Goblin_Boss_0, false);
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00018045 File Offset: 0x00016245
	[DevConsole("召唤/哥布林战神")]
	public static void AddGoblin_Warrior()
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.Goblin_Warrior_1, false);
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00018057 File Offset: 0x00016257
	[DevConsole("召唤/哥布林长老")]
	public static void AddGoblin_Elder()
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.Goblin_Elder_2, false);
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00018069 File Offset: 0x00016269
	[DevConsole("召唤/哥布林大王4")]
	public static void AddGoblin_BOSS4()
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.Goblin_Boss_3, false);
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x00018078 File Offset: 0x00016278
	[DevConsole("召唤/塞亚")]
	public static void AddSaiYa()
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.SaiYa, false);
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x00018086 File Offset: 0x00016286
	[DevConsole("召唤/黑暗塞亚")]
	public static void AddSaiYaDark()
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.SaiYaDark, false);
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x00018098 File Offset: 0x00016298
	[DevConsole("召唤/国王")]
	public static void AddKing()
	{
		GameHelperClient.localPlayer.StartSummon(EnemyType.NPC_King, GameHelperClient.localPlayer.MyTransform.position, GameHelperClient.localPlayer.netId, 1f, 1000L, 50, 60f, null, 0L, 0L, -1);
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x000180E8 File Offset: 0x000162E8
	[DevConsole("召唤/幽灵")]
	public static void AddGhost()
	{
		GameHelperClient.localPlayer.StartSummon(EnemyType.NPC_Ghost, GameHelperClient.localPlayer.MyTransform.position + new Vector3(1.5f, 0f, 1.5f), GameHelperClient.localPlayer.netId, 1f, 1000L, 50, 9999f, null, 0L, 0L, -1);
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x0001814E File Offset: 0x0001634E
	[DevConsole("召唤/哥布林将军")]
	public static void AddGoblin_General()
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.Goblin_General_0, false);
	}

	// Token: 0x060003CA RID: 970 RVA: 0x00018160 File Offset: 0x00016360
	[DevConsole("召唤/泰坦")]
	public static void AddColossus()
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.Colossus, false);
	}

	// Token: 0x060003CB RID: 971 RVA: 0x0001816F File Offset: 0x0001636F
	[DevConsole("召唤/巫妖")]
	public static void AddNecromancer()
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.Necromancer, false);
	}

	// Token: 0x060003CC RID: 972 RVA: 0x0001817E File Offset: 0x0001637E
	[DevConsole("召唤/死灵法师")]
	public static void AddWraith()
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.Wraith, false);
	}

	// Token: 0x060003CD RID: 973 RVA: 0x0001818D File Offset: 0x0001638D
	[DevConsole("召唤/骷髅士兵")]
	public static void AddSkeletonSoldier()
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.SkeletonSoldier, false);
	}

	// Token: 0x060003CE RID: 974 RVA: 0x0001819C File Offset: 0x0001639C
	[DevConsole("召唤/森林祭祀")]
	public static void AddForestGuardian()
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.ForestGuardian, false);
	}

	// Token: 0x060003CF RID: 975 RVA: 0x000181AB File Offset: 0x000163AB
	[DevConsole("召唤/宝箱怪")]
	public static void AddChest()
	{
		GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.Chest, false);
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x000181BC File Offset: 0x000163BC
	[DevConsole("召唤/陷阱")]
	public static void CreateTrapSpears()
	{
		GameHelperClient.localPlayer.CmdCreateSkill(ActiveSkillEnum.TrapSpears, GameHelperClient.localPlayer.MyTransform.position + GameHelperClient.localPlayer.MyTransform.forward * 3f, 0f, 0, 0);
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x0001820C File Offset: 0x0001640C
	[DevConsole("角色/书籍/技能书")]
	public static void GetBook()
	{
		GameHelperClient.localPlayer.playerAttribute.AddBook(ItemType.Active_Book_D, BagItemType.Book, "book_1");
		GameHelperClient.localPlayer.playerAttribute.AddBook(ItemType.Passsive_Book_D, BagItemType.Book, "book_2");
		GameHelperClient.localPlayer.playerAttribute.AddBook(ItemType.Talisman_Experience, BagItemType.HuFu, "");
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x00018267 File Offset: 0x00016467
	[DevConsole("角色/书籍/S级技能书")]
	public static void GetSBook()
	{
		GameHelperClient.localPlayer.playerAttribute.AddBook(ItemType.Active_Book_S, BagItemType.Book, "sbook1");
		GameHelperClient.localPlayer.playerAttribute.AddBook(ItemType.Passsive_Book_S, BagItemType.Book, "sbook2");
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x0001829D File Offset: 0x0001649D
	[DevConsole("角色/神符/咆哮神符")]
	public static void AddPaoXiao()
	{
		GameHelperClient.localPlayer.CmdCreateItemByPos(ItemType.Talisman_Roar, GameHelperClient.localPlayer.MyTransform.position);
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x000182BD File Offset: 0x000164BD
	[DevConsole("角色/神符/护盾神符")]
	public static void AddHuDun()
	{
		GameHelperClient.localPlayer.CmdCreateItemByPos(ItemType.Talisman_Shield, GameHelperClient.localPlayer.MyTransform.position);
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x000182DD File Offset: 0x000164DD
	[DevConsole("角色/神符/魔法神符")]
	public static void AddMagic()
	{
		GameHelperClient.localPlayer.CmdCreateItemByPos(ItemType.Talisman_Magic, GameHelperClient.localPlayer.MyTransform.position);
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x000182FD File Offset: 0x000164FD
	[DevConsole("角色/物品/创建经验神符")]
	public static void AddTalismanExperience()
	{
		GameHelperClient.localPlayer.CmdCreateItemByPos(ItemType.Talisman_Experience, GameHelperClient.localPlayer.MyTransform.position);
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x0001831D File Offset: 0x0001651D
	[DevConsole("角色/物品/塞亚的灵魂")]
	public static void AddHeroSoul()
	{
		GameHelperClient.localPlayer.CmdCreateItemByPos(ItemType.HeroSoul, GameHelperClient.localPlayer.MyTransform.position);
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x0001833D File Offset: 0x0001653D
	[DevConsole("角色/物品/勇者之剑")]
	public static void AddHeroSword()
	{
		GameHelperClient.localPlayer.CmdCreateItemByPos(ItemType.HeroSword, GameHelperClient.localPlayer.MyTransform.position);
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x0001835D File Offset: 0x0001655D
	[DevConsole("角色/物品/勇者绝技")]
	public static void AddHeroBook()
	{
		GameHelperClient.localPlayer.CmdCreateItemByPos(ItemType.HeroBook, GameHelperClient.localPlayer.MyTransform.position);
	}

	// Token: 0x060003DA RID: 986 RVA: 0x0001837D File Offset: 0x0001657D
	[DevConsole("角色/物品/创建1000金币")]
	public static void CreateGoldItem1000()
	{
		GameHelperClient.localPlayer.CmdCreateItemByPosWithNum(ItemType.Gold, GameHelperClient.localPlayer.MyTransform.position, 1000, true);
	}

	// Token: 0x060003DB RID: 987 RVA: 0x000183A3 File Offset: 0x000165A3
	[DevConsole("角色/物品/创建10骷髅币")]
	public static void CreateGemItem10()
	{
		GameHelperClient.localPlayer.CmdCreateItemByPosWithNum(ItemType.Gem, GameHelperClient.localPlayer.MyTransform.position, 10, true);
	}

	// Token: 0x060003DC RID: 988 RVA: 0x000183C6 File Offset: 0x000165C6
	[DevConsole("角色/物品/丢弃1000金币")]
	public static void DropGold1000()
	{
		GameHelperClient.localPlayer.DropGold(1000);
	}

	// Token: 0x060003DD RID: 989 RVA: 0x000183D7 File Offset: 0x000165D7
	[DevConsole("角色/物品/丢弃10骷髅币")]
	public static void DropGem10()
	{
		GameHelperClient.localPlayer.DropGem(10);
	}

	// Token: 0x060003DE RID: 990 RVA: 0x000183E5 File Offset: 0x000165E5
	[DevConsole("角色/物品/掉落卡牌")]
	public static void DropCard()
	{
		GameHelperClient.localPlayer.CmdCreateItemByPos(ItemType.Card_0 + Random.Range(0, 10), GameHelperClient.localPlayer.MyTransform.position);
	}

	// Token: 0x060003DF RID: 991 RVA: 0x00018410 File Offset: 0x00016610
	[DevConsole("角色/物品/获得所有卡牌")]
	public static void GetAllCard()
	{
		Dictionary<int, CardManager.HaveCardData> haveCardDataDic = SaveLoadManager.haveCardDataDic;
		foreach (int num in Game.GameData.CardDataDic.Keys)
		{
			haveCardDataDic[num] = new CardManager.HaveCardData
			{
				cardId = num,
				haveNum = 99
			};
		}
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x0001848C File Offset: 0x0001668C
	[DevConsole("角色/物品/沉睡之石")]
	public static void AddSleepingStone()
	{
		GameHelperClient.localPlayer.CmdCreateItemByPos(ItemType.SleepingStone, GameHelperClient.localPlayer.MyTransform.position);
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x000184AC File Offset: 0x000166AC
	[DevConsole("角色/物品/获得20万记忆")]
	public static void SaveJiYi()
	{
		SaveLoadManager.SaveJiYi(200000L);
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x000184BC File Offset: 0x000166BC
	[DevConsole("角色/物品/药水")]
	public static void CreateMedicine()
	{
		for (ItemType itemType = ItemType.Medicine_0; itemType <= ItemType.Medicine_7; itemType++)
		{
			GameHelperClient.localPlayer.CmdCreateItemByPos(itemType, GameHelperClient.localPlayer.MyTransform.position);
		}
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x000184F8 File Offset: 0x000166F8
	[DevConsole("角色/物品/PK药水")]
	public static void CreatePKMedicine()
	{
		for (int i = 0; i <= 3; i++)
		{
			GameHelperClient.localPlayer.CmdCreateItemByPos(ItemType.Medicine_0, GameHelperClient.localPlayer.MyTransform.position);
			GameHelperClient.localPlayer.CmdCreateItemByPos(ItemType.Medicine_2, GameHelperClient.localPlayer.MyTransform.position);
		}
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x0001854D File Offset: 0x0001674D
	[DevConsole("角色/显示/buff测试")]
	public static void TestBuffShow()
	{
		GameHelperClient.AddShowBuff("测试", "描述测试", "Amulet/好运神符", 3f);
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x00018569 File Offset: 0x00016769
	[DevConsole("角色/存档/删除存档")]
	public static void DeleteSave()
	{
		SaveLoadManager.OnDeleteSave();
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x00018570 File Offset: 0x00016770
	[DevConsole("角色/掉落/宝箱怪/阶段0")]
	public static void DropChest0()
	{
		DevToolTest.DropItemList("chest_0");
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x0001857C File Offset: 0x0001677C
	[DevConsole("角色/掉落/宝箱怪/阶段1")]
	public static void DropChest1()
	{
		DevToolTest.DropItemList("chest_1");
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00018588 File Offset: 0x00016788
	[DevConsole("角色/掉落/宝箱怪/阶段2")]
	public static void DropChest2()
	{
		DevToolTest.DropItemList("chest_2");
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x00018594 File Offset: 0x00016794
	[DevConsole("角色/掉落/宝箱怪/阶段3")]
	public static void DropChest3()
	{
		DevToolTest.DropItemList("chest_3");
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x000185A0 File Offset: 0x000167A0
	[DevConsole("角色/掉落/宝箱怪/阶段4")]
	public static void DropChest4()
	{
		DevToolTest.DropItemList("chest_4");
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x000185AC File Offset: 0x000167AC
	[DevConsole("角色/掉落/宝箱怪/大量掉落")]
	public static void DropAll()
	{
		for (int i = 0; i < 20; i++)
		{
			DevToolTest.DropItemList("chest_4");
		}
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x000185D0 File Offset: 0x000167D0
	[DevConsole("角色/掉落/Boss/Boss掉落")]
	public static void DropBoss()
	{
		DevToolTest.DropItemList("boss_normal");
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x000185DC File Offset: 0x000167DC
	private static void DropItemList(string dropKey)
	{
		foreach (ItemType itemType in Util.GetDropItem(dropKey, Util.GetLuckAddValue(GameHelperClient.localPlayer.lucky)))
		{
			Vector2 pointByRadian = Util.GetPointByRadian(0f, Random.value * 2f, Random.value * 360f);
			GameHelperClient.localPlayer.CmdCreateItemByPos(itemType, new Vector3(GameHelperClient.localPlayer.MyTransform.position.x + pointByRadian.x, GameHelperClient.localPlayer.MyTransform.position.y, GameHelperClient.localPlayer.MyTransform.position.z + pointByRadian.y));
		}
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x000186B8 File Offset: 0x000168B8
	[DevConsole("技能/技能面板")]
	public static void SkillPanel()
	{
		Game.UI.OpenUI<UI_SkillBook>(null);
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x000186C8 File Offset: 0x000168C8
	[DevConsole("技能/清除技能CD")]
	public static void SkillCDClear()
	{
		for (int i = 0; i < GameHelperClient.localPlayer.roleSkillList.Count; i++)
		{
			SkillBase skillBase = GameHelperClient.localPlayer.roleSkillList[i];
			if (!(skillBase is PasssiveSkill))
			{
				skillBase.updateCd = 0f;
			}
		}
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00018713 File Offset: 0x00016913
	[DevConsole("商店/清除CD")]
	public static void ClearShopCD()
	{
		ShopManager ui = Game.UI.GetUI<ShopManager>();
		if (ui == null)
		{
			return;
		}
		ui.ClearAllShopCD();
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x00018729 File Offset: 0x00016929
	[DevConsole("商店/S技能书")]
	public static void BuySkillBook()
	{
		GameHelperClient.localPlayer.playerAttribute.AddBook(ItemType.Active_Book_S, BagItemType.Book, "sbook1");
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x00018745 File Offset: 0x00016945
	[DevConsole("商店/遗物和神器")]
	public static void RelicAndEquip()
	{
		Game.UI.OpenUI<UI_RelicTool>(null);
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x00018753 File Offset: 0x00016953
	[DevConsole("商店/装备强化")]
	public static void EquipStreng()
	{
		(Game.UI.OpenUI<UI_EquipStreng>(null) as UI_EquipStreng).SetStrengItemType(ItemType.None);
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x0001876B File Offset: 0x0001696B
	[DevConsole("复活")]
	public static void FuHuo()
	{
		if (GameHelperClient.localPlayer != null && GameHelperClient.localPlayer.IsDead())
		{
			GameHelperClient.localPlayer.CmdRelife();
		}
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x00018790 File Offset: 0x00016990
	[DevConsole("战斗测试工具/取消出怪")]
	public static void CloseEnemyCreate()
	{
		(NetworkManager.singleton as MyServerNetworkManager).TestCloseEnemyCreate();
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x000187A4 File Offset: 0x000169A4
	[DevConsole("战斗测试工具/生成假人1千万血")]
	public static void CloseEnemyDummy1000()
	{
		GameHelperClient.localPlayer.CmdDummy(EnemyType.Dummy, GameHelperClient.localPlayer.MyTransform.position + GameHelperClient.localPlayer.MyTransform.forward * 3f, GameHelperClient.localPlayer.netId, 0f, 9999999, 0, 0f);
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x00018804 File Offset: 0x00016A04
	[DevConsole("战斗测试工具/生成假人1百万血")]
	public static void CloseEnemyDummy100()
	{
		GameHelperClient.localPlayer.CmdDummy(EnemyType.Dummy, GameHelperClient.localPlayer.MyTransform.position + GameHelperClient.localPlayer.MyTransform.forward * 3f, GameHelperClient.localPlayer.netId, 0f, 999999, 0, 0f);
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x00018864 File Offset: 0x00016A64
	[DevConsole("战斗测试工具/生成假人10万血")]
	public static void CloseEnemyDummy10()
	{
		GameHelperClient.localPlayer.CmdDummy(EnemyType.Dummy, GameHelperClient.localPlayer.MyTransform.position + GameHelperClient.localPlayer.MyTransform.forward * 3f, GameHelperClient.localPlayer.netId, 0f, 99999, 0, 0f);
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x000188C4 File Offset: 0x00016AC4
	[DevConsole("战斗测试工具/生成假人1万血")]
	public static void CloseEnemyDummy1()
	{
		GameHelperClient.localPlayer.CmdDummy(EnemyType.Dummy, GameHelperClient.localPlayer.MyTransform.position + GameHelperClient.localPlayer.MyTransform.forward * 3f, GameHelperClient.localPlayer.netId, 0f, 9999, 0, 0f);
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x00018924 File Offset: 0x00016B24
	[DevConsole("战斗测试工具/生成假人1千血")]
	public static void CloseEnemyDummy()
	{
		GameHelperClient.localPlayer.CmdDummy(EnemyType.Dummy, GameHelperClient.localPlayer.MyTransform.position + GameHelperClient.localPlayer.MyTransform.forward * 3f, GameHelperClient.localPlayer.netId, 0f, 999, 0, 0f);
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00018983 File Offset: 0x00016B83
	[DevConsole("战斗测试工具/降低30%血量")]
	public static void CouwnHp30()
	{
		GameHelperClient.localPlayer.CmdUpdateHp(-ConstDefine.ClampBattleValue((double)GameHelperClient.localPlayer.maxHp * 0.3), GameHelperClient.localPlayer.netId, -1);
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x000189B5 File Offset: 0x00016BB5
	[DevConsole("肉鸽/选择遗物")]
	public static void ShowRoguelikeRelic()
	{
		Util.ShowRemainsRoguelike(null, 0f);
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x000189C2 File Offset: 0x00016BC2
	[DevConsole("肉鸽/选择挑战")]
	public static void ShowRoguelikeMonster()
	{
		Game.UI.GetUI<UI_Shop>().OnBuyMonsterBtnClick(GameHelperClient.BuyMonsterIndex);
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x000189D8 File Offset: 0x00016BD8
	[DevConsole("肉鸽/恶魔契约")]
	public static void OnDemonContract()
	{
		Util.OnDemonContract(null);
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x000189E0 File Offset: 0x00016BE0
	[DevConsole("肉鸽/增加100刷新")]
	public static void AddRefresh()
	{
		GameHelperClient.RefreshNum += 100;
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x000189EF File Offset: 0x00016BEF
	[DevConsole("肉鸽/获得金币和骷髅币")]
	public static void DebugGoldGem()
	{
		Debug.LogError("共获得金币：" + GameHelperClient.localPlayer.getGoldNum.ToString() + "    共获得头骨：" + GameHelperClient.localPlayer.getGemNum.ToString());
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x00018A23 File Offset: 0x00016C23
	[DevConsole("肉鸽/关闭肉鸽")]
	public static void CloseRoguelike()
	{
		Game.UI.GetUI<UI_Roguelike>().DevCloseRoguelike();
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x00018A34 File Offset: 0x00016C34
	[DevConsole("肉鸽/失去遗物")]
	public static void RemoveRelic()
	{
		PlayerBase localPlayer = GameHelperClient.localPlayer;
		List<RelicBase> relicList = GameHelperClient.localPlayer.playerAttribute.relicList;
		localPlayer.RemoveRelic(relicList[relicList.Count - 1]);
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x00018A5C File Offset: 0x00016C5C
	[DevConsole("肉鸽/升级所有遗物")]
	public static void AddRelicLevel()
	{
		foreach (RelicBase relicBase in GameHelperClient.localPlayer.playerAttribute.relicList)
		{
			relicBase.OnLevelUp();
		}
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x00018AB8 File Offset: 0x00016CB8
	[DevConsole("肉鸽/升级所有技能")]
	public static void AddSkillLevel()
	{
		foreach (SkillBase skillBase in GameHelperClient.localPlayer.roleSkillList)
		{
			skillBase.OnLevelUp();
		}
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x00018B0C File Offset: 0x00016D0C
	[DevConsole("幸运值/100幸运")]
	public static void Lucky100()
	{
		GameHelperClient.localPlayer.Networklucky = 100;
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x00018B1A File Offset: 0x00016D1A
	[DevConsole("幸运值/200幸运")]
	public static void Lucky200()
	{
		GameHelperClient.localPlayer.Networklucky = 200;
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x00018B2B File Offset: 0x00016D2B
	[DevConsole("幸运值/300幸运")]
	public static void Lucky300()
	{
		GameHelperClient.localPlayer.Networklucky = 300;
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x00018B3C File Offset: 0x00016D3C
	[DevConsole("幸运值/400幸运")]
	public static void Lucky400()
	{
		GameHelperClient.localPlayer.Networklucky = 400;
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x00018B4D File Offset: 0x00016D4D
	[DevConsole("幸运值/500幸运")]
	public static void Lucky500()
	{
		GameHelperClient.localPlayer.Networklucky = 500;
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x00018B5E File Offset: 0x00016D5E
	[DevConsole("镜头和加速器/隐藏时间加速器")]
	public static void HideTimeScale()
	{
		Game.UI.CloseUI<UI_TimeScale>();
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x00018B6A File Offset: 0x00016D6A
	[DevConsole("镜头和加速器/显示时间加速器")]
	public static void ShowTimeScale()
	{
		Game.UI.OpenUI<UI_TimeScale>(null);
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x00018B78 File Offset: 0x00016D78
	[DevConsole("镜头和加速器/开启自由镜头")]
	public static void OpenFreeCamera()
	{
		Util.ShowTipsNoLanguage("WASD移动镜头，按住右键旋转，空格上升,Ctrl下降，Shift加速");
		if (GameHelperClient.isFreeCamera)
		{
			return;
		}
		Game.CameraManager.MyTransform.gameObject.AddComponent<DevCameraController>();
		GameHelperClient.isFreeCamera = true;
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x00018BA7 File Offset: 0x00016DA7
	[DevConsole("镜头和加速器/关闭自由镜头")]
	public static void CloseFreeCamera()
	{
		if (!GameHelperClient.isFreeCamera)
		{
			return;
		}
		Object.Destroy(Game.CameraManager.MyTransform.gameObject.GetComponent<DevCameraController>());
		GameHelperClient.isFreeCamera = false;
		Game.CameraManager.ResetCamera();
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x00002D1D File Offset: 0x00000F1D
	[DevConsole("镜头和加速器/显隐UI(按F6)")]
	public static void CloseUI()
	{
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x00018BDA File Offset: 0x00016DDA
	[DevConsole("挑战玩家/保存当前玩家数据")]
	public static void SaveCurPlayerKingData()
	{
		Game.Save.SavePlayerKingData(Util.GetLocalPlayerKingData());
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x00018BEC File Offset: 0x00016DEC
	[DevConsole("挑战玩家/召唤挑战玩家")]
	public static void LoadPlayerKingData()
	{
		NetworkClient.connection.Send<ServerNetMessage>(new ServerNetMessage
		{
			serverNetOperation = ServerNetOperation.CreateKing,
			datas = new int[]
			{
				SaveLoadManager.playerKingSave.playerKingDataList.Count - 1,
				(int)GameHelperClient.localPlayer.netId
			}
		}, 0);
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x00018C43 File Offset: 0x00016E43
	[DevConsole("挑战玩家/清空玩家数据")]
	public static void ClearPlayerKingData()
	{
		Game.Save.ClearPlayerKingData();
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x00018C4F File Offset: 0x00016E4F
	[DevConsole("挑战玩家/将当前玩家数据改为King数据")]
	public static void LoadPlayerKingDataForLocal()
	{
		PlayerBase localPlayer = GameHelperClient.localPlayer;
		List<SaveLoadManager.PlayerKingData> playerKingDataList = SaveLoadManager.playerKingSave.playerKingDataList;
		localPlayer.LoadKingDataForLocal(playerKingDataList[playerKingDataList.Count - 1]);
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00018C72 File Offset: 0x00016E72
	[DevConsole("挑战玩家/排行榜")]
	public static void OpenRank()
	{
		Game.UI.OpenUI<UI_MyRank>(null);
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x00018C80 File Offset: 0x00016E80
	[DevConsole("挑战玩家/排行榜测试/四人150-200")]
	public static void OpenRankTestRange()
	{
		DevToolTest.OpenRankTestRange(4, 100, 150);
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00018C8F File Offset: 0x00016E8F
	public static void OpenRankTestRange(int playerCount, int startRank, int endRank)
	{
		Game.UI.OpenUI<UI_MyRank>(new MyRankTestRangeData
		{
			PlayerCount = playerCount,
			StartRank = startRank,
			EndRank = endRank
		});
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x00018CB6 File Offset: 0x00016EB6
	[DevConsole("挑战玩家/显示自己属性")]
	public static void ShowSelfKing()
	{
		(Game.UI.OpenUI<UI_KingDec>(null) as UI_KingDec).SetPlayKingData(Util.GetLocalPlayerKingData());
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x00018CD2 File Offset: 0x00016ED2
	public static void LoadPlayerKingData(SaveLoadManager.PlayerKingData playerKingData)
	{
		GameHelperClient.localPlayer.LoadKingDataForLocal(playerKingData);
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x00018CE0 File Offset: 0x00016EE0
	public static void StartKingBattle(SaveLoadManager.PlayerKingData playerKingData)
	{
		GameHelperClient.isGameOver = true;
		Game.UI.CloseUI<UI_KingDec>();
		Game.UI.CloseUI<UI_MyRank>();
		SaveLoadManager.teamBuildDataList = new List<SaveLoadManager.TeamBuildData>();
		SaveLoadManager.TeamBuildData teamBuildData = new SaveLoadManager.TeamBuildData();
		teamBuildData.members = new List<SaveLoadManager.PlayerKingData>();
		teamBuildData.members.Add(playerKingData);
		SaveLoadManager.teamBuildDataList.Add(teamBuildData);
		NetworkClient.connection.Send<ServerNetMessage>(new ServerNetMessage
		{
			serverNetOperation = ServerNetOperation.KingChallenge,
			datas = new int[1]
		}, 0);
	}
}
