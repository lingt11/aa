using System;
using UnityEngine;

// Token: 0x0200040D RID: 1037
public class SimpleRotateGoblin : MonoBehaviour
{
	// Token: 0x060017AA RID: 6058 RVA: 0x00093C80 File Offset: 0x00091E80
	private void Update()
	{
		if (this.srotX)
		{
			base.transform.Rotate(Vector3.left * Time.deltaTime * this.srotXSpeed);
		}
		if (this.srotY)
		{
			base.transform.Rotate(Vector3.up * Time.deltaTime * this.srotYSpeed);
		}
		if (this.srotZ)
		{
			base.transform.Rotate(Vector3.back * Time.deltaTime * this.srotZSpeed);
		}
	}

	// Token: 0x040016AC RID: 5804
	public bool srotX;

	// Token: 0x040016AD RID: 5805
	public float srotXSpeed = 50f;

	// Token: 0x040016AE RID: 5806
	public bool srotY;

	// Token: 0x040016AF RID: 5807
	public float srotYSpeed = 50f;

	// Token: 0x040016B0 RID: 5808
	public bool srotZ;

	// Token: 0x040016B1 RID: 5809
	public float srotZSpeed = 50f;
}
