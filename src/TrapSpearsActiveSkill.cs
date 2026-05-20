using System;
using UnityEngine;

// Token: 0x020002FD RID: 765
public class TrapSpearsActiveSkill : ActiveSkillBase
{
	// Token: 0x060011A8 RID: 4520 RVA: 0x000675C0 File Offset: 0x000657C0
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.skillTime = this.activeSkillData.duration;
		GameObject gameObject = AssetManager.LoadPrefab(EffectDefine.TrapSpears, null, true);
		gameObject.transform.position = pos;
		this.trapSpears = gameObject.GetComponent<GamePlayItemTrapSpears>();
		this.trapSpears.Init(this.skillId);
	}

	// Token: 0x060011A9 RID: 4521 RVA: 0x00067638 File Offset: 0x00065838
	protected override void UpdateLocalSkill(float deltaTime)
	{
		base.UpdateLocalSkill(deltaTime);
		GamePlayItemTrapSpears gamePlayItemTrapSpears = this.trapSpears;
		if (gamePlayItemTrapSpears == null)
		{
			return;
		}
		gamePlayItemTrapSpears.LocalUpdateEvent();
	}

	// Token: 0x060011AA RID: 4522 RVA: 0x00067651 File Offset: 0x00065851
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		GamePlayItemTrapSpears gamePlayItemTrapSpears = this.trapSpears;
		if (gamePlayItemTrapSpears == null)
		{
			return;
		}
		gamePlayItemTrapSpears.UpdateEvent();
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x0006766A File Offset: 0x0006586A
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.trapSpears != null)
		{
			AssetManager.UnLoadPrefab(this.trapSpears.gameObject, false);
			this.trapSpears = null;
		}
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x00067699 File Offset: 0x00065899
	public override void StartSkillAciton()
	{
		base.StartSkillAciton();
		GamePlayItemTrapSpears gamePlayItemTrapSpears = this.trapSpears;
		if (gamePlayItemTrapSpears == null)
		{
			return;
		}
		gamePlayItemTrapSpears.OnStartTrigger();
	}

	// Token: 0x060011AD RID: 4525 RVA: 0x000676B1 File Offset: 0x000658B1
	public override void EndSkillAciton()
	{
		base.StartSkillAciton();
		GamePlayItemTrapSpears gamePlayItemTrapSpears = this.trapSpears;
		if (gamePlayItemTrapSpears == null)
		{
			return;
		}
		gamePlayItemTrapSpears.OnCloseUse();
	}

	// Token: 0x04000FCB RID: 4043
	private GamePlayItemTrapSpears trapSpears;
}
