namespace LeafSQL.Engine
{
    public static class CriticalSections
    {
        public static object AcquireLock = new object();
    }
}
