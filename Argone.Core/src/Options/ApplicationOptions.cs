namespace Argone.Core.Options
{
    using System.Collections.Generic;

    public class ApplicationOptions
    {
        public IEnumerable<WindowOptions> Windows { get; set; } = new List<WindowOptions>();
    }
}