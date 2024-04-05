public class Pokemon
{
    public string Nombre { get; private set; }
    public string Tipo { get; private set; }
    public int Salud { get; private set; }
    private List<int> Ataques { get; set; } = new List<int>();
    private List<int> Defensas { get; set; } = new List<int>();

    public Pokemon(string nombre, string tipo, int salud)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            Console.WriteLine("No puedes dejar el nombre del Pokémon vacío.");
            Environment.Exit(0);
        }

        if (string.IsNullOrWhiteSpace(tipo))
        {
            Console.WriteLine("No puedes dejar el tipo del Pokémon vacío.");
            Environment.Exit(0);
        }

        Nombre = nombre;
        Tipo = tipo;
        Salud = salud;

        GenerarAtaques();
        GenerarDefensas();
    }

    private void GenerarAtaques()
    {
        var generador = new Random();
        for (int i = 0; i < 3; i++)
        {
            Ataques.Add(generador.Next(0, 41));
        }
    }

    private void GenerarDefensas()
    {
        var generador = new Random();
        for (int i = 0; i < 2; i++)
        {
            Defensas.Add(generador.Next(10, 36));
        }
    }

    public int Atacar()
    {
        var generador = new Random();
        var indiceAtaque = generador.Next(0, Ataques.Count);
        return Ataques[indiceAtaque];
    }

    public int Defensa()
    {
        var generador = new Random();
        var indiceDefensa = generador.Next(0, Defensas.Count);
        return Defensas[indiceDefensa];
    }

    public void RecibirDanio(int daño)
{
    Salud -= daño;
}

    public bool EstaVivo()
    {
        return Salud > 0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var pokemones = new List<Pokemon>();

        for (int i = 0; i < 2; i++)
        {
            Console.WriteLine($"Por favor, introduce el nombre del Pokémon {i + 1}:");
            var nombre = Console.ReadLine();

            Console.WriteLine($"Introduce el tipo del Pokémon {i + 1}:");
            var tipo = Console.ReadLine();

            Console.WriteLine($"Introduce la salud del Pokémon {i + 1}:");
            var saludString = Console.ReadLine();

            if (!int.TryParse(saludString, out int salud) || salud <= 0)
            {
                Console.WriteLine("El valor de salud ingresado no es válido.");
                Environment.Exit(0);
            }

            var pokemon = new Pokemon(nombre, tipo, salud);
            pokemones.Add(pokemon);
        }

        Console.WriteLine($"{pokemones[0].Nombre} vs {pokemones[1].Nombre}");

        for (int i = 1; i <= 3; i++)
        {
            Console.WriteLine($"Turno {i}");

            var danio1 = pokemones[0].Atacar();
            var reduccion1 = pokemones[1].Defensa();

            if (danio1 > reduccion1)
            {
                var diferencia1 = danio1 - reduccion1;
                pokemones[1].RecibirDanio(diferencia1);
                Console.WriteLine($"{pokemones[0].Nombre} atacó con {danio1}, {pokemones[1].Nombre} se defendió con {reduccion1}. {pokemones[1].Nombre} perdió {diferencia1} puntos de salud.");
            }
            else
            {
                Console.WriteLine($"{pokemones[0].Nombre} atacó con {danio1}, pero {pokemones[1].Nombre} se defendió completamente.");
            }

            if (!pokemones[1].EstaVivo())
            {
                Console.WriteLine($"{pokemones[1].Nombre} ha sido derrotado! {pokemones[0].Nombre} es el ganador.");
                return;
            }

            var danio2 = pokemones[1].Atacar();
            var reduccion2 = pokemones[0].Defensa();

            if (danio2 > reduccion2)
            {
                var diferencia2 = danio2 - reduccion2;
                pokemones[0].RecibirDanio(diferencia2);
                Console.WriteLine($"{pokemones[1].Nombre} atacó con {danio2}, {pokemones[0].Nombre} se defendió con {reduccion2}. {pokemones[0].Nombre} perdió {diferencia2} puntos de salud.");
            }
            else
            {
                Console.WriteLine($"{pokemones[1].Nombre} atacó con {danio2}, pero {pokemones[0].Nombre} se defendió completamente.");
            }

            if (!pokemones[0].EstaVivo())
            {
                Console.WriteLine($"{pokemones[0].Nombre} ha sido Eliminado! {pokemones[1].Nombre} es el ganador.");
                return;
            }
        }

        if (pokemones[0].Salud > pokemones[1].Salud)
        {
            Console.WriteLine($"La batalla ha terminado. {pokemones[0].Nombre} es el ganador con {pokemones[0].Salud} puntos de salud restantes.");
        }
        else if (pokemones[1].Salud > pokemones[0].Salud)
        {
            Console.WriteLine($"La batalla ha terminado. {pokemones[1].Nombre} es el ganador con {pokemones[1].Salud} puntos de salud restantes.");
        }
        else
        {
            Console.WriteLine("La batalla ha terminado en empate.");
        }
    }
}
