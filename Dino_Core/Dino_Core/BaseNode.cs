using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Dino_Core
{
    /// <summary>
    /// 基本节点
    /// </summary>
    public class BaseNode
    {
        // 键
        public string Key { get; set; }

        // 值
        public string Value { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_value"></param>
        public BaseNode(string _key, string _value)
        {
            Key = _key;
            Value = _value;
        }
    }

}