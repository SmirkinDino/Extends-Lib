using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;


namespace Dino_Core
{

    /// <summary>
    /// 封装 XML 操作类
    /// </summary>
    public static class XmlHandler
    {
        /// <summary>
        /// 创建 xml 文件并添加内容到指定节点
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public static bool AddNodesToFile(string _path, string _rootNodeName, BaseNode[] _nodeList)
        {
            XmlDocument _xmlDoc = new XmlDocument();
            if (!File.Exists(_path))
            {
                try
                {
                    // 创建根节点
                    XmlElement _rootNode = _xmlDoc.CreateElement("Data-List");
                    _xmlDoc.AppendChild(_rootNode);
                    // 创建用户所要求的父节点
                    XmlElement _userRootNode = _xmlDoc.CreateElement(_rootNodeName);
                    _rootNode.AppendChild(_userRootNode);

                    for (int i = 0; i < _nodeList.Length; i++)
                        AddChildtoNode(_xmlDoc, _userRootNode, _nodeList[i]);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    return false;
                }
            }
            else
            {
                _xmlDoc.Load(_path);
                XmlElement _userRootNode = _xmlDoc.SelectSingleNode("Data-List/" + _rootNodeName) as XmlElement;
                for (int i = 0; i < _nodeList.Length; i++)
                    AddChildtoNode(_xmlDoc, _userRootNode, _nodeList[i]);
            }
            _xmlDoc.Save(_path);
            return true;
        }

        /// <summary>
        /// 读取信息从指定的 xml 文件节点
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_rootNodeName"></param>
        /// <returns></returns>
        public static ArrayList ReadNodesFromFile(string _path, string _rootNodeName)
        {
            ArrayList _resultList = new ArrayList();

            if (!File.Exists(_path))
            {
                return _resultList;
            }

            XmlDocument _xmlDoc = new XmlDocument();
            _xmlDoc.Load(_path);

            XmlNodeList _nodeList = _xmlDoc.SelectSingleNode("Data-List/" + _rootNodeName).ChildNodes;
            foreach (XmlNode _node in _nodeList)
            {
                BaseNode _xmlNode = new BaseNode(_node.Name, _node.InnerText);
                _resultList.Add(_xmlNode);
            }

            return _resultList;
        }
        public static BaseNode ReadNodeFromFile(string _path, string _rootNodeName)
        {
            if (!File.Exists(_path))
            {
                return null;
            }

            XmlDocument _xmlDoc = new XmlDocument();
            _xmlDoc.Load(_path);

            XmlNode _node = _xmlDoc.SelectSingleNode("Data-List/" + _rootNodeName);

            if (_node != null)
                return new BaseNode(_node.Name, _node.InnerText);
            else
                return null;
        }

        /// <summary>
        /// 添加节点到指定节点
        /// </summary>
        /// <param name="_targetDoc"></param>
        /// <param name="_targetNode"></param>
        /// <param name="_name"></param>
        /// <param name="_content"></param>
        private static void AddChildtoNode(XmlDocument _targetDoc, XmlElement _targetNode, BaseNode _node)
        {
            XmlElement _element = _targetDoc.CreateElement(_node.Key);
            _element.InnerText = _node.Value;
            _targetNode.AppendChild(_element);
        }

    }

}