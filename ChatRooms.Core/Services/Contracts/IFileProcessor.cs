using System.Collections.Generic;
using System.IO;

namespace FirstReact.Core.Services.Contracts
{
    public interface IFileProcessor
    {
        Dictionary<string, List<string>> ProcessCsvFile(Stream stream);
    }
}
