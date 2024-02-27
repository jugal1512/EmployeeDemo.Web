using EmployeeDemo.Domain.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemo.EF.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FirstName);
            builder.Property(e => e.LastName);
            builder.Property(e => e.Email);
            builder.Property(e => e.Gender);
            builder.Property(e => e.DOB);
            builder.Property(e => e.JoiningDate);
            builder.Property(e => e.Designation);
            builder.Property(e => e.image);
            builder.Property(e => e.Description);

            builder
                .HasMany(e => e.Skills)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
