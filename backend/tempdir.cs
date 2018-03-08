using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace TempDir.backend
{
    internal sealed class Directory
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<string> Patterns { get; }

        public Directory(string name, string path)
        {
            Name = name;
            Path = path;
            Patterns = new List<string>();
        }
        
        /* Save changes to file. */
        public void Save()
        {
            // TODO: save changes to file.
        }
        
        
        /* Add the specified pattern to the folder. */
        public void AddPattern(string pattern)
        {
            Patterns.Add(pattern);
        }
        
        /* Remove the specified pattern from the folder if present. */
        public void RemovePattern(string pattern)
        {
            if (Patterns.Contains(pattern))
                Patterns.Remove(pattern);
        }
        
        /* Edit the specified pattern. */
        public void ReplacePattern(string oldPattern, string newPattern)
        {
            Patterns[Patterns.IndexOf(oldPattern)] = newPattern;
        }
    }
}
