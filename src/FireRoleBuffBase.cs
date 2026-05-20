using System;
using UnityEngine;

// Token: 0x020000AB RID: 171
public class FireRoleBuffBase : RoleBuffBase
{
	// Token: 0x06000340 RID: 832 RVA: 0x00015708 File Offset: 0x00013908
	public override void UpdateBuff()
	{
		base.UpdateBuff();
		if (this.roleBase.isLocalPlayer)
		{
			if (this.buffTime < this.checkTime)
			{
				this.checkTime -= this.checkOffset;
				bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Buff);
				Util.OnLocalPlayerHit(this.attackRoleBase, this.roleBase, (double)Mathf.RoundToInt(this.buffValue), Util.GetV2Angle(this.roleBase.MyTransform.position, this.attackRoleBase.MyTransform.position), AttackType.Buff, isAttackWeek);
				return;
			}
		}
		else if (this.attackRoleBase != null && this.attackRoleBase.hasAuthority && this.buffTime < this.checkTime)
		{
			this.checkTime -= this.checkOffset;
			bool isAttackWeek2 = this.attackRoleBase.GetIsAttackWeek(AttackType.Buff);
			Util.OnLocalPlayerHit(this.attackRoleBase, this.roleBase, (double)Mathf.RoundToInt(this.buffValue), Util.GetV2Angle(this.roleBase.MyTransform.position, this.attackRoleBase.MyTransform.position), AttackType.Buff, isAttackWeek2);
		}
	}

	// Token: 0x04000334 RID: 820
	public float checkTime;
}
