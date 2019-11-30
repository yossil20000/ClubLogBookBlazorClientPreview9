using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Application.TodoLists.Queries.ExportTodos;
using ClubLogBook.Infrastructure.Files.Maps;
using CsvHelper;
using System.Collections.Generic;
using System.IO;

namespace ClubLogBook.Infrastructure.Files
{
    public class CsvFileBuilder : ICsvFileBuilder
    {
        public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter);

                csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
                csvWriter.WriteRecords(records);
            }

            return memoryStream.ToArray();
        }
    }
}
