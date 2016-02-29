using PokemonTextEdition.Classes;
using PokemonTextEdition.Locations;
using System.Collections.Generic;

namespace PokemonTextEdition.Collections
{
    /// <summary>
    /// Represents all of the locations in the game. The locations are accessible through the <see cref="AllLocations"/> list.
    /// </summary>
    class LocationList
    {
        public static PalletTown pallet = new PalletTown();
        public static Route1 route1 = new Route1();
        public static ViridianCity viridiancity = new ViridianCity();
        public static Route2S route2s = new Route2S();
        public static ViridianForestPart1 viridianforest1 = new ViridianForestPart1();
        public static ViridianForestPart2 viridianforest2 = new ViridianForestPart2();
        public static ViridianForestPart3 viridianforest3 = new ViridianForestPart3();
        public static Route2N route2n = new Route2N();
        public static PewterCity pewtercity = new PewterCity();
        public static Route3W route3w = new Route3W();
        public static Route3E route3e = new Route3E();
        public static MtMoonPart1 mtmoon1 = new MtMoonPart1();
        public static MtMoonPart2 mtmoon2 = new MtMoonPart2();

        //A list of every available location.
        public static List<Location> AllLocations = new List<Location>()
        {
            pallet, route1, viridiancity, route2s, viridianforest1, viridianforest2, viridianforest3, route2n, pewtercity,
            route3w, route3e, mtmoon1, mtmoon2
        };
    }
}
