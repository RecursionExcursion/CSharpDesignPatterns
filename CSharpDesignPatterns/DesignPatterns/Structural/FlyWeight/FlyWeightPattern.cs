using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDesignPatterns.DesignPatterns.Structural.FlyWeight
{

    /*
     * Flyweight
     * 
     * The foolow 5 should be true to make this pattern a good use case.
     * 1. When the app uses alot of object
     * 2. Storage costs are high
     * 3. Most of the Object state is extrinsic
     * 4. If you removed the extrinsic stat a large group of objects can be replaced be few shared objects
     * 5. The application doent requre object identitiy
     */
    public interface ICharacter
    {
        void Draw(string fontFamily, int fontSize);
    }

    /*
     * ConcreteFlyweight
     */
    public class CharacterA : ICharacter
    {
        //IntrinsicState
        private char _actualCharacter = 'a';
        //ExtrinsicState
        private string _fontFamily = string.Empty;
        private int _fontSize;

        public void Draw(string fontFamily, int fontSize)
        {
            _fontFamily = fontFamily;
            _fontSize = fontSize;
            Console.WriteLine($"Drawing {_actualCharacter}, {_fontFamily} {_fontSize}");
        }
    }
    public class CharacterB : ICharacter
    {
        //IntrinsicState
        private char _actualCharacter = 'b';
        //ExtrinsicState
        private string _fontFamily = string.Empty;
        private int _fontSize;

        public void Draw(string fontFamily, int fontSize)
        {
            _fontFamily = fontFamily;
            _fontSize = fontSize;
            Console.WriteLine($"Drawing {_actualCharacter}, {_fontFamily} {_fontSize}");
        }
    }

    /*
     * FlywieghtFactory
     */
    public class CharacterFactory
    {
        private readonly Dictionary<char, ICharacter> _characters = new();

        public ICharacter? GetCharacter(char characterId)
        {
            if (_characters.TryGetValue(characterId, out var character))
            {
                Console.WriteLine($"Character reuse");
                return character;
            }

            Console.WriteLine("Character Construction");
            switch (characterId)
            {
                case 'a':
                    _characters[characterId] = new CharacterA();
                    return _characters[characterId];
                case 'b':
                    _characters[characterId] = new CharacterB();
                    return _characters[characterId];
                //etc.
                default:
                    return null;
            }
        }

        public ICharacter CreateParagraph(List<ICharacter> characters, int location)
        {
            return new Paragraph(characters, location);
        }
    }

    /*
     * Unshared Concrete FLyweight
     */
    public class Paragraph : ICharacter
    {
        private int _location;
        private List<ICharacter> _characters = new();

        public Paragraph(List<ICharacter> characters, int location)
        {
            _location = location;
            _characters = characters;
        }

        public void Draw(string fontFamily, int fontSize)
        {
            Console.WriteLine($"Drawing in paragrpagh at location {_location}");
            foreach (var character in _characters)
            {
                character.Draw(fontFamily, fontSize);
            }
        }
    }
}
