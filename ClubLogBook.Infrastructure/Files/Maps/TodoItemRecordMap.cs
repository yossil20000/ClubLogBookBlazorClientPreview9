﻿using ClubLogBook.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;

namespace ClubLogBook.Infrastructure.Files.Maps
{
    public class TodoItemRecordMap : ClassMap<TodoItemRecord>
    {
        public TodoItemRecordMap()
        {
            //AutoMap()
            Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
        }
    }
}
