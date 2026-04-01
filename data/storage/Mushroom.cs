using System.Security.Cryptography;
using luminary.util;

namespace luminary.data.storage;


/// <summary>
/// This manages key/value objects on the filesystem.
/// 
/// Keys are hashed and the hash used as the file name.
/// </summary>
/// <param name="_path">Filesystem path where the keyvalues are stored</param>
public class Mushroom(string _path)
{
    private readonly string Path = _path;

    /// <summary>
    /// Stores the value based on the key so the value can later be retrieved by the key.
    /// </summary>
    /// <param name="_key">The key to use as the lookup parameter to store the key.</param>
    /// <param name="_jsonValue">The string value to store.</param>
    /// <param name="_overwriteExisting">This indicates to not panic if the file already exists.</param>
    /// <returns></returns>
    public async Task<Panicable> StoreAsync(string _key, string _jsonValue, bool _overwriteExisting = true)
    {
        Panicable output = new();

        try
        {
            string keyHash = Hash(_key);

            string path = System.IO.Path.Combine(Path, keyHash);
            if(!_overwriteExisting && File.Exists(path))
            {
                output.ManuallyPanic($"The file for key '{_key}' already exists.");
            }
            else
            {
                await File.WriteAllTextAsync(path, _jsonValue).ConfigureAwait(false);
            }
        }
        catch(Exception e)
        {
            output.ActivatePanic(e);
        }

        return output;
    }

    /// <summary>
    /// Retrieves the matching value for the corresponding key, or panics if the file does not exist.
    /// </summary>
    /// <param name="_key">The key used as the lookup parameter when the key was stored.</param>
    /// <returns>The file contents (string).</returns>
    public async Task<Panicable<string>> RetrieveAsync(string _key)
    {
        Panicable<string> output = new();

        try
        {
            string keyHash = Hash(_key);
            string path = System.IO.Path.Combine(Path, keyHash);
            output.ReturnValue = await File.ReadAllTextAsync(path);
        }
        catch (FileNotFoundException)
        {
            output.ManuallyPanic($"Key '{_key}' does not exist.");
        }
        catch(Exception e)
        {
            output.ActivatePanic(e);
        }

        return output;
    }

    public static string Hash(string _input)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(_input);
        using (var hash = SHA512.Create())
        {
            var hashedInputBytes = hash.ComputeHash(bytes);

            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new System.Text.StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}