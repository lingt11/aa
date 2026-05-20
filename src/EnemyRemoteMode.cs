using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000270 RID: 624
public class EnemyRemoteMode : EnemyModeBase
{
	// Token: 0x06000B7F RID: 2943 RVA: 0x0003D711 File Offset: 0x0003B911
	protected override void Awake()
	{
		base.Awake();
		this.startWeaponPos = this.weaponTransform.localPosition;
		this.startQuaternion = this.weaponTransform.localRotation;
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x0003D73C File Offset: 0x0003B93C
	public override void UpdateEvent()
	{
		base.UpdateEvent();
		for (int i = this.flyAttackList.Count - 1; i > -1; i--)
		{
			EnemyRemoteMode.FlyAttackData flyAttackData = this.flyAttackList[i];
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
				flyAttackData.myTransform.rotation = Quaternion.Lerp(flyAttackData.myTransform.rotation, Quaternion.LookRotation(attackPos - flyAttackData.myTransform.position), deltaTime * 13f);
				flyAttackData.myTransform.position += flyAttackData.myTransform.forward * deltaTime * 18f;
				if (Vector3.Distance(flyAttackData.myTransform.position, attackPos) < 1f)
				{
					if (this.enemyBase.hasAuthority)
					{
						flyAttackData.trackBase.OnHit(this.enemyBase, (double)this.enemyBase.FinalAttackPower, flyAttackData.myTransform.eulerAngles.y, AttackType.Normal, false);
					}
					AssetManager.UnLoadPrefab(flyAttackData.myTransform.gameObject, false);
					flyAttackData.myTransform = null;
					flyAttackData.trackBase = null;
					this.flyAttackList.RemoveAt(i);
				}
			}
		}
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x0003D8BC File Offset: 0x0003BABC
	public override void OnExitAttack()
	{
		base.OnExitAttack();
		this.weaponTransform.localPosition = this.startWeaponPos;
		this.weaponTransform.localRotation = this.startQuaternion;
		this.weaponTransform.gameObject.SetActive(true);
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x0003D8F8 File Offset: 0x0003BAF8
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
			if (num < 0.41f)
			{
				if (this.enemyBase.hasAuthority && num < 0.3f)
				{
					this.weaponTransform.localPosition = Vector3.Lerp(this.startWeaponPos, this.attackWeaponPos, num / 0.3f);
					this.weaponTransform.localRotation = Quaternion.Lerp(this.startQuaternion, Quaternion.Euler(this.attackEuler), num / 0.3f);
					this.enemyBase.TrackRotation(3f);
					return;
				}
			}
			else if (this.weaponTransform.gameObject.activeSelf)
			{
				this.weaponTransform.gameObject.SetActive(false);
				GameObject gameObject = AssetManager.LoadPrefab(this.weaponPath, null, true);
				EnemyRemoteMode.FlyAttackData flyAttackData = default(EnemyRemoteMode.FlyAttackData);
				flyAttackData.myTransform = gameObject.transform;
				flyAttackData.myTransform.position = this.weaponTransform.position;
				flyAttackData.myTransform.rotation = this.weaponTransform.rotation;
				flyAttackData.trackBase = this.enemyBase.trackRoleBase;
				this.flyAttackList.Add(flyAttackData);
			}
		}
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x0003DA78 File Offset: 0x0003BC78
	public override void OnExitDead()
	{
		base.OnExitDead();
		this.weaponTransform.localPosition = this.startWeaponPos;
		this.weaponTransform.localRotation = this.startQuaternion;
		this.weaponTransform.gameObject.SetActive(true);
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x0003DAB3 File Offset: 0x0003BCB3
	public override void OnStartAttack()
	{
		if (!this.enemyBase.hasAuthority)
		{
			this.enemyBase.GetTrackRole(false, base.GetAttackDistance() + 1f, true, false);
		}
		base.OnStartAttack();
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x0003DAE4 File Offset: 0x0003BCE4
	public override void OnStartDead()
	{
		base.OnStartDead();
		for (int i = this.flyAttackList.Count - 1; i > -1; i--)
		{
			EnemyRemoteMode.FlyAttackData flyAttackData = this.flyAttackList[i];
			if (flyAttackData.myTransform != null)
			{
				AssetManager.UnLoadPrefab(flyAttackData.myTransform.gameObject, false);
				flyAttackData.myTransform = null;
			}
		}
		this.flyAttackList.Clear();
	}

	// Token: 0x04000C62 RID: 3170
	[Header("投掷攻击")]
	[SerializeField]
	private Transform weaponTransform;

	// Token: 0x04000C63 RID: 3171
	[SerializeField]
	private string weaponPath;

	// Token: 0x04000C64 RID: 3172
	private Vector3 startWeaponPos;

	// Token: 0x04000C65 RID: 3173
	private Quaternion startQuaternion;

	// Token: 0x04000C66 RID: 3174
	private readonly Vector3 attackWeaponPos = new Vector3(0.1304154f, 0.08744037f, -0.02389287f);

	// Token: 0x04000C67 RID: 3175
	private readonly Vector3 attackEuler = new Vector3(-63.225f, 376.024f, -152.478f);

	// Token: 0x04000C68 RID: 3176
	private List<EnemyRemoteMode.FlyAttackData> flyAttackList = new List<EnemyRemoteMode.FlyAttackData>();

	// Token: 0x02000271 RID: 625
	private struct FlyAttackData
	{
		// Token: 0x04000C69 RID: 3177
		public Transform myTransform;

		// Token: 0x04000C6A RID: 3178
		public RoleBase trackBase;
	}
}
