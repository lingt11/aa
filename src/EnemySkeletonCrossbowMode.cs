using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000274 RID: 628
public class EnemySkeletonCrossbowMode : EnemyModeBase
{
	// Token: 0x06000BAA RID: 2986 RVA: 0x0003F22D File Offset: 0x0003D42D
	public override void OnStartAttack()
	{
		base.OnStartAttack();
		this.isPlayTip = false;
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x0003F23C File Offset: 0x0003D43C
	public override void UpdateEvent()
	{
		if (this.attackEffectTran != null)
		{
			float deltaTime = Time.deltaTime;
			this.attackEffectTran.position += this.attackEffectTran.forward * (deltaTime * 15f);
			this.flyTime += deltaTime;
			if (this.flyTime >= 1.1f)
			{
				AssetManager.UnLoadPrefab(this.attackEffectTran.gameObject, false);
				this.attackEffectTran = null;
				return;
			}
			List<RoleBase> attackRoles = this.roleBase.GetAttackRoles();
			int count = attackRoles.Count;
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckJuXing(this.attackEffectTran.position, this.attackEffectTran.eulerAngles.y, 1f, 1f, roleBase.MyTransform.position, roleBase.RoleModeBase.addRange, false, false))
				{
					if (this.roleBase.hasAuthority)
					{
						roleBase.OnHit(this.roleBase, (double)(this.roleBase.FinalAttackPower * 10L), this.attackEffectTran.eulerAngles.y, AttackType.Skill, false);
					}
					Game.EffectManager.PlayEffect(EffectDefine.CrossbowArrowHit, 2f, roleBase.GetAttackPos(), 1.5f);
					AssetManager.UnLoadPrefab(this.attackEffectTran.gameObject, false);
					this.attackEffectTran = null;
					return;
				}
			}
		}
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x0003F3D8 File Offset: 0x0003D5D8
	public override void AttackUpdate()
	{
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		float realOffsetInAttack = this.enemyBase.GetRealOffsetInAttack();
		if (this.enemyBase.timer > realOffsetInAttack)
		{
			this.enemyBase.UpdateRoleState(RoleState.Run);
			return;
		}
		if (!this.isPlayTip && this.enemyBase.timer > 0.2f / this.enemyBase.AniSpeed)
		{
			this.isPlayTip = true;
			if (this.enemyBase.roleType != RoleType.Summon)
			{
				Game.EffectManager.PlayTipLine(this.enemyBase.MyTransform.position, new Vector3(1f, 1f, 15f), this.enemyBase.MyTransform.localEulerAngles.y, 0.8f / this.enemyBase.AniSpeed);
			}
		}
		if (this.enemyBase.hasAuthority && this.enemyBase.timer < 0.2f)
		{
			this.enemyBase.TrackRotation(3f);
		}
		if (!this.roleBase.isCheckAttack && this.enemyBase.timer > 1f / this.enemyBase.AniSpeed)
		{
			this.roleBase.isCheckAttack = true;
			this.flyTime = 0f;
			GameObject gameObject = AssetManager.LoadPrefab(this.attackEffectPath, null, true);
			this.attackEffectTran = gameObject.transform;
			this.attackEffectTran.position = this.attackParent.position;
			this.attackEffectTran.localEulerAngles = new Vector3(0f, this.enemyBase.MyTransform.localEulerAngles.y, 0f);
		}
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x0003F58B File Offset: 0x0003D78B
	public override void OnExitDead()
	{
		base.OnExitDead();
		if (this.attackEffectTran != null)
		{
			AssetManager.UnLoadPrefab(this.attackEffectTran.gameObject, false);
			this.attackEffectTran = null;
		}
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x0003F5B9 File Offset: 0x0003D7B9
	public override void OnStartDead()
	{
		base.OnStartDead();
		if (this.attackEffectTran != null)
		{
			AssetManager.UnLoadPrefab(this.attackEffectTran.gameObject, false);
			this.attackEffectTran = null;
		}
	}

	// Token: 0x04000C81 RID: 3201
	[SerializeField]
	private string attackEffectPath;

	// Token: 0x04000C82 RID: 3202
	[SerializeField]
	private Transform attackParent;

	// Token: 0x04000C83 RID: 3203
	private const float FlySpeed = 15f;

	// Token: 0x04000C84 RID: 3204
	private Transform attackEffectTran;

	// Token: 0x04000C85 RID: 3205
	private bool isPlayTip;

	// Token: 0x04000C86 RID: 3206
	private float flyTime;

	// Token: 0x04000C87 RID: 3207
	private const float StartAttackTime = 0.2f;
}
