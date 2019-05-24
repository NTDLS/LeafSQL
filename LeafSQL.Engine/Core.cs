using LeafSQL.Engine.Caching;
using LeafSQL.Engine.Documents;
using LeafSQL.Engine.Health;
using LeafSQL.Engine.Indexes;
using LeafSQL.Engine.IO;
using LeafSQL.Engine.Locking;
using LeafSQL.Engine.Logging;
using LeafSQL.Engine.Query;
using LeafSQL.Engine.Schemas;
using LeafSQL.Engine.Sessions;
using LeafSQL.Engine.Transactions;
using System.Diagnostics;
using System.Reflection;

namespace LeafSQL.Engine
{
    public class Core
    {
        public SchemaManager Schemas { get; set; }
        public IOManager IO { get; set; }
        public LockManager Locking { get; set; }
        public DocumentManager Documents { get; set; }
        public TransactionManager Transactions { get; set; }
        public Library.Payloads.ServerSettings Settings { get; set; }
        public LogManager Log { get; set; }
        public HealthManager Health { get; set; }
        public SecurityManager Security { get; set; }
        public SessionManager Sessions { get; set; }
        public CacheManager Cache { get; set; }
        public PersistIndexManager Indexes { get; set; }
        public QueryManager Query { get; set; }

        public Core(Library.Payloads.ServerSettings settings)
        {
            this.Settings = settings;

            Log = new LogManager(this);

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            Log.Write(string.Format("{0} v{1} PID:{2}",
                fileVersionInfo.ProductName,
                fileVersionInfo.ProductVersion,
                Process.GetCurrentProcess().Id));

            Log.Write("Initializing cache manager.");
            Cache = new CacheManager(this);

            Log.Write("Initializing IO manager.");
            IO = new IOManager(this);

            Log.Write("Initializing Security manager.");
            Security = new SecurityManager(this);

            Log.Write("Initializing health manager.");
            Health = new HealthManager(this);

            Log.Write("Initializing index manager.");
            Indexes = new PersistIndexManager(this);

            Log.Write("Initializing session manager.");
            Sessions = new SessionManager(this);

            Log.Write("Initializing lock manager.");
            Locking = new LockManager(this);

            Log.Write("Initializing transaction manager.");
            Transactions = new TransactionManager(this);

            Log.Write("Initializing namespace manager.");
            Schemas = new SchemaManager(this);

            Log.Write("Initializing document manager.");
            Documents = new DocumentManager(this);

            Log.Write("Initializing query manager.");
            Query = new QueryManager(this);

            Log.Write("Initilization complete.");
        }

        public void Start()
        {
            Log.Write("Starting server.");

            Transactions.Recover();
        }

        public void Shutdown()
        {
            Log.Write("Shutting down server.");

            Health.Close();
            Log.Close();
        }
    }
}
