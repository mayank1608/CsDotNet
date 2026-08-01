using EventManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<OrganizeEvent> OrganizeEvents { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -------------------------------
            // Table Relationships
            // -------------------------------

            modelBuilder.Entity<OrganizeEvent>()
                .HasOne(oe => oe.Event)
                .WithMany(e => e.OrganizeEvents)
                .HasForeignKey(oe => oe.EventID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrganizeEvent>()
                .HasOne(oe => oe.Organizer)
                .WithMany(o => o.OrganizeEvents)
                .HasForeignKey(oe => oe.OrganizerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Member>()
                .HasOne(m => m.Event)
                .WithMany(e => e.Members)
                .HasForeignKey(m => m.EventID)
                .OnDelete(DeleteBehavior.Restrict);

            // -------------------------------
            // Seed tbl_Organizer
            // -------------------------------

            modelBuilder.Entity<Organizer>().HasData(
                new Organizer
                {
                    OrganizerID = 101,
                    UserName = "Organizer1",
                    OrganizerPwd = "pass1"
                },
                new Organizer
                {
                    OrganizerID = 102,
                    UserName = "Organizer2",
                    OrganizerPwd = "pass2"
                },
                new Organizer
                {
                    OrganizerID = 103,
                    UserName = "Organizer3",
                    OrganizerPwd = "pass3"
                },
                new Organizer
                {
                    OrganizerID = 104,
                    UserName = "Organizer4",
                    OrganizerPwd = "pass4"
                },
                new Organizer
                {
                    OrganizerID = 105,
                    UserName = "Organizer5",
                    OrganizerPwd = "pass5"
                }
            );

            // -------------------------------
            // Seed tbl_Events
            // -------------------------------

            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    EventID = 1001,
                    EventType = "Conference"
                },
                new Event
                {
                    EventID = 1002,
                    EventType = "Seminar"
                },
                new Event
                {
                    EventID = 1003,
                    EventType = "Workshop"
                },
                new Event
                {
                    EventID = 1004,
                    EventType = "Awards and Competition"
                }
            );

            // -------------------------------
            // Seed tbl_OrganizeEvent
            // -------------------------------

            modelBuilder.Entity<OrganizeEvent>().HasData(
                new OrganizeEvent
                {
                    OrganizeEventID = 1,
                    EventID = 1001,
                    OrganizerID = 105,
                    EventLocation = "Radison Blu Hotel",
                    City = "Mysore",
                    StartDate = new DateTime(2022, 05, 29),
                    EndDate = new DateTime(2022, 05, 31),
                    Price = 50000,
                    EventDesc = "Conference for 3 days"
                },
                new OrganizeEvent
                {
                    OrganizeEventID = 2,
                    EventID = 1002,
                    OrganizerID = 103,
                    EventLocation = "Grand Mercure",
                    City = "Bangalore",
                    StartDate = new DateTime(2022, 06, 09),
                    EndDate = new DateTime(2022, 06, 09),
                    Price = 20000,
                    EventDesc = "Seminar for 1 day"
                },
                new OrganizeEvent
                {
                    OrganizeEventID = 3,
                    EventID = 1001,
                    OrganizerID = 101,
                    EventLocation = "Golden Landmark Resorts",
                    City = "Mysore",
                    StartDate = new DateTime(2022, 06, 15),
                    EndDate = new DateTime(2022, 06, 19),
                    Price = 24000,
                    EventDesc = "Conference for 4 days"
                },
                new OrganizeEvent
                {
                    OrganizeEventID = 4,
                    EventID = 1003,
                    OrganizerID = 104,
                    EventLocation = "Ibis Hotel",
                    City = "Bangalore",
                    StartDate = new DateTime(2022, 05, 10),
                    EndDate = new DateTime(2022, 05, 11),
                    Price = 50000,
                    EventDesc = "Workshop for 2 days"
                },
                new OrganizeEvent
                {
                    OrganizeEventID = 5,
                    EventID = 1003,
                    OrganizerID = 102,
                    EventLocation = "Royalton Hotel",
                    City = "Hyderabad",
                    StartDate = new DateTime(2022, 07, 02),
                    EndDate = new DateTime(2022, 07, 06),
                    Price = 100000,
                    EventDesc = "Workshop for 5 days"
                },
                new OrganizeEvent
                {
                    OrganizeEventID = 6,
                    EventID = 1004,
                    OrganizerID = 105,
                    EventLocation = "Courtyard Marriott Hotel",
                    City = "Bangalore",
                    StartDate = new DateTime(2022, 05, 29),
                    EndDate = new DateTime(2022, 05, 29),
                    Price = 25000,
                    EventDesc = "Awards and Competition for 1 day"
                },
                new OrganizeEvent
                {
                    OrganizeEventID = 7,
                    EventID = 1004,
                    OrganizerID = 103,
                    EventLocation = "ClayWorks Create",
                    City = "Bangalore",
                    StartDate = new DateTime(2022, 06, 09),
                    EndDate = new DateTime(2022, 06, 09),
                    Price = 30000,
                    EventDesc = "Awards and Competition for 1 day"
                },
                new OrganizeEvent
                {
                    OrganizeEventID = 8,
                    EventID = 1001,
                    OrganizerID = 102,
                    EventLocation = "Taj Deccan",
                    City = "Hyderabad",
                    StartDate = new DateTime(2022, 08, 10),
                    EndDate = new DateTime(2022, 08, 13),
                    Price = 140000,
                    EventDesc = "Conference for 4 days"
                },
                new OrganizeEvent
                {
                    OrganizeEventID = 9,
                    EventID = 1001,
                    OrganizerID = 101,
                    EventLocation = "White Orchids Resort",
                    City = "Mysore",
                    StartDate = new DateTime(2022, 05, 15),
                    EndDate = new DateTime(2022, 05, 16),
                    Price = 50000,
                    EventDesc = "Conference for 2 days"
                }
            );

            // -------------------------------
            // Seed tbl_Members
            // -------------------------------

            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    MemberID = 2010,
                    MemberName = "Aron",
                    BirthDate = new DateTime(1992, 05, 29),
                    Email = "Aron@gmail.com",
                    EventID = 1001
                },
                new Member
                {
                    MemberID = 2011,
                    MemberName = "Caroline",
                    BirthDate = new DateTime(1993, 10, 09),
                    Email = "Caroline@gmail.com",
                    EventID = 1002
                },
                new Member
                {
                    MemberID = 2012,
                    MemberName = "Clinton",
                    BirthDate = new DateTime(1992, 01, 10),
                    Email = "Clinton@gmail.com",
                    EventID = 1003
                },
                new Member
                {
                    MemberID = 2013,
                    MemberName = "Cole",
                    BirthDate = new DateTime(1995, 02, 05),
                    Email = "Cole@gmail.com",
                    EventID = 1003
                },
                new Member
                {
                    MemberID = 2014,
                    MemberName = "David",
                    BirthDate = new DateTime(1992, 05, 29),
                    Email = "David@gmail.com",
                    EventID = 1003
                },
                new Member
                {
                    MemberID = 2015,
                    MemberName = "George",
                    BirthDate = new DateTime(1992, 05, 29),
                    Email = "George@gmail.com",
                    EventID = 1003
                },
                new Member
                {
                    MemberID = 2016,
                    MemberName = "Joan",
                    BirthDate = new DateTime(1990, 08, 15),
                    Email = "Joan@gmail.com",
                    EventID = 1002
                },
                new Member
                {
                    MemberID = 2017,
                    MemberName = "Myra",
                    BirthDate = new DateTime(1993, 07, 16),
                    Email = "Myra@gmail.com",
                    EventID = 1001
                },
                new Member
                {
                    MemberID = 2018,
                    MemberName = "Luke",
                    BirthDate = new DateTime(1991, 06, 20),
                    Email = "Luke@gmail.com",
                    EventID = 1004
                },
                new Member
                {
                    MemberID = 2019,
                    MemberName = "Sarah",
                    BirthDate = new DateTime(1991, 05, 25),
                    Email = "Sarah@gmail.com",
                    EventID = 1004
                },
                new Member
                {
                    MemberID = 2020,
                    MemberName = "Jack",
                    BirthDate = new DateTime(1998, 05, 02),
                    Email = "Jack@gmail.com",
                    EventID = 1001
                }
            );
        }
    }
}