﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CIS.Models
{
    internal class Seating
    {
        public Seating(int[][] places, int idHall)
        {
            Places = places;
            IdHall = idHall;
        }
        public int CountAvailablePlaces { get { return CalcCountAvailablePlaces(); } }

        public int[][] Places{ get; set; }

        public int IdHall { get; set; }
        private int CalcCountAvailablePlaces()
        {
            int countAvailablePlaces = 0;
            for (int i = 0; i < Places.GetLength(0); i++)
            {
                for (int j = 0; j < Places[i].Length; j++)
                {
                    if (Places[i][j] != -1)
                    {
                        countAvailablePlaces += 1;
                    }
                }
            }
            return countAvailablePlaces;
        }
        public bool IsPlacesNotEmpty()
        {
            return CountAvailablePlaces != 0;
        }
    }
}
