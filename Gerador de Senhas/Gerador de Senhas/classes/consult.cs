using System;

namespace Gerador_de_Senhas.classes
{
  public class Consult
  {
    public void ConsultPassword(string[] passwordsCache, string[] descriptionsCache, int howMuchPasswords)
    {
      bool descriptionFound = false;
      bool descriptionCharFound = false;
      Console.Clear();
      Console.WriteLine("Digite a descrição da senha que deseja consultar: ");
      string description = Console.ReadLine();

      if (howMuchPasswords != 0)
      {
        for (int i = 0; i < howMuchPasswords; i++)
        {
          if (description == descriptionsCache[i])
          {
            Console.WriteLine($"Senha '{passwordsCache[i]}' descrição da senha consultada: {descriptionsCache[i]}");
            descriptionFound = true;
            break;
          }
        }

        if (!descriptionFound)
        {
          for (int i = 0; i < howMuchPasswords; i++)
          {
            if (descriptionsCache[i].StartsWith(description, StringComparison.OrdinalIgnoreCase))
            {
              Console.WriteLine($"Senha '{passwordsCache[i]}' descrição da senha consultada: {descriptionsCache[i]}");
              descriptionCharFound = true;
            }
          }
        }

        if (!descriptionFound && !descriptionCharFound)
        {
          Console.WriteLine("Não há nenhuma senha com esta descrição");
        }
      }
      else
      {
        Console.WriteLine("Não há senhas para consultar!");
      }
    }
  }
}
