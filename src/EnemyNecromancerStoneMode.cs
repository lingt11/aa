using System;
using System.Collections.Generic;
using Mirror;
using PolygonArsenal;
using UnityEngine;

// Token: 0x0200026D RID: 621
public class EnemyNecromancerStoneMode : EnemyBuildingMode
{
	// Token: 0x06000B63 RID: 2915 RVA: 0x0003C894 File Offset: 0x0003AA94
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
		this.roleBase.deadMoveSpeed = 0.75f;
		this.roleBase.deadStartMoveTime = 0;
		this.roleBase.deadEndMoveTime = 3;
		if (this.effectTransform.gameObject.activeSelf)
		{
			this.effectTransform.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x0003C8F4 File Offset: 0x0003AAF4
	public override void MoveUpdate()
	{
		if (this.polygonBeamStatic != null && this.polygonBeamStatic.BeamEnd != null && this.polygonBeamStatic.BeamEnd.activeSelf)
		{
			this.checkTime -= Time.deltaTime;
			if (this.checkTime < 0f)
			{
				this.checkTime = 0.25f;
				bool flag = this.roleBase.roleType == RoleType.Enemy;
				List<RoleBase> attackRoles = this.roleBase.GetAttackRoles();
				int count = attackRoles.Count;
				long finalAttackPower = this.roleBase.FinalAttackPower;
				bool isAttackWeek = this.roleBase.GetIsAttackWeek(AttackType.Skill);
				for (int i = 0; i < count; i++)
				{
					RoleBase roleBase = attackRoles[i];
					if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.polygonBeamStatic.BeamEndTransform.position, roleBase.MyTransform.position, 1f + roleBase.RoleModeBase.addRange, false))
					{
						if (flag)
						{
							roleBase.OnHit(this.roleBase, (double)finalAttackPower, this.effectTransform.eulerAngles.y, AttackType.Skill, isAttackWeek);
						}
						else
						{
							Util.OnLocalPlayerHit(this.roleBase, roleBase, (double)finalAttackPower, this.effectTransform.eulerAngles.y, AttackType.Skill, isAttackWeek);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x0003CA71 File Offset: 0x0003AC71
	public override void OnStartShowPose()
	{
		base.OnStartShowPose();
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x0003CA79 File Offset: 0x0003AC79
	public override void OnStartDead()
	{
		base.OnStartDead();
		if (this.effectTransform.gameObject.activeSelf)
		{
			this.effectTransform.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x0003CAA4 File Offset: 0x0003ACA4
	public override void OnExitShowPose()
	{
		base.OnExitShowPose();
		if (this.effectTransform != null)
		{
			if (!this.effectTransform.gameObject.activeSelf)
			{
				this.effectTransform.gameObject.SetActive(true);
			}
			this.effectTransform.localScale = new Vector3(1f, 1f, 1f);
			this.effectTransform.SetParent(this.roleBase.MyTransform);
			this.effectTransform.localPosition = new Vector3(0f, 1.35f, 0f);
			int fatherId = this.roleBase.FatherId;
			if (fatherId != -1)
			{
				NetworkIdentity networkIdentity;
				if (NetworkClient.spawned.TryGetValue((uint)fatherId, out networkIdentity))
				{
					Enemy_NecromancerMode enemy_NecromancerMode = networkIdentity.GetComponent<RoleBase>().RoleModeBase as Enemy_NecromancerMode;
					if (enemy_NecromancerMode != null)
					{
						this.effectTransform.LookAt(enemy_NecromancerMode.SkillSwordTran.position);
						this.effectTransform.position += this.effectTransform.forward * 0.3f;
					}
				}
			}
			else
			{
				this.effectTransform.localRotation = Quaternion.identity;
			}
			this.polygonBeamStatic = this.effectTransform.gameObject.GetComponent<PolygonBeamStatic>();
			this.polygonBeamStatic.InitRadius(0.25f);
		}
	}

	// Token: 0x04000C50 RID: 3152
	[SerializeField]
	private Transform effectTransform;

	// Token: 0x04000C51 RID: 3153
	private PolygonBeamStatic polygonBeamStatic;

	// Token: 0x04000C52 RID: 3154
	private float checkTime;

	// Token: 0x04000C53 RID: 3155
	private const float AttackRange = 1f;
}
