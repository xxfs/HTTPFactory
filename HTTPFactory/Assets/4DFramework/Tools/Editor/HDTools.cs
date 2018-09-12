using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

///@brief 
///文件名称:HDTools
///功能描述:
///数据表:
///作者:梅超
///日期:2017-11-29 9:43:40
///R1:
///修改作者:
///修改日期:
///修改理由:
public class HDTools : Editor
{
    #region Rename Tools

    [CanEditMultipleObjects]
    [MenuItem("HuanDong/OtherTools/RenameChild %&D")]
    public static void RenameChildren()
    {
        GameObject[] targets = Selection.gameObjects;
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].transform.parent != null)
            {
                string prefixName = targets[i].transform.parent.GetChild(0).name.Remove(targets[i].transform.parent.GetChild(0).name.Length - 1);
                Undo.RecordObject(targets[i].gameObject, "Record GameObject Name");
                targets[i].gameObject.name =
                    string.Format("{0}{1}", prefixName,
                        targets[i].name == prefixName ? "1" : (targets[i].transform.GetSiblingIndex() + 1).ToString());
            }
        }
    }

    public static void OldRenameChildren()
    {
        GameObject[] targets = Selection.gameObjects;
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].transform.parent != null)
            {
                string prefixName = targets[i].transform.parent.GetChild(0).name;
                Undo.RecordObject(targets[i].gameObject, "Record GameObject Name");
                targets[i].gameObject.name =
                    string.Format("{0}{1}", prefixName,
                        targets[i].name == prefixName
                            ? string.Empty
                            : (targets[i].transform.GetSiblingIndex() + 1).ToString());
            }
        }
    }

    [CanEditMultipleObjects]
    [MenuItem("HuanDong/OtherTools/ShowSelectCount %&E")]
    public static void ShowSelectCount()
    {
        GameObject[] targets = Selection.gameObjects;
        EditorUtility.DisplayDialog("SmallTools", "您选中的物体总数为(只计算高亮物体):" + targets.Length, "OK");
    }

    #endregion

    #region Git Tools

    private static string localPath = Application.dataPath;

    [MenuItem("HuanDong/GitTools/EmptyDirClear")]
    [CanEditMultipleObjects]
    public static void OnEmptyDirClear()
    {
        DirectoryInfo dir = new DirectoryInfo(localPath);
        LookingForEmptyDir(dir);
        Debug.Log("执行完毕！");
        AssetDatabase.Refresh();
    }

    private static void LookingForEmptyDir(DirectoryInfo dir)
    {
        foreach (DirectoryInfo dChild in dir.GetDirectories("*"))
        {
            LookingForEmptyDir(dChild);
        }
        if (dir.GetDirectories("*").Length == 0 && dir.GetFiles("*").Length == 0)
        {
            Debug.Log("删除空文件夹:" + dir.FullName);
            dir.Delete();
            AssetDatabase.Refresh();
        }
    }
    #endregion

    #region Point Tools
    [MenuItem("HuanDong/PointTools/PlacePointsOnGround %G")]
    static void PlaneTransformOnGround()
    {
        var transforms = Selection.transforms;

        foreach (var transform in transforms)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                Undo.RecordObject(transform, "New Postion");
                if (hit.transform != null) transform.position = hit.point + Vector3.up * 0.1f;
            }
        }
    }
    #endregion

    [MenuItem("HuanDong/GunTools/CreateGun")]
    public static void CreateGun()
    {
        
    }

}
