using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002A4 RID: 676
public class RemotePlayerMode : PlayerModeBase
{
	// Token: 0x06000ED6 RID: 3798 RVA: 0x00055307 File Offset: 0x00053507
	protected override void Awake()
	{
		base.Awake();
		this.startWeaponPos = this.weaponTransform.localPosition;
		this.startQuaternion = this.weaponTransform.localRotation;
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x00055334 File Offset: 0x00053534
	public override void UpdateEvent()
	{
		for (int i = this.flyAttackList.Count - 1; i > -1; i--)
		{
			RemotePlayerMode.FlyAttackData flyAttackData = this.flyAttackList[i];
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
				bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Normal);
				long playerNormalAttackPower = this.playerBase.GetPlayerNormalAttackPower();
				if (Vector3.Distance(flyAttackData.myTransform.position, attackPos) < 1f)
				{
					if (this.playerBase.hasAuthority)
					{
						Util.OnLocalPlayerHit(this.playerBase, flyAttackData.trackBase, (double)playerNormalAttackPower, flyAttackData.myTransform.eulerAngles.y, AttackType.Normal, isAttackWeek);
					}
					AssetManager.UnLoadPrefab(flyAttackData.myTransform.gameObject, false);
					flyAttackData.myTransform = null;
					flyAttackData.trackBase = null;
					this.flyAttackList.RemoveAt(i);
				}
			}
		}
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x000554C1 File Offset: 0x000536C1
	public override void OnExitAttack()
	{
		base.OnExitAttack();
		this.weaponTransform.localPosition = this.startWeaponPos;
		this.weaponTransform.localRotation = this.startQuaternion;
		this.weaponTransform.gameObject.SetActive(true);
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x000554FC File Offset: 0x000536FC
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
			if (num < 0.41f)
			{
				if (this.playerBase.hasAuthority && num < 0.3f)
				{
					this.weaponTransform.localPosition = Vector3.Lerp(this.startWeaponPos, this.attackWeaponPos, num / 0.3f);
					this.weaponTransform.localRotation = Quaternion.Lerp(this.startQuaternion, Quaternion.Euler(this.attackEuler), num / 0.3f);
					this.playerBase.TrackRotation(3f);
				}
			}
			else if (this.weaponTransform.gameObject.activeSelf)
			{
				if (this.playerBase.trackRoleBase == null || this.playerBase.trackRoleBase.IsDead())
				{
					this.GetTrackRole(base.GetAttackDistance());
				}
				this.weaponTransform.gameObject.SetActive(false);
				GameObject gameObject = AssetManager.LoadPrefab(this.weaponPath, null, true);
				RemotePlayerMode.FlyAttackData flyAttackData = default(RemotePlayerMode.FlyAttackData);
				flyAttackData.myTransform = gameObject.transform;
				flyAttackData.myTransform.position = this.weaponTransform.position;
				flyAttackData.myTransform.rotation = this.weaponTransform.rotation;
				flyAttackData.trackBase = this.playerBase.trackRoleBase;
				this.flyAttackList.Add(flyAttackData);
				if (this.playerBase.attackNum > 1)
				{
					List<RoleBase> canAttackRoleList = this.playerBase.GetCanAttackRoleList(this.playerBase.RoleModeBase.GetAttackDistance(), this.playerBase.attackNum);
					if (canAttackRoleList.Count > 0)
					{
						int i = 0;
						int count = canAttackRoleList.Count;
						while (i < count)
						{
							gameObject = AssetManager.LoadPrefab(this.weaponPath, null, true);
							flyAttackData = default(RemotePlayerMode.FlyAttackData);
							flyAttackData.myTransform = gameObject.transform;
							flyAttackData.myTransform.position = this.weaponTransform.position;
							flyAttackData.myTransform.rotation = this.weaponTransform.rotation;
							flyAttackData.trackBase = canAttackRoleList[i];
							this.flyAttackList.Add(flyAttackData);
							i++;
						}
					}
				}
				if (this.playerBase.hasAuthority)
				{
					Game.AudioManager.PlayAttackAudio(this.attackHitSound);
				}
			}
			if (this.playerBase.hasAuthority && this.playerBase.CheckIsInputMove(num) && num < 0.41f)
			{
				this.playerBase.timer = realOffsetInAttack;
			}
		}
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x000557CB File Offset: 0x000539CB
	public override void OnExitDead()
	{
		this.weaponTransform.localPosition = this.startWeaponPos;
		this.weaponTransform.localRotation = this.startQuaternion;
		this.weaponTransform.gameObject.SetActive(true);
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x00055800 File Offset: 0x00053A00
	public override void OnStartAttack()
	{
		base.OnStartAttack();
		if (!this.playerBase.hasAuthority)
		{
			this.GetTrackRole(base.GetAttackDistance());
		}
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x00055824 File Offset: 0x00053A24
	protected void GetTrackRole(float minDistance = 9999f)
	{
		if (this.playerBase.roleType == RoleType.King)
		{
			return;
		}
		this.playerBase.trackRoleBase = null;
		List<RoleBase> attackRoles = this.roleBase.GetAttackRoles();
		int count = attackRoles.Count;
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = attackRoles[i];
			if (roleBase != null && !roleBase.IsDead())
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

	// Token: 0x06000EDD RID: 3805 RVA: 0x000558B4 File Offset: 0x00053AB4
	public override void OnClearMode()
	{
		base.OnClearMode();
		for (int i = this.flyAttackList.Count - 1; i > -1; i--)
		{
			RemotePlayerMode.FlyAttackData flyAttackData = this.flyAttackList[i];
			if (flyAttackData.myTransform != null)
			{
				AssetManager.UnLoadPrefab(flyAttackData.myTransform.gameObject, false);
				flyAttackData.myTransform = null;
				flyAttackData.trackBase = null;
			}
		}
		this.flyAttackList.Clear();
	}

	// Token: 0x04000DD3 RID: 3539
	[Header("投掷攻击")]
	[SerializeField]
	private Transform weaponTransform;

	// Token: 0x04000DD4 RID: 3540
	[SerializeField]
	private string weaponPath;

	// Token: 0x04000DD5 RID: 3541
	private Vector3 startWeaponPos;

	// Token: 0x04000DD6 RID: 3542
	private Quaternion startQuaternion;

	// Token: 0x04000DD7 RID: 3543
	private readonly Vector3 attackWeaponPos = new Vector3(0.1304154f, 0.08744037f, -0.02389287f);

	// Token: 0x04000DD8 RID: 3544
	private readonly Vector3 attackEuler = new Vector3(-63.225f, 376.024f, -152.478f);

	// Token: 0x04000DD9 RID: 3545
	private List<RemotePlayerMode.FlyAttackData> flyAttackList = new List<RemotePlayerMode.FlyAttackData>();

	// Token: 0x020002A5 RID: 677
	private struct FlyAttackData
	{
		// Token: 0x04000DDA RID: 3546
		public Transform myTransform;

		// Token: 0x04000DDB RID: 3547
		public RoleBase trackBase;
	}
}
