using System;
using UnityEngine;

// Token: 0x020003FC RID: 1020
[RequireComponent(typeof(ParticleSystem))]
public class HomingParticles : MonoBehaviour
{
	// Token: 0x0600176F RID: 5999 RVA: 0x0009256B File Offset: 0x0009076B
	private void Start()
	{
		this.particleSystem = base.GetComponent<ParticleSystem>();
		this.particleSystemMainModule = this.particleSystem.main;
	}

	// Token: 0x06001770 RID: 6000 RVA: 0x0009258C File Offset: 0x0009078C
	private void LateUpdate()
	{
		int maxParticles = this.particleSystemMainModule.maxParticles;
		if (this.particles == null || this.particles.Length < maxParticles)
		{
			this.particles = new ParticleSystem.Particle[maxParticles];
		}
		this.particleSystem.GetParticles(this.particles);
		float num = this.force * Time.deltaTime;
		Vector3 a;
		switch (this.particleSystemMainModule.simulationSpace)
		{
		case ParticleSystemSimulationSpace.Local:
			a = base.transform.InverseTransformPoint(this.target.position);
			break;
		case ParticleSystemSimulationSpace.World:
			a = this.target.position;
			break;
		case ParticleSystemSimulationSpace.Custom:
			a = this.particleSystemMainModule.customSimulationSpace.InverseTransformPoint(this.target.position);
			break;
		default:
			throw new NotSupportedException(string.Format("Unsupported simulation space '{0}'.", Enum.GetName(typeof(ParticleSystemSimulationSpace), this.particleSystemMainModule.simulationSpace)));
		}
		int particleCount = this.particleSystem.particleCount;
		for (int i = 0; i < particleCount; i++)
		{
			Vector3 forward = a - this.particles[i].position;
			Quaternion rotation = Quaternion.Lerp(Quaternion.LookRotation(this.particles[i].velocity), Quaternion.LookRotation(forward), num * this.trackSpeed);
			this.particles[i].velocity = rotation * Vector3.forward * Mathf.Min(this.maxSpeed, this.particles[i].velocity.magnitude + num);
		}
		this.particleSystem.SetParticles(this.particles, particleCount);
	}

	// Token: 0x04001635 RID: 5685
	public Transform target;

	// Token: 0x04001636 RID: 5686
	public float force = 10f;

	// Token: 0x04001637 RID: 5687
	public float trackSpeed = 0.05f;

	// Token: 0x04001638 RID: 5688
	public float maxSpeed = 6f;

	// Token: 0x04001639 RID: 5689
	private ParticleSystem particleSystem;

	// Token: 0x0400163A RID: 5690
	private ParticleSystem.Particle[] particles;

	// Token: 0x0400163B RID: 5691
	private ParticleSystem.MainModule particleSystemMainModule;
}
