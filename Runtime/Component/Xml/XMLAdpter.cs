using Cat.Extend;
using Cat.Logger;
using Cat.Scada.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using UnityEngine;

namespace Cat.XMLExtend
{
    /// <summary>
    /// xml解析器
    /// 1、只解析属性，不解析字段
    /// 2、父类中的属性，不能是private，最低是protect
    /// 3、必须有空构造
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XMLAdpter<T> where T : new()
    {
        string _filePath;
        string _xmlNavigation;
        /// <summary>
        /// XML解析器
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="xmlParentNavigation">需要解的数组的父节点</param>
        public XMLAdpter(string filePath, string xmlParentNavigation = "")
        {
            _filePath = filePath;
            _xmlNavigation = xmlParentNavigation;
        }
        /// <summary>
        /// 解析
        /// </summary>
        /// <returns></returns>
        public List<T> Resolve()
        {
            /*
            //打开xml文件
            //如果有定位，找到定位
            //开始读xml节点的属性，
            //读到一个属性就和T比较，如果T里面有属性名称和xml节点的属性名相同，则赋值
            //赋值完成后加入到ts
            */
            if (string.IsNullOrEmpty(_filePath))
                throw new System.ArgumentNullException(nameof(_filePath));
            if (!File.Exists(_filePath))
                throw new System.Exception("文件不存在" + _filePath);

            XmlDocument doc = new XmlDocument();
            doc.Load(_filePath);
            XmlNode root;
            if (string.IsNullOrEmpty(_xmlNavigation))//没导航，就直接第一个
                root = doc.FirstChild;
            else
                root = doc.SelectSingleNode(_xmlNavigation);//有导航找导航
            XmlNodeList tsNodes = root.ChildNodes;
            if (tsNodes.Count < 1)
                return null;
            List<T> ts = new List<T>();

            //遍历xml节点
            foreach (XmlNode node in tsNodes)
            {
                XmlAttributeCollection attributeCollection = node.Attributes;
                T t = new T();
                Type type = t.GetType();
                List<PropertyInfo> infos = type.GetProperties().ToList();
                int missCount = 0;
                //遍历一个节点的属性
                foreach (XmlAttribute attribute in attributeCollection)
                {
                    //查找xml节点属性值和类属性值相同的
                    PropertyInfo info = infos.Where(p => p.Name == attribute.Name).FirstOrDefault();
                    //xml中有，类中也有同名属性，尝试赋值
                    if (info != null)
                    {
                        object value = null;
                        Type valueType = info.PropertyType;
                        //object v = info.GetValue(t);
                        //Type valueType = v.GetType();
                        //尝试将xml中的文本按照对应的数据结构转化
                        try
                        {
                            if (valueType == typeof(float))
                            {
                                value = float.Parse(attribute.Value);
                            }
                            else if(valueType == typeof(double))
                            {
                                value = double.Parse(attribute.Value);
                            }
                            else if(valueType == typeof(short))
                            {
                                value = short.Parse(attribute.Value);
                            }
                            else if (valueType == typeof(int))
                            {
                                value = int.Parse(attribute.Value);
                            }
                            else if (valueType == typeof(long))
                            {
                                value = long.Parse(attribute.Value);
                            }
                            else if (valueType == typeof(string))
                            {
                                value = attribute.Value;
                            }
                            else if(valueType == typeof(Guid))
                            {
                                value = Guid.Parse(attribute.Value);
                            }
                        }
                        catch (System.FormatException)
                        {
                            throw new FormatException("XML中属性" + attribute.Name + "的值为[" + attribute.Value + "]," +
                                "但" + type.Name + "类中的类型为[" + valueType + "],无法进行转化");
                        }

                        if (value is null)
                            throw new NotImplementedException("匹配到" + attribute.Name + ",但它的类型是 : " + valueType.Name + ",还未实现该中类型的转化");

                        //在内存中给t对象赋值
                        info.SetValue(t, value);
                        //Debug.Log("匹配到" + attribute.Name);
                    }
                    //xml中有，但是类中没有，就记个数
                    else
                    {
                        missCount++;
                    }
                    //Debug.Log(t.ToString());
                }
                if(attributeCollection.Count == missCount)//如果xml中的属性值，一个都没和类属性匹配上，认为这次是失败的，不加入返回的list中
                {
                    continue;
                }
                ts.Add(t);
            }
            //Debug.Log("解析完成,数量 : "+ ts.Count);
            return ts;
        }
        /// <summary>
        /// 获得某个单节点的描述
        /// </summary>
        /// <returns></returns>
        public string GetInnerText()
        {
            if (string.IsNullOrEmpty(_filePath))
                throw new System.ArgumentNullException(nameof(_filePath));
            if (!File.Exists(_filePath))
                throw new System.Exception("文件不存在" + _filePath);

            XmlDocument doc = new XmlDocument();
            doc.Load(_filePath);
            XmlNode node;
            if (string.IsNullOrEmpty(_xmlNavigation))//没导航，就直接第一个
                node = doc.FirstChild;
            else
                node = doc.SelectSingleNode(_xmlNavigation);//有导航找导航
            if (node == null)
                throw new Exception("xml中无此节点");
            return node.InnerText;
        }
        /// <summary>
        /// 写入某个单节点的描述
        /// </summary>
        /// <param name="value"></param>
        public void SetInnerText(string value)
        {
            if (string.IsNullOrEmpty(_filePath))
                throw new System.ArgumentNullException(nameof(_filePath));
            if (!File.Exists(_filePath))
                throw new System.Exception("文件不存在" + _filePath);

            XmlDocument doc = new XmlDocument();
            doc.Load(_filePath);
            XmlNode node;
            if (string.IsNullOrEmpty(_xmlNavigation))//没导航，就直接第一个
                node = doc.FirstChild;
            else
                node = doc.SelectSingleNode(_xmlNavigation);//有导航找导航
            if (node == null)
                throw new Exception("xml中无此节点");
            node.InnerText = value;
            doc.Save(_filePath);
        }
    }
}

