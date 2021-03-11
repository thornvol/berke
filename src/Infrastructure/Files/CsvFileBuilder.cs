using System.Collections.Generic;
using System.Globalization;
using System.IO;
using BerkeGaming.Application.Common.Interfaces;
using BerkeGaming.Application.TodoLists.Queries.ExportTodos;
using BerkeGaming.Infrastructure.Files.Maps;
using CsvHelper;

namespace BerkeGaming.Infrastructure.Files
{
    public class CsvFileBuilder : ICsvFileBuilder
    {
        public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

                csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
                csvWriter.WriteRecords(records);
            }

            return memoryStream.ToArray();
        }
    }
}
