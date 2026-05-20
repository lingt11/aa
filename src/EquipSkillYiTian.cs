using System;
using UnityEngine;

// Token: 0x02000103 RID: 259
public class EquipSkillYiTian : EquipSkillBase
{
	// Token: 0x06000552 RID: 1362 RVA: 0x0001F072 File Offset: 0x0001D272
	public override void Init()
	{
		base.Init();
		PlayerBase playerBase = this.playerBase;
		playerBase.criticalEvent = (RoleBase.Critical)Delegate.Combine(playerBase.criticalEvent, new RoleBase.Critical(this.CriticalEvent));
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x0001F0A4 File Offset: 0x0001D2A4
	private void CriticalEvent(RoleBase hurtRole, long damage)
	{
		float num = 1f - ((float)this.equipNum * this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel) * 0.01f;
		for (int i = 0; i < this.playerBase.roleSkillList.Count; i++)
		{
			SkillBase skillBase = this.playerBase.roleSkillList[i];
			if (!(skillBase is PasssiveSkill))
			{
				skillBase.updateCd *= num;
			}
		}
		if (this.playerBase.roleType == RoleType.King)
		{
			this.playerBase.PlayerKingAI.UpdateSkillCd(num);
		}
		int num2 = Mathf.RoundToInt((float)this.equipNum * this.skillValueAry[1] + this.skillValueUpAry[1] * (float)this.strengLevel);
		this.playerBase.AddMp(num2);
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x0001F174 File Offset: 0x0001D374
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.criticalEvent = (RoleBase.Critical)Delegate.Remove(playerBase.criticalEvent, new RoleBase.Critical(this.CriticalEvent));
	}
}
