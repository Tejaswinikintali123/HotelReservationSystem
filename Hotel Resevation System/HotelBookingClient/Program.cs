using HotelBookingFactory;
using HotelReservationSystemModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBookingClient
{
    class Program
    {
        private static DateTime startdate, enddate;

        static void Main(string[] args)
        {
            var b = new Booking();
            b.PopulateRooms(3, 5);
            Console.WriteLine("WELCOME TO MY NEW HOTEL");
            while (true)
            {
                Console.WriteLine("1.SEARCH AVAILABLE ROOMS");
                Console.WriteLine("2.BOOK A ROOM");
                Console.WriteLine("3.SEARCH BOOKING DETAILS");
                Console.WriteLine("4.EXIT");
                Console.Write("please select an option:");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        try
                        {
                            PrintRooms(b);
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;
                    case "2":
                        try
                        {
                            Console.Write("Enter your name:");
                            var name = Console.ReadLine();
                            Console.Write("Enter emailadress:");
                            var email = Console.ReadLine();
                            var avRooms = PrintRooms(b);
                            Console.Write("Enter Room Type : ");
                            var roomType = (RoomType)Enum.Parse(typeof(RoomType), Console.ReadLine());
                            var roomData = avRooms.Where(x => x.Type == roomType).FirstOrDefault();
                            if (roomData.AvailableRoomCount > 0)
                            {
                                var bookRoom = b.BookRoom(roomType, name, email, startdate, enddate);
                                Console.WriteLine("Successfully completed your room booking:");
                                Console.WriteLine("booking confirmation no:{0}", bookRoom.ID);
                            }
                            else
                            {
                                Console.WriteLine("No rooms available for this type " + roomType);
                            }
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine($"Error:selection should be in the above list of numbers");
                        }
                        catch (ArgumentException ax)
                        {
                            Console.WriteLine($"Error:{ax.Message}");
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (BookingException hx)
                        {
                            Console.WriteLine(hx.Message);
                        }


                        catch
                        {
                            Console.WriteLine("something went wrong plese try again");
                        }
                        break;
                    case "3":
                        try
                        {
                            Console.Write("Enter booking ID:");
                            var ID = Convert.ToInt32(Console.ReadLine());
                            var details = b.GetBookingDetails(ID);
                            Console.WriteLine("Name: {0}", details.Name);
                            Console.WriteLine("Email Address: {0}", details.EmailAddress);
                            Console.WriteLine("Startdate: {0}", details.StartDate.ToShortDateString());
                            Console.WriteLine("Enddate: {0}", details.EndDate.ToShortDateString());
                            Console.WriteLine("RoomType: {0}", details.Type);
                        }
                        catch (BookingException)
                        {
                            Console.WriteLine("Booking ID does not exists");
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "4":
                        return;
                    default:
                        break;
                }
            }

        }
        private static List<BookingSearchResult> PrintRooms(Booking b)
        {


            Console.Write("enter start date:");
            startdate = Convert.ToDateTime(Console.ReadLine());
            Console.Write("enter end date:");
            enddate = Convert.ToDateTime(Console.ReadLine());
            var avRooms = b.SearchAvialableRooms(startdate.Date, enddate.Date);
            Console.WriteLine("S.NO\tROOMTYPE\tAVALABLEROOMS\tROOMPRICE");
            for (var i = 0; i < avRooms.Count; i++)
            {
                var result = avRooms[i];
                Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", i + 1, result.Type, result.AvailableRoomCount, result.RoomPrice);
            }
            return avRooms;







        }

    }
}
