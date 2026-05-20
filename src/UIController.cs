using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

// Token: 0x02000404 RID: 1028
public class UIController : MonoBehaviour
{
	// Token: 0x0600178A RID: 6026 RVA: 0x00093078 File Offset: 0x00091278
	private void Start()
	{
		StandaloneInputModule standaloneInputModule = Object.FindFirstObjectByType<StandaloneInputModule>();
		if (standaloneInputModule != null)
		{
			Debug.Log("Replacing Standalone Input Module with Input System UI Input Module.");
			GameObject gameObject = standaloneInputModule.gameObject;
			Object.Destroy(standaloneInputModule);
			gameObject.AddComponent<InputSystemUIInputModule>();
		}
		if (this.prefabHolder == null)
		{
			Debug.LogError("PrefabHolder is not assigned.");
			return;
		}
		this.lt = new List<Transform>();
		this.prefabs = this.prefabHolder.GetComponentsInChildren<Transform>(true);
		foreach (Transform transform in this.prefabs)
		{
			if (transform.parent == this.prefabHolder)
			{
				this.lt.Add(transform);
			}
		}
		this.prefabs = this.lt.ToArray();
		this.EnableActive();
	}

	// Token: 0x0600178B RID: 6027 RVA: 0x0009313C File Offset: 0x0009133C
	public void EnableActive()
	{
		for (int i = 0; i < this.prefabs.Length; i++)
		{
			if (i == this.activeNumber)
			{
				this.prefabs[i].gameObject.SetActive(true);
				this.text.text = this.prefabs[i].name;
			}
			else
			{
				this.prefabs[i].gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600178C RID: 6028 RVA: 0x000931A8 File Offset: 0x000913A8
	public void ChangeEffect(bool bo)
	{
		if (bo)
		{
			this.activeNumber++;
			if (this.activeNumber == this.prefabs.Length)
			{
				this.activeNumber = 0;
			}
		}
		else
		{
			this.activeNumber--;
			if (this.activeNumber == -1)
			{
				this.activeNumber = this.prefabs.Length - 1;
			}
		}
		this.EnableActive();
	}

	// Token: 0x0600178D RID: 6029 RVA: 0x0009320C File Offset: 0x0009140C
	public void SetDay()
	{
		if (this.directionalLight != null)
		{
			this.directionalLight.enabled = true;
		}
		if (this.reflectionProbe != null)
		{
			this.reflectionProbe.RenderProbe();
		}
		if (this.daySkyboxMaterial != null)
		{
			RenderSettings.skybox = this.daySkyboxMaterial;
		}
	}

	// Token: 0x0600178E RID: 6030 RVA: 0x00093268 File Offset: 0x00091468
	public void SetNight()
	{
		if (this.directionalLight != null)
		{
			this.directionalLight.enabled = false;
		}
		if (this.reflectionProbe != null)
		{
			this.reflectionProbe.RenderProbe();
		}
		if (this.nightSkyboxMaterial != null)
		{
			RenderSettings.skybox = this.nightSkyboxMaterial;
		}
	}

	// Token: 0x0600178F RID: 6031 RVA: 0x000932C4 File Offset: 0x000914C4
	private void Update()
	{
		Keyboard current = Keyboard.current;
		if (current != null)
		{
			if (current.qKey.wasPressedThisFrame)
			{
				this.SetDay();
			}
			if (current.eKey.wasPressedThisFrame)
			{
				this.SetNight();
			}
			if (current.aKey.wasPressedThisFrame)
			{
				this.ChangeEffect(true);
			}
			if (current.dKey.wasPressedThisFrame)
			{
				this.ChangeEffect(false);
			}
		}
	}

	// Token: 0x0400166E RID: 5742
	public Light directionalLight;

	// Token: 0x0400166F RID: 5743
	public ReflectionProbe reflectionProbe;

	// Token: 0x04001670 RID: 5744
	public Material daySkyboxMaterial;

	// Token: 0x04001671 RID: 5745
	public Material nightSkyboxMaterial;

	// Token: 0x04001672 RID: 5746
	public Transform prefabHolder;

	// Token: 0x04001673 RID: 5747
	public Text text;

	// Token: 0x04001674 RID: 5748
	private Transform[] prefabs;

	// Token: 0x04001675 RID: 5749
	private List<Transform> lt;

	// Token: 0x04001676 RID: 5750
	private int activeNumber;
}
