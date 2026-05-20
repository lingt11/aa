using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000286 RID: 646
public class CardManager
{
	// Token: 0x06000C11 RID: 3089 RVA: 0x00042D08 File Offset: 0x00040F08
	public CardManager()
	{
		this.teamCards = new List<int>();
		foreach (int key in SaveLoadManager.gameSaveData.equipCards)
		{
			CardData cardData;
			if (Game.GameData.CardDataDic.TryGetValue(key, out cardData))
			{
				int capacity = cardData.capacity;
				this.curPower += capacity;
			}
		}
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x00042D94 File Offset: 0x00040F94
	public void RefreshEquipCardsFromSave(bool refreshUI = true)
	{
		this.curPower = 0;
		foreach (int key in SaveLoadManager.gameSaveData.equipCards)
		{
			CardData cardData;
			if (Game.GameData.CardDataDic.TryGetValue(key, out cardData))
			{
				this.curPower += cardData.capacity;
			}
		}
		if (refreshUI)
		{
			UI_MyCard ui = Game.UI.GetUI<UI_MyCard>();
			if (ui == null)
			{
				return;
			}
			ui.RefreshDeckView();
		}
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x00042E2C File Offset: 0x0004102C
	public void ApplyDeck()
	{
		this.isApplyDeck = true;
		CardEntries cardEntries = default(CardEntries);
		foreach (int cardId in SaveLoadManager.gameSaveData.equipCards)
		{
			cardEntries = this.ApplyCard(cardId, cardEntries);
		}
		foreach (int cardId2 in this.teamCards)
		{
			cardEntries = this.ApplyCard(cardId2, cardEntries);
		}
		GameHelperClient.localPlayer.playerAttribute.CheckExCardData(SaveLoadManager.gameSaveData.equipCards, false, ref cardEntries);
		GameHelperClient.localPlayer.playerAttribute.CheckExCardData(this.teamCards, true, ref cardEntries);
		this.PlayerAddCard(cardEntries);
		GameHelperClient.CheckCardCheat();
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x00042F18 File Offset: 0x00041118
	public void AddEquipCard(int cardId)
	{
		int capacity = Game.GameData.CardDataDic[cardId].capacity;
		SaveLoadManager.gameSaveData.equipCards.Add(cardId);
		this.curPower += capacity;
		UI_MyCard ui = Game.UI.GetUI<UI_MyCard>();
		if (ui == null)
		{
			return;
		}
		ui.ShowCurPower(this.curPower);
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x00042F74 File Offset: 0x00041174
	public void GetCard(int cardId)
	{
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
		CardManager.HaveCardData value = default(CardManager.HaveCardData);
		value.cardId = cardId;
		value.haveNum = 1;
		SaveLoadManager.haveCardDataDic.Add(cardId, value);
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x00042FE8 File Offset: 0x000411E8
	public void RemoveEquipCard(int cardId)
	{
		int capacity = Game.GameData.CardDataDic[cardId].capacity;
		SaveLoadManager.gameSaveData.equipCards.Remove(cardId);
		this.curPower -= capacity;
		this.GetCard(cardId);
		UI_MyCard ui = Game.UI.GetUI<UI_MyCard>();
		if (ui != null)
		{
			ui.UpdateCardNum(cardId);
		}
		UI_MyCard ui2 = Game.UI.GetUI<UI_MyCard>();
		if (ui2 == null)
		{
			return;
		}
		ui2.ShowCurPower(this.curPower);
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x00043064 File Offset: 0x00041264
	private CardEntries ApplyCard(int cardId, CardEntries cardEntries)
	{
		if (!Game.GameData.CardDataDic.ContainsKey(cardId))
		{
			return cardEntries;
		}
		cardEntries = CardManager.AddCardEntries(cardId, cardEntries);
		CardData cardData = Game.GameData.CardDataDic[cardId];
		if (cardData.cardSkill != CardSkillType.None)
		{
			GameHelperClient.localPlayer.playerAttribute.AddCardSkill(cardData.cardSkill, cardData.id);
		}
		return cardEntries;
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x000430C4 File Offset: 0x000412C4
	public static CardEntries AddCardEntries(int cardId, CardEntries cardEntries)
	{
		CardData cardData = Game.GameData.CardDataDic[cardId];
		cardEntries.critical += cardData.entries.critical;
		cardEntries.criticalDamage += cardData.entries.criticalDamage;
		cardEntries.attack += cardData.entries.attack;
		cardEntries.attackSpeed += cardData.entries.attackSpeed;
		cardEntries.attackAddHp += cardData.entries.attackAddHp;
		cardEntries.moveSpeed += cardData.entries.moveSpeed;
		cardEntries.sta += cardData.entries.sta;
		cardEntries.agi += cardData.entries.agi;
		cardEntries.str += cardData.entries.str;
		cardEntries.armor += cardData.entries.armor;
		cardEntries.hpAdd += cardData.entries.hpAdd;
		cardEntries.mpAdd += cardData.entries.mpAdd;
		cardEntries.startMoney += cardData.entries.startMoney;
		cardEntries.startGem += cardData.entries.startGem;
		cardEntries.lucky += cardData.entries.lucky;
		cardEntries.skillDamage += cardData.entries.skillDamage;
		cardEntries.skillRange += cardData.entries.skillRange;
		cardEntries.skillTime += cardData.entries.skillTime;
		cardEntries.skillExpend += cardData.entries.skillExpend;
		cardEntries.skillCd += cardData.entries.skillCd;
		cardEntries.expAdd += cardData.entries.expAdd;
		cardEntries.normalDamage += cardData.entries.normalDamage;
		cardEntries.maxHp += cardData.entries.maxHp;
		cardEntries.maxMp += cardData.entries.maxMp;
		cardEntries.normalBreak += cardData.entries.normalBreak;
		cardEntries.skillBreak += cardData.entries.skillBreak;
		cardEntries.allDamage += cardData.entries.allDamage;
		cardEntries.addMoney += cardData.entries.addMoney;
		cardEntries.addEnemyLimit += cardData.entries.addEnemyLimit;
		cardEntries.refreshNum += cardData.entries.refreshNum;
		cardEntries.lifeStealing += cardData.entries.lifeStealing;
		cardEntries.reduceInjury += cardData.entries.reduceInjury;
		cardEntries.extraDamage += cardData.entries.extraDamage;
		cardEntries.dodge += cardData.entries.dodge;
		cardEntries.hpPercent += cardData.entries.hpPercent;
		cardEntries.hpSecRate += cardData.entries.hpSecRate;
		cardEntries.skillReduction += cardData.entries.skillReduction;
		cardEntries.strPercent += cardData.entries.strPercent;
		cardEntries.agiPercent += cardData.entries.agiPercent;
		cardEntries.staPercent += cardData.entries.staPercent;
		cardEntries.attackDistance += cardData.entries.attackDistance;
		cardEntries.fireDamage += cardData.entries.fireDamage;
		cardEntries.iceDamage += cardData.entries.iceDamage;
		cardEntries.lightDamage += cardData.entries.lightDamage;
		cardEntries.relicAdd += cardData.entries.relicAdd;
		cardEntries.bookAdd += cardData.entries.bookAdd;
		cardEntries.forgingAdd += cardData.entries.forgingAdd;
		cardEntries.effectDamage += cardData.entries.effectDamage;
		cardEntries.buffDamage += cardData.entries.buffDamage;
		cardEntries.relifeTime += cardData.entries.relifeTime;
		cardEntries.addCall += cardData.entries.addCall;
		cardEntries.addHenshin += cardData.entries.addHenshin;
		cardEntries.addNormalEnemy += cardData.entries.addNormalEnemy;
		cardEntries.addBossEnemy += cardData.entries.addBossEnemy;
		cardEntries.attackPercent += cardData.entries.attackPercent;
		cardEntries.forgingAddValue += cardData.entries.forgingAddValue;
		cardEntries.equipAddValue += cardData.entries.equipAddValue;
		cardEntries.hpAddUpgrade += cardData.entries.hpAddUpgrade;
		cardEntries.armedAdd += cardData.entries.armedAdd;
		cardEntries.castSpeed += cardData.entries.castSpeed;
		return cardEntries;
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x0004360C File Offset: 0x0004180C
	public static CardEntries AddCardEntriesByLevel(int cardId, CardEntries cardEntries, float level)
	{
		CardData cardData = Game.GameData.CardDataDic[cardId];
		cardEntries.critical += cardData.entries.critical * level;
		cardEntries.criticalDamage += cardData.entries.criticalDamage * level;
		cardEntries.attack += Mathf.RoundToInt((float)cardData.entries.attack * level);
		cardEntries.attackSpeed += cardData.entries.attackSpeed * level;
		cardEntries.attackAddHp += Mathf.RoundToInt((float)cardData.entries.attackAddHp * level);
		cardEntries.moveSpeed += cardData.entries.moveSpeed * level;
		cardEntries.sta += Mathf.RoundToInt((float)cardData.entries.sta * level);
		cardEntries.agi += Mathf.RoundToInt((float)cardData.entries.agi * level);
		cardEntries.str += Mathf.RoundToInt((float)cardData.entries.str * level);
		cardEntries.armor += Mathf.RoundToInt((float)cardData.entries.armor * level);
		cardEntries.hpAdd += Mathf.RoundToInt((float)cardData.entries.hpAdd * level);
		cardEntries.mpAdd += Mathf.RoundToInt((float)cardData.entries.mpAdd * level);
		cardEntries.startMoney += Mathf.RoundToInt((float)cardData.entries.startMoney * level);
		cardEntries.startGem += Mathf.RoundToInt((float)cardData.entries.startGem * level);
		cardEntries.lucky += Mathf.RoundToInt((float)cardData.entries.lucky * level);
		cardEntries.skillDamage += cardData.entries.skillDamage * level;
		cardEntries.skillRange += cardData.entries.skillRange * level;
		cardEntries.skillTime += cardData.entries.skillTime * level;
		cardEntries.skillExpend += cardData.entries.skillExpend * level;
		cardEntries.skillCd += Mathf.RoundToInt((float)cardData.entries.skillCd * level);
		cardEntries.expAdd += cardData.entries.expAdd * level;
		cardEntries.normalDamage += cardData.entries.normalDamage * level;
		cardEntries.maxHp += Mathf.RoundToInt((float)cardData.entries.maxHp * level);
		cardEntries.maxMp += Mathf.RoundToInt((float)cardData.entries.maxMp * level);
		cardEntries.normalBreak += cardData.entries.normalBreak * level;
		cardEntries.skillBreak += cardData.entries.skillBreak * level;
		cardEntries.allDamage += cardData.entries.allDamage * level;
		cardEntries.addMoney += cardData.entries.addMoney * level;
		cardEntries.addEnemyLimit += Mathf.RoundToInt((float)cardData.entries.addEnemyLimit * level);
		cardEntries.refreshNum += Mathf.RoundToInt((float)cardData.entries.refreshNum * level);
		cardEntries.lifeStealing += cardData.entries.lifeStealing * level;
		cardEntries.reduceInjury += Mathf.RoundToInt((float)cardData.entries.reduceInjury * level);
		cardEntries.extraDamage += Mathf.RoundToInt((float)cardData.entries.extraDamage * level);
		cardEntries.dodge += Mathf.RoundToInt((float)cardData.entries.dodge * level);
		cardEntries.hpPercent += cardData.entries.hpPercent * level;
		cardEntries.hpSecRate += cardData.entries.hpSecRate * level;
		cardEntries.skillReduction += Mathf.RoundToInt((float)cardData.entries.skillReduction * level);
		cardEntries.strPercent += cardData.entries.strPercent * level;
		cardEntries.agiPercent += cardData.entries.agiPercent * level;
		cardEntries.staPercent += cardData.entries.staPercent * level;
		cardEntries.attackDistance += cardData.entries.attackDistance * level;
		cardEntries.fireDamage += cardData.entries.fireDamage * level;
		cardEntries.iceDamage += cardData.entries.iceDamage * level;
		cardEntries.lightDamage += cardData.entries.lightDamage * level;
		cardEntries.relicAdd += cardData.entries.relicAdd * level;
		cardEntries.bookAdd += cardData.entries.bookAdd * level;
		cardEntries.forgingAdd += cardData.entries.forgingAdd * level;
		cardEntries.effectDamage += cardData.entries.effectDamage * level;
		cardEntries.buffDamage += cardData.entries.buffDamage * level;
		cardEntries.relifeTime += Mathf.RoundToInt((float)cardData.entries.relifeTime * level);
		cardEntries.addCall += cardData.entries.addCall * level;
		cardEntries.addHenshin += cardData.entries.addHenshin * level;
		cardEntries.addNormalEnemy += cardData.entries.addNormalEnemy * level;
		cardEntries.addBossEnemy += cardData.entries.addBossEnemy * level;
		cardEntries.attackPercent += cardData.entries.attackPercent * level;
		cardEntries.forgingAddValue += cardData.entries.forgingAddValue * level;
		cardEntries.equipAddValue += cardData.entries.equipAddValue * level;
		cardEntries.hpAddUpgrade += cardData.entries.hpAddUpgrade * level;
		cardEntries.armedAdd += cardData.entries.armedAdd * level;
		cardEntries.castSpeed += cardData.entries.castSpeed * level;
		return cardEntries;
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x00043C4C File Offset: 0x00041E4C
	private void PlayerAddCard(CardEntries cardEntries)
	{
		if (!Mathf.Approximately(cardEntries.critical, 0f))
		{
			GameHelperClient.localPlayer.AddCritical(cardEntries.critical);
		}
		if (!Mathf.Approximately(cardEntries.criticalDamage, 0f))
		{
			GameHelperClient.localPlayer.AddCriticalDamage(cardEntries.criticalDamage);
		}
		if (cardEntries.attack != 0)
		{
			GameHelperClient.localPlayer.AddAttackPower(cardEntries.attack);
		}
		if (!Mathf.Approximately(cardEntries.attackSpeed, 0f))
		{
			GameHelperClient.localPlayer.AddAttackSpeed(cardEntries.attackSpeed);
		}
		if (cardEntries.attackAddHp != 0)
		{
			GameHelperClient.localPlayer.AddXiXue((float)cardEntries.attackAddHp);
		}
		if (!Mathf.Approximately(cardEntries.moveSpeed, 0f))
		{
			GameHelperClient.localPlayer.AddMoveSpeed(cardEntries.moveSpeed);
		}
		if (cardEntries.sta != 0)
		{
			GameHelperClient.localPlayer.AddSTA(cardEntries.sta);
		}
		if (cardEntries.agi != 0)
		{
			GameHelperClient.localPlayer.AddAGI(cardEntries.agi);
		}
		if (cardEntries.str != 0)
		{
			GameHelperClient.localPlayer.AddSTR(cardEntries.str);
		}
		if (cardEntries.armor != 0)
		{
			GameHelperClient.localPlayer.AddArmor(cardEntries.armor);
		}
		if (cardEntries.hpAdd != 0)
		{
			GameHelperClient.localPlayer.AddHpAddSec(cardEntries.hpAdd);
		}
		if (cardEntries.mpAdd != 0)
		{
			GameHelperClient.localPlayer.AddMpAddSec(cardEntries.mpAdd);
		}
		if (cardEntries.startMoney != 0)
		{
			GameHelperClient.localPlayer.AddGold(GameHelperClient.localPlayer.GetHeadUIPos(), cardEntries.startMoney, true);
		}
		if (cardEntries.startGem != 0)
		{
			GameHelperClient.localPlayer.AddGem(GameHelperClient.localPlayer.GetHeadUIPos(), cardEntries.startGem, false);
		}
		if (cardEntries.lucky != 0)
		{
			GameHelperClient.localPlayer.CmdUpdateLucky(cardEntries.lucky);
		}
		if (!Mathf.Approximately(cardEntries.skillDamage, 0f))
		{
			GameHelperClient.localPlayer.skillExDamage += cardEntries.skillDamage;
		}
		if (!Mathf.Approximately(cardEntries.skillRange, 0f))
		{
			GameHelperClient.localPlayer.CmdUpdateSkillRange(cardEntries.skillRange);
		}
		if (!Mathf.Approximately(cardEntries.skillTime, 0f))
		{
			GameHelperClient.localPlayer.CmdUpdateSkillAddTime(cardEntries.skillTime);
		}
		if (!Mathf.Approximately(cardEntries.skillExpend, 0f))
		{
			GameHelperClient.localPlayer.skillMpUsed += cardEntries.skillExpend;
		}
		if (cardEntries.skillCd != 0)
		{
			GameHelperClient.localPlayer.skillCdReduce += cardEntries.skillCd;
		}
		if (!Mathf.Approximately(cardEntries.expAdd, 0f))
		{
			GameHelperClient.localPlayer.addExp += cardEntries.expAdd;
		}
		if (!Mathf.Approximately(cardEntries.normalDamage, 0f))
		{
			GameHelperClient.localPlayer.normalAttackAddDamage += cardEntries.normalDamage;
		}
		if (cardEntries.maxHp != 0)
		{
			GameHelperClient.localPlayer.CmdUpdateMaxHp((long)cardEntries.maxHp, GameHelperClient.localPlayer.netId);
		}
		if (cardEntries.maxMp != 0)
		{
			GameHelperClient.localPlayer.AddMaxMp(cardEntries.maxMp);
		}
		if (!Mathf.Approximately(cardEntries.normalBreak, 0f))
		{
			GameHelperClient.localPlayer.normalBreakShieldBase += cardEntries.normalBreak;
			GameHelperClient.localPlayer.UpdateBreakShield();
		}
		if (!Mathf.Approximately(cardEntries.skillBreak, 0f))
		{
			GameHelperClient.localPlayer.skillBreakShieldBase += cardEntries.skillBreak;
			GameHelperClient.localPlayer.UpdateBreakShield();
		}
		if (!Mathf.Approximately(cardEntries.allDamage, 0f))
		{
			GameHelperClient.localPlayer.addDamagePercent += cardEntries.allDamage;
		}
		if (!Mathf.Approximately(cardEntries.addMoney, 0f))
		{
			GameHelperClient.localPlayer.addGoldPercent += cardEntries.addMoney;
		}
		if (cardEntries.addEnemyLimit != 0)
		{
			GameHelperClient.AddEnemyLimit += cardEntries.addEnemyLimit;
		}
		if (cardEntries.refreshNum != 0)
		{
			GameHelperClient.AddRefreshNum(cardEntries.refreshNum);
		}
		if (!Mathf.Approximately(cardEntries.lifeStealing, 0f))
		{
			GameHelperClient.localPlayer.xiXueLv += cardEntries.lifeStealing;
		}
		if (cardEntries.reduceInjury != 0)
		{
			GameHelperClient.localPlayer.UpdateReduce(cardEntries.reduceInjury);
		}
		if (cardEntries.extraDamage != 0)
		{
			GameHelperClient.localPlayer.extraDamage += cardEntries.extraDamage;
		}
		if (cardEntries.dodge != 0)
		{
			GameHelperClient.localPlayer.doge += cardEntries.dodge;
			GameHelperClient.localPlayer.CmdDoge(GameHelperClient.localPlayer.doge);
		}
		if (!Mathf.Approximately(cardEntries.hpPercent, 0f))
		{
			GameHelperClient.localPlayer.CmdUpdateMaxHpAddPercent(cardEntries.hpPercent);
		}
		if (!Mathf.Approximately(cardEntries.hpSecRate, 0f))
		{
			GameHelperClient.localPlayer.hpAddSecRate += cardEntries.hpSecRate;
		}
		if (cardEntries.skillReduction != 0)
		{
			GameHelperClient.localPlayer.UpdateSkillHitDamage(cardEntries.skillReduction);
		}
		if (!Mathf.Approximately(cardEntries.strPercent, 0f))
		{
			GameHelperClient.localPlayer.StrAllAdd += cardEntries.strPercent;
		}
		if (!Mathf.Approximately(cardEntries.agiPercent, 0f))
		{
			GameHelperClient.localPlayer.AgiAllAdd += cardEntries.agiPercent;
		}
		if (!Mathf.Approximately(cardEntries.staPercent, 0f))
		{
			GameHelperClient.localPlayer.StaAllAdd += cardEntries.staPercent;
		}
		if (!Mathf.Approximately(cardEntries.attackDistance, 0f))
		{
			GameHelperClient.localPlayer.exAttackDistance += cardEntries.attackDistance;
		}
		if (!Mathf.Approximately(cardEntries.fireDamage, 0f))
		{
			GameHelperClient.localPlayer.skillFireAdd += cardEntries.fireDamage;
		}
		if (!Mathf.Approximately(cardEntries.iceDamage, 0f))
		{
			GameHelperClient.localPlayer.skillIceAdd += cardEntries.iceDamage;
		}
		if (!Mathf.Approximately(cardEntries.lightDamage, 0f))
		{
			GameHelperClient.localPlayer.skillLightingAdd += cardEntries.lightDamage;
		}
		if (!Mathf.Approximately(cardEntries.relicAdd, 0f))
		{
			GameHelperClient.localPlayer.UpdateRelicAdd(cardEntries.relicAdd);
		}
		if (!Mathf.Approximately(cardEntries.bookAdd, 0f))
		{
			GameHelperClient.localPlayer.UpdateBookAdd(cardEntries.bookAdd);
		}
		if (!Mathf.Approximately(cardEntries.forgingAdd, 0f))
		{
			GameHelperClient.localPlayer.UpdateForgingAdd(cardEntries.forgingAdd);
		}
		if (!Mathf.Approximately(cardEntries.effectDamage, 0f))
		{
			GameHelperClient.localPlayer.addAttackEffectDamage += cardEntries.effectDamage;
		}
		if (!Mathf.Approximately(cardEntries.buffDamage, 0f))
		{
			GameHelperClient.localPlayer.buffAddDamage += cardEntries.buffDamage;
		}
		if (cardEntries.relifeTime != 0)
		{
			GameHelperClient.localPlayer.CmdUpdateAddRelifeTime(cardEntries.relifeTime);
		}
		if (!Mathf.Approximately(cardEntries.addCall, 0f))
		{
			GameHelperClient.localPlayer.addCallMonsterAttack += cardEntries.addCall;
			GameHelperClient.localPlayer.addCallMonsterHp += cardEntries.addCall;
		}
		if (!Mathf.Approximately(cardEntries.addHenshin, 0f))
		{
			GameHelperClient.localPlayer.addHenshin += cardEntries.addHenshin;
		}
		if (!Mathf.Approximately(cardEntries.addNormalEnemy, 0f))
		{
			GameHelperClient.localPlayer.addNormalEnemy += cardEntries.addNormalEnemy;
			GameHelperClient.localPlayer.addEliteEnemy += cardEntries.addNormalEnemy;
		}
		if (!Mathf.Approximately(cardEntries.addBossEnemy, 0f))
		{
			GameHelperClient.localPlayer.addBossEnemy += cardEntries.addBossEnemy;
		}
		if (!Mathf.Approximately(cardEntries.attackPercent, 0f))
		{
			GameHelperClient.localPlayer.UpdateAttackPercent(cardEntries.attackPercent);
		}
		if (!Mathf.Approximately(cardEntries.forgingAddValue, 0f))
		{
			ShopManager shopManager = EntityStatic.Get<ShopManager>();
			if (shopManager != null)
			{
				shopManager.forgingManager.forgingAdd += cardEntries.forgingAddValue;
			}
		}
		if (!Mathf.Approximately(cardEntries.equipAddValue, 0f))
		{
			GameHelperClient.localPlayer.equipAddValue += cardEntries.equipAddValue;
		}
		if (!Mathf.Approximately(cardEntries.hpAddUpgrade, 0f))
		{
			GameHelperClient.localPlayer.hpAddUpgrade += cardEntries.hpAddUpgrade;
		}
		if (!Mathf.Approximately(cardEntries.armedAdd, 0f))
		{
			GameHelperClient.localPlayer.armedAdd += cardEntries.armedAdd;
		}
		if (!Mathf.Approximately(cardEntries.castSpeed, 0f))
		{
			GameHelperClient.localPlayer.castSpeed += cardEntries.castSpeed;
		}
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x000444D8 File Offset: 0x000426D8
	public int[] GetUploadTeamCards()
	{
		List<int> list = new List<int>();
		foreach (int key in SaveLoadManager.gameSaveData.equipCards)
		{
			CardData cardData;
			if (Game.GameData.CardDataDic.TryGetValue(key, out cardData) && cardData.isTeam)
			{
				list.Add(cardData.id);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x0004455C File Offset: 0x0004275C
	public void AddTeamCard(int[] teamCardId)
	{
		if (this.isApplyDeck)
		{
			CardEntries cardEntries = default(CardEntries);
			foreach (int num in teamCardId)
			{
				cardEntries = this.ApplyCard(num, cardEntries);
				this.teamCards.Add(num);
			}
			GameHelperClient.localPlayer.playerAttribute.CheckExCardData(teamCardId.ToList<int>(), true, ref cardEntries);
			this.PlayerAddCard(cardEntries);
			return;
		}
		foreach (int item in teamCardId)
		{
			this.teamCards.Add(item);
		}
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x000445E4 File Offset: 0x000427E4
	public void AddCardTotal(int cardId, int updateValue)
	{
		CardData cardData;
		if (Game.GameData.CardDataDic.TryGetValue(cardId, out cardData))
		{
			int progress = cardData.progress;
			CardManager.HaveCardData haveCardData;
			if (SaveLoadManager.haveCardDataDic.TryGetValue(cardId, out haveCardData))
			{
				haveCardData.curProgress += updateValue;
				while (haveCardData.curProgress >= progress)
				{
					haveCardData.curProgress -= progress;
					haveCardData.haveNum++;
					if (haveCardData.haveNum > 999)
					{
						haveCardData.haveNum = 999;
					}
				}
				SaveLoadManager.haveCardDataDic[cardId] = haveCardData;
				return;
			}
			CardManager.HaveCardData value = default(CardManager.HaveCardData);
			value.cardId = cardId;
			value.curProgress = updateValue;
			while (haveCardData.curProgress >= progress)
			{
				haveCardData.curProgress -= progress;
				haveCardData.haveNum++;
				if (haveCardData.haveNum > 999)
				{
					haveCardData.haveNum = 999;
				}
			}
			SaveLoadManager.haveCardDataDic.Add(cardId, value);
		}
	}

	// Token: 0x04000CDD RID: 3293
	public int curPower;

	// Token: 0x04000CDE RID: 3294
	public List<int> teamCards;

	// Token: 0x04000CDF RID: 3295
	private bool isApplyDeck;

	// Token: 0x02000287 RID: 647
	[Serializable]
	public struct HaveCardData
	{
		// Token: 0x04000CE0 RID: 3296
		public int cardId;

		// Token: 0x04000CE1 RID: 3297
		public int haveNum;

		// Token: 0x04000CE2 RID: 3298
		public int curProgress;
	}
}
