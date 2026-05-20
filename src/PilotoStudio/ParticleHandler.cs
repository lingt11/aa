using System;
using System.Collections;
using UnityEngine;

namespace PilotoStudio
{
	// Token: 0x020004B3 RID: 1203
	[ExecuteAlways]
	public class ParticleHandler : MonoBehaviour
	{
		// Token: 0x06001AA3 RID: 6819 RVA: 0x000A500C File Offset: 0x000A320C
		private void OnEnable()
		{
			this.castParticleSystem = this.castParticle.GetComponent<ParticleSystem>();
			this.loopingParticleSystem = this.loopingParticle.GetComponent<ParticleSystem>();
			this.endParticleSystem = this.endParticle.GetComponent<ParticleSystem>();
			if (!this.castParticleSystem || !this.loopingParticleSystem || !this.endParticleSystem)
			{
				Debug.LogError("ParticleHandler: Missing particle systems. Ensure they are referenced correctly.");
				return;
			}
			this.Cast();
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x000A5084 File Offset: 0x000A3284
		public void Cast()
		{
			base.StartCoroutine(this.Flow());
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x000A5093 File Offset: 0x000A3293
		private IEnumerator Flow()
		{
			this.PlayParticles(this.castParticleSystem, this.castFXDuration);
			yield return new WaitForSeconds(this.castFXDuration);
			this.PlayParticles(this.loopingParticleSystem, this.loopDuration);
			yield return new WaitForSeconds(this.loopDuration);
			this.PlayParticles(this.endParticleSystem, 0f);
			yield return this.WaitUntilParticleSystemStops(this.endParticleSystem);
			yield break;
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x000A50A2 File Offset: 0x000A32A2
		private IEnumerator WaitUntilParticleSystemStops(ParticleSystem particleSystem)
		{
			while (particleSystem.IsAlive(true))
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x000A50B4 File Offset: 0x000A32B4
		private void PlayParticles(ParticleSystem particleSystem, float duration = 0f)
		{
			particleSystem.gameObject.SetActive(true);
			ParticleSystem.EmissionModule emission = particleSystem.emission;
			if (this.startEmission == 0f)
			{
				this.startEmission = emission.rateOverTimeMultiplier;
			}
			if (particleSystem.main.startLifetime.constantMax == float.PositiveInfinity)
			{
				base.StartCoroutine(this.WaitUntilParticleSystemStops(particleSystem));
			}
			else
			{
				emission.rateOverTimeMultiplier = this.startEmission;
			}
			particleSystem.Play();
			if (duration > 0f && particleSystem.main.startLifetime.constantMax != float.PositiveInfinity)
			{
				base.StartCoroutine(this.StopParticleAfterTime(particleSystem, duration));
			}
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x000A5162 File Offset: 0x000A3362
		private IEnumerator StopParticleAfterTime(ParticleSystem particleSystem, float duration)
		{
			yield return new WaitForSeconds(duration);
			particleSystem.emission.rateOverTimeMultiplier = 0f;
			yield break;
		}

		// Token: 0x04001A18 RID: 6680
		public GameObject castParticle;

		// Token: 0x04001A19 RID: 6681
		public float castFXDuration;

		// Token: 0x04001A1A RID: 6682
		public GameObject loopingParticle;

		// Token: 0x04001A1B RID: 6683
		public float loopDuration;

		// Token: 0x04001A1C RID: 6684
		public GameObject endParticle;

		// Token: 0x04001A1D RID: 6685
		private ParticleSystem castParticleSystem;

		// Token: 0x04001A1E RID: 6686
		private ParticleSystem loopingParticleSystem;

		// Token: 0x04001A1F RID: 6687
		private ParticleSystem endParticleSystem;

		// Token: 0x04001A20 RID: 6688
		private float startEmission;
	}
}
