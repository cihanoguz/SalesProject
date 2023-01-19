using System;
namespace Mobiliva.Business.Cache
{
    public interface ICacheManager
    {
        void Add(string key, object value, int duration);

        bool IsAdd(string key);

        T Get<T>(string key);

        object Get(string key);
        //byte[] Gett(string key);
        void Remove(string key);

    }


}

