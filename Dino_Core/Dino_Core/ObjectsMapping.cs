using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

namespace Dino_Core
{

    public static class ObjectsMapping
    {
        private static Dictionary<string, Dictionary<string, string>> m_GameResourceList = new Dictionary<string, Dictionary<string, string>>();
        public static readonly string m_ResourcePath = Application.streamingAssetsPath + "/Pref_Data/";
        public static readonly string m_RootNode = "Root";

        /// <summary>
        /// 初始化初始化游戏物体数据列表
        /// </summary>
        public static void Init()
        {
            // 清空原有表项
            m_GameResourceList.Clear();

            // 得到目录文件夹下所有的文件.xml
            // 每一个XML文件对应一个表单字典
            DirectoryInfo _folder = new DirectoryInfo(m_ResourcePath);
            foreach (FileInfo _file in _folder.GetFiles("*.xml"))
            {
                // 添加子表项
                Dictionary<string, string> _childList = new Dictionary<string, string>();
                ArrayList _list = XmlHandler.ReadNodesFromFile(m_ResourcePath + _file.Name, m_RootNode);
                foreach (BaseNode _be in _list)
                {
                    if (!_childList.ContainsKey(_be.Key))
                        _childList.Add(_be.Key, _be.Value);
                    else
                        Debug.Log("重名表单项：" + _be.Key);
                }
                // 如果总表不包含子表项，则添加进总表
                if (!m_GameResourceList.ContainsKey(_file.Name))
                    m_GameResourceList.Add(_file.Name.Remove(_file.Name.LastIndexOf(".")), _childList);
                else
                    Debug.Log("重名表单：" + _file.Name);
            }
            int i = 0;
            i++;
        }

        /// <summary>
        /// 得到数据
        /// </summary>
        /// <param name="_childForm"></param>
        /// <param name="_itemName"></param>
        public static string GetPrefData(string _formName, string _itemName)
        {
            if (m_GameResourceList.ContainsKey(_formName))
                if (m_GameResourceList[_formName].ContainsKey(_itemName)) return m_GameResourceList[_formName][_itemName];
                else Debug.Log("没有找到表项" + _itemName);
            else Debug.Log("没有找到表单" + _formName);
            return "";
        }
    }
}