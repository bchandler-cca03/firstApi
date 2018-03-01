using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.Models;

namespace webApi.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)   // listen carefully to comment at 1:09 in video about "passing up"
        {

        }

        public DbSet<TodoItem> TodoItems { get; set; }

    }
}
