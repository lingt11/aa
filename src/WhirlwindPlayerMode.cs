using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002A9 RID: 681
public class WhirlwindPlayerMode : MeleePlayerMode
{
	// Token: 0x06000EEC RID: 3820 RVA: 0x000564AC File Offset: 0x000546AC
	public override void AttackUpdate()
	{
		float deltaTime = Time.deltaTime;
		this.playerBase.timer += deltaTime;
		float realOffsetInAttack = this.playerBase.GetRealOffsetInAttack();
		if (this.playerBase.timer > realOffsetInAttack)
		{
			if (this.playerBase.hasAuthority)
			{
				this.playerBase.UpdateRoleState(RoleState.Idle);
				return;
			}
		}
		else
		{
			float num = this.playerBase.timer / realOffsetInAttack;
			if (!this.playerBase.isCheckAttack && num > 0.25f && this.playerBase.trackRoleBase != null)
			{
				this.playerBase.isCheckAttack = true;
				if (this.playerBase.hasAuthority)
				{
					List<RoleBase> attackRoles = this.playerBase.GetAttackRoles();
					int count = attackRoles.Count;
					Vector3 position = this.roleBase.MyTransform.position;
					bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Normal);
					long playerNormalAttackPower = this.playerBase.GetPlayerNormalAttackPower();
					for (int i = 0; i < count; i++)
					{
						RoleBase roleBase = attackRoles[i];
						if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(position, roleBase.MyTransform.position, base.GetAttackDistance() * (1f + this.playerBase.skillRange) + roleBase.RoleModeBase.addRange, false))
						{
							Util.OnLocalPlayerHit(this.roleBase, roleBase, (double)playerNormalAttackPower, Util.GetV2Angle(roleBase.MyTransform.position, position), AttackType.Normal, isAttackWeek);
						}
					}
				}
			}
			if (this.playerBase.hasAuthority)
			{
				if (num < 0.5f && num < 0.3f)
				{
					this.playerBase.TrackRotation(3f);
				}
				if (this.playerBase.CheckIsInputMove(num) && !this.playerBase.isCheckAttack)
				{
					this.playerBase.timer = realOffsetInAttack;
				}
			}
		}
	}

	// Token: 0x06000EED RID: 3821 RVA: 0x000566A0 File Offset: 0x000548A0
	public override void OnStartAttack()
	{
		base.OnStartAttack();
		this.attackEffect.transform.localScale = Vector3.one * (base.GetAttackDistance() / 2.2f * 2.5f * (1f + this.playerBase.skillRange));
	}
}
