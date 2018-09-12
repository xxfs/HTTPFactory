using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
public class DogManager : MonoBehaviour {
	[DllImport("DogDLL.dll")]
	public static extern int CheckDog();

	void Awake()
	{
		try{
			CheckDog();
		}
        catch (System.Exception e)
        {
           System.Windows.Forms.MessageBox.Show("找不到对应的加密狗，程序退出！");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Debug.Log(e.Message);
            Application.Quit();
        }
    }
    
}
