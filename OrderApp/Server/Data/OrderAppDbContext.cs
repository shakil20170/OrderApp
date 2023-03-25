using Microsoft.EntityFrameworkCore;
using OrderApp.Server.Entity;
using System.Xml.Linq;

namespace OrderApp.Server.Data
{
    public class OrderAppDbContext : DbContext
    {
        public OrderAppDbContext(DbContextOptions<OrderAppDbContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //OrderTable
            modelBuilder.Entity<OrderTable>().HasData(new OrderTable
            {
                OrderId = 1,
                Name = "New York Building 1",
                State = "NY"
            });
            modelBuilder.Entity<OrderTable>().HasData(new OrderTable
            {
                OrderId = 2,
                Name = "California Hotel AJK",
                State = "CA"
            });


            //WindowTable
            modelBuilder.Entity<WindowTable>().HasData(new WindowTable
            {
                WindowID = 1,
                OrderTableOrderId = 1,
                WindowName = "A51",
                QuantityOfWindows = 4,
                TotalSubElements = 3,
            });
            modelBuilder.Entity<WindowTable>().HasData(new WindowTable
            {
                WindowID = 2,
                OrderTableOrderId = 1,
                WindowName = "C Zone 5",
                QuantityOfWindows = 2,
                TotalSubElements = 1,
            });
            modelBuilder.Entity<WindowTable>().HasData(new WindowTable
            {
                WindowID = 3,
                OrderTableOrderId = 2,
                WindowName = "GLB",
                QuantityOfWindows = 3,
                TotalSubElements = 2,
            });
            modelBuilder.Entity<WindowTable>().HasData(new WindowTable
            {
                WindowID = 4,
                OrderTableOrderId = 2,
                WindowName = "OHF",
                QuantityOfWindows = 10,
                TotalSubElements = 2,
            });

            // Seed data for SubElement
            modelBuilder.Entity<SubElementTable>().HasData(
                    // New York Building 1 - Window A51
                    new SubElementTable { SubElementID = 1, Element = 1, SubElementType = "Doors", Width = 1200, Height = 1850, WindowTableWindowID = 1 },
                    new SubElementTable { SubElementID = 2, Element = 2, SubElementType = "Window", Width = 800, Height = 1850, WindowTableWindowID = 1 },
                    new SubElementTable { SubElementID = 3, Element = 3, SubElementType = "Window", Width = 700, Height = 1850, WindowTableWindowID = 1 },

                    // New York Building 1 - Window C Zone 5             
                    new SubElementTable { SubElementID = 4, Element = 1, SubElementType = "Window", Width = 1500, Height = 2000, WindowTableWindowID = 2 },

                    // California Hotel AJK - Window GLB                 
                    new SubElementTable { SubElementID = 5, Element = 1, SubElementType = "Doors", Width = 1400, Height = 2200, WindowTableWindowID = 3 },
                    new SubElementTable { SubElementID = 6, Element = 2, SubElementType = "Window", Width = 600, Height = 2200, WindowTableWindowID = 3 },

                    // California Hotel AJK - Window OHF                 
                    new SubElementTable { SubElementID = 7, Element = 1, SubElementType = "Window", Width = 1500, Height = 2000, WindowTableWindowID = 4 },
                    new SubElementTable { SubElementID = 8, Element = 2, SubElementType = "Window", Width = 1500, Height = 2000, WindowTableWindowID = 4 }
                );

        }

        public DbSet<OrderTable> OrderTable { get; set; }
        public DbSet<WindowTable> WindowTable { get; set; }
        public DbSet<SubElementTable> SubElementTable { get; set; }

    }
}




