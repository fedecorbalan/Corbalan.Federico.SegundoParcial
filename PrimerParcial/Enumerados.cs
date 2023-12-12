using Entidades;
using PrimerParcial;

public enum Eespecies
{
    Mamifero,
    Ave,
    Anfibio
}
public interface ICrudRana
{
    public Refugio<Rana> ObtenerListaRanas(Refugio<Rana> lista);
    public bool AgregarRana(Rana r);
    public bool ModificarRana(Rana r);
    public bool EliminarRana(Rana r);
}
public interface ICrudOrnitorrinco
{
    public Refugio<Ornitorrinco> ObtenerListaOrnitorrincos(Refugio<Ornitorrinco> lista);
    public bool AgregarOrnitorrinco(Ornitorrinco o);
    public bool ModificarOrnitorrinco(Ornitorrinco o);
    public bool EliminarOrnitorrinco(Ornitorrinco o);
}
public interface ICrudHornero
{
    public Refugio<Hornero> ObtenerListaHorneros(Refugio<Hornero> lista);
    public bool AgregarHornero(Hornero rh);
    public bool ModificarHornero(Hornero h);
    public bool EliminarHornero(Hornero h);
}