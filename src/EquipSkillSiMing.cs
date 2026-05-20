using System;
using UnityEngine;

// Token: 0x020000F8 RID: 248
public class EquipSkillSiMing : EquipSkillBase
{
	// Token: 0x0600051A RID: 1306 RVA: 0x0001E2C7 File Offset: 0x0001C4C7
	public override void Init()
	{
		base.Init();
		PlayerBase playerBase = this.playerBase;
		playerBase.dieEvent = (RoleBase.DieEvent)Delegate.Combine(playerBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
		this.cdTime = Time.time;
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x0001E304 File Offset: 0x0001C504
	private void DieEvent(RoleBase role)
	{
		if (Time.time < this.cdTime)
		{
			return;
		}
		if (role.hasAuthority)
		{
			role.CmdRelifeByHp(ConstDefine.ClampBattleValue((double)role.maxHp * 0.25));
			float lifeTime = this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel;
			role.roleBuffManager.AddOneBuff<Buff无敌>("Buff无敌", lifeTime);
			float num = this.skillValueAry[1] + this.skillValueUpAry[1] * (float)this.strengLevel;
			this.cdTime = Time.time + num;
			if (role.isLocalPlayer)
			{
				GameHelperClient.AddShowBuff(Game.Language.Get(PathDefine.Concat("equip_", this.equipIndex), ""), GameHelperClient.localPlayer.playerAttribute.GetEquipSkillInfo(this.equipSkillType), PathDefine.Concat("Shop/equip_", this.equipIndex), num);
				Util.ShowTipsNoLanguage(Game.Language.Get(PathDefine.Concat("equip_", this.equipIndex), ""));
			}
		}
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x0001E410 File Offset: 0x0001C610
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.dieEvent = (RoleBase.DieEvent)Delegate.Remove(playerBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
	}

	// Token: 0x04000480 RID: 1152
	private float checkTime;

	// Token: 0x04000481 RID: 1153
	private float cdTime;
}
