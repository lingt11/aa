using System;
using UnityEngine;

// Token: 0x020000AF RID: 175
public class RoleBuffBase
{
	// Token: 0x0600034B RID: 843 RVA: 0x00015A5C File Offset: 0x00013C5C
	public virtual void InitBuff()
	{
		if (this.buffObject != null)
		{
			this.buffObjectTrans = this.buffObject.transform;
			this.buffObjectTrans.SetParent(this.roleBase.MyTransform);
			this.UpdateModeRange();
			this.checkModeTime = Time.time + 0.3f;
		}
		if (this.roleBase.isLocalPlayer && !string.IsNullOrEmpty(this.imgPath))
		{
			this.roleBuff = GameHelperClient.AddShowBuff(Game.Language.Get(PathDefine.Concat("Buff_", this.localBuffType), ""), Game.Language.Get(PathDefine.Concat("Buff_", this.localBuffType, "_m"), ""), this.imgPath, (this.buffTime >= 999f) ? -1f : this.buffTime);
		}
	}

	// Token: 0x0600034C RID: 844 RVA: 0x00015B4C File Offset: 0x00013D4C
	private void UpdateModeRange()
	{
		this.curModeAddRange = this.roleBase.RoleModeBase.addRange;
		this.buffObjectTrans.position = this.roleBase.GetAttackPos();
		float num = 1f + this.curModeAddRange;
		this.buffObjectTrans.localScale = new Vector3(num, num, num);
		if (!Mathf.Approximately(this.offsetY, 0f))
		{
			this.buffObjectTrans.localPosition = new Vector3(0f, this.offsetY * num, 0f);
		}
	}

	// Token: 0x0600034D RID: 845 RVA: 0x00015BDC File Offset: 0x00013DDC
	public virtual void UpdateBuff()
	{
		if (Time.time > this.checkModeTime)
		{
			if (!Mathf.Approximately(this.curModeAddRange, this.roleBase.RoleModeBase.addRange))
			{
				this.UpdateModeRange();
			}
			this.checkModeTime = Time.time + 0.3f;
		}
	}

	// Token: 0x0600034E RID: 846 RVA: 0x00015C2C File Offset: 0x00013E2C
	public virtual void ClearBuff()
	{
		if (this.roleBuff != null)
		{
			GameHelperClient.localPlayer.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = null;
		}
		if (this.buffObject != null)
		{
			AssetManager.UnLoadPrefab(this.buffObject, false);
			this.buffObject = null;
			this.buffObjectTrans = null;
		}
		this.roleBase = null;
		this.attackRoleBase = null;
	}

	// Token: 0x04000338 RID: 824
	public float buffTime;

	// Token: 0x04000339 RID: 825
	public GameObject buffObject;

	// Token: 0x0400033A RID: 826
	public Transform buffObjectTrans;

	// Token: 0x0400033B RID: 827
	public float buffValue;

	// Token: 0x0400033C RID: 828
	public RoleBase roleBase;

	// Token: 0x0400033D RID: 829
	public RoleBase attackRoleBase;

	// Token: 0x0400033E RID: 830
	public float checkOffset;

	// Token: 0x0400033F RID: 831
	public bool deadNoClear;

	// Token: 0x04000340 RID: 832
	public string imgPath;

	// Token: 0x04000341 RID: 833
	public LocalBuffType localBuffType;

	// Token: 0x04000342 RID: 834
	private RoleBuff roleBuff;

	// Token: 0x04000343 RID: 835
	public float offsetY;

	// Token: 0x04000344 RID: 836
	private float curModeAddRange;

	// Token: 0x04000345 RID: 837
	private float checkModeTime;

	// Token: 0x04000346 RID: 838
	private const float CheckModeTimeOffset = 0.3f;
}
