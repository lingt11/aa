using System;
using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

// Token: 0x02000149 RID: 329
public static class SteamAvatarLoader
{
	// Token: 0x06000656 RID: 1622 RVA: 0x00026918 File Offset: 0x00024B18
	public static IEnumerator LoadAvatarSprite(CSteamID steamId, Action<Sprite> onDone, SteamAvatarLoader.AvatarSize size = SteamAvatarLoader.AvatarSize.Large, Sprite fallback = null, int targetSize = 0)
	{
		SteamManager steamManager = EntityStatic.Get<SteamManager>();
		if (steamManager == null || !steamManager.Initialized)
		{
			if (onDone != null)
			{
				onDone(fallback);
			}
			yield break;
		}
		Sprite obj;
		if (SteamAvatarLoader._spriteCache.TryGetValue(steamId.m_SteamID, out obj))
		{
			if (onDone != null)
			{
				onDone(obj);
			}
			yield break;
		}
		int i = SteamAvatarLoader._GetAvatarHandle(steamId, size);
		int safety = 180;
		while (i <= 0)
		{
			int num = safety;
			safety = num - 1;
			if (num <= 0)
			{
				break;
			}
			yield return null;
			i = SteamAvatarLoader._GetAvatarHandle(steamId, size);
		}
		if (i <= 0)
		{
			if (onDone != null)
			{
				onDone(fallback);
			}
			yield break;
		}
		Texture2D texture2D;
		if (!SteamAvatarLoader._TryBuildTexture(i, out texture2D))
		{
			if (onDone != null)
			{
				onDone(fallback);
			}
			yield break;
		}
		if (targetSize > 0 && (texture2D.width != targetSize || texture2D.height != targetSize))
		{
			Texture2D texture2D2 = SteamAvatarLoader._UpscaleBilinear(texture2D, targetSize, targetSize, true);
			Object.Destroy(texture2D);
			texture2D = texture2D2;
		}
		texture2D.filterMode = FilterMode.Bilinear;
		texture2D.anisoLevel = 1;
		Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 100f, 0U, SpriteMeshType.FullRect);
		SteamAvatarLoader._spriteCache[steamId.m_SteamID] = sprite;
		if (onDone != null)
		{
			onDone(sprite);
		}
		yield break;
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x00026944 File Offset: 0x00024B44
	public static IEnumerator LoadMyAvatarSprite(Action<Sprite> onDone, SteamAvatarLoader.AvatarSize size = SteamAvatarLoader.AvatarSize.Large, Sprite fallback = null, int targetSize = 0)
	{
		SteamManager steamManager = EntityStatic.Get<SteamManager>();
		if (steamManager == null || !steamManager.Initialized)
		{
			if (onDone != null)
			{
				onDone(fallback);
			}
			yield break;
		}
		yield return SteamAvatarLoader.LoadAvatarSprite(SteamUser.GetSteamID(), onDone, size, fallback, targetSize);
		yield break;
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x00026968 File Offset: 0x00024B68
	public static void ClearCache()
	{
		foreach (KeyValuePair<ulong, Sprite> keyValuePair in SteamAvatarLoader._spriteCache)
		{
			if (keyValuePair.Value != null)
			{
				if (keyValuePair.Value.texture != null)
				{
					Object.Destroy(keyValuePair.Value.texture);
				}
				Object.Destroy(keyValuePair.Value);
			}
		}
		SteamAvatarLoader._spriteCache.Clear();
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x00026A00 File Offset: 0x00024C00
	private static int _GetAvatarHandle(CSteamID id, SteamAvatarLoader.AvatarSize size)
	{
		int result;
		switch (size)
		{
		case SteamAvatarLoader.AvatarSize.Small:
			result = SteamFriends.GetSmallFriendAvatar(id);
			break;
		case SteamAvatarLoader.AvatarSize.Medium:
			result = SteamFriends.GetMediumFriendAvatar(id);
			break;
		case SteamAvatarLoader.AvatarSize.Large:
			result = SteamFriends.GetLargeFriendAvatar(id);
			break;
		default:
			result = SteamFriends.GetLargeFriendAvatar(id);
			break;
		}
		return result;
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x00026A44 File Offset: 0x00024C44
	private static bool _TryBuildTexture(int imageId, out Texture2D tex)
	{
		tex = null;
		uint num;
		uint num2;
		if (!SteamUtils.GetImageSize(imageId, out num, out num2) || num == 0U || num2 == 0U)
		{
			return false;
		}
		int num3 = (int)(num * 4U);
		int num4 = (int)((ulong)num2 * (ulong)((long)num3));
		byte[] array = new byte[num4];
		if (!SteamUtils.GetImageRGBA(imageId, array, num4))
		{
			return false;
		}
		byte[] array2 = new byte[num4];
		int num5 = 0;
		while ((long)num5 < (long)((ulong)num2))
		{
			int srcOffset = num5 * num3;
			int dstOffset = (int)((num2 - 1U - (uint)num5) * (uint)num3);
			Buffer.BlockCopy(array, srcOffset, array2, dstOffset, num3);
			num5++;
		}
		tex = new Texture2D((int)num, (int)num2, TextureFormat.RGBA32, false, false);
		tex.LoadRawTextureData(array2);
		tex.Apply(false, true);
		return true;
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x00026AE0 File Offset: 0x00024CE0
	private static Texture2D _UpscaleBilinear(Texture2D src, int width, int height, bool sRGB)
	{
		RenderTexture active = RenderTexture.active;
		RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGB32, sRGB ? RenderTextureReadWrite.sRGB : RenderTextureReadWrite.Linear);
		Graphics.Blit(src, temporary);
		Texture2D texture2D = new Texture2D(width, height, TextureFormat.RGBA32, false, !sRGB);
		RenderTexture.active = temporary;
		texture2D.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0, false);
		texture2D.Apply(false, true);
		RenderTexture.active = active;
		RenderTexture.ReleaseTemporary(temporary);
		return texture2D;
	}

	// Token: 0x0400092A RID: 2346
	private static readonly Dictionary<ulong, Sprite> _spriteCache = new Dictionary<ulong, Sprite>();

	// Token: 0x0200014A RID: 330
	public enum AvatarSize
	{
		// Token: 0x0400092C RID: 2348
		Small,
		// Token: 0x0400092D RID: 2349
		Medium,
		// Token: 0x0400092E RID: 2350
		Large
	}
}
