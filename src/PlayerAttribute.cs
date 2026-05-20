using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200028F RID: 655
public class PlayerAttribute
{
	// Token: 0x06000C3E RID: 3134 RVA: 0x00047454 File Offset: 0x00045654
	public void Update()
	{
		for (int i = this.relicList.Count - 1; i >= 0; i--)
		{
			this.relicList[i].Update();
		}
		for (int j = this.equipList.Count - 1; j >= 0; j--)
		{
			this.equipList[j].UpdateEvent();
		}
		for (int k = this.equipSkillDic.Count - 1; k >= 0; k--)
		{
			this.equipSkillDic.ElementAt(k).Value.OnUpdate();
		}
		for (int l = this.cardSkillListDic.Count - 1; l >= 0; l--)
		{
			List<CardSkillBase> value = this.cardSkillListDic.ElementAt(l).Value;
			for (int m = value.Count - 1; m >= 0; m--)
			{
				value[m].Update();
			}
		}
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0004753E File Offset: 0x0004573E
	// (set) Token: 0x06000C40 RID: 3136 RVA: 0x00047546 File Offset: 0x00045746
	public int NowExp
	{
		get
		{
			return this.nowExp;
		}
		set
		{
			this.nowExp = value;
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0004754F File Offset: 0x0004574F
	public int maxExp
	{
		get
		{
			return (int)this.GetNeedExp(this.playerBase.Level);
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00047563 File Offset: 0x00045763
	public int Level
	{
		get
		{
			return this.playerBase.Level;
		}
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x00047570 File Offset: 0x00045770
	private int GetEquipIntAttribute(string attributeType)
	{
		int num = 0;
		foreach (EquipBase equipBase in this.equipList)
		{
			num += equipBase.GetIntAttributeValue(attributeType);
		}
		return num;
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x000475CC File Offset: 0x000457CC
	private float GetEquipFloatAttribute(string attributeType)
	{
		float num = 0f;
		foreach (EquipBase equipBase in this.equipList)
		{
			num += equipBase.GetAttributeValue(attributeType);
		}
		return num;
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x0004762C File Offset: 0x0004582C
	private float GetEquipFloatAttribute(params string[] attributeTypes)
	{
		float num = 0f;
		for (int i = 0; i < attributeTypes.Length; i++)
		{
			num += this.GetEquipFloatAttribute(attributeTypes[i]);
		}
		return num;
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x0004765A File Offset: 0x0004585A
	public int GetEquipSTR()
	{
		return this.GetEquipIntAttribute("STR");
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x00047667 File Offset: 0x00045867
	public int GetEquipAGI()
	{
		return this.GetEquipIntAttribute("AGI");
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x00047674 File Offset: 0x00045874
	public int GetEquipSTA()
	{
		return this.GetEquipIntAttribute("STA");
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x00047681 File Offset: 0x00045881
	public int GetEquipAttack()
	{
		return this.GetEquipIntAttribute("attack");
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x0004768E File Offset: 0x0004588E
	public int GetEquipHp()
	{
		return this.GetEquipIntAttribute("hp");
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x0004769B File Offset: 0x0004589B
	public int GetEquipMp()
	{
		return this.GetEquipIntAttribute("mp");
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x000476A8 File Offset: 0x000458A8
	public float GetEquipAttackSpeed()
	{
		float equipFloatAttribute = this.GetEquipFloatAttribute("attackSpeed");
		return this.playerBase.mAttackSpeed * equipFloatAttribute;
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x000476CE File Offset: 0x000458CE
	public int GetEquipArmor()
	{
		return this.GetEquipIntAttribute("Armor");
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x000476DC File Offset: 0x000458DC
	public void RefreshEquipPower()
	{
		this.playerBase.equipSTR = (int)((float)this.GetEquipSTR() * (1f + this.playerBase.equipAddValue));
		this.playerBase.equipAGI = (int)((float)this.GetEquipAGI() * (1f + this.playerBase.equipAddValue));
		int sta = this.playerBase.STA;
		this.playerBase.equipSTA = (int)((float)this.GetEquipSTA() * (1f + this.playerBase.equipAddValue));
		this.playerBase.AddPlayerSTAHp(this.playerBase.STA - sta);
		int num = (int)((float)this.GetEquipHp() * (1f + this.playerBase.equipAddValue));
		if (this.playerBase.equipHp != num)
		{
			this.playerBase.CmdUpdateMaxHp((long)(num - this.playerBase.equipHp), this.playerBase.netId);
			this.playerBase.equipHp = num;
		}
		int num2 = (int)((float)this.GetEquipMp() * (1f + this.playerBase.equipAddValue));
		if (this.playerBase.equipMp != num2)
		{
			this.playerBase.AddMaxMp(num2 - this.playerBase.equipMp);
			this.playerBase.equipMp = num2;
		}
		this.playerBase.equipAttack = (int)((float)this.GetEquipAttack() * (1f + this.playerBase.equipAddValue));
		this.playerBase.equipAttackSpeed = this.GetEquipAttackSpeed() * (1f + this.playerBase.equipAddValue);
		int num3 = (int)((float)this.GetEquipArmor() * (1f + this.playerBase.equipAddValue));
		if (this.playerBase.equipArmor != num3)
		{
			this.playerBase.equipArmor = num3;
			this.playerBase.CmdEquipArmor(num3);
		}
		this.playerBase.equipMoveSpeed = this.GetEquipMoveSpeed() * (1f + this.playerBase.equipAddValue);
		int num4 = (int)((float)this.GetEquipDoge() * (1f + this.playerBase.equipAddValue));
		if (this.playerBase.equipDoge != num4)
		{
			this.playerBase.equipDoge = num4;
			this.playerBase.CmdEquipDoge(num4);
		}
		int num5 = (int)((float)this.GetEquipLuck() * (1f + this.playerBase.equipAddValue));
		if (this.playerBase.equipLuck != num5)
		{
			this.playerBase.CmdUpdateLucky(num5 - this.playerBase.equipLuck);
			this.playerBase.equipLuck = num5;
		}
		this.playerBase.equipHpAddSec = (int)((float)this.GetEquipHpAddSec() * (1f + this.playerBase.equipAddValue));
		int num6 = (int)((float)this.GetEquipMpAddSec() * (1f + this.playerBase.equipAddValue));
		if (this.playerBase.equipMpAddSec != num6)
		{
			this.playerBase.AddMpAddSec(num6 - this.playerBase.equipMpAddSec);
			this.playerBase.equipMpAddSec = num6;
		}
		this.playerBase.equipBaoJiLv = (int)((float)this.GetEquipBaoJiLv() * (1f + this.playerBase.equipAddValue));
		this.playerBase.equipBaoJiDamage = (int)((float)this.GetEquipBaoJiDamage() * (1f + this.playerBase.equipAddValue));
		this.playerBase.equipXiXue = (int)((float)this.GetEquipXiXue() * (1f + this.playerBase.equipAddValue));
		this.playerBase.equipXiXueLV = this.GetEquipXiXueLv() * (1f + this.playerBase.equipAddValue);
		this.playerBase.equipSkillDamage = this.GetEquipSkillDamage() * (1f + this.playerBase.equipAddValue);
		this.playerBase.equipNormalBreakingShield = this.GetEquipBreakingShield() * (1f + this.playerBase.equipAddValue);
		this.playerBase.equipSkillBreakingShield = this.GetEquipSkillBreakingShield() * (1f + this.playerBase.equipAddValue);
		this.playerBase.equipSkillCd = (int)((float)this.GetEquipSkillCd() * (1f + this.playerBase.equipAddValue));
		this.playerBase.equipSkillReduction = (int)((float)this.GetSkillReduction() * (1f + this.playerBase.equipAddValue));
		this.RefreshEquipExtraAttributes(1f + this.playerBase.equipAddValue);
		this.playerBase.UpdateBreakShield();
		UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
		if (ui != null)
		{
			ui.RefreshPlayerStateUI();
		}
		UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
		if (ui2 == null)
		{
			return;
		}
		ui2.RefreshPlayerStateUI();
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x00047B6C File Offset: 0x00045D6C
	private void RefreshEquipExtraAttributes(float equipAddRatio)
	{
		this.UpdateEquipFloatAttribute(ref this.lastEquipNormalAttackAddDamage, this.GetEquipFloatAttribute("normalAttackAddDamage") * equipAddRatio, delegate(float value)
		{
			this.playerBase.normalAttackAddDamage += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipStaAllAdd, this.GetEquipFloatAttribute("staAllAdd") * equipAddRatio, delegate(float value)
		{
			this.playerBase.StaAllAdd += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipStrAllAdd, this.GetEquipFloatAttribute("strAllAdd") * equipAddRatio, delegate(float value)
		{
			this.playerBase.StrAllAdd += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipAgiAllAdd, this.GetEquipFloatAttribute("agiAllAdd") * equipAddRatio, delegate(float value)
		{
			this.playerBase.AgiAllAdd += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipAttackPercent, this.GetEquipFloatAttribute("attackPercent") * equipAddRatio, delegate(float value)
		{
			this.playerBase.UpdateAttackPercent(value);
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipAddDamage, this.GetEquipFloatAttribute("addDamage") * equipAddRatio, delegate(float value)
		{
			this.playerBase.addDamagePercent += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipSkillNoneDamage, this.GetEquipFloatAttribute("skillNoneDamage") * equipAddRatio, delegate(float value)
		{
			this.playerBase.skillNoneAdd += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipFireDamage, this.GetEquipFloatAttribute("fireDamage") * equipAddRatio, delegate(float value)
		{
			this.playerBase.skillFireAdd += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipIceDamage, this.GetEquipFloatAttribute("iceDamage") * equipAddRatio, delegate(float value)
		{
			this.playerBase.skillIceAdd += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipLightDamage, this.GetEquipFloatAttribute("lightDamage") * equipAddRatio, delegate(float value)
		{
			this.playerBase.skillLightingAdd += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipSkillRange, this.GetEquipFloatAttribute("skillRange") * equipAddRatio, delegate(float value)
		{
			this.playerBase.CmdUpdateSkillRange(value);
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipSkillTime, this.GetEquipFloatAttribute("skillTime") * equipAddRatio, delegate(float value)
		{
			this.playerBase.CmdUpdateSkillAddTime(value);
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipAttackDistance, this.GetEquipFloatAttribute("attackDistance") * equipAddRatio, delegate(float value)
		{
			this.playerBase.exAttackDistance += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipBuffDamage, this.GetEquipFloatAttribute("buffDamage") * equipAddRatio, delegate(float value)
		{
			this.playerBase.buffAddDamage += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipHaloRangeAdd, this.GetEquipFloatAttribute("haloRangeAdd") * equipAddRatio, delegate(float value)
		{
			this.playerBase.CmdUpdateHaloRangeAdd(value);
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipForgeAdd, this.GetEquipFloatAttribute("forgeAdd") * equipAddRatio, delegate(float value)
		{
			this.playerBase.UpdateForgingAdd(value);
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipMaxHpAddPercent, this.GetEquipFloatAttribute("maxHpAddPercent") * equipAddRatio, delegate(float value)
		{
			this.playerBase.CmdUpdateMaxHpAddPercent(value);
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipSkillExpend, this.GetEquipFloatAttribute("skillExpend") * equipAddRatio, delegate(float value)
		{
			this.playerBase.skillMpUsed += value;
		});
		this.UpdateEquipIntAttribute(ref this.lastEquipReduceInjury, (int)((float)this.GetEquipIntAttribute("reduceInjury") * equipAddRatio), delegate(int value)
		{
			this.playerBase.UpdateReduce(value);
		});
		this.UpdateEquipIntAttribute(ref this.lastEquipExtraDamage, (int)((float)this.GetEquipIntAttribute("extraDamage") * equipAddRatio), delegate(int value)
		{
			this.playerBase.extraDamage += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipCastSpeed, this.GetEquipFloatAttribute("castSpeed") * equipAddRatio, delegate(float value)
		{
			this.playerBase.CmdUpdateCastSpeed(value);
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipHpAddUpgrade, this.GetEquipFloatAttribute("hpAddUpgrade") * equipAddRatio, delegate(float value)
		{
			this.playerBase.hpAddUpgrade += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipAddCallMonster, this.GetEquipFloatAttribute("addCallMonster") * equipAddRatio, delegate(float value)
		{
			this.playerBase.addCallMonsterHp += value;
			this.playerBase.addCallMonsterAttack += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipAddCallMonsterTime, this.GetEquipFloatAttribute("addCallMonsterTime") * equipAddRatio, delegate(float value)
		{
			this.playerBase.addCallMonsterTime += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipAddHenshin, this.GetEquipFloatAttribute("addHenshin") * equipAddRatio, delegate(float value)
		{
			this.playerBase.addHenshin += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipAddHenshinTime, this.GetEquipFloatAttribute("addHenshinTime") * equipAddRatio, delegate(float value)
		{
			this.playerBase.UpdateAddHenshinTime(value);
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipArmedAdd, this.GetEquipFloatAttribute("armedAdd") * equipAddRatio, delegate(float value)
		{
			this.playerBase.armedAdd += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipHpSecRate, this.GetEquipFloatAttribute("hpSecRate") * equipAddRatio, delegate(float value)
		{
			this.playerBase.hpAddSecRate += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipMagicXiXue, this.GetEquipFloatAttribute("magicXiXue") * equipAddRatio, delegate(float value)
		{
			this.playerBase.magicXiXue += value;
		});
		this.UpdateEquipFloatAttribute(ref this.lastEquipEffectDamage, this.GetEquipFloatAttribute("effectDamage") * equipAddRatio, delegate(float value)
		{
			this.playerBase.addAttackEffectDamage += value;
		});
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x00047FD4 File Offset: 0x000461D4
	private void UpdateEquipFloatAttribute(ref float oldValue, float newValue, Action<float> updateAction)
	{
		float num = newValue - oldValue;
		if (Mathf.Approximately(num, 0f))
		{
			return;
		}
		updateAction(num);
		oldValue = newValue;
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x00048000 File Offset: 0x00046200
	private void UpdateEquipIntAttribute(ref int oldValue, int newValue, Action<int> updateAction)
	{
		int num = newValue - oldValue;
		if (num == 0)
		{
			return;
		}
		updateAction(num);
		oldValue = newValue;
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x00048020 File Offset: 0x00046220
	private int GetEquipXiXue()
	{
		return this.GetEquipIntAttribute("xixue");
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x0004802D File Offset: 0x0004622D
	private float GetEquipSkillDamage()
	{
		return this.GetEquipFloatAttribute("skillDamage");
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x0004803A File Offset: 0x0004623A
	private float GetEquipBreakingShield()
	{
		return this.GetEquipFloatAttribute("breakShield");
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x00048047 File Offset: 0x00046247
	private float GetEquipSkillBreakingShield()
	{
		return this.GetEquipFloatAttribute("skillBreakShield");
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x00048054 File Offset: 0x00046254
	private int GetEquipSkillCd()
	{
		return this.GetEquipIntAttribute("skillCd");
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x00048061 File Offset: 0x00046261
	private float GetEquipXiXueLv()
	{
		return this.GetEquipFloatAttribute("hptouqu");
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x0004806E File Offset: 0x0004626E
	private int GetEquipBaoJiDamage()
	{
		return this.GetEquipIntAttribute("baojiDamage");
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x0004807B File Offset: 0x0004627B
	private int GetEquipBaoJiLv()
	{
		return this.GetEquipIntAttribute("baojilv");
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x00048088 File Offset: 0x00046288
	private int GetEquipHpAddSec()
	{
		return this.GetEquipIntAttribute("HPadd");
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x00048095 File Offset: 0x00046295
	private int GetEquipMpAddSec()
	{
		return this.GetEquipIntAttribute("MPadd");
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x000480A2 File Offset: 0x000462A2
	private float GetEquipMoveSpeed()
	{
		return this.GetEquipFloatAttribute("moveSpeed");
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x000480AF File Offset: 0x000462AF
	private int GetEquipDoge()
	{
		return this.GetEquipIntAttribute("doge");
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x000480BC File Offset: 0x000462BC
	private int GetSkillReduction()
	{
		return this.GetEquipIntAttribute("skillReduction");
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x000480C9 File Offset: 0x000462C9
	private int GetEquipLuck()
	{
		return this.GetEquipIntAttribute("luck");
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x000480D6 File Offset: 0x000462D6
	public float GetNeedExp(int num)
	{
		if (num == 0)
		{
			return 0f;
		}
		return (float)(2100 * num - 1800);
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x000480EF File Offset: 0x000462EF
	public bool BagIsFull()
	{
		return this.bagItemList.Count >= 6;
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x00048102 File Offset: 0x00046302
	public int BagNum()
	{
		return this.bagItemList.Count;
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x00048110 File Offset: 0x00046310
	public void AddItem(BagItem bagItem)
	{
		this.bagItemList.Add(bagItem);
		if (GameHelperClient.localPlayer.addBagItemEvent != null)
		{
			GameHelperClient.localPlayer.addBagItemEvent(bagItem.bookType);
		}
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.RefreshBookUI();
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x00048160 File Offset: 0x00046360
	public void AddBook(ItemType bookType, BagItemType bagItemType, string id)
	{
		if (bagItemType == BagItemType.HuFu)
		{
			GameHelperClient.localPlayer.UseHuFu(bookType);
			return;
		}
		if (GameHelperClient.localPlayer.playerAttribute.BagIsFull())
		{
			Util.ShowTips("背包已满");
			return;
		}
		BagItem bagItem = new BagItem();
		bagItem.bookType = bookType;
		bagItem.bagItemType = bagItemType;
		bagItem.id = id;
		if (GameHelperClient.localPlayer.addBagItemEvent != null)
		{
			GameHelperClient.localPlayer.addBagItemEvent(bookType);
		}
		this.bagItemList.Add(bagItem);
		if (bookType == ItemType.Active_Book_D || bookType == ItemType.Passsive_Book_D)
		{
			if ((GameHelperClient.AutoSellBookMask & 1) != 0)
			{
				GameHelperClient.localPlayer.playerAttribute.SellBook(bagItem.id, bagItem);
			}
		}
		else if (bookType == ItemType.Active_Book_C || bookType == ItemType.Passsive_Book_C)
		{
			if ((GameHelperClient.AutoSellBookMask & 2) != 0)
			{
				GameHelperClient.localPlayer.playerAttribute.SellBook(bagItem.id, bagItem);
			}
		}
		else if (bookType == ItemType.Active_Book_B || bookType == ItemType.Passsive_Book_B)
		{
			if ((GameHelperClient.AutoSellBookMask & 4) != 0)
			{
				GameHelperClient.localPlayer.playerAttribute.SellBook(bagItem.id, bagItem);
			}
		}
		else if (bookType == ItemType.Active_Book_A || bookType == ItemType.Passsive_Book_A)
		{
			if ((GameHelperClient.AutoSellBookMask & 8) != 0)
			{
				GameHelperClient.localPlayer.playerAttribute.SellBook(bagItem.id, bagItem);
			}
		}
		else if ((bookType == ItemType.Active_Book_S || bookType == ItemType.Passsive_Book_S) && (GameHelperClient.AutoSellBookMask & 16) != 0)
		{
			GameHelperClient.localPlayer.playerAttribute.SellBook(bagItem.id, bagItem);
		}
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.RefreshBookUI();
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x000482F0 File Offset: 0x000464F0
	public void SellBook(string id, BagItem bookType)
	{
		int num = 0;
		if (bookType.bagItemType == BagItemType.Remains)
		{
			RemainsData remainsData;
			if (Game.GameData.RemainsDataDic.TryGetValue(bookType.bookType, out remainsData))
			{
				num = ConstDefine.RelicSellGold[remainsData.grade];
			}
		}
		else
		{
			num = ((bookType.bagItemType == BagItemType.Card) ? 0 : ExcelManager.allExcelData["shop"].DIC(id).DIC("sell"));
		}
		string text = Game.Language.Get(Game.GameData.ItemDataDic[bookType.bookType].name, "");
		this.bagItemList.Remove(bookType);
		GameHelperClient.localPlayer.AddGold(GameHelperClient.localPlayer.GetHeadUIPos(), num, false);
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui != null)
		{
			ui.RefreshBookUI();
		}
		UI_Msg ui2 = Game.UI.GetUI<UI_Msg>();
		if (ui2 == null)
		{
			return;
		}
		ui2.ShowMsg(string.Format("{0}{1}{2}{3}{4}", new object[]
		{
			Game.Language.Get("sell", ""),
			text,
			Game.Language.Get("get", ""),
			num,
			Game.Language.Get("gold", "")
		}), false);
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x00048436 File Offset: 0x00046636
	public void RemoveBook(BagItem bookType)
	{
		this.bagItemList.Remove(bookType);
		if (GameHelperClient.localPlayer != null)
		{
			GameHelperClient.localPlayer.CmdCreateItem(bookType);
		}
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.RefreshBookUI();
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x00048474 File Offset: 0x00046674
	public void SellEquip(EquipBase equipBase, bool isAutoDestroy = false)
	{
		Game.UI.GetUI<UI_PlayerState>().RemoveEquip(equipBase);
		GameHelperClient.localPlayer.playerAttribute.RemoveEquip(equipBase);
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui != null)
		{
			ui.RefreshPlayerEquip();
		}
		if (isAutoDestroy)
		{
			return;
		}
		string key = PathDefine.Concat("equip_", equipBase.equipIndex);
		if (equipBase.IsMyth())
		{
			key = "equip_0";
		}
		int num = Mathf.RoundToInt((float)ExcelManager.allExcelData["shop"].DIC(key).DIC("sell") * (1f + GameHelperClient.localPlayer.GetShopDiscountAdd()));
		string name = equipBase.name;
		GameHelperClient.localPlayer.AddGold(GameHelperClient.localPlayer.GetHeadUIPos(), num, false);
		UI_Msg ui2 = Game.UI.GetUI<UI_Msg>();
		if (ui2 == null)
		{
			return;
		}
		ui2.ShowMsg(string.Format("{0}{1}{2}{3}{4}", new object[]
		{
			Game.Language.Get("sell", ""),
			name,
			Game.Language.Get("get", ""),
			num,
			Game.Language.Get("gold", "")
		}), false);
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x000485A8 File Offset: 0x000467A8
	public void UseBook(BagItem bagItem)
	{
		if (GameHelperClient.localPlayer.useItemEvent != null)
		{
			GameHelperClient.localPlayer.useItemEvent(bagItem.bookType);
		}
		if (bagItem.bagItemType == BagItemType.Book)
		{
			if (!Util.CheckCanRoguelike())
			{
				return;
			}
			string[] array = bagItem.bookType.ToString().Split("_Book_", StringSplitOptions.None);
			if (array[0].Equals("Active"))
			{
				if (GameHelperClient.CantLearnActiveSkill > 0)
				{
					Util.ShowTips("无法学习主动");
					return;
				}
				GameHelperClient.localPlayer.GetRandomActiveSkill(array[1]);
				EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/学习主动技能", 1f, 3f);
			}
			else
			{
				if (GameHelperClient.CantLearnPasssiveSkill > 0)
				{
					Util.ShowTips("无法学习被动");
					return;
				}
				GameHelperClient.localPlayer.GetRandomPassiveSkill(array[1]);
				EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/学习被动技能", 1f, 3f);
			}
			RoleBase.SkillBookEvent skillBookEvent = this.playerBase.skillBookEvent;
			if (skillBookEvent != null)
			{
				skillBookEvent(this.playerBase);
			}
		}
		else if (bagItem.bagItemType == BagItemType.HuFu)
		{
			GameHelperClient.localPlayer.UseHuFu(bagItem.bookType);
		}
		else if (bagItem.bagItemType == BagItemType.XieHuangBao)
		{
			EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/喝药", 1f, 3f);
			Buff回血 buff回血 = (Buff回血)GameHelperClient.localPlayer.roleBuffManager.AddOneBuff<Buff回血>("Buff回血", 10f);
			buff回血.hpRate = 0.5f;
			buff回血.OnInit();
			Buff回蓝 buff回蓝 = (Buff回蓝)GameHelperClient.localPlayer.roleBuffManager.AddOneBuff<Buff回蓝>("Buff回蓝", 10f);
			buff回蓝.mpRate = 0.5f;
			buff回蓝.OnInit();
		}
		else if (bagItem.bagItemType == BagItemType.UseItem)
		{
			if (Util.IsMedicineItem(bagItem.bookType))
			{
				if (!this.AddMedicine(bagItem.bookType))
				{
					return;
				}
				EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/喝药", 1f, 3f);
			}
			else
			{
				ItemType bookType = bagItem.bookType;
				switch (bookType)
				{
				case ItemType.HeroSoul:
					if (GameHelperClient.isReady)
					{
						Util.ShowTips("战斗使用提示");
						return;
					}
					GameHelperClient.localPlayer.StartSummon(EnemyType.SaiYa, GameHelperClient.localPlayer.MyTransform.position + GameHelperClient.localPlayer.MyTransform.forward * 3.5f, GameHelperClient.localPlayer.netId, 1f, 1000000L, 75000, 60f, null, 0L, 0L, -1);
					break;
				case ItemType.HeroSword:
					if (GameHelperClient.localPlayer.playerAttribute.equipList.Count == GameHelperClient.MaxEquipNum)
					{
						Util.ShowTips("装备已满，请先出售装备！");
						return;
					}
					ShopManager.OnBuyEquipSuccess("equip_1002", 0, null);
					Util.ShowTips("获得《勇者之剑》");
					break;
				case ItemType.HeroBook:
				{
					RoguelikeUIData roguelikeUIData = default(RoguelikeUIData);
					roguelikeUIData.data = 613.ToString();
					if (GameHelperClient.localPlayer.roleSkillList.Count > GameHelperClient.MaxSkillNum - 1)
					{
						Game.UI.GetUI<UI_PlayerState>().OnSwitchSkill(roguelikeUIData, new Action<RoguelikeUIData, SkillBase>(Game.SkillManager.OnActiveSkillSwitchSkill));
					}
					else
					{
						Game.SkillManager.OnActiveSkillSwitchSkill(roguelikeUIData, null);
					}
					break;
				}
				case ItemType.SleepingStone:
					if (!Util.CheckCanRoguelike())
					{
						return;
					}
					if (!this.ShowRelicLevelUpRoguelike())
					{
						return;
					}
					break;
				default:
					if (bookType - ItemType.EquipAdd_1 <= 5)
					{
						if (!ShopManager.TryOpenEquipStreng(bagItem.bookType))
						{
							return;
						}
					}
					break;
				}
			}
		}
		else if (bagItem.bagItemType == BagItemType.Remains)
		{
			GameHelperClient.localPlayer.AddRelic((int)bagItem.bookType, 0);
		}
		else if (bagItem.bagItemType == BagItemType.Card)
		{
			int cardId = bagItem.bookType - ItemType.Card_0;
			Util.ShowTipsNoLanguage(PathDefine.Concat(Game.Language.Get("get", ""), string.Format(ColorDefine.NormalColor, PathDefine.Concat(Game.Language.Get("【卡牌】", ""), Game.Language.Get("card_" + cardId.ToString(), "")))));
			EntityStatic.Get<CardManager>().GetCard(cardId);
		}
		this.bagItemList.Remove(bagItem);
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.RefreshBookUI();
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x000489D8 File Offset: 0x00046BD8
	private bool ShowRelicLevelUpRoguelike()
	{
		List<RelicBase> canLevelUpRelics = this.GetCanLevelUpRelics();
		if (canLevelUpRelics.Count == 0)
		{
			Util.ShowTips("没有可以强化的遗物！");
			return false;
		}
		List<RelicBase> randomRelics = PlayerAttribute.GetRandomLevelUpRelics(canLevelUpRelics, 3);
		RoguelikeUIData[] array = new RoguelikeUIData[randomRelics.Count];
		for (int i = 0; i < randomRelics.Count; i++)
		{
			RelicBase relicBase = randomRelics[i];
			int num = relicBase.level + 1;
			array[i] = new RoguelikeUIData
			{
				name = Util.GetLevelStarName(Game.Language.Get("pickitem_" + relicBase.keyIndex, ""), num),
				dec = RelicBase.GetLevelCompareFormatDec(Game.Language.Get("pickitem_" + relicBase.keyIndex + "_m", ""), relicBase.relicData, relicBase.level, num),
				icon = "Bundles/UI/Icon/Remains/" + relicBase.relicData.DIC("icon"),
				data = i.ToString(),
				quality = relicBase.quality
			};
		}
		UI_Roguelike ui_Roguelike = Game.UI.OpenUI<UI_Roguelike>(null) as UI_Roguelike;
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/学习主动技能", 1f, 3f);
		ui_Roguelike.ShowRoguelike(array, delegate(RoguelikeUIData roguelikeData)
		{
			int index = int.Parse(roguelikeData.data);
			this.playerBase.AddRelicLevel(randomRelics[index]);
			UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
			if (ui != null)
			{
				ui.RefreshRelic();
			}
			Util.ShowTipsNoLanguage(PathDefine.Concat(string.Format(ColorDefine.NormalColor, Game.Language.Get("pickitem_" + randomRelics[index].keyIndex, "")), Game.Language.Get("强化成功", "")));
			EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/拾取物品", 1f, 3f);
		}, Game.Language.Get("遗物强化", ""), null, null, 0f, null, "relic_levelup");
		return true;
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x00048B7C File Offset: 0x00046D7C
	private List<RelicBase> GetCanLevelUpRelics()
	{
		List<RelicBase> list = new List<RelicBase>();
		for (int i = 0; i < this.relicList.Count; i++)
		{
			RelicBase relicBase = this.relicList[i];
			if (relicBase != null && relicBase.relicData != null && !string.IsNullOrEmpty(relicBase.keyIndex) && !relicBase.relicData.DIC("levelLock"))
			{
				list.Add(relicBase);
			}
		}
		return list;
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x00048BE4 File Offset: 0x00046DE4
	private static List<RelicBase> GetRandomLevelUpRelics(List<RelicBase> canLevelRelics, int count)
	{
		List<RelicBase> list = new List<RelicBase>(canLevelRelics);
		for (int i = 0; i < list.Count; i++)
		{
			int num = Random.Range(i, list.Count);
			List<RelicBase> list2 = list;
			int index = i;
			List<RelicBase> list3 = list;
			int index2 = num;
			RelicBase value = list[num];
			RelicBase value2 = list[i];
			list2[index] = value;
			list3[index2] = value2;
		}
		if (list.Count > count)
		{
			list.RemoveRange(count, list.Count - count);
		}
		return list;
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x00048C63 File Offset: 0x00046E63
	public void AddKingAIEquip(string id, int strengLevel)
	{
		this.AddKingAIEquip(id, strengLevel, null);
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x00048C70 File Offset: 0x00046E70
	public void AddKingAIEquip(string id, int strengLevel, string[] equipEvolutionSkill)
	{
		EquipBase equipBase = new EquipBase();
		equipBase.equipIndex = id;
		equipBase.Init(this.playerBase.roleType);
		equipBase.level = strengLevel;
		List<EquipEvolutionEntryData> skillEntries = EquipEvolutionEntryData.GetSkillEntries(id, equipEvolutionSkill);
		for (int i = 0; i < skillEntries.Count; i++)
		{
			EquipEvolutionEntryData equipEvolutionEntryData = skillEntries[i];
			if (equipEvolutionEntryData != null)
			{
				equipEvolutionEntryData.ApplyTo(equipBase);
			}
		}
		this.AddEquip(equipBase);
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x00048CD8 File Offset: 0x00046ED8
	public void OnEquipLevelUpSuccess(EquipBase equip)
	{
		foreach (EquipSkillType equipSkillType in equip.GetEquipSkills())
		{
			EquipSkillBase equipSkillBase;
			if ((this.playerBase.roleType != RoleType.King || !EquipBase.IsEquipSkillKingLocked(equipSkillType)) && this.equipSkillDic.TryGetValue(equipSkillType, out equipSkillBase))
			{
				if (equip.onlySkill)
				{
					using (List<EquipBase>.Enumerator enumerator2 = this.equipList.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							EquipBase equipBase = enumerator2.Current;
							if (equipBase.HasEquipSkill(equipSkillType))
							{
								if (equipBase == equip)
								{
									equipSkillBase.OnUpdateStrengLevel(1);
									break;
								}
								break;
							}
						}
						continue;
					}
				}
				equipSkillBase.OnUpdateStrengLevel(1);
			}
		}
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x00048DB0 File Offset: 0x00046FB0
	public void AddEquip(EquipBase equip)
	{
		foreach (EquipSkillType equipSkill in equip.GetEquipSkills())
		{
			this.AddEquipSkill(equip, equipSkill);
		}
		this.equipList.Add(equip);
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x00048E0C File Offset: 0x0004700C
	private void AddEquipSkill(EquipBase equip, EquipSkillType equipSkill)
	{
		if (this.playerBase.roleType == RoleType.King && EquipBase.IsEquipSkillKingLocked(equipSkill))
		{
			return;
		}
		EquipSkillBase equipSkillBase;
		if (this.equipSkillDic.TryGetValue(equipSkill, out equipSkillBase))
		{
			if (!equip.onlySkill)
			{
				equipSkillBase.OnUpdateStrengLevel(equip.level);
				equipSkillBase.AddEquipNum();
			}
			return;
		}
		EquipSkillBase equipSkillBase2 = null;
		EquipEventBase equipEventBase = null;
		switch (equipSkill)
		{
		case EquipSkillType.Sputtering:
			equipSkillBase2 = new EquipSkillSputtering();
			break;
		case EquipSkillType.DragonHeart:
			equipEventBase = new EquipEventDragonHeart();
			break;
		case EquipSkillType.BladeRuined:
		case EquipSkillType.BladeRuinedEvolution:
			equipSkillBase2 = new EquipSkillBladeRuined();
			break;
		case EquipSkillType.MaskSkillFire:
		case EquipSkillType.MaskSkillFireEvolution:
			equipSkillBase2 = new EquipSkillFire();
			break;
		case EquipSkillType.SunFire:
			equipSkillBase2 = new EquipSkillSunFire();
			break;
		case EquipSkillType.MadMan:
			equipSkillBase2 = new EquipSkillMadMan();
			break;
		case EquipSkillType.HpAddUpgrade:
			equipSkillBase2 = new EquipSkillHpAddUpgrade();
			break;
		case EquipSkillType.Scythe:
			equipSkillBase2 = new EquipSkillScythe();
			break;
		case EquipSkillType.DemonMask:
			equipSkillBase2 = new EquipSkillDemonMask();
			break;
		case EquipSkillType.StoneMask:
			equipSkillBase2 = new EquipSkillStoneMask();
			break;
		case EquipSkillType.WizardGloves:
		case EquipSkillType.WizardGlovesEvolution:
			equipSkillBase2 = new EquipSkillWizardGloves();
			break;
		case EquipSkillType.SiMing:
		case EquipSkillType.SiMingEvolution:
			equipSkillBase2 = new EquipSkillSiMing();
			break;
		case EquipSkillType.GodGloves:
			equipSkillBase2 = new EquipSkillGodGloves();
			break;
		case EquipSkillType.ProofCourage:
			equipSkillBase2 = new EquipSkillProofCourage();
			break;
		case EquipSkillType.PiggyBank:
			equipEventBase = new EquipEventPiggyBank();
			break;
		case EquipSkillType.SteelClaws:
			equipSkillBase2 = new EquipSkillSteelClaws();
			break;
		case EquipSkillType.SteelHeart:
			equipSkillBase2 = new EquipSkillSteelHeart();
			break;
		case EquipSkillType.NinjaScabbard:
		case EquipSkillType.NinjaScabbardEvolution:
			equipSkillBase2 = new EquipSkillNinjaScabbard();
			break;
		case EquipSkillType.YiTian:
			equipSkillBase2 = new EquipSkillYiTian();
			break;
		case EquipSkillType.TuLong:
			equipSkillBase2 = new EquipSkillTuLong();
			break;
		case EquipSkillType.FangTian:
			equipSkillBase2 = new EquipSkillFangTian();
			break;
		case EquipSkillType.Sakura:
			equipSkillBase2 = new EquipSkillSakura();
			break;
		case EquipSkillType.TimeCore:
			equipSkillBase2 = new EquipSkillTimeCore();
			break;
		case EquipSkillType.SheepKnife:
		case EquipSkillType.SheepKnifeEvolution:
			equipSkillBase2 = new EquipSkillSheepKnife();
			break;
		case EquipSkillType.SpiritCalling:
			equipSkillBase2 = new EquipSkillSpiritCalling();
			break;
		case EquipSkillType.HenshinBelt:
			equipSkillBase2 = new EquipSkillHenshinBelt();
			break;
		case EquipSkillType.HolySword:
			equipEventBase = new EquipEventHolySword();
			break;
		case EquipSkillType.BrokenMasterSword:
			equipEventBase = new EquipSkillBrokenMasterSword();
			break;
		case EquipSkillType.MoonlightGreatsword:
			equipSkillBase2 = new EquipSkillMoonlightGreatsword();
			break;
		case EquipSkillType.HeavySword:
			equipSkillBase2 = new EquipSkillHeavySword();
			break;
		case EquipSkillType.FrierenStaff:
			equipSkillBase2 = new EquipSkillFrierenStaff();
			break;
		case EquipSkillType.MagicalGirl:
			equipSkillBase2 = new EquipSkillMagicalGirl();
			break;
		case EquipSkillType.VampireCode:
			equipSkillBase2 = new EquipSkillVampireCode();
			break;
		case EquipSkillType.HeroSword:
			equipSkillBase2 = new EquipSkillHeroSword();
			break;
		case EquipSkillType.GorgeousCape:
			equipSkillBase2 = new EquipSkillGorgeousCape();
			break;
		case EquipSkillType.RingOfLife:
			equipSkillBase2 = new EquipSkillRingOfLife();
			break;
		case EquipSkillType.ArmedCore:
			equipSkillBase2 = new EquipSkillArmedCore();
			break;
		case EquipSkillType.OldSword:
			equipEventBase = new EquipSkillOldSword();
			break;
		}
		if (equipSkillBase2 != null)
		{
			equipSkillBase2.equipBase = equip;
			equipSkillBase2.equipSkillType = equipSkill;
			equipSkillBase2.playerBase = this.playerBase;
			equipSkillBase2.equipIndex = equip.equipIndex;
			equipSkillBase2.skillValueAry = equip.GetSkillValueAry(equipSkill);
			equipSkillBase2.skillValueUpAry = equip.GetSkillValueUpAry(equipSkill);
			equipSkillBase2.Init();
			equipSkillBase2.OnUpdateStrengLevel(equip.level);
			this.equipSkillDic.Add(equipSkill, equipSkillBase2);
		}
		if (equipEventBase != null)
		{
			equipEventBase.playerBase = this.playerBase;
			equipEventBase.strengLevel = equip.level;
			equipEventBase.skillValueAry = equip.GetSkillValueAry(equipSkill);
			equipEventBase.skillValueUpAry = equip.GetSkillValueUpAry(equipSkill);
			equipEventBase.Init(equip);
			equip.AddEquipEvent(equipEventBase);
		}
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x0004912C File Offset: 0x0004732C
	public void RemoveEquip(EquipBase equip)
	{
		foreach (EquipSkillType equipSkill in equip.GetEquipSkills())
		{
			this.RemoveEquipSkill(equip, equipSkill);
		}
		foreach (EquipEventBase equipEventBase in equip.GetEquipEvents())
		{
			equipEventBase.Clear();
		}
		this.equipList.Remove(equip);
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x000491C0 File Offset: 0x000473C0
	private void RemoveEquipSkill(EquipBase equip, EquipSkillType equipSkill)
	{
		EquipSkillBase equipSkillBase;
		if (!this.equipSkillDic.TryGetValue(equipSkill, out equipSkillBase))
		{
			return;
		}
		bool flag = false;
		if (equip.onlySkill)
		{
			flag = true;
			using (List<EquipBase>.Enumerator enumerator = this.equipList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EquipBase equipBase = enumerator.Current;
					if (equipBase.HasEquipSkill(equipSkill) && equipBase != equip)
					{
						flag = false;
						equipSkillBase.OnUpdateStrengLevel(equipBase.level - equip.level);
						break;
					}
				}
				goto IL_8D;
			}
		}
		equipSkillBase.OnUpdateStrengLevel(-equip.level);
		equipSkillBase.RemoveEquipNum();
		if (equipSkillBase.equipNum == 0)
		{
			flag = true;
		}
		IL_8D:
		if (flag)
		{
			equipSkillBase.Clear();
			this.equipSkillDic.Remove(equipSkill);
		}
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x00049280 File Offset: 0x00047480
	public string GetEquipSkillInfo(EquipSkillType equipSkillType)
	{
		foreach (EquipBase equipBase in this.equipList)
		{
			if (equipBase.HasEquipSkill(equipSkillType))
			{
				return equipBase.GetInfoResult(equipSkillType, false, 0);
			}
		}
		return "";
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x000492E8 File Offset: 0x000474E8
	public void AddCardSkill(CardSkillType cardSkillType, int cardId)
	{
		CardSkillBase cardSkillBase = null;
		switch (cardSkillType)
		{
		case CardSkillType.TrollHeart:
			cardSkillBase = new CardSkillTrollHeart();
			break;
		case CardSkillType.Reverse:
			cardSkillBase = new CardSkillReverse();
			break;
		case CardSkillType.DrawSword:
			cardSkillBase = new CardSkillDrawSword();
			break;
		case CardSkillType.Self:
			cardSkillBase = new CardSkillSelf();
			break;
		case CardSkillType.AddEquip:
			cardSkillBase = new CardSkillAddEquip();
			break;
		case CardSkillType.AddSkill:
			cardSkillBase = new CardSkillAddSkill();
			break;
		case CardSkillType.Chicken:
			cardSkillBase = new CardSkillChicken();
			break;
		case CardSkillType.GoblinKing:
			cardSkillBase = new CardSkillGoblinKing();
			break;
		case CardSkillType.GoblinWarrior:
			cardSkillBase = new CardSkillGoblinWarrior();
			break;
		case CardSkillType.GoblinElder:
			cardSkillBase = new CardSkillGoblinElder();
			break;
		case CardSkillType.LowCostCard:
			cardSkillBase = new CardSkillLowCostCard();
			break;
		case CardSkillType.GoblinGeneral:
			cardSkillBase = new CardSkillGoblinGeneral();
			break;
		case CardSkillType.ManaVoid:
			cardSkillBase = new CardSkillManaVoid();
			break;
		case CardSkillType.AllCostAlliance:
			cardSkillBase = new CardSkillAllCostAlliance();
			break;
		case CardSkillType.Uniform:
			cardSkillBase = new CardSkillUniform();
			break;
		case CardSkillType.ArtExplosion:
			cardSkillBase = new CardSkillArtExplosion();
			break;
		case CardSkillType.SecBrotatoWeapon:
			cardSkillBase = new CardSkillSecBrotatoWeapon();
			break;
		}
		if (cardSkillBase != null)
		{
			cardSkillBase.cardId = cardId;
			cardSkillBase.playerBase = this.playerBase;
			if (!this.cardSkillListDic.ContainsKey(cardSkillType))
			{
				this.cardSkillListDic.Add(cardSkillType, new List<CardSkillBase>
				{
					cardSkillBase
				});
			}
			else
			{
				this.cardSkillListDic[cardSkillType].Add(cardSkillBase);
			}
			cardSkillBase.Enter();
		}
	}

	// Token: 0x06000C75 RID: 3189 RVA: 0x00049424 File Offset: 0x00047624
	public void RemoveCardSkill(CardSkillType cardSkillType)
	{
		List<CardSkillBase> list;
		if (this.cardSkillListDic.TryGetValue(cardSkillType, out list))
		{
			foreach (CardSkillBase cardSkillBase in list)
			{
				cardSkillBase.Exit();
			}
			this.cardSkillListDic.Remove(cardSkillType);
		}
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x0004948C File Offset: 0x0004768C
	public void CheckExCardData(List<int> cardIdList, bool isTeamCard, ref CardEntries cardEntries)
	{
		foreach (List<CardSkillBase> list in this.cardSkillListDic.Values)
		{
			foreach (CardSkillBase cardSkillBase in list)
			{
				cardSkillBase.CheckExCardData(cardIdList, isTeamCard, ref cardEntries);
			}
		}
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x00049518 File Offset: 0x00047718
	public void AddKingCard(int cardId)
	{
		if (!Game.GameData.CardDataDic.ContainsKey(cardId))
		{
			return;
		}
		CardData cardData = Game.GameData.CardDataDic[cardId];
		if (cardData.cardSkill != CardSkillType.None && !cardData.kingLock)
		{
			this.AddCardSkill(cardData.cardSkill, cardData.id);
		}
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x0004956C File Offset: 0x0004776C
	public bool AddMedicine(ItemType itemType)
	{
		if (!Util.IsMedicineItem(itemType))
		{
			return false;
		}
		string medicineShopId = Util.GetMedicineShopId(itemType);
		object obj = ExcelManager.allExcelData["shop"].DIC(medicineShopId);
		if (obj == null)
		{
			return false;
		}
		if (GameHelperClient.IsFinalKingBattle())
		{
			Util.ShowTips(string.Format(ColorDefine.RedForColor, Game.Language.Get("决战阶段无法使用", "")));
			return false;
		}
		this.AddMedicine(this.CreateMedicineShopItem(obj));
		return true;
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x000495E0 File Offset: 0x000477E0
	private ShopItem CreateMedicineShopItem(object data)
	{
		ShopItem shopItem = new ShopItem();
		shopItem.id = data.DIC("id");
		shopItem.gold = data.DIC("gold");
		shopItem.goldAdd = data.DIC("goldAdd");
		shopItem.gem = data.DIC("zhuan");
		shopItem.gemAdd = data.DIC("zhuanAdd");
		shopItem.iconPath = data.DIC("icon");
		shopItem.type = data.DIC("type");
		shopItem.times = data.DIC("time");
		shopItem.cdSet = 1f;
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
		return shopItem;
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x000496E8 File Offset: 0x000478E8
	public void AddMedicine(ShopItem shopItem)
	{
		MedicineBase medicineBase = null;
		string type = shopItem.type;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(type);
		if (num <= 2226667892U)
		{
			if (num <= 1154925651U)
			{
				if (num != 409314466U)
				{
					if (num == 1154925651U)
					{
						if (type == "SkillDamage")
						{
							medicineBase = new MedicineSkillDamage();
							goto IL_16D;
						}
					}
				}
				else if (type == "Relife")
				{
					medicineBase = new MedicineRelife();
					goto IL_16D;
				}
			}
			else if (num != 2000437136U)
			{
				if (num == 2226667892U)
				{
					if (type == "Armor")
					{
						medicineBase = new MedicineArmor();
						goto IL_16D;
					}
				}
			}
			else if (type == "Luck")
			{
				medicineBase = new MedicineLuck();
				goto IL_16D;
			}
		}
		else if (num <= 2851477549U)
		{
			if (num != 2343121693U)
			{
				if (num == 2851477549U)
				{
					if (type == "BossDamage")
					{
						medicineBase = new MedicineBossDamage();
						goto IL_16D;
					}
				}
			}
			else if (type == "Attack")
			{
				medicineBase = new MedicineAttackAdd();
				goto IL_16D;
			}
		}
		else if (num != 3737199180U)
		{
			if (num == 3832593461U)
			{
				if (type == "AllDamage")
				{
					medicineBase = new MedicineAllDamage();
					goto IL_16D;
				}
			}
		}
		else if (type == "Invincible")
		{
			this.playerBase.roleBuffManager.AddOneBuff<Buff无敌>("Buff无敌", shopItem.values[0]);
			goto IL_16D;
		}
		medicineBase = new MedicineBase();
		IL_16D:
		if (medicineBase != null)
		{
			medicineBase.Init(shopItem, this.playerBase);
			this.medicineDic.Add(medicineBase);
		}
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x00049880 File Offset: 0x00047A80
	public void OnWaveAdd()
	{
		for (int i = this.medicineDic.Count - 1; i > -1; i--)
		{
			MedicineBase medicineBase = this.medicineDic[i];
			medicineBase.waveCount--;
			if (medicineBase.waveCount == 0)
			{
				medicineBase.Clear();
				this.medicineDic.RemoveAt(i);
			}
			else
			{
				medicineBase.OnWaveAdd();
			}
		}
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x000498E4 File Offset: 0x00047AE4
	public void OnGameOver()
	{
		foreach (MedicineBase medicineBase in this.medicineDic)
		{
			medicineBase.Clear();
		}
		this.medicineDic.Clear();
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x00049940 File Offset: 0x00047B40
	public void RemoveMedicine(MedicineBase medicineBase)
	{
		int num = this.medicineDic.IndexOf(medicineBase);
		if (num != -1)
		{
			this.medicineDic[num].Clear();
			this.medicineDic.RemoveAt(num);
		}
	}

	// Token: 0x04000CF8 RID: 3320
	public PlayerBase playerBase;

	// Token: 0x04000CF9 RID: 3321
	public List<EquipBase> equipList = new List<EquipBase>();

	// Token: 0x04000CFA RID: 3322
	public List<RelicBase> relicList = new List<RelicBase>();

	// Token: 0x04000CFB RID: 3323
	public List<BagItem> bagItemList = new List<BagItem>();

	// Token: 0x04000CFC RID: 3324
	public Dictionary<EquipSkillType, EquipSkillBase> equipSkillDic = new Dictionary<EquipSkillType, EquipSkillBase>();

	// Token: 0x04000CFD RID: 3325
	public Dictionary<CardSkillType, List<CardSkillBase>> cardSkillListDic = new Dictionary<CardSkillType, List<CardSkillBase>>();

	// Token: 0x04000CFE RID: 3326
	public List<MedicineBase> medicineDic = new List<MedicineBase>();

	// Token: 0x04000CFF RID: 3327
	private float lastEquipNormalAttackAddDamage;

	// Token: 0x04000D00 RID: 3328
	private float lastEquipStaAllAdd;

	// Token: 0x04000D01 RID: 3329
	private float lastEquipStrAllAdd;

	// Token: 0x04000D02 RID: 3330
	private float lastEquipAgiAllAdd;

	// Token: 0x04000D03 RID: 3331
	private float lastEquipAttackPercent;

	// Token: 0x04000D04 RID: 3332
	private float lastEquipAddDamage;

	// Token: 0x04000D05 RID: 3333
	private float lastEquipSkillNoneDamage;

	// Token: 0x04000D06 RID: 3334
	private float lastEquipFireDamage;

	// Token: 0x04000D07 RID: 3335
	private float lastEquipIceDamage;

	// Token: 0x04000D08 RID: 3336
	private float lastEquipLightDamage;

	// Token: 0x04000D09 RID: 3337
	private float lastEquipSkillRange;

	// Token: 0x04000D0A RID: 3338
	private float lastEquipSkillTime;

	// Token: 0x04000D0B RID: 3339
	private float lastEquipAttackDistance;

	// Token: 0x04000D0C RID: 3340
	private float lastEquipBuffDamage;

	// Token: 0x04000D0D RID: 3341
	private float lastEquipHaloRangeAdd;

	// Token: 0x04000D0E RID: 3342
	private float lastEquipForgeAdd;

	// Token: 0x04000D0F RID: 3343
	private float lastEquipMaxHpAddPercent;

	// Token: 0x04000D10 RID: 3344
	private float lastEquipSkillExpend;

	// Token: 0x04000D11 RID: 3345
	private int lastEquipReduceInjury;

	// Token: 0x04000D12 RID: 3346
	private int lastEquipExtraDamage;

	// Token: 0x04000D13 RID: 3347
	private float lastEquipCastSpeed;

	// Token: 0x04000D14 RID: 3348
	private float lastEquipHpAddUpgrade;

	// Token: 0x04000D15 RID: 3349
	private float lastEquipAddCallMonster;

	// Token: 0x04000D16 RID: 3350
	private float lastEquipAddCallMonsterTime;

	// Token: 0x04000D17 RID: 3351
	private float lastEquipAddHenshin;

	// Token: 0x04000D18 RID: 3352
	private float lastEquipAddHenshinTime;

	// Token: 0x04000D19 RID: 3353
	private float lastEquipArmedAdd;

	// Token: 0x04000D1A RID: 3354
	private float lastEquipHpSecRate;

	// Token: 0x04000D1B RID: 3355
	private float lastEquipMagicXiXue;

	// Token: 0x04000D1C RID: 3356
	private float lastEquipEffectDamage;

	// Token: 0x04000D1D RID: 3357
	private int nowExp;
}
