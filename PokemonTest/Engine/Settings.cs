using System;

namespace PokemonTextEdition.Engine
{
    enum Platform
    {
        None,
        Console,
        Windows,
        Mobile
    }

    class Settings
    {   /// <summary>
        /// The current version of the game in a string format.
        /// </summary>
        public static string GameVersion
        {
            get
            {
                return GameVersionMajor.ToString() + "." + GameVersionMinor.ToString() + "." + GameVersionHotfix.ToString();
            }
        }

        private const int GameVersionMajor = 0;
        private const int GameVersionMinor = 2;
        private const int GameVersionHotfix = 6;

        /// <summary>
        /// The platform that the game is running on, i.e. Console or mobile.
        /// </summary>
        public const Platform GamePlatform = Platform.Console;

        /// <summary>
        /// The game's developer mode, which determines if cheats and developer commands may be used.
        /// This should be set to false for a release.
        /// </summary>
        public static bool GodMode = true;

        /// <summary>
        /// This parameter determines how important a message needs to be in order to be logged.
        /// 0 = trivial, 1 = important, 2 = vital.
        /// </summary>
        public const int LogLevel = 1;

        /// <summary>
        /// The color of the font of general messages in the Windows console. (NYI)
        /// </summary>
        public static ConsoleColor Program_FontColor = ConsoleColor.Yellow;

        public const string DefaultPlayerName = "Red";
        public const string DefaultRivalName = "Blue"; 

        #region Battle-related

        /// <summary>
        /// The chance that an attack will be a critical hit, expressed in %.
        /// </summary>
        public const int CriticalHitChance = 10;

        /// <summary>
        /// The amount of extra damage damage that a critical hit will deal, expressed in float - i.e. 1.2 for 20% extra damage.
        /// </summary>
        public const float CriticalHitDamageMultiplier = 1.2f;

        /// <summary>
        /// The chance that a paralyzed Pokemon will be able to attack, expressed in %.
        /// </summary>
        public const int ParalysisAttackChance = 75;

        #endregion
    }
}
