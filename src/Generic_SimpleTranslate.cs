using System;
using UnityEngine;

// Token: 0x0200040B RID: 1035
public class Generic_SimpleTranslate : MonoBehaviour
{
	// Token: 0x060017A5 RID: 6053 RVA: 0x00093A04 File Offset: 0x00091C04
	private void Update()
	{
		if (this.moveX)
		{
			base.transform.Translate(Vector3.left * Time.deltaTime * this.moveXSpeed);
		}
		if (this.moveY)
		{
			base.transform.Translate(Vector3.up * Time.deltaTime * this.moveYSpeed);
		}
		if (this.moveZ)
		{
			base.transform.Translate(Vector3.back * Time.deltaTime * this.moveZSpeed);
		}
	}

	// Token: 0x0400169F RID: 5791
	public bool moveX;

	// Token: 0x040016A0 RID: 5792
	public float moveXSpeed = 2f;

	// Token: 0x040016A1 RID: 5793
	public bool moveY;

	// Token: 0x040016A2 RID: 5794
	public float moveYSpeed = 2f;

	// Token: 0x040016A3 RID: 5795
	public bool moveZ;

	// Token: 0x040016A4 RID: 5796
	public float moveZSpeed = 2f;
}
