using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003C7 RID: 967
[DisallowMultipleComponent]
public class LocalHeroModelBinding : MonoBehaviour
{
	// Token: 0x0600162A RID: 5674 RVA: 0x00089850 File Offset: 0x00087A50
	public void Bind(GameObject instance, List<Renderer> renderersToHide, List<Renderer> overrideVisibleRenderers, List<LocalHeroAttachmentRestoreData> movedAttachmentData, RoleModeBase roleModeBaseValue, Animator originalAnimatorValue, Animator overrideAnimatorValue, List<Material> materialsToDispose, Texture2D overrideTextureValue)
	{
		this.ClearOverride();
		this.roleModeBase = roleModeBaseValue;
		this.overrideInstance = instance;
		this.originalAnimator = originalAnimatorValue;
		this.overrideAnimator = overrideAnimatorValue;
		this.overrideTexture = overrideTextureValue;
		this.originalAvatar = ((originalAnimatorValue != null) ? originalAnimatorValue.avatar : null);
		this.originalMyRenderers = ((roleModeBaseValue != null && roleModeBaseValue.myRenderers != null) ? new List<Renderer>(roleModeBaseValue.myRenderers) : new List<Renderer>());
		if (renderersToHide == null)
		{
			renderersToHide = new List<Renderer>();
		}
		this.hiddenRenderers.AddRange(renderersToHide);
		if (overrideVisibleRenderers != null)
		{
			this.overrideRenderers.AddRange(overrideVisibleRenderers);
		}
		if (movedAttachmentData != null)
		{
			this.movedAttachments.AddRange(movedAttachmentData);
		}
		if (this.roleModeBase != null)
		{
			this.roleModeBase.myRenderers = new List<Renderer>(this.overrideRenderers);
		}
		if (materialsToDispose == null)
		{
			return;
		}
		this.overrideMaterials.AddRange(materialsToDispose);
	}

	// Token: 0x0600162B RID: 5675 RVA: 0x0008993C File Offset: 0x00087B3C
	public void ClearOverride()
	{
		if (this.roleModeBase != null && this.originalAnimator != null)
		{
			this.roleModeBase.myAnim = this.originalAnimator;
			this.originalAnimator.avatar = this.originalAvatar;
			this.originalAnimator.enabled = true;
			this.roleModeBase.myRenderers = (this.originalMyRenderers ?? new List<Renderer>());
		}
		if (this.overrideAnimator != null)
		{
			this.overrideAnimator.enabled = false;
		}
		for (int i = 0; i < this.movedAttachments.Count; i++)
		{
			LocalHeroAttachmentRestoreData localHeroAttachmentRestoreData = this.movedAttachments[i];
			if (localHeroAttachmentRestoreData != null && !(localHeroAttachmentRestoreData.attachment == null) && !(localHeroAttachmentRestoreData.originalParent == null))
			{
				localHeroAttachmentRestoreData.attachment.SetParent(localHeroAttachmentRestoreData.originalParent, false);
				localHeroAttachmentRestoreData.attachment.SetSiblingIndex(Mathf.Clamp(localHeroAttachmentRestoreData.siblingIndex, 0, localHeroAttachmentRestoreData.originalParent.childCount - 1));
				localHeroAttachmentRestoreData.attachment.localPosition = localHeroAttachmentRestoreData.localPosition;
				localHeroAttachmentRestoreData.attachment.localRotation = localHeroAttachmentRestoreData.localRotation;
				localHeroAttachmentRestoreData.attachment.localScale = localHeroAttachmentRestoreData.localScale;
				localHeroAttachmentRestoreData.attachment.gameObject.SetActive(localHeroAttachmentRestoreData.activeSelf);
			}
		}
		for (int j = 0; j < this.hiddenRenderers.Count; j++)
		{
			Renderer renderer = this.hiddenRenderers[j];
			if (renderer != null)
			{
				renderer.enabled = true;
			}
		}
		this.hiddenRenderers.Clear();
		this.overrideRenderers.Clear();
		this.movedAttachments.Clear();
		for (int k = 0; k < this.overrideMaterials.Count; k++)
		{
			if (this.overrideMaterials[k] != null)
			{
				Object.Destroy(this.overrideMaterials[k]);
			}
		}
		this.overrideMaterials.Clear();
		if (this.overrideInstance != null)
		{
			Object.Destroy(this.overrideInstance);
			this.overrideInstance = null;
		}
		if (this.overrideTexture != null)
		{
			Object.Destroy(this.overrideTexture);
			this.overrideTexture = null;
		}
		this.roleModeBase = null;
		this.originalAnimator = null;
		this.overrideAnimator = null;
		this.originalAvatar = null;
		this.originalMyRenderers = null;
	}

	// Token: 0x0600162C RID: 5676 RVA: 0x00089B97 File Offset: 0x00087D97
	private void OnDestroy()
	{
		this.ClearOverride();
	}

	// Token: 0x040014CF RID: 5327
	private readonly List<Renderer> hiddenRenderers = new List<Renderer>();

	// Token: 0x040014D0 RID: 5328
	private readonly List<Material> overrideMaterials = new List<Material>();

	// Token: 0x040014D1 RID: 5329
	private readonly List<Renderer> overrideRenderers = new List<Renderer>();

	// Token: 0x040014D2 RID: 5330
	private readonly List<LocalHeroAttachmentRestoreData> movedAttachments = new List<LocalHeroAttachmentRestoreData>();

	// Token: 0x040014D3 RID: 5331
	private RoleModeBase roleModeBase;

	// Token: 0x040014D4 RID: 5332
	private GameObject overrideInstance;

	// Token: 0x040014D5 RID: 5333
	private Animator originalAnimator;

	// Token: 0x040014D6 RID: 5334
	private Animator overrideAnimator;

	// Token: 0x040014D7 RID: 5335
	private Avatar originalAvatar;

	// Token: 0x040014D8 RID: 5336
	private List<Renderer> originalMyRenderers;

	// Token: 0x040014D9 RID: 5337
	private Texture2D overrideTexture;
}
