using DinoData.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DinoData;

public class Dinosaurs {
    private readonly JurassicDbContext _context = new();

    public int RaptorCount() {
        return _context.Dinosaurs.Count(dino => dino.Species == null || dino.Species.Name == "Velociraptors");
    }

    public int? GetSpeciesId(string name) {
        var species = _context.Species.FirstOrDefault(x => x.Name == name);
        return species?.Id;
    }

    public IEnumerable<(DateTime, string)> FindDino(Dinosaur dino) {
        return _context.LocationDinosaurs
            .Include(x => x.Dinosaur)
            .Where(x => x.Dinosaur == dino)
            .Include(x => x.Location)
            .AsEnumerable()
            .Select(x => (DateTime.Parse(x.CreatedAt), x.Location?.Name ?? ""));
    }

    public async Task<int> StoreDino(Dinosaur dino) {
        int id = 1;
        try {
            id = _context.Dinosaurs.Max(x => x.Id) + 1;
        }
        catch {
            // ignored
        }

        dino.Id = id;

        await _context.Dinosaurs.AddAsync(dino);
        var rows = await _context.SaveChangesAsync();
        return rows;
    }

    public async Task SeedDino(int dinoId) {
        for (int i = 1; i < 4; i++) {
            LocationDinosaur ld = new() {
                DinosaurId = dinoId,
                LocationId = await GetCurrentLocId(),
                Id = Random.Shared.Next(10_000, 23_000),
                CreatedAt = DateTime.Now.AddDays(Random.Shared.Next(1, 10)).ToLongDateString()
            };
            _context.LocationDinosaurs.Add(ld);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<int> GetCurrentLocId() {
        var all = await _context.Locations.Select(x => x.Id).ToListAsync();
        return all[Random.Shared.Next(all.Count)];
    }

    public List<string> Table() {
        var table = _context.Dinosaurs
            .Include(x => x.Species)
            .GroupBy(x => x.Species)
            .Select(x => x.Key == null ? "" : $"{x.Key.Name} {x.Count()} {x.Max(y => y.Version)}");

        return [.. table];
    }
}