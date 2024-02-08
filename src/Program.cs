using System;
using System.Text;

namespace B64Converter{

 
    public class Program{
            // Base64-Encodes a string.
           public static string Base64Encode(string plaintext){
                var bytes = Encoding.UTF8.GetBytes(plaintext);
                return System.Convert.ToBase64String(bytes);
            }
            // decodes a Base64-encoded string.
            public static string Base64Decode(string encoded){
                var plaintext =  System.Convert.FromBase64String(encoded);
                return Encoding.UTF8.GetString(plaintext);
            }

            public static void Main(string[] args){
                // validate argument length 
                if(args.Length != 2){
                    Console.WriteLine("Error: This program accepts exactly two arguments");
                    return;
                }
                string command = args[0];
                string filename = args[1];
                // verify that the file exists
                if (!File.Exists(filename)){
                    Console.WriteLine(String.Format("Error: The file \"{0}\" could not be found!", filename));
                    return;
                }
                // read the file's contents
                string contents = "";
                string tmp;
                StreamReader sr = File.OpenText(filename);
                while ((tmp = sr.ReadLine()) != null){
                    contents += tmp + "\n";
                }
                sr.Close();
                // remove the the appended newline
                contents = contents[..^1]; 
                // perform the requested opperation to the file contents
                switch (command){
                    case "encode":
                        try{
                            contents = Base64Encode(contents);
                            Console.WriteLine("The file was encoded successfully");
                        }
                        catch{
                            Console.WriteLine("Error: Failed to encode the file!");
                            return;
                        }
                        break;
                    case "decode":
                        try{
                            contents = Base64Decode(contents);
                            Console.WriteLine("The file was decoded successfully");
                        }
                        catch{
                            Console.WriteLine("Error: Invalid Base64-encoded file");
                            return;
                        }
                        break;
                    default:
                        Console.WriteLine(String.Format("Error: Unrecognized command \"{0}\"", command));
                        return;
                }
                // write the updated file contents
                StreamWriter sw = File.CreateText(filename);
                sw.Write(contents);
                sw.Close();
            }
    }


}