using System;
using System.IO;
using UnityEngine;

// Token: 0x020003CF RID: 975
public static class LocalWorkshopManifestLoader
{
	// Token: 0x0600165D RID: 5725 RVA: 0x0008B2C0 File Offset: 0x000894C0
	public static bool TryLoad(string itemRoot, out LocalWorkshopManifest manifest)
	{
		manifest = null;
		if (string.IsNullOrEmpty(itemRoot))
		{
			return false;
		}
		string path = Path.Combine(itemRoot, "manifest.json");
		if (!File.Exists(path))
		{
			return false;
		}
		manifest = JsonUtility.FromJson<LocalWorkshopManifest>(File.ReadAllText(path));
		return manifest != null;
	}

	// Token: 0x0600165E RID: 5726 RVA: 0x0008B304 File Offset: 0x00089504
	public static bool TryLoadEditorState(string itemRoot, out LocalWorkshopEditorStateInfo editorState)
	{
		editorState = null;
		if (string.IsNullOrEmpty(itemRoot))
		{
			return false;
		}
		string path = Path.Combine(itemRoot, "editor_state.json");
		if (!File.Exists(path))
		{
			return false;
		}
		editorState = JsonUtility.FromJson<LocalWorkshopEditorStateInfo>(File.ReadAllText(path));
		return editorState != null;
	}
}
