using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Adalon.Transliterators.Internal
{
    internal struct FrugalLocalList<T> : IEnumerable<T>
    {
        internal static readonly List<T> Sentinel = new List<T>();

        private T _value;
        private List<T> _list;

        public FrugalLocalListEnumerator<T> GetEnumerator()
        {
            return new FrugalLocalListEnumerator<T>(_value, _list);
        }

        public int Count
        {
#if SINCE461
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get
            {
                if (_list == Sentinel) return 1;
                return _list?.Count ?? 0;
            }
        }

        public List<T> ListRef => _list;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            switch (Count)
            {
                case 0:
                    _value = item;
                    _list = Sentinel;
                    break;
                case 1:
                    _list = new List<T> { _value, item };
                    _value = default(T);
                    break;
                default:
                    _list.Add(item);
                    break;
            }
        }

        public T this[int index]
        {
            get
            {
                if (_list == Sentinel)
                {
                    if (index == 0) return _value;
                    throw new IndexOutOfRangeException();
                }
                if (_list != null) return _list[index];
                throw new IndexOutOfRangeException();
            }
        }
    }

    public struct FrugalLocalListEnumerator<T> : IEnumerator<T>
    {
        private readonly T _value;
        private readonly List<T> _list;
        private int _idx;

        internal FrugalLocalListEnumerator(T value, List<T> list)
        {
            _value = value;
            _list = list;
            _idx = -1;
            Current = default(T);
        }

        object IEnumerator.Current => Current;

        public T Current { get; private set; }

        public bool MoveNext()
        {
            if (_list == null)
                return false;

            _idx++;
            if (_list == FrugalLocalList<T>.Sentinel)
            {
                if (_idx == 0)
                {
                    Current = _value;
                    return true;
                }
            }
            else
            {
                if (_idx < _list.Count)
                {
                    Current = _list[_idx];
                    return true;
                }
            }

            Current = default(T);
            return false;
        }

        public void Reset()
        {
            _idx = -1;
            Current = default(T);
        }

        public void Dispose()
        {
            // do nothing
        }
    }
}
