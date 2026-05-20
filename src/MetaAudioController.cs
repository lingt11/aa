using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x020003FE RID: 1022
public class MetaAudioController : MonoBehaviour
{
	// Token: 0x06001777 RID: 6007 RVA: 0x00092A1B File Offset: 0x00090C1B
	private void Start()
	{
		this.globalProgress = 0f;
	}

	// Token: 0x06001778 RID: 6008 RVA: 0x00092A28 File Offset: 0x00090C28
	public void EmitParticleExplosion(Vector3 pos, bool big)
	{
		if (big)
		{
			Object.Instantiate<GameObject>(this.explosionSfxPregabs[Random.Range(0, this.explosionSfxPregabs.Length)], pos, base.transform.rotation);
			return;
		}
		Object.Instantiate<GameObject>(this.smallExplosionSfxPregabs[Random.Range(0, this.smallExplosionSfxPregabs.Length)], pos, base.transform.rotation);
	}

	// Token: 0x06001779 RID: 6009 RVA: 0x00092A88 File Offset: 0x00090C88
	private void Update()
	{
		Mouse current = Mouse.current;
		if (current != null)
		{
			if (current.leftButton.isPressed)
			{
				this.globalProgress = 1f;
			}
			if (current.leftButton.wasPressedThisFrame || current.rightButton.wasPressedThisFrame)
			{
				Object.Instantiate<GameObject>(this.waveSfxPrefabs[Random.Range(0, this.waveSfxPrefabs.Length)], base.transform.position, base.transform.rotation);
			}
		}
		if (this.globalProgress >= 0f)
		{
			this.globalProgress -= Time.deltaTime * this.globalProgressSpeed;
		}
		this.loopingSFX.volume = this.globalProgress;
	}

	// Token: 0x0400164C RID: 5708
	public AudioSource loopingSFX;

	// Token: 0x0400164D RID: 5709
	public GameObject[] waveSfxPrefabs;

	// Token: 0x0400164E RID: 5710
	public GameObject[] explosionSfxPregabs;

	// Token: 0x0400164F RID: 5711
	public GameObject[] smallExplosionSfxPregabs;

	// Token: 0x04001650 RID: 5712
	public float globalProgressSpeed = 1f;

	// Token: 0x04001651 RID: 5713
	private float globalProgress;
}
