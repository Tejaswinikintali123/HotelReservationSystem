﻿using HotelResevationSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelResevationSystem
{
    /// <summary>
    /// Bookings class
    /// </summary>
    public class Booking
    {
        private  Dictionary<int,Room> roomList = new Dictionary<int,Room>();
        /// <summary>
        /// Booking class constructor
        /// </summary>
        public Booking()
        {
            
        }
        private List<Room> GetAllRooms()
        {
            return roomList.Values.ToList();
        }
        #region methods

        /// <summary>
        /// List of all availablerooms with the selection of Roomtype
        /// </summary>
        /// <param name="type"></param>
        /// <returns></"availablerooms">
        public List<Room> GetAvailableRooms(Roomtype type)
        {
            var availablerooms =  new List<Room>();
            var rooms = GetAllRooms();
            foreach(Room r in rooms )
            {
                if (r.Type == type && r.IsBooked==false) 
                {
                    availablerooms.Add(r);
                }
            }
            return availablerooms;
        }
        /// <summary>
        ///Booking a room 
        /// </summary>
        /// <param name="roomno"></param>
        /// <returns></true/false>
        public bool BookRoom(int roomno)
        {
            if(roomList.Keys.Contains(roomno) == true && roomList[roomno].IsBooked == false)
            {
                roomList[roomno].IsBooked = true;
                return true;
            }
            return false;
        }
        /// <summary>
        /// populate rooms with respective of nooffloors & noofroomsperfloor
        /// </summary>
        /// <param name="noOfFloors"></param>
        /// <param name="noOfRoomsPerFloor"></param>
        public void PopulateRooms(int noOfFloors, int noOfRoomsPerFloor)
        {
            for (int i = 1; i <= noOfRoomsPerFloor; i++)
            {
                var price = 0;
                var type = Roomtype.king;
                switch (i)
                {
                    case 1:
                    case 5:
                        price = 200;
                        type = Roomtype.king;
                        break;
                    case 2:
                    case 6:
                    case 9:
                        price = 100;
                        type = Roomtype.Queen;
                        break;
                    case 3:
                    case 7:
                        price = 150;
                        type = Roomtype.Twin;
                        break;
                    case 4:
                    case 10:
                    case 8:
                        price = 100;
                        type = Roomtype.single;
                        break;
                }
                for (int k = 100; k <= noOfFloors * 100; k += 100)
                {
                    var room = new Room();
                    room.Price = price;
                    room.Type = type;
                    room.ID = k + i;
                    roomList.Add(room.ID, room);
                }
            }
        }
        #endregion
    }
}


