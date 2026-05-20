using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B9 RID: 697
public class RoleBuff
{
	// Token: 0x06001064 RID: 4196 RVA: 0x0005C55E File Offset: 0x0005A75E
	public void SetSpecialStr(string specialStrValue)
	{
		this.specialStr = specialStrValue;
		if (this.myText != null)
		{
			this.myText.text = this.specialStr;
		}
	}

	// Token: 0x06001065 RID: 4197 RVA: 0x0005C586 File Offset: 0x0005A786
	public void SetLifeTime(float t)
	{
		this.lifeTimeSet = t;
		this.lifeTime = t;
		if (t < 0f)
		{
			this.isNoLife = true;
		}
	}

	// Token: 0x06001067 RID: 4199 RVA: 0x0005C5B8 File Offset: 0x0005A7B8
	public void Init(RoleBase role, string name)
	{
		this.buffName = name;
		this.roleBase = role;
		this.OnInit();
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void OnInit()
	{
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x0005C5CE File Offset: 0x0005A7CE
	public virtual void Update()
	{
		if (!this.isNoLife)
		{
			this.lifeTime -= Time.deltaTime;
			if (this.lifeTime <= 0f)
			{
				this.Clear();
			}
		}
	}

	// Token: 0x0600106A RID: 4202 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void OnExit()
	{
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x0005C5FD File Offset: 0x0005A7FD
	public void Clear()
	{
		this.OnExit();
		this.roleBase.roleBuffManager.RemoveBuff(this);
	}

	// Token: 0x04000E5C RID: 3676
	public string buffName;

	// Token: 0x04000E5D RID: 3677
	public string icon;

	// Token: 0x04000E5E RID: 3678
	public RoleBase roleBase;

	// Token: 0x04000E5F RID: 3679
	public float lifeTime = 1f;

	// Token: 0x04000E60 RID: 3680
	public float lifeTimeSet;

	// Token: 0x04000E61 RID: 3681
	public string info;

	// Token: 0x04000E62 RID: 3682
	public Image cdImage;

	// Token: 0x04000E63 RID: 3683
	public Text myText;

	// Token: 0x04000E64 RID: 3684
	public bool isShow;

	// Token: 0x04000E65 RID: 3685
	public bool isNoLife;

	// Token: 0x04000E66 RID: 3686
	public string specialStr;
}
