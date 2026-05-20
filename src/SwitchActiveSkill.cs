using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002FB RID: 763
public class SwitchActiveSkill : ActiveSkillBase
{
	// Token: 0x060011A1 RID: 4513 RVA: 0x000671C8 File Offset: 0x000653C8
	protected void InitSwitchSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, int skillBookId)
	{
		this.activeSkillEnum = activeSkillType;
		if (attackRole.isLocalPlayer)
		{
			List<SkillBase> roleSkillList = GameHelperClient.localPlayer.roleSkillList;
			int count = roleSkillList.Count;
			for (int i = 0; i < count; i++)
			{
				if (roleSkillList[i].skillBookId == skillBookId)
				{
					this.roleSkillBase = roleSkillList[i];
					this.roleSkillBase.useSkillId = this.skillId;
					this.roleSkillBase.isSwitch = true;
					this.curSkillBookId = skillBookId;
					GameObject switchGo = this.roleSkillBase.skillUI.switchGo;
					if (!switchGo.activeSelf)
					{
						switchGo.SetActive(true);
					}
					this.nextMpCostTime = this.skillTime - 1f;
					return;
				}
			}
			return;
		}
		if (attackRole.roleType == RoleType.King && attackRole.hasAuthority)
		{
			PlayerBase playerBase = attackRole as PlayerBase;
			if (playerBase != null)
			{
				playerBase.StartSwitchSkill(activeSkillType);
			}
		}
	}

	// Token: 0x060011A2 RID: 4514 RVA: 0x000672A0 File Offset: 0x000654A0
	protected override void UpdateLocalSkill(float time)
	{
		base.UpdateLocalSkill(time);
		if (this.roleSkillBase != null)
		{
			List<SkillBase> roleSkillList = GameHelperClient.localPlayer.roleSkillList;
			bool flag = false;
			int count = roleSkillList.Count;
			for (int i = 0; i < count; i++)
			{
				if (roleSkillList[i].skillBookId == this.curSkillBookId)
				{
					flag = true;
					break;
				}
			}
			if (!flag || this.attackRoleBase.IsDead())
			{
				GameHelperClient.localPlayer.OnCloseSwitchSkill(this.roleSkillBase, this.skillId);
				return;
			}
		}
		else if (this.attackRoleBase != null && this.attackRoleBase.IsDead())
		{
			GameHelperClient.localPlayer.CmdClearSkill(this.skillId);
			if (this.attackRoleBase.roleType == RoleType.King)
			{
				PlayerBase playerBase = this.attackRoleBase as PlayerBase;
				if (playerBase != null)
				{
					playerBase.ClearSwitchSkill(this.activeSkillEnum);
				}
			}
		}
	}

	// Token: 0x060011A3 RID: 4515 RVA: 0x00067372 File Offset: 0x00065572
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		this.roleSkillBase = null;
	}

	// Token: 0x04000FC5 RID: 4037
	private SkillBase roleSkillBase;

	// Token: 0x04000FC6 RID: 4038
	private float nextMpCostTime;

	// Token: 0x04000FC7 RID: 4039
	private int curSkillBookId;
}
