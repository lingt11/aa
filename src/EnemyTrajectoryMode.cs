using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000276 RID: 630
public class EnemyTrajectoryMode : EnemyModeBase
{
	// Token: 0x06000BC2 RID: 3010 RVA: 0x0003FFA8 File Offset: 0x0003E1A8
	public override void UpdateEvent()
	{
		base.UpdateEvent();
		for (int i = this.flyAttackList.Count - 1; i > -1; i--)
		{
			EnemyTrajectoryMode.FlyAttackData flyAttackData = this.flyAttackList[i];
			if (flyAttackData.myTransform == null)
			{
				this.flyAttackList.RemoveAt(i);
			}
			else if (flyAttackData.trackBase == null || flyAttackData.trackBase.IsDead())
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
				flyAttackData.myTransform.position += flyAttackData.myTransform.forward * deltaTime * 18f;
				if (num < 0.5f)
				{
					if (this.enemyBase.hasAuthority)
					{
						if (this.enemyBase.roleType == RoleType.Summon)
						{
							Util.OnLocalPlayerHit(this.enemyBase, flyAttackData.trackBase, (double)((float)this.enemyBase.FinalAttackPower * this.attackDamageLevel), flyAttackData.myTransform.eulerAngles.y, AttackType.Skill, false);
						}
						else
						{
							flyAttackData.trackBase.OnHit(this.enemyBase, (double)((float)this.enemyBase.FinalAttackPower * this.attackDamageLevel), flyAttackData.myTransform.eulerAngles.y, AttackType.Normal, false);
						}
					}
					AssetManager.UnLoadPrefab(flyAttackData.myTransform.gameObject, false);
					flyAttackData.myTransform = null;
					flyAttackData.trackBase = null;
					this.flyAttackList.RemoveAt(i);
				}
			}
		}
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x000401C8 File Offset: 0x0003E3C8
	public override void AttackUpdate()
	{
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		float realOffsetInAttack = this.enemyBase.GetRealOffsetInAttack();
		if (this.enemyBase.timer > realOffsetInAttack)
		{
			if (this.enemyBase.hasAuthority)
			{
				this.enemyBase.UpdateRoleState(RoleState.Run);
				return;
			}
		}
		else
		{
			float num = this.enemyBase.timer / realOffsetInAttack;
			if (num < this.curCheckNormolized)
			{
				if (this.enemyBase.hasAuthority && num < 0.3f)
				{
					this.enemyBase.TrackRotation(3f);
					return;
				}
			}
			else if (!this.enemyBase.isCheckAttack)
			{
				if (this.bulletCount > 1 && this.attackIndex < this.bulletCount - 1)
				{
					this.attackIndex++;
					this.curCheckNormolized += this.checkNormolized;
					this.enemyBase.GetTrackRole(false, base.GetAttackDistance(), true, false);
				}
				else
				{
					this.enemyBase.isCheckAttack = true;
				}
				if (this.enemyBase.trackRoleBase != null)
				{
					if (string.IsNullOrEmpty(this.trajectoryPath))
					{
						Debug.LogError("EnemyTrajectoryMode trajectoryPath is empty: " + base.gameObject.name);
						this.enemyBase.isCheckAttack = true;
						return;
					}
					GameObject gameObject = this.LoadTrajectoryPrefab();
					if (gameObject == null)
					{
						this.enemyBase.isCheckAttack = true;
						return;
					}
					Transform transform = (this.createTransform != null) ? this.createTransform : this.enemyBase.MyTransform;
					if (transform == null)
					{
						Debug.LogError("EnemyTrajectoryMode fire point is null: " + base.gameObject.name);
						this.enemyBase.isCheckAttack = true;
						return;
					}
					EnemyTrajectoryMode.FlyAttackData flyAttackData = default(EnemyTrajectoryMode.FlyAttackData);
					flyAttackData.myTransform = gameObject.transform;
					flyAttackData.myTransform.position = transform.position;
					flyAttackData.myTransform.rotation = ((this.enemyBase.MyTransform != null) ? this.enemyBase.MyTransform.rotation : transform.rotation);
					flyAttackData.trackBase = this.enemyBase.trackRoleBase;
					this.flyAttackList.Add(flyAttackData);
				}
			}
		}
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x00040410 File Offset: 0x0003E610
	public override void OnStartAttack()
	{
		if (!this.enemyBase.hasAuthority)
		{
			this.enemyBase.GetTrackRole(false, base.GetAttackDistance() + 1f, true, false);
		}
		this.attackIndex = 0;
		this.curCheckNormolized = this.checkNormolized;
		base.OnStartAttack();
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x00040460 File Offset: 0x0003E660
	private GameObject LoadTrajectoryPrefab()
	{
		GameObject result;
		try
		{
			result = AssetManager.LoadPrefab(this.trajectoryPath, null, true);
		}
		catch (Exception ex)
		{
			Debug.LogError("EnemyTrajectoryMode load trajectory failed: " + this.trajectoryPath + ", " + ex.Message);
			result = null;
		}
		return result;
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x000404B4 File Offset: 0x0003E6B4
	public override void OnExitDead()
	{
		base.OnExitDead();
		for (int i = this.flyAttackList.Count - 1; i > -1; i--)
		{
			EnemyTrajectoryMode.FlyAttackData flyAttackData = this.flyAttackList[i];
			if (flyAttackData.myTransform != null)
			{
				AssetManager.UnLoadPrefab(flyAttackData.myTransform.gameObject, false);
				flyAttackData.myTransform = null;
				this.flyAttackList.RemoveAt(i);
			}
		}
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x00040520 File Offset: 0x0003E720
	public override void OnStartDead()
	{
		base.OnStartDead();
		for (int i = this.flyAttackList.Count - 1; i > -1; i--)
		{
			EnemyTrajectoryMode.FlyAttackData flyAttackData = this.flyAttackList[i];
			if (flyAttackData.myTransform != null)
			{
				AssetManager.UnLoadPrefab(flyAttackData.myTransform.gameObject, false);
				flyAttackData.myTransform = null;
			}
		}
		this.flyAttackList.Clear();
	}

	// Token: 0x04000C94 RID: 3220
	[Header("弹道攻击")]
	[SerializeField]
	private float checkNormolized = 0.41f;

	// Token: 0x04000C95 RID: 3221
	[SerializeField]
	protected Transform createTransform;

	// Token: 0x04000C96 RID: 3222
	[SerializeField]
	private string trajectoryPath;

	// Token: 0x04000C97 RID: 3223
	private List<EnemyTrajectoryMode.FlyAttackData> flyAttackList = new List<EnemyTrajectoryMode.FlyAttackData>();

	// Token: 0x04000C98 RID: 3224
	[SerializeField]
	private int bulletCount;

	// Token: 0x04000C99 RID: 3225
	[SerializeField]
	private float attackDamageLevel = 1f;

	// Token: 0x04000C9A RID: 3226
	private int attackIndex;

	// Token: 0x04000C9B RID: 3227
	private float curCheckNormolized;

	// Token: 0x02000277 RID: 631
	private struct FlyAttackData
	{
		// Token: 0x04000C9C RID: 3228
		public Transform myTransform;

		// Token: 0x04000C9D RID: 3229
		public RoleBase trackBase;
	}
}
