using CinemaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CIS.Tests
{
    public class TestingConservationOfModelInvariants
    {
        //Проверка сохранения инвариантв фильмов при конструировании 
        [Fact]
        public void Creating_a_Film_with_a_negative_year()
        {
            Assert.Throws<Exception>(() => new Film("фильм 1","жанр","описание",-100));
        }
        [Fact]
        public void Creating_a_Film_with_a_zero_year()
        {
            Assert.Throws<Exception>(() => new Film("фильм 1", "жанр", "описание", 0));
        }
        [Fact]
        public void Creating_a_Film_with_a_empty_name()
        {
            Assert.Throws<Exception>(() => new Film("", "жанр", "описание", 2023));
        }
        [Fact]
        public void Creating_a_Film_with_a_null_name()
        {
            Assert.Throws<Exception>(() => new Film(null, "жанр", "описание", 2023));
        }
        //Проверка сохранения инвариантв мест при конструировании 
        [Fact]
        public void Creating_a_Place_with_a_negative_row()
        {
            Assert.Throws<Exception>(() => new Place(-1, 0, 100));
        }
        [Fact]
        public void Creating_a_Place_with_a_negative_colum()
        {
            Assert.Throws<Exception>(() => new Place(0, -1, 100));
        }
        [Fact]
        public void Creating_a_Place_with_price_equal_to_minus_one_indicating_employment()
        {
            Action testCode = () => {new Place(0, 0, -1);};
            var ex = Record.Exception(testCode);
            Assert.Null(ex);
        }
        [Fact]
        public void Creating_a_Place_with_negative_price_except_minus_one()
        {
            Assert.Throws<Exception>(() => new Place(0, 0, -2));
        }
        //Проверка сохранения инвариантв залов при конструировании 
        [Fact]
        public void Creating_a_Hall_with_negative_id()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { 1, 2, 3, 4 };
            places[1] = new int[4] { 5, 6, 7, 8 };
            places[2] = new int[4] { 9, 10, 11, 12 };
            places[3] = new int[4] { 13, 14, 15, 16 };
            Assert.Throws<Exception>(() => new Hall(-1, "", places));
        }
        [Fact]
        public void Creating_a_Hall_with_prices_equal_to_minus_one_indicating_employment()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { -1, 2, 3, 4 };
            places[1] = new int[4] { 5, -6, 7, 8 };
            places[2] = new int[4] { 9, 10, -11, 12 };
            places[3] = new int[4] { 13, 14, 15, -16 };
            Action testCode = () => new Hall(-1, "", places);
            var ex = Record.Exception(testCode);
            Assert.Null(ex);
        }
        public void Creating_a_Hall_with_negative_price_except_minus_one()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { -5, 2, 3, 4 };
            places[1] = new int[4] { 5, -6, 7, 8 };
            places[2] = new int[4] { 9, 10, -11, 12 };
            places[3] = new int[4] { 13, 14, 15, -16 };
            Assert.Throws<Exception>(() => new Hall(-1, "", places));
        }
        public void Creating_a_Hall_with_negative_price()
        {
            int[][] places = new int[4][];
            places[0] = new int[4] { -1, -1, 3, 4 };
            places[1] = new int[4] { 5, -6, 7, 8 };
            places[2] = new int[4] { 9, 10, -11, 12 };
            places[3] = new int[4] { 13, 14, 15, -16 };
            Assert.Throws<Exception>(() => new Hall(-1, "", places));
        }
        public void Creating_a_Hall_with_a_different_number_of_seats_in_a_row()
        {
            int[][] places = new int[4][];
            places[0] = new int[2] { 100, 100};
            places[1] = new int[4] { 5, -6, 7, 8 };
            places[2] = new int[4] { 9, 10, -11, 12 };
            places[3] = new int[4] { 13, 14, 15, -16 };
            Assert.Throws<Exception>(() => new Hall(-1, "", places));
        }
        //Проверка сохранения инвариантв показов при конструировании 
        //Проверка сохранения инвариантв кинотеатров при конструировании 
        //Проверка сохранения инвариантв билетов при конструировании 
        //Проверка сохранения инвариантв рассадки при конструировании
    }
}
