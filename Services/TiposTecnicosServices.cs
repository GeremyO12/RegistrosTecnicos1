using RegistrosTecnicos1.Models;
using RegistrosTecnicos1.DAL;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RegistrosTecnicos1.Services;

public class TiposTecnicosServices
{
    private readonly Contexto _context;

    public TiposTecnicosServices(Contexto contexto)
    {
        _context = contexto;
    }

    public async Task<bool> Existe(int tipoTecnicoId)
    {
        return await _context.TiposTecnicos
            .AnyAsync(p => p.TipoTecnicoId == tipoTecnicoId);
    }

    private async Task<bool> Insertar(TiposTecnicos tiposTecnicos)
    {

        _context.TiposTecnicos.Add(tiposTecnicos);
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(TiposTecnicos tiposTecnicos)
    {
        _context.Update(tiposTecnicos);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(TiposTecnicos tiposTecnicos)
    {
        if (await ExisteDescripcion(tiposTecnicos.Descripcion, tiposTecnicos.TipoTecnicoId))
        {
            return false;
        }
        if (!await Existe(tiposTecnicos.TipoTecnicoId))
            return await Insertar(tiposTecnicos);
        else
        {
            return await Modificar(tiposTecnicos);
        }
    }

    public async Task<bool> Eliminar(int id)
    {
        var prioridades = await _context.TiposTecnicos
            .Where(t => t.TipoTecnicoId == id)
            .ExecuteDeleteAsync();
        return prioridades > 0;
    }

    public async Task<TiposTecnicos?> Buscar(int id)
    {
        return await _context.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TipoTecnicoId == id);
    }

    public List<TiposTecnicos> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
    {
        return _context.TiposTecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToList();
    }
    public async Task<bool> ExisteDescripcion(string descripcion, int? idTipoTecnico = null)
    {
        if (idTipoTecnico.HasValue)
        {
            return await _context.TiposTecnicos.AnyAsync(t => t.Descripcion == descripcion && t.TipoTecnicoId != idTipoTecnico);
        }
        else
        {
            return await _context.TiposTecnicos.AnyAsync(t => t.Descripcion == descripcion);
        }
    }
}
