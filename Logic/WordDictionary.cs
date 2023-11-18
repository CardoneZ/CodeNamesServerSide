using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
     public class WordDictionary
    {
        private Dictionary<string, string> words;

        public WordDictionary()
        {
            words = new Dictionary<string, string>
        {
            { "casa", "house" },
            { "perro", "dog" },
            { "coche", "car" },
            { "hola", "hello" },
            { "gato", "cat" },
            { "montaña", "mountain" },
            { "océano", "ocean" },
            { "libro", "book" },
            { "café", "coffee" },
            { "computadora", "computer" },
            { "guitarra", "guitar" },
            { "playa", "beach" },
            { "vino", "wine" },
            { "felicidad", "happiness" },
            { "aventura", "adventure" },
            { "helado", "icecream" },
            { "reloj", "watch" },
            { "jirafa", "giraffe" },
            { "luna", "moon" },
            { "estrella", "star" },
            { "cielo", "sky" },
            { "bosque", "forest" },
            { "flor", "flower" },
            { "viaje", "trip" },
            { "mañana", "morning" },
            { "amor", "love" },
            { "mariposa", "butterfly" },
            { "chocolate", "chocolate" },

        };
        }
    }
}
