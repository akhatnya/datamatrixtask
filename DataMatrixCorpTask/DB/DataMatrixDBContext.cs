using DataMatrixCorpTask.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DataMatrixCorpTask.DB;

public class DataMatrixDbContext : DbContext
{
    public DataMatrixDbContext(DbContextOptions<DataMatrixDbContext> options) : base(options)
    {
         
    }
    
    public DbSet<Contact> Contacts { get; set; }
}