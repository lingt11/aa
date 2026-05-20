using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002A6 RID: 678
public class TrajectoryPlayerMode : PlayerModeBase
{
	// Token: 0x06000EDF RID: 3807 RVA: 0x0005597C File Offset: 0x00053B7C
	public override void UpdateEvent()
	{
		for (int i = this.flyAttackList.Count - 1; i > -1; i--)
		{
			TrajectoryPlayerMode.FlyAttackData flyAttackData = this.flyAttackList[i];
			if (flyAttackData.trackBase == null || flyAttackData.trackBase.IsDead())
			{
				AssetManager.UnLoadPrefab(flyAttackData.myTransform.gameObject, false);
				flyAttackData.myTransform = null;
				this.flyAttackList.RemoveAt(i);
			}
			else
			{
				float deltaTime = Time.deltaTime;
				Vector3 attackPos = flyAttackData.trackBase.GetAttackPos();
				float num = Vector3.Distance(flyAttackData.myTransform.position, attackPos);
				if (num < 1.5f)
				{
					flyAttackData.myTransform.rotation = Quaternion.LookRotation(attackPos - flyAttackData.myTransform.position);
				}
				else
				{
					flyAttackData.myTransform.rotation = Quaternion.Lerp(flyAttackData.myTransform.rotation, Quaternion.LookRotation(attackPos - flyAttackData.myTransform.position), deltaTime * 20f);
				}
				flyAttackData.myTransform.position += flyAttackData.myTransform.forward * (deltaTime * 18f);
				if (num < 0.5f)
				{
					if (this.playerBase.hasAuthority)
					{
						long playerNormalAttackPower = this.playerBase.GetPlayerNormalAttackPower();
						bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Normal);
						Util.OnLocalPlayerHit(this.playerBase, flyAttackData.trackBase, (double)Mathf.RoundToInt((float)playerNormalAttackPower * this.attackDamageLevel), flyAttackData.myTransform.eulerAngles.y, AttackType.Normal, isAttackWeek);
					}
					AssetManager.UnLoadPrefab(flyAttackData.myTransform.gameObject, false);
					flyAttackData.myTransform = null;
					flyAttackData.trackBase = null;
					this.flyAttackList.RemoveAt(i);
				}
			}
		}
	}

	// Token: 0x06000EE0 RID: 3808 RVA: 0x00055B40 File Offset: 0x00053D40
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
			if (num < this.curCheckNormolized)
			{
				if (this.playerBase.hasAuthority)
				{
					this.playerBase.TrackRotation(3f);
				}
			}
			else if (!this.playerBase.isCheckAttack)
			{
				if (this.bulletCount > 1 && this.attackIndex < this.bulletCount - 1)
				{
					this.attackIndex++;
					this.curCheckNormolized += this.checkNormolized;
					this.GetTrackRole(base.GetAttackDistance());
				}
				else
				{
					this.playerBase.isCheckAttack = true;
				}
				if (this.playerBase.trackRoleBase == null || this.playerBase.trackRoleBase.IsDead())
				{
					this.GetTrackRole(base.GetAttackDistance());
				}
				if (this.playerBase.trackRoleBase != null)
				{
					GameObject gameObject = AssetManager.LoadPrefab(this.trajectoryPath, null, true);
					TrajectoryPlayerMode.FlyAttackData flyAttackData = default(TrajectoryPlayerMode.FlyAttackData);
					flyAttackData.myTransform = gameObject.transform;
					flyAttackData.myTransform.position = this.createTransform.position;
					flyAttackData.myTransform.rotation = this.playerBase.MyTransform.rotation;
					flyAttackData.trackBase = this.playerBase.trackRoleBase;
					this.flyAttackList.Add(flyAttackData);
					if (this.playerBase.hasAuthority)
					{
						Game.AudioManager.PlayAttackAudio(this.attackHitSound);
					}
				}
				if (this.playerBase.attackNum > 1)
				{
					List<RoleBase> canAttackRoleList = this.playerBase.GetCanAttackRoleList(base.GetAttackDistance(), this.playerBase.attackNum);
					if (canAttackRoleList.Count > 0)
					{
						int i = 0;
						int count = canAttackRoleList.Count;
						while (i < count)
						{
							GameObject gameObject2 = AssetManager.LoadPrefab(this.trajectoryPath, null, true);
							TrajectoryPlayerMode.FlyAttackData flyAttackData2 = default(TrajectoryPlayerMode.FlyAttackData);
							flyAttackData2.myTransform = gameObject2.transform;
							flyAttackData2.myTransform.position = this.createTransform.position;
							flyAttackData2.myTransform.rotation = this.playerBase.MyTransform.rotation;
							flyAttackData2.trackBase = canAttackRoleList[i];
							this.flyAttackList.Add(flyAttackData2);
							i++;
						}
					}
				}
			}
			if (this.playerBase.hasAuthority && this.playerBase.CheckIsInputMove(num) && !this.playerBase.isCheckAttack)
			{
				this.playerBase.timer = realOffsetInAttack;
			}
		}
	}

	// Token: 0x06000EE1 RID: 3809 RVA: 0x00055E18 File Offset: 0x00054018
	public override void OnStartAttack()
	{
		base.OnStartAttack();
		if (!this.playerBase.hasAuthority)
		{
			this.GetTrackRole(base.GetAttackDistance());
		}
		this.attackIndex = 0;
		this.curCheckNormolized = this.checkNormolized;
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x00055E4C File Offset: 0x0005404C
	protected void GetTrackRole(float minDistance = 9999f)
	{
		if (this.playerBase.roleType == RoleType.King)
		{
			return;
		}
		if (this.playerBase.hasAuthority && GameHelperClient.ClickTrackRole != null)
		{
			this.playerBase.trackRoleBase = GameHelperClient.ClickTrackRole;
			return;
		}
		this.playerBase.trackRoleBase = null;
		List<RoleBase> attackRoles = this.roleBase.GetAttackRoles();
		int count = attackRoles.Count;
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = attackRoles[i];
			if (roleBase != null && roleBase.RoleState != RoleState.Dead)
			{
				float distanceV = this.playerBase.GetDistanceV2(roleBase.MyTransform.position);
				if (distanceV < minDistance)
				{
					this.playerBase.trackRoleBase = roleBase;
					minDistance = distanceV;
				}
			}
		}
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x00055F08 File Offset: 0x00054108
	public override void OnClearMode()
	{
		base.OnClearMode();
		for (int i = this.flyAttackList.Count - 1; i > -1; i--)
		{
			TrajectoryPlayerMode.FlyAttackData flyAttackData = this.flyAttackList[i];
			if (flyAttackData.myTransform != null)
			{
				AssetManager.UnLoadPrefab(flyAttackData.myTransform.gameObject, false);
				flyAttackData.myTransform = null;
				flyAttackData.trackBase = null;
			}
		}
		this.flyAttackList.Clear();
	}

	// Token: 0x04000DDC RID: 3548
	[Header("弹道攻击")]
	[SerializeField]
	private float checkNormolized = 0.41f;

	// Token: 0x04000DDD RID: 3549
	[SerializeField]
	private Transform createTransform;

	// Token: 0x04000DDE RID: 3550
	[SerializeField]
	private string trajectoryPath;

	// Token: 0x04000DDF RID: 3551
	protected List<TrajectoryPlayerMode.FlyAttackData> flyAttackList = new List<TrajectoryPlayerMode.FlyAttackData>();

	// Token: 0x04000DE0 RID: 3552
	[SerializeField]
	private int bulletCount;

	// Token: 0x04000DE1 RID: 3553
	[SerializeField]
	private float attackDamageLevel = 1f;

	// Token: 0x04000DE2 RID: 3554
	private int attackIndex;

	// Token: 0x04000DE3 RID: 3555
	private float curCheckNormolized;

	// Token: 0x020002A7 RID: 679
	protected struct FlyAttackData
	{
		// Token: 0x04000DE4 RID: 3556
		public Transform myTransform;

		// Token: 0x04000DE5 RID: 3557
		public RoleBase trackBase;
	}
}
