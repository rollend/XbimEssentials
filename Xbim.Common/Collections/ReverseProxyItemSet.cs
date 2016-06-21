﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Xbim.Common.Exceptions;

namespace Xbim.Common.Collections
{
    public class ReverseProxyItemSet<TInner, TOuter> : IItemSet<TOuter>, IDisposable, IList where TOuter :class, TInner
    {
        private readonly IItemSet<TInner> _inner;
        private IList List { get { return _inner as IList; } }

        public ReverseProxyItemSet(IItemSet<TInner> inner)
        {
            _inner = inner;
            if (List == null)
                throw new XbimException("Inner list has to implement IList");

            _inner.PropertyChanged += InnerOnPropertyChanged;
            _inner.CollectionChanged += InnerOnCollectionChanged;
        }

        private void InnerOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            OnCollectionChanged(notifyCollectionChangedEventArgs);
        }

        private void InnerOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OnPropertyChanged(propertyChangedEventArgs.PropertyName);
        }

        public IEnumerator<TOuter> GetEnumerator()
        {
            return new ReverseProxyEnumerator(_inner.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(TOuter item)
        {
            _inner.Add(item);
        }

        public int Add(object value)
        {
            return List.Add(value);
        }

        public bool Contains(object value)
        {
            return List.Contains(value);
        }

        void IList.Clear()
        {
            _inner.Clear();
        }

        public int IndexOf(object value)
        {
            return List.IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            List.Insert(index, value);
        }

        public void Remove(object value)
        {
            List.Remove(value);
        }

        void IList.RemoveAt(int index)
        {
            List.RemoveAt(index);
        }

        object IList.this[int index]
        {
            get { return List[index]; }
            set { List[index] = value; }
        }

        bool IList.IsReadOnly
        {
            get { return List.IsReadOnly; }
        }

        public bool IsFixedSize
        {
            get { return List.IsFixedSize; }
        }

        void ICollection<TOuter>.Clear()
        {
            _inner.Clear();
        }

        public bool Contains(TOuter item)
        {
            return _inner.Contains(item);
        }

        public void CopyTo(TOuter[] array, int arrayIndex)
        {
            var result = new TInner[array.Length];
            _inner.CopyTo(result, arrayIndex);
            for (var i = 0; i < array.Length; i++)
                array[i] = result[i] as TOuter;
        }

        public bool Remove(TOuter item)
        {
            return _inner.Remove(item);
        }

        public void CopyTo(Array array, int index)
        {
            List.CopyTo(array, index);
        }

        int ICollection.Count
        {
            get { return ((ICollection)_inner).Count; }
        }

        public object SyncRoot
        {
            get { return List.SyncRoot; }
        }

        public bool IsSynchronized
        {
            get { return List.IsSynchronized; }
        }

        int ICollection<TOuter>.Count
        {
            get { return ((ICollection)_inner).Count; }
        }

        bool ICollection<TOuter>.IsReadOnly
        {
            get { return List.IsReadOnly; }
        }

        public int IndexOf(TOuter item)
        {
            return _inner.IndexOf(item);
        }

        public void Insert(int index, TOuter item)
        {
            _inner.Insert(index, item);
        }

        void IList<TOuter>.RemoveAt(int index)
        {
            _inner.RemoveAt(index);
        }

        public TOuter this[int index]
        {
            get { return _inner[index] as TOuter; }
            set
            {
                _inner[index] = value;
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public IPersistEntity OwningEntity
        {
            get { return _inner.OwningEntity; }
        }

        public TOuter GetAt(int index)
        {
            return _inner.GetAt(index) as TOuter;
        }

        public void AddRange(IEnumerable<TOuter> values)
        {
            _inner.AddRange(values);
        }

        public TOuter First
        {
            get { return _inner.First as TOuter; }
        }

        public TOuter FirstOrDefault()
        {
            return _inner.FirstOrDefault() as TOuter;
        }

        public TOuter FirstOrDefault(Func<TOuter, bool> predicate)
        {
            return _inner.FirstOrDefault(predicate);
        }

        public TF FirstOrDefault<TF>(Func<TF, bool> predicate)
        {
            return _inner.FirstOrDefault(predicate);
        }

        public IEnumerable<TW> Where<TW>(Func<TW, bool> predicate)
        {
            return _inner.Where(predicate);
        }

        public IEnumerable<TO> OfType<TO>()
        {
            return _inner.OfType<TO>();
        }

        private bool _disposed;

        public void Dispose()
        {
            if (_disposed)
                return;

            _inner.PropertyChanged -= InnerOnPropertyChanged;
            _inner.CollectionChanged -= InnerOnCollectionChanged;
            _disposed = true;
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var handler = CollectionChanged;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        internal class ReverseProxyEnumerator: IEnumerator<TOuter>
        {
            private readonly IEnumerator<TInner> _inner;

            public ReverseProxyEnumerator(IEnumerator<TInner> inner)
            {
                _inner = inner;
            }

            public void Dispose()
            {
                _inner.Dispose();
            }

            public bool MoveNext()
            {
                return _inner.MoveNext();
            }

            public void Reset()
            {
                _inner.Reset();
            }

            public TOuter Current
            {
                get { return _inner.Current as TOuter; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }
        }
    }
}
