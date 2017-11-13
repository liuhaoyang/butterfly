using System.Collections.Generic;

namespace Butterfly.OpenTracing
{
    public class TagCollection : Dictionary<string, string>
    {
        /// <summary>
        /// The software package, framework, library, or module that generated the associated Span.
        /// E.g., "grpc", "django", "JDBI".
        /// </summary>
        public string Component { get => this[Tags.Component]; set => this[Tags.Component] = value; }

        /// <summary>
        /// Database instance name. E.g. the "Initial Catalog" value from a SQL connection string.
        /// </summary>
        public string DbInstance { get => this[Tags.DbInstance]; set => this[Tags.DbInstance] = value; }

        /// <summary>
        /// A database statement for the given database type.
        /// E.g., for db.type="sql", "SELECT * FROM wuser_table"; for db.type="redis", "SET mykey 'WuValue'".
        /// </summary>
        public string DbStatement { get => this[Tags.DbStatement]; set => this[Tags.DbStatement] = value; }

        /// <summary>
        /// Database type. For any SQL database, "sql". For others, the lower-case database category,
        /// e.g. "cassandra", "hbase", or "redis".
        /// </summary>
        public string DbType { get => this[Tags.DbType]; set => this[Tags.DbType] = value; }

        /// <summary>
        /// Username for accessing database. E.g., "readonly_user" or "reporting_user".
        /// </summary>
        public string DbUser { get => this[Tags.DbUser]; set => this[Tags.DbUser] = value; }

        /// <summary>
        /// <c>true</c> if and only if the application considers the operation represented by the Span to have failed.
        /// </summary>
        public string Error { get => this[Tags.Error]; set => this[Tags.Error] = value; }

        /// <summary>
        /// HTTP method of the request for the associated Span. E.g., "GET", "POST".
        /// </summary>
        public string HttpMethod { get => this[Tags.HttpMethod]; set => this[Tags.HttpMethod] = value; }

        /// <summary>
        /// HTTP response status code for the associated Span. E.g., 200, 503, 404.
        /// </summary>
        public string HttpStatusCode { get => this[Tags.HttpStatusCode]; set => this[Tags.HttpStatusCode] = value; }

        /// <summary>
        /// URL of the request being handled in this segment of the trace, in standard URI format.
        /// E.g., "https://domain.net/path/to?resource=here".
        /// </summary>
        public string HttpUrl { get => this[Tags.HttpUrl]; set => this[Tags.HttpUrl] = value; }

        /// <summary>
        /// An address at which messages can be exchanged. E.g. A Kafka record has an associated "topic name"
        /// that can be extracted by the instrumented producer or consumer and stored using this tag.
        /// </summary>
        public string MessageBusDestination { get => this[Tags.MessageBusDestination]; set => this[Tags.MessageBusDestination] = value; }

        /// <summary>
        /// Remote "address", suitable for use in a networking client library.
        /// This may be a "ip:port", a bare "hostname", a FQDN, or even a JDBC substring like "mysql://prod-db:3306".
        /// </summary>
        public string PeerAddress { get => this[Tags.PeerAddress]; set => this[Tags.PeerAddress] = value; }

        /// <summary>
        /// Remote hostname. E.g., "opentracing.io", "internal.dns.name".
        /// </summary>
        public string PeerHostname { get => this[Tags.PeerHostname]; set => this[Tags.PeerHostname] = value; }

        /// <summary>
        /// Remote IPv4 address as a .-separated tuple. E.g., "127.0.0.1".
        /// </summary>
        public string PeerIpV4 { get => this[Tags.PeerIpV4]; set => this[Tags.PeerIpV4] = value; }

        /// <summary>
        /// Remote IPv6 address as a string of colon-separated 4-char hex tuples.
        /// E.g., "2001:0db8:85a3:0000:0000:8a2e:0370:7334".
        /// </summary>
        public string PeerIpV6 { get => this[Tags.PeerIpV6]; set => this[Tags.PeerIpV6] = value; }

        /// <summary>
        /// Remote port. E.g., 80.
        /// </summary>
        public string PeerPort { get => this[Tags.PeerPort]; set => this[Tags.PeerPort] = value; }

        /// <summary>
        /// Remote service name (for some unspecified definition of "service").
        /// E.g., "elasticsearch", "a_custom_microservice", "memcache".
        /// </summary>
        public string PeerService { get => this[Tags.PeerService]; set => this[Tags.PeerService] = value; }

        /// <summary>
        /// If greater than 0, a hint to the Tracer to do its best to capture the trace.
        /// If 0, a hint to the trace to not-capture the trace.
        /// If absent, the Tracer should use its default sampling mechanism.
        /// </summary>
        public string SamplingPriority { get => this[Tags.SamplingPriority]; set => this[Tags.SamplingPriority] = value; }

        /// <summary>
        /// Either "client" or "server" for the appropriate roles in an RPC,
        /// and "producer" or "consumer" for the appropriate roles in a messaging scenario.
        /// </summary>
        public string SpanKind { get => this[Tags.SpanKind]; set => this[Tags.SpanKind] = value; }

        /// <summary>
        /// A constant for setting the "span.kind" to indicate that it represents a client span.
        /// </summary>
        public string SpanKindClient { get => this[Tags.SpanKindClient]; set => this[Tags.SpanKindClient] = value; }

        /// <summary>
        /// A constant for setting the "span.kind" to indicate that it represents a consumer span,
        /// in a messaging scenario.
        /// </summary>
        public string SpanKindConsumer { get => this[Tags.SpanKindConsumer]; set => this[Tags.SpanKindConsumer] = value; }

        /// <summary>
        /// A constant for setting the "span.kind" to indicate that it represents a producer span,
        /// in a messaging scenario.
        /// </summary>
        public string SpanKindProducer { get => this[Tags.SpanKindProducer]; set => this[Tags.SpanKindProducer] = value; }

        /// <summary>
        /// A constant for setting the "span.kind" to indicate that it represents a server span.
        /// </summary>
        public string SpanKindServer { get => this[Tags.SpanKindServer]; set => this[Tags.SpanKindServer] = value; }
    }
}