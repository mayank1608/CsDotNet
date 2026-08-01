using HotelBookingBL.Interfaces;
using HotelBookingBL.Services;
using HotelBookingDAL.Interfaces;
using HotelBookingDAL.Repositories;
using HotelBookingDTO.Data;
using HotelBookingDTO.DTOs;
using HotelBookingDTO.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

try
{
    IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

    ServiceProvider serviceProvider = ConfigureServices(configuration);

    IHotelBookingService hotelBookingService = serviceProvider.GetRequiredService<IHotelBookingService>();

    await ShowMenuAsync(hotelBookingService);
}
catch (Exception exception)
{
    Console.WriteLine($"Application error: {exception.Message}");
}

static ServiceProvider ConfigureServices(IConfiguration configuration)
{
    ServiceCollection services = new();

    services.AddDbContext<HotelBookingDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("HotelBookingDbConnection")));

    services.AddScoped<IHotelBookingRepository, HotelBookingRepository>();
    services.AddScoped<IHotelBookingService, HotelBookingService>();

    return services.BuildServiceProvider();
}

static async Task ShowMenuAsync(IHotelBookingService hotelBookingService)
{
    bool continueApplication = true;

    while (continueApplication)
    {
        Console.WriteLine();
        Console.WriteLine("===== Hotel Booking System =====");
        Console.WriteLine("1. Get bookings by status");
        Console.WriteLine("2. Find available hotels");
        Console.WriteLine("3. Add new room");
        Console.WriteLine("4. Add new booking");
        Console.WriteLine("5. Cancel booking");
        Console.WriteLine("6. Exit");
        Console.Write("Enter your choice: ");

        string? choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    await GetBookingsByStatusAsync(hotelBookingService);
                    break;

                case "2":
                    await GetAvailableHotelsAsync(hotelBookingService);
                    break;

                case "3":
                    await AddRoomAsync(hotelBookingService);
                    break;

                case "4":
                    await AddBookingAsync(hotelBookingService);
                    break;

                case "5":
                    await CancelBookingAsync(hotelBookingService);
                    break;

                case "6":
                    continueApplication = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error: {exception.Message}");
        }
    }
}

static async Task GetBookingsByStatusAsync(IHotelBookingService hotelBookingService)
{
    Console.WriteLine("Select booking status:");
    Console.WriteLine("1. Confirmed");
    Console.WriteLine("2. Canceled");
    Console.WriteLine("3. Pending");
    Console.Write("Enter status: ");

    int statusInput = Convert.ToInt32(Console.ReadLine());

    BookingStatus status = statusInput switch
    {
        1 => BookingStatus.Confirmed,
        2 => BookingStatus.Canceled,
        3 => BookingStatus.Pending,
        _ => throw new ArgumentException("Invalid booking status.")
    };

    var bookings = await hotelBookingService.GetBookingsByStatusAsync(status);

    foreach (var booking in bookings)
    {
        Console.WriteLine(
            $"Booking ID: {booking.Id}, Customer: {booking.Customer.Name}, Hotel: {booking.Room.Hotel.Name}, Room: {booking.Room.RoomNumber}, Status: {booking.Status}");
    }
}

static async Task GetAvailableHotelsAsync(IHotelBookingService hotelBookingService)
{
    Console.Write("Enter check-in date yyyy-mm-dd: ");
    DateTime checkInDate = Convert.ToDateTime(Console.ReadLine());

    Console.Write("Enter check-out date yyyy-mm-dd: ");
    DateTime checkOutDate = Convert.ToDateTime(Console.ReadLine());

    var hotels = await hotelBookingService.GetAvailableHotelsAsync(checkInDate, checkOutDate);

    foreach (var hotel in hotels)
    {
        Console.WriteLine($"Hotel ID: {hotel.Id}, Name: {hotel.Name}, Location: {hotel.Location}");

        foreach (var room in hotel.Rooms)
        {
            Console.WriteLine($"  Room: {room.RoomNumber}, Type: {room.Type}, Price: {room.PricePerNight}");
        }
    }
}

static async Task AddRoomAsync(IHotelBookingService hotelBookingService)
{
    Console.Write("Enter hotel ID: ");
    int hotelId = Convert.ToInt32(Console.ReadLine());

    Console.Write("Enter room number: ");
    string roomNumber = Console.ReadLine() ?? string.Empty;

    Console.Write("Enter room type: ");
    string type = Console.ReadLine() ?? string.Empty;

    Console.Write("Enter price per night: ");
    decimal pricePerNight = Convert.ToDecimal(Console.ReadLine());

    AddRoomRequestDto addRoomRequestDto = new()
    {
        HotelId = hotelId,
        RoomNumber = roomNumber,
        Type = type,
        PricePerNight = pricePerNight
    };

    var room = await hotelBookingService.AddRoomAsync(addRoomRequestDto);

    Console.WriteLine($"Room added successfully. Room ID: {room.Id}");
}

static async Task AddBookingAsync(IHotelBookingService hotelBookingService)
{
    Console.Write("Enter customer ID: ");
    int customerId = Convert.ToInt32(Console.ReadLine());

    Console.Write("Enter room ID: ");
    int roomId = Convert.ToInt32(Console.ReadLine());

    Console.Write("Enter check-in date yyyy-mm-dd: ");
    DateTime checkInDate = Convert.ToDateTime(Console.ReadLine());

    Console.Write("Enter check-out date yyyy-mm-dd: ");
    DateTime checkOutDate = Convert.ToDateTime(Console.ReadLine());

    AddBookingRequestDto addBookingRequestDto = new()
    {
        CustomerId = customerId,
        RoomId = roomId,
        CheckInDate = checkInDate,
        CheckOutDate = checkOutDate,
        Status = BookingStatus.Confirmed
    };

    var booking = await hotelBookingService.AddBookingAsync(addBookingRequestDto);

    Console.WriteLine($"Booking added successfully. Booking ID: {booking.Id}");
}

static async Task CancelBookingAsync(IHotelBookingService hotelBookingService)
{
    Console.Write("Enter booking ID: ");
    int bookingId = Convert.ToInt32(Console.ReadLine());

    bool isCanceled = await hotelBookingService.CancelBookingAsync(bookingId);

    Console.WriteLine(isCanceled
        ? "Booking canceled successfully."
        : "Booking not found.");
}