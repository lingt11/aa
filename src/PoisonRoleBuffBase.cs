using System;
using UnityEngine;

// Token: 0x020000AD RID: 173
public class PoisonRoleBuffBase : RoleBuffBase
{
	// Token: 0x06000345 RID: 837 RVA: 0x00015878 File Offset: 0x00013A78
	public override void UpdateBuff()
	{
		base.UpdateBuff();
		if (this.roleBase.isLocalPlayer)
		{
			if (this.buffTime < this.checkTime)
			{
				this.checkTime -= this.checkOffset;
				bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Buff);
				Util.OnLocalPlayerHit(this.attackRoleBase, this.roleBase, (double)(Mathf.RoundToInt(this.buffValue) * (this.level + 1)), Util.GetV2Angle(this.roleBase.MyTransform.position, this.attackRoleBase.MyTransform.position), AttackType.Buff, isAttackWeek);
				return;
			}
		}
		else if (this.attackRoleBase != null && this.attackRoleBase.hasAuthority && this.buffTime < this.checkTime)
		{
			this.checkTime -= this.checkOffset;
			bool isAttackWeek2 = this.attackRoleBase.GetIsAttackWeek(AttackType.Buff);
			Util.OnLocalPlayerHit(this.attackRoleBase, this.roleBase, (double)(Mathf.RoundToInt(this.buffValue) * (this.level + 1)), Util.GetV2Angle(this.roleBase.MyTransform.position, this.attackRoleBase.MyTransform.position), AttackType.Buff, isAttackWeek2);
		}
	}

	// Token: 0x06000346 RID: 838 RVA: 0x000159B8 File Offset: 0x00013BB8
	public void AddLevel(float updateBuffValue, float updateTime, int levelValue)
	{
		this.buffValue = Mathf.Max(this.buffValue, updateBuffValue);
		float num = this.buffTime - this.checkTime;
		this.buffTime = updateTime;
		this.checkTime = this.buffTime - num;
		this.level += levelValue;
	}

	// Token: 0x04000335 RID: 821
	public float checkTime;

	// Token: 0x04000336 RID: 822
	public int level;
}
