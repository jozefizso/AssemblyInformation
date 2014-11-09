using System;

namespace AssemblyInformation
{
    internal class ReferringAssemblyStatusChangeEventArgs : EventArgs
    {
        public bool Cancel { get; set; }

        public int Progress { get; set; }

        public string StatusText { get; set; }
    }
}
