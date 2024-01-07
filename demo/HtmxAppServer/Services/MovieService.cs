using System.Collections.ObjectModel;
using HtmxAppServer.Models;

namespace HtmxAppServer.Services;

public class MovieService
{
    private readonly Dictionary<int, Character> _characters = new()
    {
        [1] = new Character(1, "Anakin", "Skywalker"),
        [2] = new Character(2, "Obi-Wan", "Kenobi"),
        [3] = new Character(3, "Padme", "Amidala"),
        [4] = new Character(4, "Luke", "Skywalker"),
        [5] = new Character(5, "Leia", "Organa"),
        [6] = new Character(6, "Han", "Solo"),
        [7] = new Character(7, "Chewbacca", ""),
        [8] = new Character(8, "Lando", "Calrissian"),
        [9] = new Character(9, "Yoda", ""),
        [10] = new Character(10, "Palpatine", ""),
        [11] = new Character(11, "Darth", "Vader"),
        [12] = new Character(12, "Boba", "Fett"),
        [13] = new Character(13, "Jabba", "the Hutt"),
        // [14] = new Character(14, "Qui-Gon", "Jinn"),
        // [15] = new Character(15, "Mace", "Windu"),
        // [16] = new Character(16, "Jar Jar", "Binks"),
        // [17] = new Character(17, "Darth", "Maul"),
        // [18] = new Character(18, "Count", "Dooku"),
        // [19] = new Character(19, "General", "Grievous"),
        // [20] = new Character(20, "Ahsoka", "Tano"),
        // [21] = new Character(21, "Kylo", "Ren"),
        // [22] = new Character(22, "Rey", ""),
        // [23] = new Character(23, "Finn", ""),
        // [24] = new Character(24, "Poe", "Dameron"),
        // [25] = new Character(25, "BB-8", ""),
        // [26] = new Character(26, "R2-D2", ""),
        // [27] = new Character(27, "C-3PO", ""),
        // [28] = new Character(28, "Rose", "Tico"),
        // [29] = new Character(29, "Jyn", "Erso"),
        // [30] = new Character(30, "Cassian", "Andor"),
    };
    
    public ReadOnlyDictionary<int, Character> GetCharacters(int page = 1, int pageSize = 5)
    {
        var dictionary = _characters
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToDictionary();
        
        return new ReadOnlyDictionary<int, Character>(dictionary);
    }
}