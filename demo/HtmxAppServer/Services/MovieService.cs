using System.Collections.ObjectModel;
using HtmxAppServer.Models;

namespace HtmxAppServer.Services;

public class MovieService
{
    private readonly List<Character> _characters =
    [
        new Character(1, "Luke", "Skywalker", "Star Wars", 1977, "Jedi Knight and hero of the Rebellion"),
        new Character(2, "Darth", "Vader", "Star Wars", 1977, "Former Jedi turned to the dark side"),
        new Character(3, "Princess", "Leia", "Star Wars", 1977, "Leader of the Rebel Alliance"),
        new Character(4, "Han", "Solo", "Star Wars", 1977, "Smuggler and pilot of the Millennium Falcon"),
        new Character(5, "Obi-Wan", "Kenobi", "Star Wars", 1977, "Jedi Master and mentor to Luke"),
        new Character(6, "Yoda", string.Empty, "Star Wars", 1980, "Grand Jedi Master, 900 years old"),
        new Character(7, "Chewbacca", string.Empty, "Star Wars", 1977, "Wookiee warrior and Han's co-pilot"),
        new Character(8, "Harry", "Potter", "Harry Potter", 2001, "The Boy Who Lived, wizard student"),
        new Character(9, "Hermione", "Granger", "Harry Potter", 2001, "Brilliant witch and Harry's best friend"),
        new Character(10, "Ron", "Weasley", "Harry Potter", 2001, "Loyal friend and brave wizard"),
        new Character(11, "Gandalf", "the Grey", "Lord of the Rings", 2001, "Powerful wizard and guide"),
        new Character(12, "Frodo", "Baggins", "Lord of the Rings", 2001, "Hobbit ring-bearer"),
        new Character(13, "Aragorn", string.Empty, "Lord of the Rings", 2001, "Ranger and rightful king of Gondor"),
        new Character(14, "Legolas", string.Empty, "Lord of the Rings", 2001, "Elven archer and fellowship member"),
        new Character(15, "Tony", "Stark", "Iron Man", 2008, "Genius billionaire superhero"),
        new Character(16, "Steve", "Rogers", "Captain America", 2011, "Super soldier and team leader"),
        new Character(17, "Black", "Widow", "Iron Man 2", 2010, "Master spy and assassin"),
        new Character(18, "Thor", string.Empty, "Thor", 2011, "God of Thunder from Asgard"),
        new Character(19, "Indiana", "Jones", "Raiders of the Lost Ark", 1981, "Archaeologist and adventurer"),
        new Character(20, "James", "Bond", "Dr. No", 1962, "Secret agent 007"),
    ];
    
    public ReadOnlyDictionary<int, Character> GetCharacters(int page = 1, int pageSize = 5)
    {
        Dictionary<int, Character> dictionary = _characters
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToDictionary(c => c.Id, c => c);
        
        return new ReadOnlyDictionary<int, Character>(dictionary);
    }

    public IEnumerable<Character> SearchCharacters(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return Enumerable.Empty<Character>();
        }

        query = query.Trim();
        
        return _characters
            .Where(c => 
                c.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase)
                    || c.LastName.Contains(query, StringComparison.OrdinalIgnoreCase)
                    || c.Movie.Contains(query, StringComparison.OrdinalIgnoreCase)
                    || c.Description.Contains(query, StringComparison.OrdinalIgnoreCase))
            .OrderBy(c => c.Name);
    }

    public IEnumerable<Character> GetAllCharacters()
    {
        return _characters;
    }
}
