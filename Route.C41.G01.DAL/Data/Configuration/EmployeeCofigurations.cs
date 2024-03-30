using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.C41.G01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G01.DAL.Data.Configuration
{
    internal class EmployeeCofigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Fluent APIs for "Emplyee" Model

            builder.Property(E => E.Name).HasColumnType("varchar")
                                        .HasMaxLength(50)
                                        .IsRequired();

            builder.Property(E => E.Address).IsRequired();

            builder.Property(E => E.Salary).HasColumnType("decimal(12,2)");

            builder.Property(E => E.Gender)
                   .HasConversion(
                (Gender) => Gender.ToString(), // Sotred to DataBase
                (genderAsString) => (Gender)Enum.Parse(typeof(Gender), genderAsString, true));

            builder.Property(E => E.EmployeeType)
                   .HasConversion(
                (EmpType) => EmpType.ToString(),
                (EmpTypeAsString) => (EmpType)Enum.Parse(typeof(EmpType), EmpTypeAsString, true));

            builder.Property(E => E.Name).IsRequired().HasMaxLength(50);
        }
    }
}
