using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class DisplayManager : MonoBehaviour {
	[DllImport("user32.dll")]
	public static extern System.IntPtr FindWindowEx(IntPtr parent,IntPtr childe,string strclass,string strname);

	[DllImport("user32.dll")]
	public static extern bool MoveWindow(IntPtr hWnd,int x,int y,int width,int height,bool repaint);

	[DllImport("user32.dll")]
	public static extern int SetWindowLong(IntPtr hWnd,int nIndex,int dwNewLong);

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForEndOfFrame();
		//分辨率设置
		if(true)
		{
			if(true)
			{
				StartCoroutine("NonExclusiveFullScreen");
			}
			else
			{
				Screen.SetResolution(2048, 1024,true);
			}
		}
		else
		{
			Screen.SetResolution(2048, 1024,false);
		}
	}

	IEnumerator NonExclusiveFullScreen()
	{
        //首先设置分辨率
        //Screen.SetResolution(SystemParams.Instance().screenWidth, SystemParams.Instance().screenHeight,false);
        ////等待1秒,防止两个设置冲突
        //yield return new WaitForSeconds(1.0f);
        //IntPtr p=FindWindowEx(System.IntPtr.Zero,System.IntPtr.Zero,null, Application.productName);
        ////设置窗口无边框
        //SetWindowLong(p,-16,369164288);
        ////移动窗口到左上角，设置分辨率
        //MoveWindow(p,0,0, SystemParams.Instance().screenWidth, SystemParams.Instance().screenHeight,false);
        //SetWindowLong(p,-16,369164288);
        //MoveWindow(p,0,0, SystemParams.Instance().screenWidth, SystemParams.Instance().screenHeight,false);
        //SetWindowLong(p,-16,369164288);
        //MoveWindow(p,0,0, SystemParams.Instance().screenWidth, SystemParams.Instance().screenHeight,false);
        yield return null;
	}
	

}
