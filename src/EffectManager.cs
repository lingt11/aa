using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012E RID: 302
public class EffectManager : IUpdate, IApplicationQuit
{
	// Token: 0x060005AD RID: 1453 RVA: 0x00021438 File Offset: 0x0001F638
	public EffectManager()
	{
		this.spellGroundPlane = new Plane(Vector3.up, Vector3.zero);
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x00002D1D File Offset: 0x00000F1D
	public void OnApplicationQuit()
	{
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x00021498 File Offset: 0x0001F698
	public void Update()
	{
		float deltaTime = Time.deltaTime;
		for (int i = this.curEffectInfo.Count - 1; i > -1; i--)
		{
			EffectManager.EffectInfo effectInfo = this.curEffectInfo[i];
			effectInfo.lifeTime -= deltaTime;
			if (effectInfo.lifeTime < 0f)
			{
				AssetManager.UnLoadPrefab(effectInfo.go, false);
				this.curEffectInfo.RemoveAt(i);
			}
		}
		for (int j = this.curTips.Count - 1; j > -1; j--)
		{
			TipEffect tipEffect = this.curTips[j];
			tipEffect.timer += deltaTime;
			if (tipEffect.timer > tipEffect.lifeTime)
			{
				this.curTips.RemoveAt(j);
				if (tipEffect.type == 0)
				{
					this.PushTipSector(tipEffect);
				}
				else if (tipEffect.type == 1)
				{
					this.PushTipLine(tipEffect);
				}
				else
				{
					this.PushTipLineHero(tipEffect);
				}
			}
			else
			{
				tipEffect.materialBlock.SetFloat(ShaderDefine.Property_Progress, tipEffect.timer / tipEffect.lifeTime);
				tipEffect.renderer.SetPropertyBlock(tipEffect.materialBlock);
			}
		}
		if (GameHelperClient.ClickTrackRole != null && (GameHelperClient.ClickTrackRole.IsDead() || GameHelperClient.ClickTrackRole.RoleState == RoleState.None))
		{
			GameHelperClient.ClickTrackRole = null;
		}
		RaycastHit raycastHit2;
		if (this.isUseSkill)
		{
			if (this.useSkillType == 0)
			{
				if (Input.GetMouseButtonDown(0))
				{
					this.UseMouseBtnDown();
				}
				else if (Input.GetMouseButtonDown(1))
				{
					this.HideSpellGroundTip();
				}
				else
				{
					this.UpdateSpellGround();
				}
				Cursor.SetCursor(GameHelperClient.gameConfig.NormalCursor, Vector2.zero, CursorMode.Auto);
				return;
			}
			if (Input.GetMouseButtonDown(1))
			{
				this.HideSpellGroundTip();
				return;
			}
			if (!this.CheckGroundPosInCastingRange(this.GetMouseGroundPos()))
			{
				Cursor.SetCursor(GameHelperClient.gameConfig.DisableCursor, new Vector2(32f, 32f), CursorMode.Auto);
				return;
			}
			RaycastHit raycastHit;
			if (!Physics.Raycast(Camera.main.ScreenPointToRay(Util.GetMousePos()), out raycastHit, 100f, LayerUtil.EnemyLayerMask))
			{
				Cursor.SetCursor(GameHelperClient.gameConfig.TargetCursor, new Vector2(32f, 32f), CursorMode.Auto);
				return;
			}
			if (!Input.GetMouseButtonDown(0))
			{
				Cursor.SetCursor(GameHelperClient.gameConfig.AttackCursor, Vector2.zero, CursorMode.Auto);
				return;
			}
			Transform parent = raycastHit.transform.parent;
			RoleBase roleBase = (parent != null) ? parent.GetComponent<RoleBase>() : null;
			if (roleBase == null)
			{
				roleBase = raycastHit.transform.GetComponent<RoleBase>();
			}
			if (roleBase.roleType == RoleType.Enemy || roleBase.roleType == RoleType.King)
			{
				Vector3 position = GameHelperClient.localPlayer.MyTransform.position;
				Vector2 movePos = new Vector2(roleBase.MyTransform.position.x - position.x, roleBase.MyTransform.position.z - position.z);
				GameHelperClient.localPlayer.SetSyncRotationY(GameHelperClient.localPlayer.GetMoveAngle(movePos));
				Util.OnLocalCmdSkill(GameHelperClient.localPlayer, this.curActiveSkillEnum, roleBase.MyTransform.position, Game.GameData.ActiveSkillDataDic[this.curActiveSkillEnum], 0f, (int)roleBase.netId, this.curSkillBookId);
				this.HideSpellGroundTip();
				return;
			}
		}
		else if (Physics.Raycast(Camera.main.ScreenPointToRay(Util.GetMousePos()), out raycastHit2, 100f, LayerUtil.EnemyLayerMask))
		{
			Transform parent2 = raycastHit2.transform.parent;
			RoleBase roleBase2 = (parent2 != null) ? parent2.GetComponent<RoleBase>() : null;
			if (roleBase2 == null)
			{
				roleBase2 = raycastHit2.transform.GetComponent<RoleBase>();
			}
			if (roleBase2.roleType == RoleType.Enemy || roleBase2.roleType == RoleType.King)
			{
				if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
				{
					GameHelperClient.ClickTrackRole = null;
					if (!roleBase2.IsDead())
					{
						GameHelperClient.ClickTrackRole = roleBase2;
					}
				}
				Cursor.SetCursor(GameHelperClient.gameConfig.AttackCursor, Vector2.zero, CursorMode.Auto);
				return;
			}
		}
		else
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
			{
				GameHelperClient.ClickTrackRole = null;
			}
			Cursor.SetCursor(GameHelperClient.gameConfig.NormalCursor, Vector2.zero, CursorMode.Auto);
		}
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x000218A4 File Offset: 0x0001FAA4
	public void UseMouseBtnDown()
	{
		this.UpdateSpellGround();
		Vector3 vector = (this.spellGroundTip != null) ? this.spellGroundTip.position : this.GetCastingRangeGroundPos(this.GetMouseGroundPos());
		if (!this.CheckGroundPosCanSpell(vector))
		{
			return;
		}
		Vector3 position = GameHelperClient.localPlayer.MyTransform.position;
		Vector2 movePos = new Vector2(vector.x - position.x, vector.z - position.z);
		GameHelperClient.localPlayer.SetSyncRotationY(GameHelperClient.localPlayer.GetMoveAngle(movePos));
		Util.OnLocalCmdSkill(GameHelperClient.localPlayer, this.curActiveSkillEnum, new Vector3(vector.x, 0f, vector.z), Game.GameData.ActiveSkillDataDic[this.curActiveSkillEnum], 0f, -1, this.curSkillBookId);
		this.HideSpellGroundTip();
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x00021980 File Offset: 0x0001FB80
	public Transform PlayEffect(string effectName, float lifeTime, Vector3 pos, float scale = 1f)
	{
		GameObject gameObject = AssetManager.LoadPrefab(effectName, null, true);
		Transform transform = gameObject.transform;
		transform.position = pos;
		transform.localScale = new Vector3(scale, scale, scale);
		EffectManager.EffectInfo effectInfo = new EffectManager.EffectInfo();
		effectInfo.go = gameObject;
		effectInfo.effectName = effectName;
		effectInfo.lifeTime = lifeTime;
		this.curEffectInfo.Add(effectInfo);
		return transform;
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x000219DC File Offset: 0x0001FBDC
	public Transform PlayEffectByPos(string effectName, float lifeTime, Vector3 pos, float scale = 1f)
	{
		if (Util.GetV2Distance(GameHelperClient.localPlayer.MyTransform.position, pos) < 35f)
		{
			GameObject gameObject = AssetManager.LoadPrefab(effectName, null, true);
			Transform transform = gameObject.transform;
			transform.position = pos;
			transform.localScale = new Vector3(scale, scale, scale);
			EffectManager.EffectInfo effectInfo = new EffectManager.EffectInfo();
			effectInfo.go = gameObject;
			effectInfo.effectName = effectName;
			effectInfo.lifeTime = lifeTime;
			this.curEffectInfo.Add(effectInfo);
			return transform;
		}
		return null;
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x00021A58 File Offset: 0x0001FC58
	public Transform PlayEffect(string effectName, float lifeTime, Vector3 pos, Vector3 scale, Vector3 localEuler)
	{
		GameObject gameObject = AssetManager.LoadPrefab(effectName, null, true);
		Transform transform = gameObject.transform;
		transform.position = pos;
		transform.localScale = scale;
		transform.localEulerAngles = localEuler;
		EffectManager.EffectInfo effectInfo = new EffectManager.EffectInfo();
		effectInfo.go = gameObject;
		effectInfo.effectName = effectName;
		effectInfo.lifeTime = lifeTime;
		this.curEffectInfo.Add(effectInfo);
		return transform;
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x00021AB4 File Offset: 0x0001FCB4
	public void ShowSpellGroundTip(ActiveSkillEnum skillName, float range, int useSkillValue, int skillBookId)
	{
		this.curActiveSkillEnum = skillName;
		this.curSkillBookId = skillBookId;
		this.curCastingRange = Game.GameData.ActiveSkillDataDic[skillName].castingRange;
		if (GameHelperClient.IsSmartCasting)
		{
			this.SmartCast(useSkillValue);
			return;
		}
		this.isUseSkill = true;
		this.useSkillType = useSkillValue;
		if (this.useSkillType == 0)
		{
			if (this.spellGroundTip == null)
			{
				GameObject gameObject = AssetManager.LoadPrefab(EffectDefine.SpellGroundTip, null, true);
				this.spellGroundTip = gameObject.transform;
			}
			else if (!this.spellGroundTip.gameObject.activeSelf)
			{
				this.spellGroundTip.gameObject.SetActive(true);
			}
			range += range * GameHelperClient.localPlayer.skillRange;
			this.spellGroundTip.localScale = new Vector3(2f, 2f, 2f) * range;
			this.UpdateSpellGround();
		}
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x00021B9A File Offset: 0x0001FD9A
	private void SmartCast(int useSkillValue)
	{
		this.HideSpellGroundTip();
		if (useSkillValue == 0)
		{
			this.UseSmartGroundSkill();
			return;
		}
		this.UseSmartTargetSkill();
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x00021BB4 File Offset: 0x0001FDB4
	private void UseSmartGroundSkill()
	{
		Vector3 castingRangeGroundPos = this.GetCastingRangeGroundPos(this.GetMouseGroundPos());
		if (!this.CheckGroundPosCanSpell(castingRangeGroundPos))
		{
			castingRangeGroundPos = this.GetCastingRangeGroundPos(Util.GetSaveMapPos(castingRangeGroundPos));
			if (!this.CheckGroundPosCanSpell(castingRangeGroundPos))
			{
				Util.ShowTips("当前位置无法释放");
				return;
			}
		}
		Vector3 position = GameHelperClient.localPlayer.MyTransform.position;
		Vector2 movePos = new Vector2(castingRangeGroundPos.x - position.x, castingRangeGroundPos.z - position.z);
		GameHelperClient.localPlayer.SetSyncRotationY(GameHelperClient.localPlayer.GetMoveAngle(movePos));
		Util.OnLocalCmdSkill(GameHelperClient.localPlayer, this.curActiveSkillEnum, new Vector3(castingRangeGroundPos.x, 0f, castingRangeGroundPos.z), Game.GameData.ActiveSkillDataDic[this.curActiveSkillEnum], 0f, -1, this.curSkillBookId);
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x00021C88 File Offset: 0x0001FE88
	private void UseSmartTargetSkill()
	{
		Vector3 castingRangeGroundPos = this.GetCastingRangeGroundPos(this.GetMouseGroundPos());
		Collider[] array = Physics.OverlapSphere(castingRangeGroundPos, 3f, LayerUtil.EnemyLayerMask);
		RoleBase roleBase = null;
		float num = float.MaxValue;
		HashSet<RoleBase> hashSet = new HashSet<RoleBase>();
		for (int i = 0; i < array.Length; i++)
		{
			Transform parent = array[i].transform.parent;
			RoleBase roleBase2 = (parent != null) ? parent.GetComponent<RoleBase>() : null;
			if (roleBase2 == null)
			{
				roleBase2 = array[i].transform.GetComponent<RoleBase>();
			}
			if (!(roleBase2 == null) && !hashSet.Contains(roleBase2))
			{
				hashSet.Add(roleBase2);
				if (roleBase2.roleType == RoleType.Enemy || roleBase2.roleType == RoleType.King)
				{
					Vector3 position = roleBase2.MyTransform.position;
					if (this.CheckGroundPosInCastingRange(position))
					{
						float num2 = position.x - castingRangeGroundPos.x;
						float num3 = position.z - castingRangeGroundPos.z;
						float num4 = num2 * num2 + num3 * num3;
						if (num4 < num)
						{
							num = num4;
							roleBase = roleBase2;
						}
					}
				}
			}
		}
		if (roleBase == null)
		{
			Util.ShowTips("没有释放对象");
			return;
		}
		Vector3 position2 = GameHelperClient.localPlayer.MyTransform.position;
		Vector2 movePos = new Vector2(roleBase.MyTransform.position.x - position2.x, roleBase.MyTransform.position.z - position2.z);
		GameHelperClient.localPlayer.SetSyncRotationY(GameHelperClient.localPlayer.GetMoveAngle(movePos));
		Util.OnLocalCmdSkill(GameHelperClient.localPlayer, this.curActiveSkillEnum, roleBase.MyTransform.position, Game.GameData.ActiveSkillDataDic[this.curActiveSkillEnum], 0f, (int)roleBase.netId, this.curSkillBookId);
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x00021E42 File Offset: 0x00020042
	public void HideSpellGroundTip()
	{
		this.isUseSkill = false;
		if (this.spellGroundTip != null && this.spellGroundTip.gameObject.activeSelf)
		{
			this.spellGroundTip.gameObject.SetActive(false);
		}
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x00021E7C File Offset: 0x0002007C
	private bool CheckGroundPosCanSpell(Vector3 pos)
	{
		Vector2 canSpellArea = GameHelperClient.CanSpellArea;
		if ((canSpellArea.x > 0f || canSpellArea.y > 0f) && (Mathf.Abs(pos.x) > canSpellArea.x || Mathf.Abs(pos.z) > canSpellArea.y))
		{
			return false;
		}
		Vector2 noSpellArea = GameHelperClient.NoSpellArea;
		return Mathf.Abs(pos.x) >= noSpellArea.x || Mathf.Abs(pos.z) >= noSpellArea.y;
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x00021F00 File Offset: 0x00020100
	private bool CheckGroundPosInCastingRange(Vector3 pos)
	{
		if (this.curCastingRange <= 0f || GameHelperClient.localPlayer == null)
		{
			return true;
		}
		Vector3 position = GameHelperClient.localPlayer.MyTransform.position;
		float num = pos.x - position.x;
		float num2 = pos.z - position.z;
		return num * num + num2 * num2 <= this.curCastingRange * this.curCastingRange;
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x00021F6C File Offset: 0x0002016C
	private Vector3 GetCastingRangeGroundPos(Vector3 pos)
	{
		if (this.curCastingRange <= 0f || GameHelperClient.localPlayer == null)
		{
			return pos;
		}
		Vector3 position = GameHelperClient.localPlayer.MyTransform.position;
		Vector3 vector = new Vector3(pos.x - position.x, 0f, pos.z - position.z);
		if (vector.sqrMagnitude <= this.curCastingRange * this.curCastingRange)
		{
			return pos;
		}
		Vector3 vector2 = position + vector.normalized * this.curCastingRange;
		return new Vector3(vector2.x, pos.y, vector2.z);
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x00022014 File Offset: 0x00020214
	private void UpdateSpellGround()
	{
		if (this.spellGroundTip != null)
		{
			Vector3 castingRangeGroundPos = this.GetCastingRangeGroundPos(this.GetMouseGroundPos());
			bool flag = this.CheckGroundPosCanSpell(castingRangeGroundPos);
			this.spellGroundTip.position = castingRangeGroundPos;
			Transform child = this.spellGroundTip.GetChild(0);
			if (child.gameObject.activeSelf != flag)
			{
				child.gameObject.SetActive(flag);
				this.spellGroundTip.GetChild(1).gameObject.SetActive(!flag);
			}
		}
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x00022094 File Offset: 0x00020294
	public Vector3 GetMouseGroundPos()
	{
		Ray ray = Camera.main.ScreenPointToRay(Util.GetMousePos());
		float distance;
		if (this.spellGroundPlane.Raycast(ray, out distance))
		{
			return ray.GetPoint(distance) + new Vector3(0f, 0.1f, 0f);
		}
		return Vector3.zero;
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x000220E8 File Offset: 0x000202E8
	public TipEffect PlayTipSector(Vector3 pos, float scale, float rotation, float range, float lifeTime, float cutLong = 0f)
	{
		TipEffect tipSector = this.GetTipSector();
		tipSector.transform.position = new Vector3(pos.x, 0.35f, pos.z);
		tipSector.transform.localScale = Vector3.one * scale;
		tipSector.transform.localEulerAngles = new Vector3(90f, rotation + 180f, 0f);
		tipSector.lifeTime = lifeTime;
		tipSector.timer = 0f;
		tipSector.materialBlock.SetFloat(ShaderDefine.Property_Range, range);
		tipSector.materialBlock.SetFloat(ShaderDefine.Property_Progress, 0.001f);
		tipSector.materialBlock.SetFloat(ShaderDefine.Property_Long, cutLong);
		tipSector.renderer.SetPropertyBlock(tipSector.materialBlock);
		this.curTips.Add(tipSector);
		return tipSector;
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x000221C0 File Offset: 0x000203C0
	private TipEffect GetTipSector()
	{
		int count = this.poolTipSectors.Count;
		if (count > 0)
		{
			TipEffect tipEffect = this.poolTipSectors[count - 1];
			this.poolTipSectors.RemoveAt(count - 1);
			tipEffect.go.SetActive(true);
			return tipEffect;
		}
		GameObject gameObject = AssetManager.LoadPrefab(EffectDefine.TipSector, null, true);
		return new TipEffect
		{
			go = gameObject,
			transform = gameObject.transform,
			materialBlock = new MaterialPropertyBlock(),
			renderer = gameObject.GetComponent<Renderer>(),
			type = 0
		};
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x0002224C File Offset: 0x0002044C
	public TipEffect PlayTipLine(Vector3 pos, Vector3 scale, float rotation, float lifeTime)
	{
		TipEffect tipLine = this.GetTipLine();
		tipLine.transform.position = new Vector3(pos.x, 0.35f, pos.z);
		tipLine.transform.localScale = scale;
		tipLine.transform.localEulerAngles = new Vector3(0f, rotation, 0f);
		tipLine.lifeTime = lifeTime;
		tipLine.timer = 0f;
		tipLine.materialBlock.SetFloat(ShaderDefine.Property_Progress, 0.001f);
		tipLine.renderer.SetPropertyBlock(tipLine.materialBlock);
		this.curTips.Add(tipLine);
		return tipLine;
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x000222F0 File Offset: 0x000204F0
	private TipEffect GetTipLine()
	{
		int count = this.poolTipLines.Count;
		if (count > 0)
		{
			TipEffect tipEffect = this.poolTipLines[count - 1];
			this.poolTipLines.RemoveAt(count - 1);
			tipEffect.go.SetActive(true);
			return tipEffect;
		}
		GameObject gameObject = AssetManager.LoadPrefab(EffectDefine.TipLine, null, true);
		return new TipEffect
		{
			go = gameObject,
			transform = gameObject.transform,
			materialBlock = new MaterialPropertyBlock(),
			renderer = gameObject.transform.GetChild(0).GetComponent<Renderer>(),
			type = 1
		};
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x00022384 File Offset: 0x00020584
	public void PushTipSector(TipEffect tipEffect)
	{
		tipEffect.go.SetActive(false);
		this.poolTipSectors.Add(tipEffect);
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x0002239E File Offset: 0x0002059E
	public void PushTipLine(TipEffect tipEffect)
	{
		tipEffect.go.SetActive(false);
		this.poolTipLines.Add(tipEffect);
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x000223B8 File Offset: 0x000205B8
	private TipEffect GetTipLineHero()
	{
		int count = this.poolTipLineHeros.Count;
		if (count > 0)
		{
			TipEffect tipEffect = this.poolTipLineHeros[count - 1];
			this.poolTipLineHeros.RemoveAt(count - 1);
			tipEffect.go.SetActive(true);
			return tipEffect;
		}
		GameObject gameObject = AssetManager.LoadPrefab(EffectDefine.TipLineHero, null, true);
		return new TipEffect
		{
			go = gameObject,
			transform = gameObject.transform,
			materialBlock = new MaterialPropertyBlock(),
			renderer = gameObject.transform.GetChild(0).GetComponent<Renderer>(),
			type = 2
		};
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x0002244C File Offset: 0x0002064C
	public void PushTipLineHero(TipEffect tipEffect)
	{
		tipEffect.go.SetActive(false);
		this.poolTipLineHeros.Add(tipEffect);
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x00022468 File Offset: 0x00020668
	public TipEffect PlayTipLineHero(Vector3 pos, Vector3 scale, float rotation, float lifeTime)
	{
		TipEffect tipLineHero = this.GetTipLineHero();
		tipLineHero.transform.position = new Vector3(pos.x, 0.35f, pos.z);
		tipLineHero.transform.localScale = scale;
		tipLineHero.transform.localEulerAngles = new Vector3(0f, rotation, 0f);
		tipLineHero.lifeTime = lifeTime;
		tipLineHero.timer = 0f;
		tipLineHero.materialBlock.SetFloat(ShaderDefine.Property_Progress, 0.001f);
		tipLineHero.renderer.SetPropertyBlock(tipLineHero.materialBlock);
		this.curTips.Add(tipLineHero);
		return tipLineHero;
	}

	// Token: 0x04000820 RID: 2080
	private const float SmartCastingTargetRange = 3f;

	// Token: 0x04000821 RID: 2081
	private List<EffectManager.EffectInfo> curEffectInfo = new List<EffectManager.EffectInfo>();

	// Token: 0x04000822 RID: 2082
	private Transform spellGroundTip;

	// Token: 0x04000823 RID: 2083
	private ActiveSkillEnum curActiveSkillEnum;

	// Token: 0x04000824 RID: 2084
	private int curSkillBookId;

	// Token: 0x04000825 RID: 2085
	private float curCastingRange;

	// Token: 0x04000826 RID: 2086
	public bool isUseSkill;

	// Token: 0x04000827 RID: 2087
	private int useSkillType;

	// Token: 0x04000828 RID: 2088
	private Plane spellGroundPlane;

	// Token: 0x04000829 RID: 2089
	private List<TipEffect> poolTipSectors = new List<TipEffect>();

	// Token: 0x0400082A RID: 2090
	private List<TipEffect> poolTipLines = new List<TipEffect>();

	// Token: 0x0400082B RID: 2091
	private List<TipEffect> poolTipLineHeros = new List<TipEffect>();

	// Token: 0x0400082C RID: 2092
	private List<TipEffect> curTips = new List<TipEffect>();

	// Token: 0x0200012F RID: 303
	private class EffectInfo
	{
		// Token: 0x0400082D RID: 2093
		public string effectName;

		// Token: 0x0400082E RID: 2094
		public float lifeTime;

		// Token: 0x0400082F RID: 2095
		public GameObject go;
	}
}
