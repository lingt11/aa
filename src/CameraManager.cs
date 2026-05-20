using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class CameraManager : ILateUpdate
{
	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000137 RID: 311 RVA: 0x0000814E File Offset: 0x0000634E
	public Transform MyTransform
	{
		get
		{
			return this.myTransform;
		}
	}

	// Token: 0x06000138 RID: 312 RVA: 0x00008158 File Offset: 0x00006358
	public CameraManager()
	{
		GameObject gameObject = AssetManager.LoadPrefab("Prefabs/Camera", null, true);
		this.myTransform = gameObject.transform;
		this.defaultEulerAngles = this.myTransform.eulerAngles;
		this.cameraTran = this.myTransform.GetChild(0);
		this.camera = this.cameraTran.GetComponent<Camera>();
		this.playerLookPos = this.myTransform.position;
		this.curDistance = (this.targetDistance = this.MaxDistance);
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00002D1D File Offset: 0x00000F1D
	public void InitCamera()
	{
	}

	// Token: 0x0600013A RID: 314 RVA: 0x000081E9 File Offset: 0x000063E9
	public void SetLookPlayer(Transform t)
	{
		this.lookTransform = t;
		this.offset = this.MyTransform.position - this.lookTransform.position;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00008214 File Offset: 0x00006414
	public void LookPlayer(Vector3 playerPos)
	{
		Vector3 normalized = this.playerLookPos.normalized;
		this.myTransform.position = new Vector3(normalized.x * this.curDistance + playerPos.x, normalized.y * this.curDistance, normalized.z * this.curDistance + playerPos.z);
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00008274 File Offset: 0x00006474
	private Vector3 GetCurrentFollowPlayerPos(Vector3 fallbackPlayerPos)
	{
		Vector3 normalized = this.playerLookPos.normalized;
		return new Vector3(this.myTransform.position.x - normalized.x * this.curDistance, fallbackPlayerPos.y, this.myTransform.position.z - normalized.z * this.curDistance);
	}

	// Token: 0x0600013D RID: 317 RVA: 0x000082D4 File Offset: 0x000064D4
	public void BeginAttackCameraFollowLimit(float followSpeed, float returnSmoothTime, float maxDuration)
	{
		Vector3 fallbackPlayerPos = (GameHelperClient.localPlayer != null) ? GameHelperClient.localPlayer.MyTransform.position : this.myTransform.position;
		this.startFollowPlayerPos = fallbackPlayerPos;
		this.attackFollowPlayerPos = this.GetCurrentFollowPlayerPos(fallbackPlayerPos);
		this.attackReturnOffset = Vector3.zero;
		this.attackReturnVelocity = Vector3.zero;
		this.attackFollowSpeed = Mathf.Max(0f, followSpeed);
		this.attackReturnSmoothTime = Mathf.Max(0.01f, returnSmoothTime);
		this.attackFollowEndTime = Time.time + Mathf.Max(0.01f, maxDuration);
		this.isAttackFollowLimited = true;
		this.isAttackFollowReturning = false;
	}

	// Token: 0x0600013E RID: 318 RVA: 0x0000837C File Offset: 0x0000657C
	public void EndAttackCameraFollowLimit()
	{
		if (!this.isAttackFollowLimited && !this.isAttackFollowReturning)
		{
			return;
		}
		Vector3 targetPlayerPos = (GameHelperClient.localPlayer != null) ? GameHelperClient.localPlayer.MyTransform.position : this.attackFollowPlayerPos;
		this.BeginAttackCameraReturn(targetPlayerPos);
	}

	// Token: 0x0600013F RID: 319 RVA: 0x000083C8 File Offset: 0x000065C8
	public bool TryEndAttackCameraFollowLimitByDistance(Vector3 currentPlayerPos, float maxFollowDistance)
	{
		if (!this.isAttackFollowLimited)
		{
			return false;
		}
		maxFollowDistance = Mathf.Max(0f, maxFollowDistance);
		float num = maxFollowDistance * maxFollowDistance;
		if ((currentPlayerPos - this.startFollowPlayerPos).sqrMagnitude <= num)
		{
			return false;
		}
		this.BeginAttackCameraReturn(currentPlayerPos);
		return true;
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00008411 File Offset: 0x00006611
	private void BeginAttackCameraReturn(Vector3 targetPlayerPos)
	{
		this.isAttackFollowLimited = false;
		this.isAttackFollowReturning = true;
		this.attackReturnOffset = this.attackFollowPlayerPos - targetPlayerPos;
		this.attackReturnVelocity = Vector3.zero;
	}

	// Token: 0x06000141 RID: 321 RVA: 0x00008440 File Offset: 0x00006640
	private Vector3 GetAttackCameraFollowPos(Vector3 playerPos)
	{
		if (this.isAttackFollowLimited && Time.time >= this.attackFollowEndTime)
		{
			this.BeginAttackCameraReturn(playerPos);
		}
		if (this.isAttackFollowLimited)
		{
			if (this.attackFollowSpeed > 0f)
			{
				this.attackFollowPlayerPos = Vector3.Lerp(this.attackFollowPlayerPos, playerPos, Time.deltaTime * this.attackFollowSpeed);
			}
			return this.attackFollowPlayerPos;
		}
		if (!this.isAttackFollowReturning)
		{
			return playerPos;
		}
		this.attackReturnOffset = Vector3.SmoothDamp(this.attackReturnOffset, Vector3.zero, ref this.attackReturnVelocity, this.attackReturnSmoothTime);
		this.attackFollowPlayerPos = playerPos + this.attackReturnOffset;
		if (this.attackReturnOffset.sqrMagnitude < 0.0004f)
		{
			this.isAttackFollowReturning = false;
			this.attackReturnOffset = Vector3.zero;
			this.attackReturnVelocity = Vector3.zero;
			return playerPos;
		}
		return this.attackFollowPlayerPos;
	}

	// Token: 0x06000142 RID: 322 RVA: 0x00008518 File Offset: 0x00006718
	public void UpdateTargetDistance(float value)
	{
		this.targetDistance = value;
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00008524 File Offset: 0x00006724
	public void LateUpdate()
	{
		if (GameHelperClient.isFreeCamera)
		{
			return;
		}
		if (GameHelperClient.localPlayer != null)
		{
			UI_Shop ui = Game.UI.GetUI<UI_Shop>();
			if (ui != null && (!ui.isOpenShop || ui.GetShopType != UI_Shop.ShopType.Equip))
			{
				float axis = Input.GetAxis("Mouse ScrollWheel");
				if (!Mathf.Approximately(axis, 0f))
				{
					this.UpdateCameraDistance(axis);
				}
			}
			if (!Mathf.Approximately(this.curDistance, this.targetDistance))
			{
				if (Mathf.Abs(this.curDistance - this.targetDistance) < 0.1f)
				{
					this.curDistance = this.targetDistance;
				}
				else
				{
					this.curDistance = Mathf.Lerp(this.curDistance, this.targetDistance, Time.deltaTime * 5f);
				}
			}
			Vector3 position = GameHelperClient.localPlayer.MyTransform.position;
			if (GameHelperClient.isKingBattle && GameHelperClient.ChallengePlayerNum > 1 && !GameHelperClient.isGameOver && GameHelperClient.localPlayer.IsDead())
			{
				PlayerBase lookPlayer = this.GetLookPlayer();
				if (lookPlayer != null)
				{
					position = lookPlayer.MyTransform.position;
				}
			}
			this.LookPlayer(this.GetAttackCameraFollowPos(position));
		}
		if (this.lookTransform != null)
		{
			this.myTransform.position = this.lookTransform.position + this.offset;
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00008670 File Offset: 0x00006870
	public PlayerBase GetLookPlayer()
	{
		foreach (PlayerBase playerBase in Game.PlayerManagerClient.clientPlayerDic.Values)
		{
			if (!playerBase.isLocalPlayer && !playerBase.IsDead())
			{
				return playerBase;
			}
		}
		return null;
	}

	// Token: 0x06000145 RID: 325 RVA: 0x000086DC File Offset: 0x000068DC
	public void UpdateCameraDistance(float scroll)
	{
		this.targetDistance = Mathf.Clamp(this.targetDistance - scroll * 5f, 3.5f, this.MaxDistance);
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00008704 File Offset: 0x00006904
	public void ShakeCamera(float duration, float strength = 1f, int vibrato = 10, bool isCover = false)
	{
		if (this.tweener != null && this.tweener.IsActive() && this.tweener.IsPlaying())
		{
			return;
		}
		this.tweener = this.cameraTran.DOShakePosition(duration, strength, vibrato, 90f, false, true, ShakeRandomnessMode.Full);
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00008750 File Offset: 0x00006950
	public void ShakeCameraByPos(Vector3 shakePos, float duration, float strength = 1f, int vibrato = 10, bool isCover = false)
	{
		if (Util.GetV2Distance(GameHelperClient.localPlayer.MyTransform.position, shakePos) > 20f)
		{
			return;
		}
		if (this.tweener != null && this.tweener.IsActive() && this.tweener.IsPlaying())
		{
			return;
		}
		this.tweener = this.cameraTran.DOShakePosition(duration, strength, vibrato, 90f, false, true, ShakeRandomnessMode.Full);
	}

	// Token: 0x06000148 RID: 328 RVA: 0x000087BA File Offset: 0x000069BA
	public void ResetCamera()
	{
		this.myTransform.eulerAngles = this.defaultEulerAngles;
	}

	// Token: 0x04000158 RID: 344
	public Camera camera;

	// Token: 0x04000159 RID: 345
	private Vector3 playerLookPos;

	// Token: 0x0400015A RID: 346
	private Transform myTransform;

	// Token: 0x0400015B RID: 347
	private Transform cameraTran;

	// Token: 0x0400015C RID: 348
	public Transform lookTransform;

	// Token: 0x0400015D RID: 349
	private Vector3 offset;

	// Token: 0x0400015E RID: 350
	private Vector3 attackFollowPlayerPos;

	// Token: 0x0400015F RID: 351
	private Vector3 startFollowPlayerPos;

	// Token: 0x04000160 RID: 352
	private Vector3 attackReturnOffset;

	// Token: 0x04000161 RID: 353
	private Vector3 attackReturnVelocity;

	// Token: 0x04000162 RID: 354
	private bool isAttackFollowLimited;

	// Token: 0x04000163 RID: 355
	private bool isAttackFollowReturning;

	// Token: 0x04000164 RID: 356
	private float attackFollowSpeed;

	// Token: 0x04000165 RID: 357
	private float attackReturnSmoothTime;

	// Token: 0x04000166 RID: 358
	private float attackFollowEndTime;

	// Token: 0x04000167 RID: 359
	private float curDistance;

	// Token: 0x04000168 RID: 360
	public float MaxDistance = 11.6619f;

	// Token: 0x04000169 RID: 361
	public const float MinDistance = 3.5f;

	// Token: 0x0400016A RID: 362
	public const float ScrollLevel = 5f;

	// Token: 0x0400016B RID: 363
	private float targetDistance;

	// Token: 0x0400016C RID: 364
	private Tweener tweener;

	// Token: 0x0400016D RID: 365
	private Vector3 defaultEulerAngles;
}
