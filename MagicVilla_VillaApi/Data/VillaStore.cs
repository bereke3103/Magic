﻿using MagicVilla_VillaApi.Models.Dto;

namespace MagicVilla_VillaApi.Data
{
    public static class VillaStore
    {
        public static List <VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto {Id = 1, Name = "Pool View", Sqft=100, Occupancy= 50},
            new VillaDto {Id = 2, Name = "Beach View", Sqft=50, Occupancy= 10}
        };
    }
}
