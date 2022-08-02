using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageCenter :Singelton<MessageCenter>
{
    Dictionary<int, Action<object>> dic = new Dictionary<int, Action<object>>();

    public void OnAddListen(int id, Action<object> obj)
    {
        if (dic.ContainsKey(id))
        {
            dic[id] += obj;
        }
        else
        {
            dic.Add(id, obj);
        }
    }
    public void RemoveAllListen(int id)
    {
        if (dic.ContainsKey(id))
        {
            dic.Remove(id);
        }
    }
    public void RemoveListen(int id, Action<object> obj)
    {
        if (dic.ContainsKey(id))
        {
            dic[id] -= obj;
            if (dic[id] == null)
            {
                dic.Remove(id);
            }
        }
    }

    public void OnDisPatch(int id, params object[] obj)
    {
        if (dic.ContainsKey(id))
        {
            dic[id](obj);
        }
        else
        {
            Debug.Log("消息" + id + "未注册");
        }
    }
}
