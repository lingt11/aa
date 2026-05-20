using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001BF RID: 447
public class H土豆兄弟 : PasssiveSkill
{
	// Token: 0x06000843 RID: 2115 RVA: 0x0002F6F4 File Offset: 0x0002D8F4
	public override void Enter()
	{
		this.initRoleMode = this.roleBase.RoleModeBase;
		if (this.initRoleMode != null)
		{
			this.initRoleMode.canAttack = false;
		}
		if (this.roleBase.hasAuthority)
		{
			if (this.roleBase.roleType == RoleType.Player)
			{
				this.brotatoWeaponType = this.randomSkills[Random.Range(0, this.randomSkills.Count)];
				this.AddSkill();
			}
			PlayerBase roleBase = this.roleBase;
			roleBase.onPlayerLevelUp = (Action)Delegate.Combine(roleBase.onPlayerLevelUp, new Action(this.OnPlayerLevelUp));
		}
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x0002F798 File Offset: 0x0002D998
	public override void Exit()
	{
		if (this.initRoleMode != null)
		{
			this.initRoleMode.canAttack = true;
		}
		if (this.roleBase.hasAuthority)
		{
			foreach (uint skillId in this.skillIndexList)
			{
				this.roleBase.CmdRemoveBrotatoWeapon(skillId);
			}
			PlayerBase roleBase = this.roleBase;
			roleBase.onPlayerLevelUp = (Action)Delegate.Remove(roleBase.onPlayerLevelUp, new Action(this.OnPlayerLevelUp));
		}
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x0002F840 File Offset: 0x0002DA40
	private void OnPlayerLevelUp()
	{
		this.AddSkill();
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x0002F848 File Offset: 0x0002DA48
	public override int GetSaveSkillData()
	{
		return (int)this.brotatoWeaponType;
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x0002F850 File Offset: 0x0002DA50
	private void AddSkill()
	{
		int grade = this.GetGrade();
		if (grade != this.curGrade)
		{
			if (this.curGrade != -1)
			{
				foreach (uint skillId in this.skillIndexList)
				{
					this.roleBase.CmdRemoveBrotatoWeapon(skillId);
				}
			}
			this.curGrade = grade;
			Dictionary<string, object> dictionary = (Dictionary<string, object>)ExcelManager.allExcelData["passsiveSkill"];
			int num = this.randomSkills.IndexOf(this.brotatoWeaponType);
			int num2 = this.randomSkillIds[this.curGrade] + num;
			object obj = dictionary[num2.ToString()];
			string text = obj.DIC("value");
			if (this.roleBase.isLocalPlayer)
			{
				this.exDec = string.Format(ColorDefine.QuaText[this.curGrade], Game.Language.Get(PathDefine.Concat("p_", num2), "")) + "   " + SkillBase.GetSkillInfo(num2.ToString(), obj, true, false);
			}
			int num3 = 1;
			if (this.roleBase.playerAttribute.cardSkillListDic.ContainsKey(CardSkillType.SecBrotatoWeapon))
			{
				num3++;
			}
			this.skillIndexList.Clear();
			for (int i = 0; i < num3; i++)
			{
				uint syncPassSkillIndex = SkillManager.GetSyncPassSkillIndex();
				this.skillIndexList.Add(syncPassSkillIndex);
				if (!string.IsNullOrEmpty(text))
				{
					float[] array = Array.ConvertAll<string, float>(text.Split('|', StringSplitOptions.None), new Converter<string, float>(float.Parse));
					array[0] *= 1.2f;
					array[1] *= 1.2f;
					this.roleBase.CmdAddBrotatoWeapon(this.brotatoWeaponType, syncPassSkillIndex, array, this.curGrade);
				}
			}
		}
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x0002FA34 File Offset: 0x0002DC34
	public void InitKingData(int kingData)
	{
		if (this.roleBase.hasAuthority)
		{
			this.brotatoWeaponType = (BrotatoWeaponType)kingData;
			this.AddSkill();
		}
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x0002FA50 File Offset: 0x0002DC50
	private int GetGrade()
	{
		int level = this.roleBase.Level;
		int num = this.GradeIndex.Length;
		int result = 0;
		for (int i = 0; i < num; i++)
		{
			if (level >= this.GradeIndex[i])
			{
				result = i + 1;
			}
		}
		return result;
	}

	// Token: 0x04000B7D RID: 2941
	private BrotatoWeaponType brotatoWeaponType;

	// Token: 0x04000B7E RID: 2942
	private List<uint> skillIndexList = new List<uint>();

	// Token: 0x04000B7F RID: 2943
	private int curGrade = -1;

	// Token: 0x04000B80 RID: 2944
	private readonly int[] randomSkillIds = new int[]
	{
		34,
		137,
		236,
		313,
		408
	};

	// Token: 0x04000B81 RID: 2945
	private readonly List<BrotatoWeaponType> randomSkills = new List<BrotatoWeaponType>
	{
		BrotatoWeaponType.Sword,
		BrotatoWeaponType.Pistol,
		BrotatoWeaponType.RPG,
		BrotatoWeaponType.Flamethrower
	};

	// Token: 0x04000B82 RID: 2946
	private readonly int[] GradeIndex = new int[]
	{
		5,
		15,
		30,
		100
	};

	// Token: 0x04000B83 RID: 2947
	private const float AddDamageLevel = 0.2f;

	// Token: 0x04000B84 RID: 2948
	private RoleModeBase initRoleMode;
}
