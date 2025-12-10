// See https://aka.ms/new-console-template for more information

using DinoData;
using DinoData.Model;

// 1 rész
var dinos = new Dinosaurs();

// 2. rész
var count = dinos.RaptorCount();
Console.WriteLine(count);

// 3. rész
var speciesName = "Triceratops";
var speciesId = dinos.GetSpeciesId(speciesName);

// 4. rész
var myTriceratop = new Dinosaur
{
    Nickname = "Roquentin",
    Birth = DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
    Height = 3,
    Length = Random.Shared.Next(9, 11),
    Weight = Random.Shared.Next(7_000, 13_000),
    SpeciesId = speciesId,
    Version = "3.9",
};

var row = await dinos.StoreDino(myTriceratop);
Console.WriteLine(row);

// 5. rész
var dino = myTriceratop;
Console.WriteLine("Triceraptop id: " + dino.Id);

await dinos.SeedDino(dino.Id);

var loc = dinos.FindDino(dino);
Console.WriteLine(string.Join("; ", loc)); 


// 6. rész
var table = dinos.Table();
Console.WriteLine(string.Join(Environment.NewLine, table));
