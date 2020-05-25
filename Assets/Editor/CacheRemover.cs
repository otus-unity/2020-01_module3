using UnityEngine;
using UnityEditor;


public class CacheRemover : EditorWindow
{
	[MenuItem("Cache/CacheRemover")]
	static void Init()
	{
		CacheRemover window = (CacheRemover)EditorWindow.GetWindow (typeof(CacheRemover));
	}

	void OnGUI ()
	{
		if(GUILayout.Button("Remove Cache"))
		{
			FileUtil.DeleteFileOrDirectory (Application.dataPath + "/cachedimage.png");
		}
	}
}
