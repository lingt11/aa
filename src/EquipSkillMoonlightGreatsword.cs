using System;
using UnityEngine;

// Token: 0x020000EF RID: 239
public class EquipSkillMoonlightGreatsword : EquipSkillBase
{
	// Token: 0x060004EF RID: 1263 RVA: 0x0001D605 File Offset: 0x0001B805
	public override void Init()
	{
		base.Init();
		PlayerBase playerBase = this.playerBase;
		playerBase.onStartAttackEvent = (RoleBase.OnStartAttackEvent)Delegate.Combine(playerBase.onStartAttackEvent, new RoleBase.OnStartAttackEvent(this.OnStartAttackEvent));
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0001D634 File Offset: 0x0001B834
	private void OnStartAttackEvent(RoleBase hurtRole, float realAttackOffset)
	{
		if (Time.time < this.canAttackTimer)
		{
			return;
		}
		if (hurtRole == null)
		{
			return;
		}
		int realCost = Util.GetRealCost(this.playerBase, Mathf.RoundToInt(this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel));
		if (this.playerBase.mp < realCost)
		{
			return;
		}
		this.canAttackTimer = Time.time + realAttackOffset;
		this.playerBase.AddMp(-realCost);
		this.playerBase.CmdCreateSkill(ActiveSkillEnum.MoonlightGreatsword, this.playerBase.MyTransform.position, Util.GetV2Angle(hurtRole.MyTransform.position, this.playerBase.MyTransform.position), 0, 0);
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x0001D6EE File Offset: 0x0001B8EE
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.onStartAttackEvent = (RoleBase.OnStartAttackEvent)Delegate.Remove(playerBase.onStartAttackEvent, new RoleBase.OnStartAttackEvent(this.OnStartAttackEvent));
	}

	// Token: 0x0400046F RID: 1135
	private float canAttackTimer;
}
