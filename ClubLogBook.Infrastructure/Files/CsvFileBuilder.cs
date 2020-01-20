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
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (TextWriter tw = new StreamWriter(memoryStream))
                using (CsvWriter csvWriter = new CsvWriter(tw, new System.Globalization.CultureInfo(1)))
                {


                    csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
                    csvWriter.WriteRecords(records);
                }
                return memoryStream.ToArray();
            }
        }


    }
}
