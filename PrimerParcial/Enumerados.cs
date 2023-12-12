using Entidades;
using PrimerParcial;

/// <summary>
/// Enumeración que representa las posibles especies de animales.
/// </summary>
public enum Eespecies
{
    Mamifero,
    Ave,
    Anfibio
}

/// <summary>
/// Interfaz para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en la lista de ranas.
/// </summary>
public interface ICrudRana
{
    /// <summary>
    /// Obtiene la lista de ranas.
    /// </summary>
    /// <param name="lista">Lista de ranas existente.</param>
    /// <returns>La lista de ranas actualizada.</returns>
    public Refugio<Rana> ObtenerListaRanas(Refugio<Rana> lista);

    /// <summary>
    /// Agrega una rana a la lista.
    /// </summary>
    /// <param name="r">La rana a agregar.</param>
    /// <returns>True si se agregó correctamente, false en caso contrario.</returns>
    public bool AgregarRana(Rana r);

    /// <summary>
    /// Modifica una rana en la lista.
    /// </summary>
    /// <param name="r">La rana a modificar.</param>
    /// <returns>True si se modificó correctamente, false en caso contrario.</returns>
    public bool ModificarRana(Rana r);

    /// <summary>
    /// Elimina una rana de la lista.
    /// </summary>
    /// <param name="r">La rana a eliminar.</param>
    /// <returns>True si se eliminó correctamente, false en caso contrario.</returns>
    public bool EliminarRana(Rana r);
}

/// <summary>
/// Interfaz para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en la lista de ornitorrincos.
/// </summary>
public interface ICrudOrnitorrinco
{
    /// <summary>
    /// Obtiene la lista de ornitorrincos.
    /// </summary>
    /// <param name="lista">Lista de ornitorrincos existente.</param>
    /// <returns>La lista de ornitorrincos actualizada.</returns>
    public Refugio<Ornitorrinco> ObtenerListaOrnitorrincos(Refugio<Ornitorrinco> lista);

    /// <summary>
    /// Agrega un ornitorrinco a la lista.
    /// </summary>
    /// <param name="o">El ornitorrinco a agregar.</param>
    /// <returns>True si se agregó correctamente, false en caso contrario.</returns>
    public bool AgregarOrnitorrinco(Ornitorrinco o);

    /// <summary>
    /// Modifica un ornitorrinco en la lista.
    /// </summary>
    /// <param name="o">El ornitorrinco a modificar.</param>
    /// <returns>True si se modificó correctamente, false en caso contrario.</returns>
    public bool ModificarOrnitorrinco(Ornitorrinco o);

    /// <summary>
    /// Elimina un ornitorrinco de la lista.
    /// </summary>
    /// <param name="o">El ornitorrinco a eliminar.</param>
    /// <returns>True si se eliminó correctamente, false en caso contrario.</returns>
    public bool EliminarOrnitorrinco(Ornitorrinco o);
}

/// <summary>
/// Interfaz para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en la lista de horneros.
/// </summary>
public interface ICrudHornero
{
    /// <summary>
    /// Obtiene la lista de horneros.
    /// </summary>
    /// <param name="lista">Lista de horneros existente.</param>
    /// <returns>La lista de horneros actualizada.</returns>
    public Refugio<Hornero> ObtenerListaHorneros(Refugio<Hornero> lista);

    /// <summary>
    /// Agrega un hornero a la lista.
    /// </summary>
    /// <param name="rh">El hornero a agregar.</param>
    /// <returns>True si se agregó correctamente, false en caso contrario.</returns>
    public bool AgregarHornero(Hornero rh);

    /// <summary>
    /// Modifica un hornero en la lista.
    /// </summary>
    /// <param name="h">El hornero a modificar.</param>
    /// <returns>True si se modificó correctamente, false en caso contrario.</returns>
    public bool ModificarHornero(Hornero h);

    /// <summary>
    /// Elimina un hornero de la lista.
    /// </summary>
    /// <param name="h">El hornero a eliminar.</param>
    /// <returns>True si se eliminó correctamente, false en caso contrario.</returns>
    public bool EliminarHornero(Hornero h);
}
