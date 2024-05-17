using System;

namespace Gerador_de_Senhas.classes
{
  internal class Generate
  {
    static int cont = 0;
    static string[] choosedPasswordOptions = new string[4];
    private bool useExistingDescription = false;
    private string existingDescription = "", passwordDescription = "", passwordOption = "";
    static Random rnd = new Random();

    public void descriptionUse(bool useDescription, string description)
    {
      useExistingDescription = useDescription;
      existingDescription = description;
    }

    public void GeneratePassword(int index = -1)
    {
      Console.Clear();
      Console.WriteLine("Você deseja quantos caracteres na sua senha(1-50): ");
      string passwordRange = Console.ReadLine();

      if (!int.TryParse(passwordRange, out int _passwordRange))
      {
        Console.WriteLine("Digite um número inteiro!");
        return;
      }

      if (_passwordRange < 1 || _passwordRange > 50)
      {
        Console.WriteLine("A senha só pode conter de 1 a 50 caracteres!");
        return;
      }

      string passwordChars = "";

      while (true)
      {
        Console.WriteLine("\nQuanto a complexidade de sua senha, selecione uma opção para adicionar a sua senha: ");
        Console.WriteLine("1. Letras minúsculas");
        Console.WriteLine("2. Letras maiúsculas");
        Console.WriteLine("3. Números");
        Console.WriteLine("4. Caracteres especiais");
        passwordOption = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(passwordOption))
        {
          if (checkChoosedOption(passwordOption))
          {
            Console.WriteLine("Essa opção já foi adicionada!");
            continue;
          }
        }

        switch (passwordOption)
        {
          case "1":
            passwordChars += "abcdefghijklmnopqrstuvwxyz";
            break;
          case "2":
            passwordChars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            break;
          case "3":
            passwordChars += "0123456789";
            break;
          case "4":
            passwordChars += "!@#$%^&*()-_=+[]{}|;:',.<>/?";
            break;
          default:
            Console.WriteLine("Digite uma opção válida!");
            continue;
        }

        if (cont == 4)
        {
          Console.WriteLine("\nTodas as opções foram selecionadas.");
          break;
        }

        Console.WriteLine("\nDeseja adicionar outra opção? Se sim digite 'S' ou 'sim'.");
        string passwordChoose = Console.ReadLine().ToUpper();

        if (passwordChoose != "S" && passwordChoose != "SIM")
        {
          break;
        }
      }


      if (useExistingDescription)
      {
        passwordDescription = existingDescription;
      }
      else
      {
        Console.WriteLine("\nAdicione uma descrição para a senha: ");
        passwordDescription = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(passwordDescription))
        {
          checkDescription(passwordDescription);
        }
        else
        {
          Console.WriteLine("Digite uma descrição válida!");
          return;
        }
      }

      char[] password = new char[_passwordRange];

      for (int i = 0; i < _passwordRange; i++)
      {
        password[i] = passwordChars[rnd.Next(passwordChars.Length)];
      }

      string generatedPassword = new string(password);

      Console.WriteLine($"\nSenha gerada: '{generatedPassword}', descrição da senha: {passwordDescription}");
      Save.SavePassword(generatedPassword, passwordDescription, index);

      clearChoosedOptions();
    }

    static void checkDescription(string passwordDescription)
    {
      for (int i = 0; i < Save.descriptionsCache.Length; i++)
      {
        if (passwordDescription == Save.descriptionsCache[i])
        {
          Console.WriteLine("Já existe uma senha com essa descrição");
          Console.WriteLine("\nAdicione uma descrição para a senha: ");
          passwordDescription = Console.ReadLine();
          checkDescription(passwordDescription);
        }
      }
    }

    static bool checkChoosedOption(string passwordOption)
    {
      for (int i = 0; i < cont; i++)
      {
        if (passwordOption == choosedPasswordOptions[i])
        {
          return true;
        }
      }

      if (cont < choosedPasswordOptions.Length)
      {
        choosedPasswordOptions[cont] = passwordOption;
        cont++;
      }

      return false;
    }

    static void clearChoosedOptions()
    {
      for (int i = 0; i < choosedPasswordOptions.Length; i++)
      {
        choosedPasswordOptions[i] = "";
      }
      cont = 0;
    }
  }
}
