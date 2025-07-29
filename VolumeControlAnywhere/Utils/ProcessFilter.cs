using System.Text.RegularExpressions;

namespace VolumeControlAnywhere.Utils;

public class ProcessFilter
{
    public static String NombreProceso(String nombre)
    {
        return Path.GetFileNameWithoutExtension(nombre);
    }
}