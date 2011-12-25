using System;
using System.Collections.Generic;
using System.Text;

namespace TableGenerator
{
    public class cSet<T>:Dictionary<T, T>, IEnumerable<T>
    {
        public bool Add(T a_item)
        {
            if (!ContainsKey(a_item))
            {
                base.Add(a_item, a_item);
                return true;
            }
            else
                return false;
        }

        public bool AddRange(IEnumerable<T> a_items)
        {
            bool _flag = false;
            foreach(T _item in a_items)
                _flag = Add(_item) | _flag;
            return _flag;
        }

        public new IEnumerator<T> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        public T[] ToArray()
        {
            T[] _retArr = new T[Count];
            int i = 0;
            foreach (T _item in this)
                _retArr[i++] = _item;
            return _retArr;
        }

        public bool ContainsAny(IEnumerable<T> a_items)
        {
            foreach (T _item in a_items)
            {
                if (ContainsKey(_item))
                    return true;
            }
            return false;
        }

    }
}
