using System;
using System.Collections;
using System.Collections.Generic;

namespace Butterfly.APM.Core.OpenTracing
{
    public sealed class ReadOnlyBaggage : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly IEnumerable<KeyValuePair<string, string>> _collection;

        internal ReadOnlyBaggage(IEnumerable<KeyValuePair<string, string>> collection)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
}