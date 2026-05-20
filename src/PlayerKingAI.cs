using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200029B RID: 667
public class PlayerKingAI
{
	// Token: 0x17000097 RID: 151
	// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00053ABC File Offset: 0x00051CBC
	public List<PlayerKingAI.KingAISkillCheck> AiAttackChecks
	{
		get
		{
			return this.aiAttackChecks;
		}
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x00053AC4 File Offset: 0x00051CC4
	public void InitKingAI(SaveLoadManager.PlayerKingData playerKingData, PlayerBase playerBaseValue)
	{
		this.playerBase = playerBaseValue;
		if (playerKingData.card != null && playerKingData.card.Length != 0)
		{
			for (int i = 0; i < playerKingData.card.Length; i++)
			{
				int cardId = playerKingData.card[i];
				this.playerBase.playerAttribute.AddKingCard(cardId);
			}
		}
		this.aiAttackChecks = new List<PlayerKingAI.KingAISkillCheck>();
		if (playerKingData.skill != null && playerKingData.skill.Length != 0)
		{
			int num = playerKingData.skill.Length;
			for (int j = 0; j < num; j++)
			{
				SaveLoadManager.PlayerKingSkillData playerKingSkillData = playerKingData.skill[j];
				string[] array = playerKingSkillData.skillName.Split("_", StringSplitOptions.None);
				if (array[0].Equals("a"))
				{
					ActiveSkillEnum activeSkillEnum = (ActiveSkillEnum)int.Parse(array[1]);
					ActiveSkillData activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillEnum];
					PlayerKingAI.KingAISkillCheck kingAISkillCheck = new PlayerKingAI.KingAISkillCheck();
					kingAISkillCheck.skill = activeSkillEnum;
					kingAISkillCheck.checkDistance = 10f;
					kingAISkillCheck.skillCd = activeSkillData.cd;
					this.aiAttackChecks.Add(kingAISkillCheck);
					this.playerBase.CmdUpdateSyncActiveSkillEnum(activeSkillEnum);
					this.InitPlayerKingData(activeSkillEnum, playerKingSkillData.skillData, this.playerBase);
				}
				else
				{
					string key = array[1];
					Dictionary<string, object> dictionary = (Dictionary<string, object>)ExcelManager.allExcelData["passsiveSkill"].DIC(key);
					if (!dictionary.DIC("kingLock"))
					{
						PasssiveSkill passsiveSkill = Util.GetPasssiveSkill(dictionary.DIC("class"));
						if (passsiveSkill != null)
						{
							passsiveSkill.skillBookId = GameHelperClient.GetLocalSkillBookId();
							passsiveSkill.iconName = dictionary.DIC("icon");
							passsiveSkill.cdTime = dictionary.DIC("cd");
							passsiveSkill.SetData(dictionary);
							passsiveSkill.skillName = dictionary.DIC("name");
							passsiveSkill.roleBase = this.playerBase;
							passsiveSkill.isPasssiveSkill = true;
							passsiveSkill.Enter();
							passsiveSkill.languageName = Game.Language.Get(PathDefine.Concat("p_", passsiveSkill.skillId), "");
							this.InitPlayerKingData(passsiveSkill.skillName, playerKingSkillData.skillData, passsiveSkill);
							this.playerBase.roleSkillList.Add(passsiveSkill);
						}
					}
				}
			}
		}
		if (playerKingData.equip != null && playerKingData.equip.Length != 0)
		{
			for (int k = 0; k < playerKingData.equip.Length; k++)
			{
				SaveLoadManager.PlayerKingEquipData playerKingEquipData = playerKingData.equip[k];
				this.playerBase.playerAttribute.AddKingAIEquip(playerKingEquipData.equip, playerKingEquipData.equipData, playerKingEquipData.equipEvolutionSkill);
			}
		}
		if (playerKingData.relic != null && playerKingData.relic.Length != 0)
		{
			for (int l = 0; l < playerKingData.relic.Length; l++)
			{
				SaveLoadManager.PlayerKingRelicData playerKingRelicData = playerKingData.relic[l];
				this.playerBase.AddRelic(int.Parse(playerKingRelicData.relicName), playerKingRelicData.relicLevel);
			}
		}
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x00053DBC File Offset: 0x00051FBC
	private void InitPlayerKingData(ActiveSkillEnum activeSkillEnum, int skillData, PlayerBase playerBase)
	{
		if (activeSkillEnum == ActiveSkillEnum.SoulDevourer && playerBase.RoleModeBase != null)
		{
			PlayerKoboldMode playerKoboldMode = playerBase.RoleModeBase as PlayerKoboldMode;
			if (playerKoboldMode != null)
			{
				playerKoboldMode.SkillLevel = skillData;
			}
		}
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x00053DF8 File Offset: 0x00051FF8
	private void InitPlayerKingData(string passSkillName, int skillData, PasssiveSkill passsiveSkill)
	{
		if (passSkillName.Equals("土豆兄弟"))
		{
			H土豆兄弟 h土豆兄弟 = passsiveSkill as H土豆兄弟;
			if (h土豆兄弟 != null)
			{
				h土豆兄弟.InitKingData(skillData);
			}
		}
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x00053E24 File Offset: 0x00052024
	public void UpdateEvent()
	{
		float deltaTime = Time.deltaTime;
		int count = this.aiAttackChecks.Count;
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				PlayerKingAI.KingAISkillCheck kingAISkillCheck = this.aiAttackChecks[i];
				if (kingAISkillCheck.attackCd > 0f)
				{
					kingAISkillCheck.attackCd -= deltaTime;
				}
			}
		}
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x00053E7C File Offset: 0x0005207C
	public PlayerKingAI.KingAISkillCheck StartAIAttack()
	{
		int count = this.aiAttackChecks.Count;
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				PlayerKingAI.KingAISkillCheck kingAISkillCheck = this.aiAttackChecks[i];
				if (kingAISkillCheck.attackCd <= 0f)
				{
					ActiveSkillData activeSkillData = Game.GameData.ActiveSkillDataDic[kingAISkillCheck.skill];
					if ((!activeSkillData.indicator.Equals(IndicatorDefine.Switch) || !kingAISkillCheck.isOpen) && this.playerBase.mp >= Util.GetRealCost(this.playerBase, activeSkillData.cost))
					{
						return kingAISkillCheck;
					}
				}
			}
		}
		return new PlayerKingAI.KingAISkillCheck
		{
			skill = ActiveSkillEnum.None
		};
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x00053F1B File Offset: 0x0005211B
	public void SetCd(PlayerKingAI.KingAISkillCheck kingAISkillCheck)
	{
		kingAISkillCheck.attackCd = kingAISkillCheck.skillCd * Util.GetCdReduce(this.playerBase.AllSkillCd);
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x00053F3C File Offset: 0x0005213C
	public void ClearSwitchSkill(ActiveSkillEnum activeSkill)
	{
		int count = this.aiAttackChecks.Count;
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				PlayerKingAI.KingAISkillCheck kingAISkillCheck = this.aiAttackChecks[i];
				if (kingAISkillCheck.skill == activeSkill)
				{
					kingAISkillCheck.isOpen = false;
				}
			}
		}
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x00053F84 File Offset: 0x00052184
	public void StartSwitchSkill(ActiveSkillEnum activeSkill)
	{
		int count = this.aiAttackChecks.Count;
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				PlayerKingAI.KingAISkillCheck kingAISkillCheck = this.aiAttackChecks[i];
				if (kingAISkillCheck.skill == activeSkill)
				{
					kingAISkillCheck.isOpen = true;
				}
			}
		}
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x00053FCC File Offset: 0x000521CC
	public void UpdateSkillCd(float percentage)
	{
		int count = this.aiAttackChecks.Count;
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				PlayerKingAI.KingAISkillCheck kingAISkillCheck = this.aiAttackChecks[i];
				if (kingAISkillCheck.attackCd > 0f)
				{
					kingAISkillCheck.attackCd *= percentage;
				}
			}
		}
	}

	// Token: 0x04000DA9 RID: 3497
	private List<PlayerKingAI.KingAISkillCheck> aiAttackChecks;

	// Token: 0x04000DAA RID: 3498
	private float[] attackCdList;

	// Token: 0x04000DAB RID: 3499
	private PlayerBase playerBase;

	// Token: 0x0200029C RID: 668
	public class KingAISkillCheck
	{
		// Token: 0x04000DAC RID: 3500
		public float checkDistance;

		// Token: 0x04000DAD RID: 3501
		public float attackCd;

		// Token: 0x04000DAE RID: 3502
		public float skillCd;

		// Token: 0x04000DAF RID: 3503
		public ActiveSkillEnum skill;

		// Token: 0x04000DB0 RID: 3504
		public bool isOpen;
	}
}
