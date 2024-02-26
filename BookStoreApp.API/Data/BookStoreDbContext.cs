using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Data;

public partial class BookStoreDbContext : IdentityDbContext<ApiUser>
{
    public BookStoreDbContext()
    {
    }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
                Id = "1514ed7e-2074-4e14-b2bc-a5827ade00eb",
            },
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = "067ce6cb-d8ba-4260-a6ab-e37a11ecee5d"
            }
        );

        var hasher = new PasswordHasher<ApiUser>();

        modelBuilder.Entity<ApiUser>().HasData(
            new ApiUser
            {
                Email = "john.doe@gmail.com",
                NormalizedEmail = "JOHN.DOE@GMAIL.COM",
                Id = "4063871d-9c55-47b5-93e7-22c4c92d2bae",
                UserName = "john.doe@gmail.com",
                NormalizedUserName = "JOHN.DOE@GMAIL.COM",
                FirstName = "John",
                LastName = "Doe",
                PasswordHash = hasher.HashPassword(null!, "Exemple@123")
            },
            new ApiUser
            {
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                Id = "2c155adc-7584-40ba-a57d-99872511460e",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                FirstName = "System",
                LastName = "Admin",
                PasswordHash = hasher.HashPassword(null!, "Exemple@123")
            }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { RoleId = "1514ed7e-2074-4e14-b2bc-a5827ade00eb", UserId = "4063871d-9c55-47b5-93e7-22c4c92d2bae" },
            new IdentityUserRole<string> { RoleId = "067ce6cb-d8ba-4260-a6ab-e37a11ecee5d", UserId = "2c155adc-7584-40ba-a57d-99872511460e" }
            );
    }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }

}
