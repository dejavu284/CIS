using System;
using Xunit;
using System.IO;
using CinemaModel;

namespace CIS.Tests
{
    public class Tests
    {
        /*классы для тестирования:
            Basket, CinemaChain, Seating, Schedule*/
        [Fact]
        public void Booking_the_right_place()
        {
            //arrange
            int[][] places = new int[4][];
            places[0] = new int[4] { 1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };
            Seating seating = new Seating(places, 1);

            Place placeForBooking = new Place(0,0,100);
            int[][] placePositiv = new int[4][];
            placePositiv[0] = new int[4] { -1, 2, 3, 4 };
            placePositiv[1] = new int[4] { 5, 6, 7, 8 };
            placePositiv[2] = new int[4] { 9, 10, 11, 12 };
            placePositiv[3] = new int[4] { 13, 14, 15, 16 };
            Seating seatingPositiv = new Seating(placePositiv, 1);

            //act
            seating.BookingPlace(placeForBooking);

            //assert
            Assert.Equal(seating.Places, seatingPositiv.Places);
        }

        [Fact]
        public void Booking_an_already_booked_seat()
        {
            //arrange
            int[][] places = new int[4][];
            places[0] = new int[4] { -1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };
            Seating seating = new Seating(places, 1);

            Place placeForBooking = new Place(0, 0, 100);
            int[][] placePositiv = new int[4][];
            placePositiv[0] = new int[4] { -1, 2, 3, 4 };
            placePositiv[1] = new int[4] { 5, 6, 7, 8 };
            placePositiv[2] = new int[4] { 9, 10, 11, 12 };
            placePositiv[3] = new int[4] { 13, 14, 15, 16 };
            Seating seatingPositiv = new Seating(placePositiv, 1);

            //act
            seating.BookingPlace(placeForBooking);

            //assert
            Assert.Equal(seating.Places, seatingPositiv.Places);
        }

        [Fact]
        public void Booking_a_seat_that_is_not_in_the_hall()
        {
            //arrange
            int[][] places = new int[4][];
            places[0] = new int[4] { 1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };
            Seating seating = new Seating(places, 1);

            Place placeForBooking = new Place(4, 4, 100);
            int[][] placePositiv = new int[4][];
            placePositiv[0] = new int[4] { 1, 2, 3, 4 };
            placePositiv[1] = new int[4] { 5, 6, 7, 8 };
            placePositiv[2] = new int[4] { 9, 10, 11, 12 };
            placePositiv[3] = new int[4] { 13, 14, 15, 16 };
            Seating seatingPositiv = new Seating(placePositiv, 1);

            //act
            seating.BookingPlace(placeForBooking);

            //assert
            Assert.Equal(seating.Places, seatingPositiv.Places);
        }

    }
}