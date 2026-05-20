using System;
using UnityEngine;

// Token: 0x0200040A RID: 1034
public class Generic_SimpleRotate : MonoBehaviour
{
	// Token: 0x060017A3 RID: 6051 RVA: 0x00093944 File Offset: 0x00091B44
	private void Update()
	{
		if (this.rotX)
		{
			base.transform.Rotate(Vector3.left * Time.deltaTime * this.rotXSpeed);
		}
		if (this.rotY)
		{
			base.transform.Rotate(Vector3.up * Time.deltaTime * this.rotYSpeed);
		}
		if (this.rotZ)
		{
			base.transform.Rotate(Vector3.back * Time.deltaTime * this.rotZSpeed);
		}
	}

	// Token: 0x04001699 RID: 5785
	public bool rotX;

	// Token: 0x0400169A RID: 5786
	public float rotXSpeed = 50f;

	// Token: 0x0400169B RID: 5787
	public bool rotY;

	// Token: 0x0400169C RID: 5788
	public float rotYSpeed = 50f;

	// Token: 0x0400169D RID: 5789
	public bool rotZ;

	// Token: 0x0400169E RID: 5790
	public float rotZSpeed = 50f;
}
