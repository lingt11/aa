using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using UnityEngine;

// Token: 0x0200004B RID: 75
public class ExcelManager
{
	// Token: 0x06000149 RID: 329 RVA: 0x000087CD File Offset: 0x000069CD
	public ExcelManager()
	{
		this.LoadAllData();
	}

	// Token: 0x0600014A RID: 330 RVA: 0x000087DB File Offset: 0x000069DB
	public object GetData(string tableName)
	{
		return ExcelManager.allExcelData[tableName];
	}

	// Token: 0x0600014B RID: 331 RVA: 0x000087E8 File Offset: 0x000069E8
	private void LoadAllData()
	{
		ExcelManager.allExcelData.Clear();
		if (Launch.GameMode == GameMode.Develop)
		{
			this.LoadExcelData(Path.Combine(Application.dataPath, ".Excel"));
		}
		else
		{
			this.LoadExcelConfig();
		}
		GC.Collect();
	}

	// Token: 0x0600014C RID: 332 RVA: 0x00008820 File Offset: 0x00006A20
	private void LoadExcelConfig()
	{
		foreach (string name in Resources.Load<SOExcelConfig>("Bundles/SO/SOExcelConfig").configs)
		{
			this.LoadAbExcel(name);
		}
	}

	// Token: 0x0600014D RID: 333 RVA: 0x0000887C File Offset: 0x00006A7C
	private void LoadAbExcel(string name)
	{
		byte[] bytes = Resources.Load<TextAsset>("Bundles/" + name).bytes;
		MemoryStream memoryStream = new MemoryStream();
		memoryStream.Write(bytes, 0, bytes.Length);
		memoryStream.Seek(0L, SeekOrigin.Begin);
		this.LoadStream(memoryStream);
	}

	// Token: 0x0600014E RID: 334 RVA: 0x000088C4 File Offset: 0x00006AC4
	private void LoadExcelData(string path)
	{
		foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles())
		{
			if (fileInfo.FullName.EndsWith("xlsx") && !fileInfo.Name.StartsWith("~"))
			{
				this.GameReadExcel(fileInfo);
			}
		}
	}

	// Token: 0x0600014F RID: 335 RVA: 0x0000891C File Offset: 0x00006B1C
	private void GameReadExcel(FileInfo file)
	{
		FileStream fileStream = file.Open(FileMode.Open, FileAccess.Read);
		foreach (object obj in ExcelReaderFactory.CreateOpenXmlReader(fileStream, null).AsDataSet(null).Tables)
		{
			DataTable exceltable = (DataTable)obj;
			this.ReadOneTable(exceltable);
		}
		fileStream.Close();
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00008990 File Offset: 0x00006B90
	public void LoadStream(MemoryStream stream)
	{
		foreach (object obj in ExcelReaderFactory.CreateOpenXmlReader(stream, null).AsDataSet(null).Tables)
		{
			DataTable exceltable = (DataTable)obj;
			this.ReadOneTable(exceltable);
		}
	}

	// Token: 0x06000151 RID: 337 RVA: 0x000089F8 File Offset: 0x00006BF8
	private void ReadOneTable(DataTable _exceltable)
	{
		if (_exceltable.TableName.StartsWith("_"))
		{
			return;
		}
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		List<string> list = new List<string>();
		int count = _exceltable.Columns.Count;
		int count2 = _exceltable.Rows.Count;
		List<string> list2 = new List<string>();
		int num = 0;
		for (int i = 0; i < count2; i++)
		{
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			string text = "";
			for (int j = 0; j < count; j++)
			{
				object obj = _exceltable.Rows[i][j];
				if (i == 0)
				{
					string text2 = obj.ToString();
					if (text2 == "id")
					{
						num = j;
					}
					list.Add(text2);
				}
				else if (i != 1)
				{
					if (i == 2)
					{
						list2.Add(obj.ToString());
					}
					else if (i != 3 && i != 4 && !string.IsNullOrEmpty(list[j]))
					{
						if (obj == DBNull.Value)
						{
							obj = list2[j];
						}
						dictionary2.Add(list[j], obj);
					}
				}
				if (i >= ExcelManager.notDataLines && j == num)
				{
					text = obj.ToString();
				}
			}
			if (i >= ExcelManager.notDataLines && !string.IsNullOrEmpty(text))
			{
				if (dictionary.ContainsKey(text))
				{
					Debug.LogError("sss" + text);
				}
				dictionary.Add(text, dictionary2);
			}
		}
		ExcelManager.allExcelData.Add(_exceltable.TableName, dictionary);
	}

	// Token: 0x0400016E RID: 366
	public static Dictionary<string, object> allExcelData = new Dictionary<string, object>();

	// Token: 0x0400016F RID: 367
	private static int notDataLines = 5;
}
