using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class SimpleRotate : MonoBehaviour
{
	// Token: 0x06000043 RID: 67 RVA: 0x00002FE8 File Offset: 0x000011E8
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

	// Token: 0x04000048 RID: 72
	public bool rotX;

	// Token: 0x04000049 RID: 73
	public float rotXSpeed = 50f;

	// Token: 0x0400004A RID: 74
	public bool rotY;

	// Token: 0x0400004B RID: 75
	public float rotYSpeed = 50f;

	// Token: 0x0400004C RID: 76
	public bool rotZ;

	// Token: 0x0400004D RID: 77
	public float rotZSpeed = 50f;
}
