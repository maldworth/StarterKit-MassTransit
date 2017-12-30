namespace StarterKit.Contracts
{
    using System.Collections.Generic;

    public interface DoSomeWork
    {
        string Data1 { get; }
        int Data2 { get; }
        IEnumerable<string> Data3 { get; }
    }
}
