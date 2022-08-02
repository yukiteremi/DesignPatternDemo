using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singelton<T> where T:class,new()
{
    public static T ins;
    public static object obj = new object();
    
    public static T GetSingelton()
    {
        if (ins==null)
        {
            lock (obj)
            {
                if (ins==null)
                {
                    ins = new T();
                }
            }
        }
        return ins;
    }
}
