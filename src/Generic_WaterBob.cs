using System;
using UnityEngine;

// Token: 0x0200040C RID: 1036
public class Generic_WaterBob : MonoBehaviour
{
	// Token: 0x060017A7 RID: 6055 RVA: 0x00093AC4 File Offset: 0x00091CC4
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

	// Token: 0x060017A8 RID: 6056 RVA: 0x00093B4C File Offset: 0x00091D4C
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

	// Token: 0x040016A5 RID: 5797
	public float bobbingHeight = 0.08f;

	// Token: 0x040016A6 RID: 5798
	public float bobbingSpeed = 1.5f;

	// Token: 0x040016A7 RID: 5799
	public float rotationAmount = 0.8f;

	// Token: 0x040016A8 RID: 5800
	public bool randomOffset = true;

	// Token: 0x040016A9 RID: 5801
	public Vector2 randomRange = new Vector2(0.1f, 1f);

	// Token: 0x040016AA RID: 5802
	private Vector3 startPos;

	// Token: 0x040016AB RID: 5803
	private Quaternion startRotation;
}
