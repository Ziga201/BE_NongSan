﻿using Microsoft.EntityFrameworkCore;
using ThucTap.Entities;

namespace ThucTap.Context
{
    public class AppDbContext:DbContext
    {
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<CartItem> CartItem { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<ConfirmEmail> ConfirmEmail { get; set; }
        public virtual DbSet<Decentralization> Decentralization { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }
        public virtual DbSet<ProductReview> ProductReview { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<BlogType> BlogType { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer($"Server = ZIGA\\SQLEXPRESS; Database = QLBanNongSan; Trusted_Connection = True; " +
            //    $"TrustServerCertificate=True");            
            optionsBuilder.UseSqlServer($"Data Source=SQL5112.site4now.net,1433;Initial Catalog=db_aa423c_login;User Id=db_aa423c_login_admin;Password=Nametung1!;");
        }
    }
}
