using RegistrosTecnicos1.Models;
using RegistrosTecnicos1.DAL;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RegistrosTecnicos1.Services;

public class TecnicosServices
{
    private readonly Contexto _context;

    public TecnicosServices(Contexto contexto)
    {
        _context = contexto;
    }

    public async Task<bool> Existe(int idTecnico)
    {
        return await _context.Tecnicos
            .AnyAsync(p => p.IdTecnico == idTecnico);
    }

    private async Task<bool> Insertar(Tecnicos tecnicos)
    {

        _context.Tecnicos.Add(tecnicos);
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tecnicos tecnicos)
    {
        _context.Update(tecnicos);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Tecnicos tecnicos)
    {
        if (await ExisteNombre(tecnicos.Nombre, tecnicos.IdTecnico))
        {
            return false;
        }
        if (!await Existe(tecnicos.IdTecnico))
            return await Insertar(tecnicos);
        else
        {
            return await Modificar(tecnicos);
        }
    }

    public async Task<bool> Eliminar(int id)
    {
        var prioridades = await _context.Tecnicos
            .Where(p => p.IdTecnico == id)
            .ExecuteDeleteAsync();
        return prioridades > 0;
    }

    public async Task<Tecnicos?> Buscar(int id)
    {
        return await _context.Tecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.IdTecnico == id);
    }

    public List<Tecnicos> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        return _context.Tecnicos
            .Include(t => t.TipoTecnico)
            .AsNoTracking()
            .Where(criterio)
            .ToList();
    }
    public async Task<bool> ExisteNombre(string nombre, int? idTecnico = null)
    {
        if (idTecnico.HasValue)
        {
            return await _context.Tecnicos.AnyAsync(t => t.Nombre == nombre && t.IdTecnico != idTecnico);
        }
        else
        {
            return await _context.Tecnicos.AnyAsync(t => t.Nombre == nombre);
        }
    }
}
