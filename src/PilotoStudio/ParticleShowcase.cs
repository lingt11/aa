using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PilotoStudio
{
	// Token: 0x020004B7 RID: 1207
	public class ParticleShowcase : MonoBehaviour
	{
		// Token: 0x06001ABC RID: 6844 RVA: 0x000A5340 File Offset: 0x000A3540
		private void Start()
		{
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				this.particles.Add(transform.gameObject);
			}
			this.PostUpdateLogic();
			this.particles[this.currentlyActive].SetActive(true);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x000A53C0 File Offset: 0x000A35C0
		private void PostUpdateLogic()
		{
			this.displayName.text = this.particles[this.currentlyActive].name;
			ParticleHandler particleHandler;
			if (this.particles[this.currentlyActive].TryGetComponent<ParticleHandler>(out particleHandler))
			{
				particleHandler.Cast();
			}
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x000A5410 File Offset: 0x000A3610
		public void ActivateNext()
		{
			if (this.currentlyActive + 1 >= this.particles.Count)
			{
				this.particles[this.currentlyActive].SetActive(false);
				this.currentlyActive = 0;
				this.particles[this.currentlyActive].SetActive(true);
			}
			else
			{
				this.particles[this.currentlyActive].SetActive(false);
				this.currentlyActive++;
				this.particles[this.currentlyActive].SetActive(true);
			}
			this.PostUpdateLogic();
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x000A54AC File Offset: 0x000A36AC
		public void ActivatePrevious()
		{
			if (this.currentlyActive - 1 < 0)
			{
				this.particles[this.currentlyActive].SetActive(false);
				this.currentlyActive = this.particles.Count - 1;
				this.particles[this.currentlyActive].SetActive(true);
			}
			else
			{
				this.particles[this.currentlyActive].SetActive(false);
				this.currentlyActive--;
				this.particles[this.currentlyActive].SetActive(true);
			}
			this.PostUpdateLogic();
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x000A554C File Offset: 0x000A374C
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				this.ActivatePrevious();
			}
			if (Input.GetKeyDown(KeyCode.W))
			{
				this.ActivateNext();
			}
			if (Input.GetKeyDown(KeyCode.Space))
			{
				ParticleSystem particleSystem;
				if (this.particles[this.currentlyActive].TryGetComponent<ParticleSystem>(out particleSystem))
				{
					particleSystem.Play();
				}
				this.PostUpdateLogic();
			}
		}

		// Token: 0x04001A2B RID: 6699
		[SerializeField]
		private List<GameObject> particles = new List<GameObject>();

		// Token: 0x04001A2C RID: 6700
		[SerializeField]
		private int currentlyActive;

		// Token: 0x04001A2D RID: 6701
		[SerializeField]
		private Text displayName;
	}
}
