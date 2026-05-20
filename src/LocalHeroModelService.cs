using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020003C9 RID: 969
public static class LocalHeroModelService
{
	// Token: 0x06001632 RID: 5682 RVA: 0x00089C70 File Offset: 0x00087E70
	public static bool TryPreloadOverrideForHero(int heroId)
	{
		string itemRoot;
		LocalWorkshopManifest manifest;
		LocalHeroModelService.CachedWorkshopItem cachedWorkshopItem;
		return heroId > 0 && LocalHeroModelService.TryResolveItemRoot(heroId, out itemRoot, out manifest) && LocalHeroModelService.TryGetOrLoadCachedItem(itemRoot, heroId, manifest, out cachedWorkshopItem);
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x00089C9C File Offset: 0x00087E9C
	public static bool TryGetEnabledHeroName(HeroType heroType, out string heroName)
	{
		heroName = string.Empty;
		string text;
		LocalWorkshopManifest localWorkshopManifest;
		if (!LocalHeroModelService.TryResolveItemRoot((int)heroType, out text, out localWorkshopManifest))
		{
			return false;
		}
		if (localWorkshopManifest == null)
		{
			return false;
		}
		if (!string.IsNullOrWhiteSpace(localWorkshopManifest.heroName))
		{
			heroName = localWorkshopManifest.heroName.Trim();
			return true;
		}
		if (!string.IsNullOrWhiteSpace(localWorkshopManifest.title))
		{
			heroName = localWorkshopManifest.title.Trim();
			return true;
		}
		return false;
	}

	// Token: 0x06001634 RID: 5684 RVA: 0x00089CFC File Offset: 0x00087EFC
	public static bool TryGetEnabledHeroIcon(HeroType heroType, out Sprite heroIcon)
	{
		heroIcon = null;
		string itemRoot;
		LocalWorkshopManifest localWorkshopManifest;
		return LocalHeroModelService.TryResolveItemRoot((int)heroType, out itemRoot, out localWorkshopManifest) && localWorkshopManifest != null && ((!string.IsNullOrWhiteSpace(localWorkshopManifest.heroIcon) && LocalHeroModelService.TryLoadPackageSprite(itemRoot, localWorkshopManifest.heroIcon, out heroIcon)) || (!string.IsNullOrWhiteSpace(localWorkshopManifest.previewImage) && LocalHeroModelService.TryLoadPackageSprite(itemRoot, localWorkshopManifest.previewImage, out heroIcon)));
	}

	// Token: 0x06001635 RID: 5685 RVA: 0x00089D5C File Offset: 0x00087F5C
	public static bool TryLoadPackagePreviewSprite(string itemRoot, LocalWorkshopManifest manifest, out Sprite previewSprite)
	{
		previewSprite = null;
		return manifest != null && !string.IsNullOrWhiteSpace(manifest.previewImage) && LocalHeroModelService.TryLoadPackageSprite(itemRoot, manifest.previewImage, out previewSprite);
	}

	// Token: 0x06001636 RID: 5686 RVA: 0x00089D80 File Offset: 0x00087F80
	public static bool TryLoadModelPrefab(string itemRoot, int heroId, out GameObject modelPrefab)
	{
		modelPrefab = null;
		LocalWorkshopManifest localWorkshopManifest;
		if (!LocalWorkshopManifestLoader.TryLoad(itemRoot, out localWorkshopManifest))
		{
			return false;
		}
		if (localWorkshopManifest.heroId != heroId || string.IsNullOrEmpty(localWorkshopManifest.bundleRelativePath) || string.IsNullOrEmpty(localWorkshopManifest.bundleAssetName))
		{
			return false;
		}
		LocalHeroModelService.CachedWorkshopItem cachedWorkshopItem;
		if (!LocalHeroModelService.TryGetOrLoadCachedItem(itemRoot, heroId, localWorkshopManifest, out cachedWorkshopItem))
		{
			return false;
		}
		modelPrefab = cachedWorkshopItem.modelPrefab;
		return modelPrefab != null;
	}

	// Token: 0x06001637 RID: 5687 RVA: 0x00089DE0 File Offset: 0x00087FE0
	public static bool TryApplyLocalOverride(PlayerBase playerBase)
	{
		if (playerBase == null || playerBase.RoleModeBase == null)
		{
			return false;
		}
		int heroType = (int)playerBase.heroType;
		string text;
		LocalWorkshopManifest localWorkshopManifest;
		if (!LocalHeroModelService.TryResolveItemRoot(heroType, out text, out localWorkshopManifest))
		{
			return false;
		}
		LocalHeroModelService.CachedWorkshopItem cachedWorkshopItem;
		if (!LocalHeroModelService.TryGetOrLoadCachedItem(text, heroType, localWorkshopManifest, out cachedWorkshopItem) || cachedWorkshopItem.modelPrefab == null)
		{
			Debug.LogWarning("LocalHeroModelService: failed to load local model package for hero " + heroType.ToString() + " from " + text);
			return false;
		}
		Texture2D mainTextureOverride = cachedWorkshopItem.mainTextureOverride;
		bool flag = localWorkshopManifest != null && localWorkshopManifest.preserveSourceMaterials;
		LocalWorkshopEditorStateInfo localWorkshopEditorStateInfo;
		if (!flag && LocalWorkshopManifestLoader.TryLoadEditorState(text, out localWorkshopEditorStateInfo) && localWorkshopEditorStateInfo != null && localWorkshopEditorStateInfo.sourceMode == 1)
		{
			flag = true;
		}
		return LocalHeroModelService.TryApplyModelPrefab(playerBase.RoleModeBase, cachedWorkshopItem.modelPrefab, mainTextureOverride, flag);
	}

	// Token: 0x06001638 RID: 5688 RVA: 0x00089EA0 File Offset: 0x000880A0
	public static bool TryApplyEnabledHeroPreview(RoleModeBase roleModeBase, HeroType heroType)
	{
		if (roleModeBase == null)
		{
			return false;
		}
		if (heroType <= HeroType.None)
		{
			return false;
		}
		string itemRoot;
		LocalWorkshopManifest localWorkshopManifest;
		if (!LocalHeroModelService.TryResolveItemRoot((int)heroType, out itemRoot, out localWorkshopManifest))
		{
			return false;
		}
		LocalHeroModelService.CachedWorkshopItem cachedWorkshopItem;
		if (!LocalHeroModelService.TryGetOrLoadCachedItem(itemRoot, (int)heroType, localWorkshopManifest, out cachedWorkshopItem) || cachedWorkshopItem.modelPrefab == null)
		{
			return false;
		}
		Texture2D mainTextureOverride = cachedWorkshopItem.mainTextureOverride;
		bool flag = localWorkshopManifest != null && localWorkshopManifest.preserveSourceMaterials;
		LocalWorkshopEditorStateInfo localWorkshopEditorStateInfo;
		if (!flag && LocalWorkshopManifestLoader.TryLoadEditorState(itemRoot, out localWorkshopEditorStateInfo) && localWorkshopEditorStateInfo != null && localWorkshopEditorStateInfo.sourceMode == 1)
		{
			flag = true;
		}
		return LocalHeroModelService.TryApplyModelPrefab(roleModeBase, cachedWorkshopItem.modelPrefab, mainTextureOverride, flag);
	}

	// Token: 0x06001639 RID: 5689 RVA: 0x00089F30 File Offset: 0x00088130
	public static bool RefreshLocalPlayerOverrideForHero(int heroId)
	{
		PlayerBase localPlayer = GameHelperClient.localPlayer;
		if (localPlayer == null || localPlayer.RoleModeBase == null)
		{
			return false;
		}
		if (localPlayer.heroType != (HeroType)heroId)
		{
			return false;
		}
		LocalHeroModelService.ClearOverride(localPlayer.RoleModeBase);
		return LocalHeroModelService.TryApplyLocalOverride(localPlayer);
	}

	// Token: 0x0600163A RID: 5690 RVA: 0x00089F78 File Offset: 0x00088178
	public static void ClearOverride(RoleModeBase roleModeBase)
	{
		if (roleModeBase == null)
		{
			return;
		}
		LocalHeroModelBinding component = roleModeBase.GetComponent<LocalHeroModelBinding>();
		if (component != null)
		{
			component.ClearOverride();
		}
	}

	// Token: 0x0600163B RID: 5691 RVA: 0x00089FA8 File Offset: 0x000881A8
	public static bool TryApplyModelPrefab(RoleModeBase roleModeBase, GameObject modelPrefab, Texture2D mainTextureOverride = null, bool preserveSourceMaterials = false)
	{
		if (roleModeBase == null || modelPrefab == null)
		{
			return false;
		}
		Animator component = roleModeBase.GetComponent<Animator>();
		if (component == null)
		{
			Debug.LogWarning("LocalHeroModelService: target role mode is missing Animator.");
			return false;
		}
		List<Renderer> originalRenderers = LocalHeroModelService.GetOriginalRenderers(roleModeBase, null);
		LocalHeroModelBinding localHeroModelBinding = roleModeBase.GetComponent<LocalHeroModelBinding>();
		if (localHeroModelBinding == null)
		{
			localHeroModelBinding = roleModeBase.gameObject.AddComponent<LocalHeroModelBinding>();
		}
		localHeroModelBinding.ClearOverride();
		GameObject gameObject = Object.Instantiate<GameObject>(modelPrefab, roleModeBase.transform);
		gameObject.name = "__LocalWorkshopModel";
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = Vector3.one;
		bool result;
		try
		{
			Animator componentInChildren = gameObject.GetComponentInChildren<Animator>(true);
			if (componentInChildren == null || componentInChildren.avatar == null || !componentInChildren.avatar.isValid || !componentInChildren.avatar.isHuman)
			{
				Debug.LogWarning("LocalHeroModelService: local model is missing a valid humanoid Animator avatar.");
				Object.Destroy(gameObject);
				result = false;
			}
			else
			{
				componentInChildren.runtimeAnimatorController = component.runtimeAnimatorController;
				componentInChildren.applyRootMotion = component.applyRootMotion;
				componentInChildren.updateMode = component.updateMode;
				componentInChildren.cullingMode = component.cullingMode;
				componentInChildren.avatar = componentInChildren.avatar;
				componentInChildren.Rebind();
				componentInChildren.Update(0f);
				component.avatar = componentInChildren.avatar;
				component.enabled = false;
				roleModeBase.myAnim = componentInChildren;
				LocalHeroAnimatorProxy localHeroAnimatorProxy = componentInChildren.gameObject.GetComponent<LocalHeroAnimatorProxy>();
				if (localHeroAnimatorProxy == null)
				{
					localHeroAnimatorProxy = componentInChildren.gameObject.AddComponent<LocalHeroAnimatorProxy>();
				}
				localHeroAnimatorProxy.Bind(roleModeBase);
				WorkshopAttachmentConfig component2 = gameObject.GetComponent<WorkshopAttachmentConfig>();
				List<Renderer> list;
				List<LocalHeroAttachmentRestoreData> movedAttachmentData = LocalHeroModelService.MoveBoundAttachments(roleModeBase as PlayerModeBase, component, componentInChildren, component2, out list);
				List<Renderer> list2 = new List<Renderer>();
				foreach (Renderer renderer in originalRenderers)
				{
					if (!(renderer == null) && !list.Contains(renderer) && renderer.enabled)
					{
						renderer.enabled = false;
						list2.Add(renderer);
					}
				}
				List<Renderer> overrideRenderers = LocalHeroModelService.GetOverrideRenderers(gameObject.transform);
				for (int i = 0; i < list.Count; i++)
				{
					Renderer renderer2 = list[i];
					if (renderer2 != null && !overrideRenderers.Contains(renderer2))
					{
						overrideRenderers.Add(renderer2);
					}
				}
				List<Material> materialsToDispose = LocalHeroModelService.ApplyPlayerMaterials(originalRenderers, overrideRenderers, mainTextureOverride, preserveSourceMaterials);
				localHeroModelBinding.Bind(gameObject, list2, overrideRenderers, movedAttachmentData, roleModeBase, component, componentInChildren, materialsToDispose, mainTextureOverride);
				result = true;
			}
		}
		catch (Exception ex)
		{
			string str = "LocalHeroModelService: failed to apply local model override.\n";
			Exception ex2 = ex;
			Debug.LogWarning(str + ((ex2 != null) ? ex2.ToString() : null));
			localHeroModelBinding.ClearOverride();
			if (gameObject != null)
			{
				Object.Destroy(gameObject);
			}
			result = false;
		}
		return result;
	}

	// Token: 0x0600163C RID: 5692 RVA: 0x0008A2AC File Offset: 0x000884AC
	private static List<LocalHeroAttachmentRestoreData> MoveBoundAttachments(PlayerModeBase playerModeBase, Animator oldAnimator, Animator newAnimator, WorkshopAttachmentConfig attachmentConfig, out List<Renderer> movedAttachmentRenderers)
	{
		List<LocalHeroAttachmentRestoreData> list = new List<LocalHeroAttachmentRestoreData>();
		movedAttachmentRenderers = new List<Renderer>();
		if (playerModeBase == null || oldAnimator == null || newAnimator == null)
		{
			return list;
		}
		Dictionary<HumanBodyBones, PlayerHumanoidAttachmentBinding> dictionary = LocalHeroModelService.BuildOriginalBindingMap(playerModeBase.humanoidAttachmentBindings);
		List<WorkshopAttachmentEntry> list2 = LocalHeroModelService.BuildRuntimeAttachmentEntries(playerModeBase, newAnimator, attachmentConfig);
		for (int i = 0; i < list2.Count; i++)
		{
			WorkshopAttachmentEntry workshopAttachmentEntry = list2[i];
			if (workshopAttachmentEntry != null)
			{
				Transform boneTransform = newAnimator.GetBoneTransform(workshopAttachmentEntry.bone);
				if (!(boneTransform == null))
				{
					Transform transform = LocalHeroModelService.EnsureAttachmentSocket(boneTransform, workshopAttachmentEntry, i);
					PlayerHumanoidAttachmentBinding binding;
					dictionary.TryGetValue(workshopAttachmentEntry.bone, out binding);
					if (workshopAttachmentEntry.mode == WorkshopAttachmentMode.KeepOriginal)
					{
						LocalHeroModelService.MoveOriginalAttachments(binding, transform, list, movedAttachmentRenderers);
					}
					else
					{
						bool flag = LocalHeroModelService.HasTargetAttachmentName(workshopAttachmentEntry);
						if (flag && workshopAttachmentEntry.keepUntargetedOriginalAttachments)
						{
							LocalHeroModelService.MoveOriginalAttachments(binding, transform, list, movedAttachmentRenderers);
						}
						bool flag2 = LocalHeroModelService.DeactivateOriginalAttachments(binding, list, movedAttachmentRenderers, workshopAttachmentEntry.targetAttachmentName);
						if (flag && !flag2)
						{
							Debug.LogWarning("LocalHeroModelService: target attachment was not found on " + workshopAttachmentEntry.bone.ToString() + ": " + workshopAttachmentEntry.targetAttachmentName);
						}
						if (workshopAttachmentEntry.mode == WorkshopAttachmentMode.ReplacePrefab && workshopAttachmentEntry.replacementPrefab != null)
						{
							GameObject gameObject = Object.Instantiate<GameObject>(workshopAttachmentEntry.replacementPrefab, transform, false);
							gameObject.transform.localPosition = Vector3.zero;
							gameObject.transform.localRotation = Quaternion.identity;
							gameObject.transform.localScale = Vector3.one;
							LocalHeroModelService.CollectRenderers(gameObject.GetComponentsInChildren<Renderer>(true), movedAttachmentRenderers);
						}
						else if (workshopAttachmentEntry.mode == WorkshopAttachmentMode.ReplaceMesh && workshopAttachmentEntry.replacementMesh != null)
						{
							GameObject gameObject2 = new GameObject("__WorkshopAttachmentMesh_" + workshopAttachmentEntry.bone.ToString());
							gameObject2.transform.SetParent(transform, false);
							gameObject2.AddComponent<MeshFilter>().sharedMesh = workshopAttachmentEntry.replacementMesh;
							MeshRenderer meshRenderer = gameObject2.AddComponent<MeshRenderer>();
							meshRenderer.sharedMaterials = ((workshopAttachmentEntry.replacementMaterials != null && workshopAttachmentEntry.replacementMaterials.Length != 0) ? workshopAttachmentEntry.replacementMaterials : new Material[1]);
							movedAttachmentRenderers.Add(meshRenderer);
						}
					}
				}
			}
		}
		return list;
	}

	// Token: 0x0600163D RID: 5693 RVA: 0x0008A4E0 File Offset: 0x000886E0
	private static Dictionary<HumanBodyBones, PlayerHumanoidAttachmentBinding> BuildOriginalBindingMap(List<PlayerHumanoidAttachmentBinding> bindings)
	{
		Dictionary<HumanBodyBones, PlayerHumanoidAttachmentBinding> dictionary = new Dictionary<HumanBodyBones, PlayerHumanoidAttachmentBinding>();
		if (bindings == null)
		{
			return dictionary;
		}
		for (int i = 0; i < bindings.Count; i++)
		{
			PlayerHumanoidAttachmentBinding playerHumanoidAttachmentBinding = bindings[i];
			if (playerHumanoidAttachmentBinding != null && !dictionary.ContainsKey(playerHumanoidAttachmentBinding.bone))
			{
				dictionary.Add(playerHumanoidAttachmentBinding.bone, playerHumanoidAttachmentBinding);
			}
		}
		return dictionary;
	}

	// Token: 0x0600163E RID: 5694 RVA: 0x0008A530 File Offset: 0x00088730
	private static bool TryResolveItemRoot(int heroId, out string itemRoot, out LocalWorkshopManifest manifest)
	{
		itemRoot = string.Empty;
		manifest = null;
		string text;
		if (LocalHeroModelRegistry.TryGetItemRoot(heroId, out text) && LocalHeroModelService.TryValidateItemRoot(text, heroId, out manifest))
		{
			itemRoot = text;
			return true;
		}
		LocalWorkshopModSettings localWorkshopModSettings = Game.LocalWorkshopModSettings;
		SteamWorkshopService steamWorkshopService = Game.SteamWorkshopService;
		string text2;
		if (localWorkshopModSettings != null && localWorkshopModSettings.TryGetEnabledLocalFileItemRoot(heroId, out text2) && LocalHeroModelService.TryValidateItemRoot(text2, heroId, out manifest))
		{
			itemRoot = text2;
			return true;
		}
		if (localWorkshopModSettings == null || steamWorkshopService == null)
		{
			return false;
		}
		ulong publishedFileId;
		if (!localWorkshopModSettings.TryGetEnabledItemId(heroId, out publishedFileId))
		{
			return false;
		}
		WorkshopInstalledItem workshopInstalledItem;
		if (!steamWorkshopService.TryGetInstalledItem(publishedFileId, out workshopInstalledItem) || workshopInstalledItem == null)
		{
			return false;
		}
		if (!LocalHeroModelService.TryValidateItemRoot(workshopInstalledItem.installFolder, heroId, out manifest))
		{
			return false;
		}
		itemRoot = workshopInstalledItem.installFolder;
		return true;
	}

	// Token: 0x0600163F RID: 5695 RVA: 0x0008A5CC File Offset: 0x000887CC
	private static bool TryValidateItemRoot(string itemRoot, int heroId, out LocalWorkshopManifest manifest)
	{
		manifest = null;
		return !string.IsNullOrEmpty(itemRoot) && Directory.Exists(itemRoot) && LocalWorkshopManifestLoader.TryLoad(itemRoot, out manifest) && manifest != null && manifest.heroId == heroId && !string.IsNullOrEmpty(manifest.bundleRelativePath) && !string.IsNullOrEmpty(manifest.bundleAssetName) && File.Exists(Path.Combine(itemRoot, manifest.bundleRelativePath));
	}

	// Token: 0x06001640 RID: 5696 RVA: 0x0008A638 File Offset: 0x00088838
	private static List<WorkshopAttachmentEntry> BuildRuntimeAttachmentEntries(PlayerModeBase playerModeBase, Animator newAnimator, WorkshopAttachmentConfig attachmentConfig)
	{
		if (attachmentConfig != null && attachmentConfig.entries != null && attachmentConfig.entries.Count > 0)
		{
			return attachmentConfig.entries;
		}
		List<WorkshopAttachmentEntry> list = new List<WorkshopAttachmentEntry>();
		if (playerModeBase == null || playerModeBase.humanoidAttachmentBindings == null)
		{
			return list;
		}
		for (int i = 0; i < playerModeBase.humanoidAttachmentBindings.Count; i++)
		{
			PlayerHumanoidAttachmentBinding playerHumanoidAttachmentBinding = playerModeBase.humanoidAttachmentBindings[i];
			if (playerHumanoidAttachmentBinding != null)
			{
				list.Add(new WorkshopAttachmentEntry
				{
					bone = playerHumanoidAttachmentBinding.bone,
					mode = WorkshopAttachmentMode.KeepOriginal,
					preserveWorldScale = true,
					localPosition = Vector3.zero,
					localEuler = Vector3.zero,
					localScale = LocalHeroModelService.GetDefaultSocketScale(true, (newAnimator != null) ? newAnimator.GetBoneTransform(playerHumanoidAttachmentBinding.bone) : null)
				});
			}
		}
		return list;
	}

	// Token: 0x06001641 RID: 5697 RVA: 0x0008A70D File Offset: 0x0008890D
	private static bool HasTargetAttachmentName(WorkshopAttachmentEntry entry)
	{
		return entry != null && !string.IsNullOrWhiteSpace(entry.targetAttachmentName);
	}

	// Token: 0x06001642 RID: 5698 RVA: 0x0008A724 File Offset: 0x00088924
	private static void MoveOriginalAttachments(PlayerHumanoidAttachmentBinding binding, Transform socket, List<LocalHeroAttachmentRestoreData> restoreData, List<Renderer> movedAttachmentRenderers)
	{
		if (binding == null || binding.attachments == null || socket == null)
		{
			return;
		}
		for (int i = 0; i < binding.attachments.Count; i++)
		{
			Transform transform = binding.attachments[i];
			if (!(transform == null))
			{
				LocalHeroModelService.AddRestoreDataIfMissing(restoreData, transform);
				Vector3 localPosition = transform.localPosition;
				Quaternion localRotation = transform.localRotation;
				Vector3 localScale = transform.localScale;
				transform.SetParent(socket, false);
				transform.localPosition = localPosition;
				transform.localRotation = localRotation;
				transform.localScale = localScale;
				LocalHeroModelService.CollectRenderers(transform.GetComponentsInChildren<Renderer>(true), movedAttachmentRenderers);
			}
		}
	}

	// Token: 0x06001643 RID: 5699 RVA: 0x0008A7BC File Offset: 0x000889BC
	private static bool DeactivateOriginalAttachments(PlayerHumanoidAttachmentBinding binding, List<LocalHeroAttachmentRestoreData> restoreData, List<Renderer> movedAttachmentRenderers, string targetAttachmentName)
	{
		if (binding == null || binding.attachments == null)
		{
			return false;
		}
		bool flag = !string.IsNullOrWhiteSpace(targetAttachmentName);
		bool result = false;
		for (int i = 0; i < binding.attachments.Count; i++)
		{
			Transform transform = binding.attachments[i];
			if (!(transform == null))
			{
				if (!flag)
				{
					LocalHeroModelService.DeactivateAttachmentTransform(transform, restoreData, movedAttachmentRenderers);
					result = true;
				}
				else
				{
					foreach (Transform transform2 in transform.GetComponentsInChildren<Transform>(true))
					{
						if (LocalHeroModelService.AttachmentNameMatches(transform2.name, targetAttachmentName))
						{
							LocalHeroModelService.DeactivateAttachmentTransform(transform2, restoreData, movedAttachmentRenderers);
							result = true;
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06001644 RID: 5700 RVA: 0x0008A85B File Offset: 0x00088A5B
	private static void DeactivateAttachmentTransform(Transform attachment, List<LocalHeroAttachmentRestoreData> restoreData, List<Renderer> movedAttachmentRenderers)
	{
		if (attachment == null)
		{
			return;
		}
		LocalHeroModelService.AddRestoreDataIfMissing(restoreData, attachment);
		attachment.gameObject.SetActive(false);
		LocalHeroModelService.CollectRenderers(attachment.GetComponentsInChildren<Renderer>(true), movedAttachmentRenderers);
	}

	// Token: 0x06001645 RID: 5701 RVA: 0x0008A888 File Offset: 0x00088A88
	private static void AddRestoreDataIfMissing(List<LocalHeroAttachmentRestoreData> restoreData, Transform attachment)
	{
		if (restoreData == null || attachment == null)
		{
			return;
		}
		for (int i = 0; i < restoreData.Count; i++)
		{
			LocalHeroAttachmentRestoreData localHeroAttachmentRestoreData = restoreData[i];
			if (localHeroAttachmentRestoreData != null && localHeroAttachmentRestoreData.attachment == attachment)
			{
				return;
			}
		}
		restoreData.Add(new LocalHeroAttachmentRestoreData
		{
			attachment = attachment,
			originalParent = attachment.parent,
			siblingIndex = attachment.GetSiblingIndex(),
			activeSelf = attachment.gameObject.activeSelf,
			localPosition = attachment.localPosition,
			localRotation = attachment.localRotation,
			localScale = attachment.localScale
		});
	}

	// Token: 0x06001646 RID: 5702 RVA: 0x0008A92C File Offset: 0x00088B2C
	private static bool AttachmentNameMatches(string actualName, string targetAttachmentName)
	{
		if (string.IsNullOrWhiteSpace(actualName) || string.IsNullOrWhiteSpace(targetAttachmentName))
		{
			return false;
		}
		string a = LocalHeroModelService.NormalizeAttachmentName(actualName);
		string b = LocalHeroModelService.NormalizeAttachmentName(targetAttachmentName);
		return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
	}

	// Token: 0x06001647 RID: 5703 RVA: 0x0008A960 File Offset: 0x00088B60
	private static string NormalizeAttachmentName(string value)
	{
		string text = value.Trim();
		if (text.EndsWith("(Clone)", StringComparison.OrdinalIgnoreCase))
		{
			text = text.Substring(0, text.Length - "(Clone)".Length).TrimEnd();
		}
		return text;
	}

	// Token: 0x06001648 RID: 5704 RVA: 0x0008A9A4 File Offset: 0x00088BA4
	private static void CollectRenderers(Renderer[] renderers, List<Renderer> movedAttachmentRenderers)
	{
		if (renderers == null)
		{
			return;
		}
		foreach (Renderer renderer in renderers)
		{
			if (renderer != null && !movedAttachmentRenderers.Contains(renderer))
			{
				movedAttachmentRenderers.Add(renderer);
			}
		}
	}

	// Token: 0x06001649 RID: 5705 RVA: 0x0008A9E0 File Offset: 0x00088BE0
	private static Transform EnsureAttachmentSocket(Transform newBone, WorkshopAttachmentEntry entry, int entryIndex)
	{
		string text = "__WorkshopAttachmentSocket_" + entry.bone.ToString() + "_" + entryIndex.ToString();
		Transform transform = newBone.Find(text);
		if (transform == null)
		{
			transform = new GameObject(text).transform;
			transform.SetParent(newBone, false);
		}
		transform.localPosition = entry.localPosition;
		transform.localRotation = Quaternion.Euler(entry.localEuler);
		transform.localScale = ((entry.localScale == Vector3.zero) ? LocalHeroModelService.GetDefaultSocketScale(entry.preserveWorldScale, newBone) : entry.localScale);
		return transform;
	}

	// Token: 0x0600164A RID: 5706 RVA: 0x0008AA84 File Offset: 0x00088C84
	private static Vector3 GetDefaultSocketScale(bool preserveWorldScale, Transform bone)
	{
		if (!preserveWorldScale)
		{
			return Vector3.one;
		}
		return LocalHeroModelService.GetInverseLossyScale(bone);
	}

	// Token: 0x0600164B RID: 5707 RVA: 0x0008AA98 File Offset: 0x00088C98
	private static Vector3 GetInverseLossyScale(Transform target)
	{
		if (target == null)
		{
			return Vector3.one;
		}
		Vector3 lossyScale = target.lossyScale;
		return new Vector3(LocalHeroModelService.SafeInverse(lossyScale.x), LocalHeroModelService.SafeInverse(lossyScale.y), LocalHeroModelService.SafeInverse(lossyScale.z));
	}

	// Token: 0x0600164C RID: 5708 RVA: 0x0008AAE1 File Offset: 0x00088CE1
	private static float SafeInverse(float value)
	{
		if (Mathf.Abs(value) >= 0.0001f)
		{
			return 1f / value;
		}
		return 1f;
	}

	// Token: 0x0600164D RID: 5709 RVA: 0x0008AB00 File Offset: 0x00088D00
	private static List<Renderer> GetOriginalRenderers(RoleModeBase roleModeBase, Transform overrideRoot)
	{
		List<Renderer> list = new List<Renderer>();
		foreach (Renderer renderer in roleModeBase.GetComponentsInChildren<Renderer>(true))
		{
			if (!(renderer == null) && (!(overrideRoot != null) || !renderer.transform.IsChildOf(overrideRoot)))
			{
				list.Add(renderer);
			}
		}
		return list;
	}

	// Token: 0x0600164E RID: 5710 RVA: 0x0008AB58 File Offset: 0x00088D58
	private static List<Renderer> GetOverrideRenderers(Transform overrideRoot)
	{
		List<Renderer> list = new List<Renderer>();
		foreach (Renderer renderer in overrideRoot.GetComponentsInChildren<Renderer>(true))
		{
			if (renderer != null)
			{
				list.Add(renderer);
			}
		}
		return list;
	}

	// Token: 0x0600164F RID: 5711 RVA: 0x0008AB98 File Offset: 0x00088D98
	private static List<Material> ApplyPlayerMaterials(List<Renderer> originalRenderers, List<Renderer> overrideRenderers, Texture2D mainTextureOverride, bool preserveSourceMaterials)
	{
		List<Material> list = new List<Material>();
		Material fallbackMaterial = LocalHeroModelService.GetFallbackMaterial(originalRenderers);
		for (int i = 0; i < overrideRenderers.Count; i++)
		{
			Renderer renderer = overrideRenderers[i];
			if (!(renderer == null))
			{
				Material[] sharedMaterials = renderer.sharedMaterials;
				Material[] array = new Material[sharedMaterials.Length];
				Renderer renderer2 = (i < originalRenderers.Count) ? originalRenderers[i] : null;
				for (int j = 0; j < sharedMaterials.Length; j++)
				{
					Material material = sharedMaterials[j];
					Material material2 = preserveSourceMaterials ? material : (LocalHeroModelService.GetBaseMaterial(renderer2, j) ?? fallbackMaterial);
					if (material2 == null)
					{
						array[j] = material;
					}
					else
					{
						Material material3 = new Material(material2);
						Texture texture = LocalHeroModelService.GetMainTexture(material);
						if (texture == null && sharedMaterials.Length != 0)
						{
							texture = LocalHeroModelService.GetMainTexture(sharedMaterials[0]);
						}
						if (texture == null)
						{
							texture = mainTextureOverride;
						}
						LocalHeroModelService.ApplyMainTexture(material3, material, texture);
						array[j] = material3;
						list.Add(material3);
					}
				}
				renderer.sharedMaterials = array;
			}
		}
		return list;
	}

	// Token: 0x06001650 RID: 5712 RVA: 0x0008ACAC File Offset: 0x00088EAC
	private static Material GetFallbackMaterial(List<Renderer> originalRenderers)
	{
		for (int i = 0; i < originalRenderers.Count; i++)
		{
			Material baseMaterial = LocalHeroModelService.GetBaseMaterial(originalRenderers[i], 0);
			if (baseMaterial != null)
			{
				return baseMaterial;
			}
		}
		return null;
	}

	// Token: 0x06001651 RID: 5713 RVA: 0x0008ACE4 File Offset: 0x00088EE4
	private static Material GetBaseMaterial(Renderer renderer, int materialIndex)
	{
		if (renderer == null)
		{
			return null;
		}
		Material[] sharedMaterials = renderer.sharedMaterials;
		if (sharedMaterials == null || sharedMaterials.Length == 0)
		{
			return null;
		}
		if (materialIndex >= 0 && materialIndex < sharedMaterials.Length && sharedMaterials[materialIndex] != null)
		{
			return sharedMaterials[materialIndex];
		}
		return sharedMaterials[0];
	}

	// Token: 0x06001652 RID: 5714 RVA: 0x0008AD28 File Offset: 0x00088F28
	private static Texture GetMainTexture(Material sourceMaterial)
	{
		if (sourceMaterial == null)
		{
			return null;
		}
		if (sourceMaterial.HasProperty("_MainTex"))
		{
			return sourceMaterial.GetTexture("_MainTex");
		}
		if (sourceMaterial.HasProperty("_BaseMap"))
		{
			return sourceMaterial.GetTexture("_BaseMap");
		}
		string[] texturePropertyNames = sourceMaterial.GetTexturePropertyNames();
		if (texturePropertyNames != null && texturePropertyNames.Length != 0)
		{
			return sourceMaterial.GetTexture(texturePropertyNames[0]);
		}
		return sourceMaterial.mainTexture;
	}

	// Token: 0x06001653 RID: 5715 RVA: 0x0008AD90 File Offset: 0x00088F90
	private static void ApplyMainTexture(Material targetMaterial, Material sourceMaterial, Texture texture)
	{
		if (targetMaterial == null)
		{
			return;
		}
		if (targetMaterial.HasProperty(LocalHeroModelService.MainTexId))
		{
			targetMaterial.SetTexture(LocalHeroModelService.MainTexId, texture);
			if (sourceMaterial != null)
			{
				targetMaterial.SetTextureScale(LocalHeroModelService.MainTexId, sourceMaterial.mainTextureScale);
				targetMaterial.SetTextureOffset(LocalHeroModelService.MainTexId, sourceMaterial.mainTextureOffset);
			}
		}
		if (targetMaterial.HasProperty(LocalHeroModelService.BaseMapId))
		{
			targetMaterial.SetTexture(LocalHeroModelService.BaseMapId, texture);
			if (sourceMaterial != null)
			{
				targetMaterial.SetTextureScale(LocalHeroModelService.BaseMapId, sourceMaterial.mainTextureScale);
				targetMaterial.SetTextureOffset(LocalHeroModelService.BaseMapId, sourceMaterial.mainTextureOffset);
			}
		}
		targetMaterial.mainTexture = texture;
	}

	// Token: 0x06001654 RID: 5716 RVA: 0x0008AE38 File Offset: 0x00089038
	private static Texture2D LoadTextureOverride(string itemRoot, LocalWorkshopManifest manifest)
	{
		if (manifest == null || string.IsNullOrEmpty(manifest.mainTextureOverride))
		{
			return null;
		}
		LocalHeroModelService.CachedWorkshopItem cachedWorkshopItem;
		if (!LocalHeroModelService.TryGetOrLoadCachedItem(itemRoot, manifest.heroId, manifest, out cachedWorkshopItem))
		{
			return null;
		}
		return cachedWorkshopItem.mainTextureOverride;
	}

	// Token: 0x06001655 RID: 5717 RVA: 0x0008AE70 File Offset: 0x00089070
	private static bool TryGetOrLoadCachedItem(string itemRoot, int heroId, LocalWorkshopManifest manifest, out LocalHeroModelService.CachedWorkshopItem cachedItem)
	{
		cachedItem = null;
		if (string.IsNullOrEmpty(itemRoot) || manifest == null)
		{
			return false;
		}
		string fullPath = Path.GetFullPath(itemRoot);
		string text = Path.Combine(fullPath, manifest.bundleRelativePath);
		LocalHeroModelService.FileSignature fileSignature;
		if (string.IsNullOrEmpty(manifest.bundleAssetName) || !LocalHeroModelService.TryGetFileSignature(text, out fileSignature))
		{
			return false;
		}
		string text2 = string.IsNullOrEmpty(manifest.mainTextureOverride) ? string.Empty : Path.Combine(fullPath, manifest.mainTextureOverride);
		LocalHeroModelService.FileSignature fileSignature2 = default(LocalHeroModelService.FileSignature);
		bool flag = !string.IsNullOrEmpty(text2) && LocalHeroModelService.TryGetFileSignature(text2, out fileSignature2);
		if (!LocalHeroModelService.CachedItems.TryGetValue(fullPath, out cachedItem) || cachedItem == null)
		{
			cachedItem = new LocalHeroModelService.CachedWorkshopItem
			{
				itemRoot = fullPath
			};
			LocalHeroModelService.CachedItems[fullPath] = cachedItem;
		}
		if (cachedItem.modelPrefab == null || cachedItem.heroId != heroId || !string.Equals(cachedItem.bundlePath, text, StringComparison.OrdinalIgnoreCase) || !string.Equals(cachedItem.bundleAssetName, manifest.bundleAssetName, StringComparison.Ordinal) || cachedItem.bundleLength != fileSignature.length || cachedItem.bundleLastWriteTicks != fileSignature.lastWriteTicks)
		{
			AssetBundle assetBundle = AssetBundle.LoadFromFile(text);
			if (assetBundle == null)
			{
				return false;
			}
			try
			{
				cachedItem.modelPrefab = assetBundle.LoadAsset<GameObject>(manifest.bundleAssetName);
			}
			finally
			{
				assetBundle.Unload(false);
			}
			if (cachedItem.modelPrefab == null)
			{
				return false;
			}
			cachedItem.heroId = heroId;
			cachedItem.bundlePath = text;
			cachedItem.bundleAssetName = manifest.bundleAssetName;
			cachedItem.bundleLength = fileSignature.length;
			cachedItem.bundleLastWriteTicks = fileSignature.lastWriteTicks;
		}
		if (!flag)
		{
			cachedItem.texturePath = text2;
			cachedItem.textureLength = 0L;
			cachedItem.textureLastWriteTicks = 0L;
			cachedItem.mainTextureOverride = null;
			return true;
		}
		if (!(cachedItem.mainTextureOverride == null) && string.Equals(cachedItem.texturePath, text2, StringComparison.OrdinalIgnoreCase) && cachedItem.textureLength == fileSignature2.length && cachedItem.textureLastWriteTicks == fileSignature2.lastWriteTicks)
		{
			return true;
		}
		byte[] array = File.ReadAllBytes(text2);
		if (array == null || array.Length == 0)
		{
			return true;
		}
		Texture2D texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, false);
		if (!texture2D.LoadImage(array))
		{
			Object.Destroy(texture2D);
			return true;
		}
		texture2D.name = "LocalWorkshopMainTexture";
		cachedItem.texturePath = text2;
		cachedItem.textureLength = fileSignature2.length;
		cachedItem.textureLastWriteTicks = fileSignature2.lastWriteTicks;
		cachedItem.mainTextureOverride = texture2D;
		return true;
	}

	// Token: 0x06001656 RID: 5718 RVA: 0x0008B0F4 File Offset: 0x000892F4
	private static bool TryGetFileSignature(string path, out LocalHeroModelService.FileSignature signature)
	{
		signature = default(LocalHeroModelService.FileSignature);
		if (string.IsNullOrEmpty(path) || !File.Exists(path))
		{
			return false;
		}
		FileInfo fileInfo = new FileInfo(path);
		signature.length = fileInfo.Length;
		signature.lastWriteTicks = fileInfo.LastWriteTimeUtc.Ticks;
		return true;
	}

	// Token: 0x06001657 RID: 5719 RVA: 0x0008B144 File Offset: 0x00089344
	public static bool TryLoadPackageSprite(string itemRoot, string relativePath, out Sprite heroIcon)
	{
		heroIcon = null;
		if (string.IsNullOrEmpty(itemRoot) || string.IsNullOrEmpty(relativePath))
		{
			return false;
		}
		string text = Path.Combine(Path.GetFullPath(itemRoot), relativePath);
		LocalHeroModelService.FileSignature fileSignature;
		if (!LocalHeroModelService.TryGetFileSignature(text, out fileSignature))
		{
			return false;
		}
		LocalHeroModelService.CachedHeroIcon cachedHeroIcon;
		if (LocalHeroModelService.CachedHeroIcons.TryGetValue(text, out cachedHeroIcon) && cachedHeroIcon != null && cachedHeroIcon.sprite != null && cachedHeroIcon.length == fileSignature.length && cachedHeroIcon.lastWriteTicks == fileSignature.lastWriteTicks)
		{
			heroIcon = cachedHeroIcon.sprite;
			return true;
		}
		byte[] array = File.ReadAllBytes(text);
		if (array == null || array.Length == 0)
		{
			return false;
		}
		Texture2D texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, false);
		if (!texture2D.LoadImage(array))
		{
			Object.Destroy(texture2D);
			return false;
		}
		texture2D.name = "LocalWorkshopHeroIcon";
		texture2D.filterMode = FilterMode.Bilinear;
		Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 100f, 0U, SpriteMeshType.FullRect);
		LocalHeroModelService.CachedHeroIcons[text] = new LocalHeroModelService.CachedHeroIcon
		{
			path = text,
			length = fileSignature.length,
			lastWriteTicks = fileSignature.lastWriteTicks,
			sprite = sprite
		};
		heroIcon = sprite;
		return true;
	}

	// Token: 0x040014DB RID: 5339
	private const string LocalModelRootName = "__LocalWorkshopModel";

	// Token: 0x040014DC RID: 5340
	private static readonly int MainTexId = Shader.PropertyToID("_MainTex");

	// Token: 0x040014DD RID: 5341
	private static readonly int BaseMapId = Shader.PropertyToID("_BaseMap");

	// Token: 0x040014DE RID: 5342
	private static readonly Dictionary<string, LocalHeroModelService.CachedWorkshopItem> CachedItems = new Dictionary<string, LocalHeroModelService.CachedWorkshopItem>(StringComparer.OrdinalIgnoreCase);

	// Token: 0x040014DF RID: 5343
	private static readonly Dictionary<string, LocalHeroModelService.CachedHeroIcon> CachedHeroIcons = new Dictionary<string, LocalHeroModelService.CachedHeroIcon>(StringComparer.OrdinalIgnoreCase);

	// Token: 0x020003CA RID: 970
	private sealed class CachedWorkshopItem
	{
		// Token: 0x040014E0 RID: 5344
		public string itemRoot;

		// Token: 0x040014E1 RID: 5345
		public int heroId;

		// Token: 0x040014E2 RID: 5346
		public string bundlePath;

		// Token: 0x040014E3 RID: 5347
		public string bundleAssetName;

		// Token: 0x040014E4 RID: 5348
		public long bundleLength;

		// Token: 0x040014E5 RID: 5349
		public long bundleLastWriteTicks;

		// Token: 0x040014E6 RID: 5350
		public GameObject modelPrefab;

		// Token: 0x040014E7 RID: 5351
		public string texturePath;

		// Token: 0x040014E8 RID: 5352
		public long textureLength;

		// Token: 0x040014E9 RID: 5353
		public long textureLastWriteTicks;

		// Token: 0x040014EA RID: 5354
		public Texture2D mainTextureOverride;
	}

	// Token: 0x020003CB RID: 971
	private struct FileSignature
	{
		// Token: 0x040014EB RID: 5355
		public long length;

		// Token: 0x040014EC RID: 5356
		public long lastWriteTicks;
	}

	// Token: 0x020003CC RID: 972
	private sealed class CachedHeroIcon
	{
		// Token: 0x040014ED RID: 5357
		public string path;

		// Token: 0x040014EE RID: 5358
		public long length;

		// Token: 0x040014EF RID: 5359
		public long lastWriteTicks;

		// Token: 0x040014F0 RID: 5360
		public Sprite sprite;
	}
}
