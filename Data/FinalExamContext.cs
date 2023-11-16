using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinalExam.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FinalExam.Data
{
    public class FinalExamContext : IdentityDbContext
    {
        public FinalExamContext (DbContextOptions<FinalExamContext> options)
            : base(options)
        {
        }

        public DbSet<FinalExam.Models.Course> Course { get; set; } = default!;

        public DbSet<FinalExam.Models.StudentProfile> StudentProfile { get; set; } = default!;
    }
}
