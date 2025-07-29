using System.Text.RegularExpressions;

namespace VolumeControlAnywhere.Utils;

public class ProcessFilter
{
    public static String NombreProceso(String nombre)
    {
        string patron = @"\\([^%]*)%";
        
        var match = Regex.Matches(nombre, patron);
        return match.First().Value;
    }
}