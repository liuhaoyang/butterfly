using System.Collections;
using System.Collections.Generic;

namespace Butterfly.APM.Core.OpenTracing
{
    public sealed class ReadOnlyBaggage : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly IEnumerable<KeyValuePair<string, string>> _collection;

        internal ReadOnlyBaggage(Baggage baggage)
        {
            _collection = baggage;
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