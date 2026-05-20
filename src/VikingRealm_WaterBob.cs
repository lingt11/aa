using System;
using UnityEngine;

// Token: 0x0200040F RID: 1039
public class VikingRealm_WaterBob : MonoBehaviour
{
	// Token: 0x060017AF RID: 6063 RVA: 0x00093EFC File Offset: 0x000920FC
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

	// Token: 0x060017B0 RID: 6064 RVA: 0x00093F84 File Offset: 0x00092184
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

	// Token: 0x040016B9 RID: 5817
	public float bobbingHeight = 0.08f;

	// Token: 0x040016BA RID: 5818
	public float bobbingSpeed = 1.5f;

	// Token: 0x040016BB RID: 5819
	public float rotationAmount = 0.8f;

	// Token: 0x040016BC RID: 5820
	public bool randomOffset = true;

	// Token: 0x040016BD RID: 5821
	public Vector2 randomRange = new Vector2(0.1f, 1f);

	// Token: 0x040016BE RID: 5822
	private Vector3 startPos;

	// Token: 0x040016BF RID: 5823
	private Quaternion startRotation;
}
