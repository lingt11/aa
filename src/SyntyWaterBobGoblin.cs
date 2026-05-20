using System;
using UnityEngine;

// Token: 0x0200040E RID: 1038
public class SyntyWaterBobGoblin : MonoBehaviour
{
	// Token: 0x060017AC RID: 6060 RVA: 0x00093D40 File Offset: 0x00091F40
	private void Start()
	{
		this.startPos = base.transform.position;
		this.startRotation = base.transform.rotation;
		if (this.randomOffset)
		{
			this.bobbingSpeed += Random.Range(this.randomRange.x, this.randomRange.y);
			this.rotationAmount += Random.Range(this.randomRange.x, this.randomRange.y);
		}
	}

	// Token: 0x060017AD RID: 6061 RVA: 0x00093DC8 File Offset: 0x00091FC8
	private void Update()
	{
		float y = this.startPos.y + Mathf.Sin(Time.time * this.bobbingSpeed) * this.bobbingHeight;
		Vector3 position = new Vector3(base.transform.position.x, y, base.transform.position.z);
		base.transform.position = position;
		float x = Mathf.Sin(Time.time * this.bobbingSpeed * 0.5f) * this.rotationAmount;
		float y2 = Mathf.Sin(Time.time * this.bobbingSpeed * 0.7f) * this.rotationAmount;
		float z = Mathf.Sin(Time.time * this.bobbingSpeed * 0.9f) * this.rotationAmount;
		Quaternion rhs = Quaternion.Euler(x, y2, z);
		base.transform.rotation = this.startRotation * rhs;
	}

	// Token: 0x040016B2 RID: 5810
	public float bobbingHeight = 0.08f;

	// Token: 0x040016B3 RID: 5811
	public float bobbingSpeed = 1.5f;

	// Token: 0x040016B4 RID: 5812
	public float rotationAmount = 0.8f;

	// Token: 0x040016B5 RID: 5813
	public bool randomOffset = true;

	// Token: 0x040016B6 RID: 5814
	public Vector2 randomRange = new Vector2(0.1f, 1f);

	// Token: 0x040016B7 RID: 5815
	private Vector3 startPos;

	// Token: 0x040016B8 RID: 5816
	private Quaternion startRotation;
}
