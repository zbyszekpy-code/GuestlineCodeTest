using GuestlineCodeTest.Application;
using GuestlineCodeTest.Application.Availability;
using GuestlineCodeTest.Application.Search;
using GuestlineCodeTest.Command;
using GuestlineCodeTest.Repositories;
using GuestlineCodeTest.Repositories.InputModel;

namespace GuestlineCodeTest;

class Program
{
    static void Main(string[] args)
    {
        var hotelsOption = new Option<FileInfo?>(
            name: "--hotels",
            description: "The file with hotels.") { IsRequired = true };

        var bookingOption = new Option<FileInfo?>(
            name: "--bookings",
            description: "The file with bookings.") { IsRequired = true };

        var rootCommand = new RootCommand("Guestline code test Zbigniew Pytka");
        rootCommand.AddOption(hotelsOption);
        rootCommand.AddOption(bookingOption);

        rootCommand.SetHandler((hotelsFile, bookingsFile) =>
            { 
                var hotels = new HotelRepository(hotelsFile is null ? Array.Empty<HotelDto>() : JsonSerializer.Deserialize<IReadOnlyCollection<HotelDto>>(File.ReadAllText(hotelsFile.FullName)) ?? Array.Empty<HotelDto>());
                var booking = new BookingRepository(bookingsFile is null ? Array.Empty<BookingDto>() : JsonSerializer.Deserialize<IReadOnlyCollection<BookingDto>>(File.ReadAllText(bookingsFile.FullName)) ?? Array.Empty<BookingDto>());
                DoRootCommand(hotels, booking);
            },
            hotelsOption, bookingOption);

        rootCommand.Invoke(args);
    }

    private static void DoRootCommand(IHotelRepository hotel, IBookingRepository booking)
    {
        bool end = false;
        while (!end)
        {
            var input = Console.ReadLine();
            end = string.IsNullOrWhiteSpace(input);
            if (!end)
            {
                RunCommand(input!, hotel, booking);
            }
        }
    }

    private static void RunCommand(string input, IHotelRepository hotel, IBookingRepository booking)
    {
        if (input.StartsWith(nameof(Availability)))
        {
            // extract part between brackets
            var @params = GetParams(input);
            var parts = @params.Split(',');
            var hotelId = parts[0].Trim();
            var dateRange = parts[1].Trim();
            var roomType = parts[2].Trim();
            var availability = new Availability(hotelId, dateRange, roomType);

            var result = new AvailabilityCommandHandler(hotel, booking).Handle(availability.GetCommand());
            Console.WriteLine($"({result.AvailableRooms}, {dateRange})");
        }
        else if (input.StartsWith(nameof(Search)))
        {
            var @params = GetParams(input);
            var parts = @params.Split(',');
            var hotelId = parts[0].Trim();
            var daysAhead = int.Parse(parts[1]);
            var roomType = parts[2].Trim();
            var searchCommand = new Search(hotelId, daysAhead, roomType);
            var result = new SearchCommandHandler(hotel, booking).Handle(searchCommand.GetCommand());
            if (!result.Availability.Any())
            {
                Console.WriteLine();
            }

            foreach (var range in result.Availability)
            {
                Console.WriteLine($"({range.From.ToString(Constants.DateFormat)}-{range.To.ToString(Constants.DateFormat)}, {range.Count})");
            }
        }
        else
        {
            Console.WriteLine("Unknown command");
        }
    }

    private static string GetParams(string input)
    {
        var openBracket = input.IndexOf('(');
        return input.Substring(openBracket + 1, input.IndexOf(')') - openBracket - 1);
    }
}