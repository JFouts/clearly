namespace DomainModeling.Crud.WebUi;

public static class SynchronizationHelper
{
    public static void CriticalInitializer<T>(object @lock, ref T? variable, Func<T> initializer)
    {
        if (variable == null)
        {
            lock(@lock)
            {
                if (variable == null)
                {
                    variable = initializer();
                }
            }
        }
    }
}