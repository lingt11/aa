using System;
using UnityEngine;

// Token: 0x0200008A RID: 138
public class MeleeBrotatoWeapon : BrotatoWeapon
{
	// Token: 0x0600030F RID: 783 RVA: 0x00014F4E File Offset: 0x0001314E
	public void ShowTrail()
	{
		this.trailTransform.gameObject.SetActive(true);
	}

	// Token: 0x06000310 RID: 784 RVA: 0x00014F61 File Offset: 0x00013161
	public void HideTrail()
	{
		this.trailTransform.gameObject.SetActive(false);
	}

	// Token: 0x06000311 RID: 785 RVA: 0x00014F74 File Offset: 0x00013174
	public new void Clear()
	{
		this.ShowTrail();
		base.Clear();
	}

	// Token: 0x04000293 RID: 659
	public Quaternion startRotation;

	// Token: 0x04000294 RID: 660
	public Quaternion endRotation;

	// Token: 0x04000295 RID: 661
	public GameObject trailTransform;
}
