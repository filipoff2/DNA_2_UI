using System.Collections.Generic;

public static class DnaParser
{
    public static List<string> GetCodons(string dna)
    {
        var codons = new List<string>();
        for (int i = 0; i < dna.Length - 2; i += 3)
        {
            codons.Add(dna.Substring(i, 3));
        }
        return codons;
    }
}
