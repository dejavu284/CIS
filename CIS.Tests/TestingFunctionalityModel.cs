using System;
using Xunit;
using System.IO;
using CinemaModel;
using System.Net.Sockets;
using Xunit.Sdk;

namespace CIS.Tests
{
    public class TestingFunctionalityModel
    {
        //Seating.CheckAvailabilityPlace()
        /*
         * ��������� ����� � ��������
         * ������� ����� � ��������
         * ���� �� � ��������
        */
        //Seating.CheckExistenceRow()
        /*
         * ��� � ��������
         * ��� �� � ��������
        */
        //Seating.CheckAvailabilityRow()
        /*
         * ��������� ������� ��� � ��������
         * ����������� ������� ��� � ��������
         * ��������� ��� � ��������
        */
        //Seating.BookingPlace() =>
        /*
         * ������������ ������������������ ����� � ����
         * ������������ ���������������� ����� � ����
         * ������������ ����� �������� ��� � ����
        */
        //Schedule.FindByName()
        /*
         * ����� ������� �� �������� ������ � ������� ��� �������
         * ����� ������� �� �������� ������ � ������� ��� ������� � ������ ���������
         * ����� ������� �� �������� ������ � ������� ������ � �������� � ������ ���������
         * ����� ������� �� �������� ������ � ������� � �������� � ������ � ��������� ����������
        */
        //Schedule.FindByDate()
        /*
         * ����� ������� �� ���� � ������� ��� �������
         * ����� ������� �� ���� � ������� ��� ������� � ������ �����
         * ����� ������� �� ���� � ������� ������ � �������� � ������ �����
         * ����� ������� �� ���� � ������� � �������� � ������ � ��������� ������
        */
        //Schedule.IsEmpty()
        /*
         * �������� �� ������� ������� �������
         * �������� �� ������� �� ������� �������
        */
        //Basket.AddTicket()
        /*
         * ���������� � ������� ������ �������� ��� ���
         * ���������� � ������� ������ ������� ��� ����
        */
        //
        [Fact]
        public void CheckingAvalibleRow()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { 1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);
            bool result = seating.CheckAvailabilityPlace(1, 1);
            Assert.True(result);
        }
        [Fact]
        public void CheckingFillBookedRow()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { -1, -1, -1, -1 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);
            bool result = seating.CheckAvailabilityPlace(1, 1);
            Assert.False(result);
        }
        [Fact]
        public void CheckingNotFillBookedRow()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { -1, -1, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);
            bool result = seating.CheckAvailabilityPlace(1, 1);
            Assert.False(result);
        }
        [Fact]
        public void CheckingRowInSeationgByExistence()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { 1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);
            bool result = seating.CheckExistenceRow(1);
            Assert.True(result);
        }
        [Fact]
        public void CheckingRowOutsideSeationgByExistence()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { 1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);
            bool result = seating.CheckExistenceRow(0);
            Assert.False(result);
        }
        [Fact]
        public void CheckingAvaliblePlaseInSeating()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { 1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);
            bool result = seating.CheckAvailabilityPlace(1,1);
            Assert.True(result);
        }
        [Fact]
        public void CheckingBookedPlaseInSeating()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { -1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);
            bool result = seating.CheckAvailabilityPlace(1, 1);
            Assert.False(result);
        }
        [Fact]
        public void CheckingPlaseOutsideSeating()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { -1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);
            bool result = seating.CheckAvailabilityPlace(0, 0);
            Assert.False(result);
        }
        public static IEnumerable<object[]> ElementsDataSetForBookingPlace()
        {
            yield return new object[] {
                new Seating(new int[2][] { new int[2] { 100, 100 }, new int[2] { 100, 100 } } , 1), new Place(0, 0, 100)
            };
            yield return new object[] {
                new Seating(new int[2][] { new int[2] { -1, 100 }, new int[2] { 100, 100 } } , 1), new Place(0, 0, 100)
            };
            yield return new object[] {
                new Seating(new int[2][] { new int[2] { 100, 100 }, new int[2] { 100, 100 } } , 1), new Place(2, 2, 100)
            };
        }
        [Theory]
        [MemberData(nameof(ElementsDataSetForBookingPlace))]
        public void Booking_place(Seating seating,Place placeForBooking)
        {
            //arrange
            Seating seatingForTest = new (seating.Places,seating.IdHall);

            //act
            seatingForTest.BookingPlace(placeForBooking);

            //assert
            Assert.Equal(seating.Places, seatingForTest.Places);
        }
        [Fact]
        public void Find_for_show_by_film_name_in_the_schedule_without_show()
        {
            Schedule scheduleAll = new Schedule(new List<Show> {
            });

            Schedule scheduleForFilmOne = new Schedule(new List<Show>
            {
            });

            Schedule scheduleForFilmOneTest = scheduleAll.FindByName("����� 1");

            Assert.Equal(scheduleForFilmOneTest.Shows, scheduleForFilmOne.Shows);
        }
        [Fact]
        public void Find_for_show_by_film_name_in_the_schedule_without_show_with_necessary_name()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { 1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);
            Schedule scheduleAll = new Schedule(new List<Show> {
                new Show("����� 2", new DateOnly(2023,11,2), new TimeOnly(12,13), seating, 0),
                new Show("����� 2", new DateOnly(2023,10,2), new TimeOnly(12,14), seating, 1),
                new Show("����� 2", new DateOnly(2023,11,2), new TimeOnly(12,15), seating, 2)
            });

            Schedule scheduleForFilmOne = new Schedule(new List<Show> {
            });

            Schedule scheduleForFilmOneTest = scheduleAll.FindByName("����� 1");

            Assert.Equal(scheduleForFilmOneTest.Shows, scheduleForFilmOne.Shows);
        }
        [Fact]
        public void Find_for_show_by_film_name_in_the_schedule_with_show_with_only_necessary_name()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { 1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);
            Schedule scheduleAll = new Schedule(new List<Show> {
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,10),seating, 1),
                new Show("����� 1", new DateOnly(2023,11,2), new TimeOnly(12,11),seating, 1),
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,12), seating, 1),
            });

            Schedule scheduleForFilmOne = new Schedule(new List<Show> {
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,10), seating, 1),
                new Show("����� 1", new DateOnly(2023,11,2), new TimeOnly(12,11), seating, 1),
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,12), seating, 1)
            });

            Schedule scheduleForFilmOneTest = scheduleAll.FindByName("����� 1");

            Assert.Equal(scheduleForFilmOneTest.Shows, scheduleForFilmOne.Shows);
        }
        [Fact]
        public void Find_for_show_by_film_name_in_the_schedule_with_show_with_necessary_and_unnecessary_name()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { 1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);
            Schedule scheduleAll = new Schedule(new List<Show> { 
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,10), seating, 1),
                new Show("����� 1", new DateOnly(2023,11,2), new TimeOnly(12,11), seating, 1),
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,12), seating, 1),
                new Show("����� 2", new DateOnly(2023,11,2), new TimeOnly(12,13), seating, 1),
                new Show("����� 2", new DateOnly(2023,10,2), new TimeOnly(12,14), seating, 1),
                new Show("����� 2", new DateOnly(2023,11,2), new TimeOnly(12,15), seating, 1)
            });

            Schedule scheduleForFilmOne = new Schedule(new List<Show> {
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,10), seating, 1),
                new Show("����� 1", new DateOnly(2023,11,2), new TimeOnly(12,11), seating, 1),
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,12), seating, 1)
            });

            Schedule scheduleForFilmOneTest = scheduleAll.FindByName("����� 1");

            Assert.Equal(scheduleForFilmOneTest.Shows, scheduleForFilmOne.Shows);
        }
        [Fact]
        public void Find_for_show_by_date_in_the_schedule_without_show()
        {
            Schedule scheduleAll = new Schedule(new List<Show> 
            {
            });

            Schedule scheduleShowsByOneDate = new Schedule(new List<Show>
            {
            });

            Schedule scheduleShowsByOneDateTest = scheduleAll.FindByDate(new DateOnly(2023, 10, 2));

            Assert.Equal(scheduleShowsByOneDate.Shows, scheduleShowsByOneDateTest.Shows);
        }
        [Fact]
        public void Find_for_show_by_date_in_the_schedule_without_show_with_necessary_date()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { 1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);
            Schedule scheduleAll = new Schedule(new List<Show> { 
                new Show("����� 1", new DateOnly(2023,11,2), new TimeOnly(12,11), seating, 1),
                new Show("����� 2", new DateOnly(2023,11,2), new TimeOnly(12,13), seating, 1),
                new Show("����� 2", new DateOnly(2023,11,2), new TimeOnly(12,15), seating, 1)
            });

            Schedule scheduleShowsByOneDate = new Schedule(new List<Show> {
            });

            Schedule scheduleShowsByOneDateTest = scheduleAll.FindByDate(new DateOnly(2023, 10, 2));

            Assert.Equal(scheduleShowsByOneDate.Shows, scheduleShowsByOneDateTest.Shows);
        }
        [Fact]
        public void Find_for_show_by_date_in_the_schedule_only_with_show_with_necessary_date()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { 1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);
            Schedule scheduleAll = new Schedule(new List<Show> {
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,10), seating, 1),
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,12), seating, 1),
                new Show("����� 2", new DateOnly(2023,10,2), new TimeOnly(12,14), seating, 1),
            });

            Schedule scheduleShowsByOneDate = new Schedule(new List<Show> {
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,10), seating, 1),
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,12), seating, 1),
                new Show("����� 2", new DateOnly(2023,10,2), new TimeOnly(12,14), seating, 1)
            });

            Schedule scheduleShowsByOneDateTest = scheduleAll.FindByDate(new DateOnly(2023, 10, 2));

            Assert.Equal(scheduleShowsByOneDate.Shows, scheduleShowsByOneDateTest.Shows);
        }
        [Fact]
        public void Find_for_show_by_date_in_the_schedule_with_show_with_necessary_and_unnecessary_date()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { 1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);
            Schedule scheduleAll = new Schedule(new List<Show> {
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,10), seating, 1),
                new Show("����� 1", new DateOnly(2023,11,2), new TimeOnly(12,11), seating, 1),
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,12), seating, 1),
                new Show("����� 2", new DateOnly(2023,11,2), new TimeOnly(12,13), seating, 1),
                new Show("����� 2", new DateOnly(2023,10,2), new TimeOnly(12,14), seating, 1),
                new Show("����� 2", new DateOnly(2023,11,2), new TimeOnly(12,15), seating, 1)
            });

            Schedule scheduleShowsByOneDate = new Schedule(new List<Show> {
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,10), seating, 1),
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,12), seating, 1),
                new Show("����� 2", new DateOnly(2023,10,2), new TimeOnly(12,14), seating, 1)
            });

            Schedule scheduleShowsByOneDateTest = scheduleAll.FindByDate(new DateOnly(2023, 10, 2));

            Assert.Equal(scheduleShowsByOneDate.Shows, scheduleShowsByOneDateTest.Shows);
        }

        [Fact]
        public void Checking_a_non_empty_schedule_for_emptiness()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { 1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);
            Schedule schedule = new Schedule(new List<Show> {
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,12), seating, 1),
                new Show("����� 1", new DateOnly(2023,10,2), new TimeOnly(12,12), seating, 1),
                new Show("����� 2", new DateOnly(2023,10,2), new TimeOnly(12,12), seating, 1)
            });

            bool result = schedule.IsEmpty();

            Assert.False(result);
        }
        [Fact]
        public void Checking_a_schedule_with_empty_shows_for_emptiness()
        {
            Schedule schedule = new Schedule(new List<Show> {});

            bool result = schedule.IsEmpty();

            Assert.True(result);
        }
        [Fact]
        public void Adding_a_new_ticket_to_the_basket()
        {
            Basket basket= new Basket();

            int[][] places = new int[4][];
            places[0] = new int[4] { 1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);

            Place placeForBooking = new Place(0, 0, 100);

            Ticket ticket = new Ticket(
                1,
                new Show("����� 1", new DateOnly(2023, 10, 2), new TimeOnly(12, 12), seating, 1),
                placeForBooking
                );

            basket.AddTicket(ticket);

            Assert.Same(basket.Tickets[0], ticket);
        }
        [Fact]
        public void Adding_a_ticket_that_is_already_in_the_basket()
        {
            Basket basket = new Basket();

            int[][] places = new int[4][];
            places[0] = new int[4] { 1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };

            Seating seating = new Seating(places, 1);

            Place placeForBooking = new Place(0, 0, 100);

            Ticket ticket = new Ticket(
                1,
                new Show("����� 1", new DateOnly(2023, 10, 2), new TimeOnly(12, 12), seating, 1),
                placeForBooking
                );

            basket.AddTicket(ticket);
            
            Assert.Throws<Exception>(() => basket.AddTicket(ticket));
        }
    }
}