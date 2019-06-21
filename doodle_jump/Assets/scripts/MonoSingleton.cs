
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour//这是一个单例类型，即一个类只能有一个实例
    where T : MonoBehaviour//此处用法：where T:xxx   泛例必须包含某种属性
{
    static T m_instance;

    public static T Instance {
        get {
            return m_instance;
        }
    }

    protected virtual void Awake()
    {
        m_instance = this as T;
    }

}